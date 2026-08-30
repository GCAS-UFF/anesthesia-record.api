using System.Globalization;
using Microsoft.EntityFrameworkCore;
using UFF.FichaAnestesica.CrossCutting.Extensions;
using UFF.FichaAnestesica.Domain.Commands.Reports;
using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Enums;
using UFF.FichaAnestesica.Domain.Repositories;
using UFF.FichaAnestesica.Domain.Response.Reports;
using UFF.FichaAnestesica.Infra.Context;

namespace UFF.FichaAnestesica.Infra.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly SigaDbCtx _context;

        public ReportRepository(SigaDbCtx context)
        {
            _context = context;
        }

        public async Task<ReportsSummaryResponse> GetSummaryAsync(ReportFilterQuery filter)
        {
            var baseQuery = BuildAnesthesiaRecordQuery(filter);

            var totalSurgeries = await baseQuery.CountAsync();
            var completed = await baseQuery.CountAsync(a => a.Status == SurgeryStatusEnum.Completed);
            var canceled = await baseQuery.CountAsync(a => a.Status == SurgeryStatusEnum.Canceled);
            var signedRecords = await baseQuery.CountAsync(a => a.SignatureDate != null);

            var eventsCount = await BuildClinicalEventQuery(filter).CountAsync();
            var agentsCount = await BuildAdministeredAgentQuery(filter).CountAsync();

            return new ReportsSummaryResponse
            {
                TotalSurgeries = totalSurgeries,
                CompletedSurgeries = completed,
                CompletedPercentage = Percentage(completed, totalSurgeries),
                CanceledSurgeries = canceled,
                CanceledPercentage = Percentage(canceled, totalSurgeries),
                SignedAnesthesiaRecords = signedRecords,
                ClinicalEventsCount = eventsCount,
                AdministeredAgentsCount = agentsCount
            };
        }

        public async Task<ClinicalEventsReportResponse> GetClinicalEventsAsync(ReportFilterQuery filter)
        {
            var query = BuildClinicalEventQuery(filter);

            var total = await query.CountAsync();

            var distinctSurgeries = await query
                .Select(ce => ce.MonitoringRecord.AnesthesiaRecordId)
                .Distinct()
                .CountAsync();

            var byType = await query
                .GroupBy(ce => ce.EventType)
                .Select(g => new { Type = g.Key, Count = g.Count() })
                .ToListAsync();

            var byAnesthetist = await query
                .Where(ce => ce.MonitoringRecord.AnesthesiaRecord.FirstAnesthesiologistId != null)
                .GroupBy(ce => new
                {
                    ce.MonitoringRecord.AnesthesiaRecord.FirstAnesthesiologistId,
                    ce.MonitoringRecord.AnesthesiaRecord.FirstAnesthesiologist!.Name
                })
                .Select(g => new { g.Key.FirstAnesthesiologistId, g.Key.Name, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .ToListAsync();

            var eventDates = await query.Select(ce => ce.Date.Date).ToListAsync();

            var byDay = eventDates
                .GroupBy(d => d)
                .Select(g => new DateCountItem { Date = DateOnly.FromDateTime(g.Key), Count = g.Count() })
                .OrderBy(x => x.Date)
                .ToList();

            return new ClinicalEventsReportResponse
            {
                TotalEvents = total,
                DistinctEventTypes = byType.Count,
                SurgeriesWithEvents = distinctSurgeries,
                ByType = byType
                    .Select(x => new ClinicalEventTypeBreakdown
                    {
                        EventType = x.Type,
                        EventTypeLabel = EnumExtensions.GetDescription(x.Type),
                        Count = x.Count,
                        Percentage = Percentage(x.Count, total)
                    })
                    .OrderByDescending(x => x.Count)
                    .ToList(),
                ByAnesthetist = byAnesthetist
                    .Select(x => new NamedCountItem { Id = x.FirstAnesthesiologistId, Name = x.Name, Count = x.Count })
                    .ToList(),
                ByDay = byDay
            };
        }

        public async Task<DrugConsumptionReportResponse> GetDrugConsumptionAsync(ReportFilterQuery filter, DrugCategoryEnum? category)
        {
            var query = BuildAdministeredAgentQuery(filter);

            if (category.HasValue)
                query = query.Where(a => a.Drug.Category == category);

            var total = await query.CountAsync();
            var distinctDrugs = await query.Select(a => a.DrugId).Distinct().CountAsync();

            var byDrug = await query
                .GroupBy(a => new { a.DrugId, a.Drug.Description, a.Drug.Category })
                .Select(g => new { g.Key.DrugId, g.Key.Description, g.Key.Category, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .ToListAsync();

            var byCategory = byDrug
                .GroupBy(x => x.Category)
                .Select(g => new { Category = g.Key, Count = g.Sum(x => x.Count) })
                .OrderByDescending(x => x.Count)
                .ToList();

            return new DrugConsumptionReportResponse
            {
                TotalAdministrations = total,
                DistinctDrugs = distinctDrugs,
                TopDrug = byDrug.FirstOrDefault()?.Description,
                TopCategory = byCategory.Count > 0 ? EnumExtensions.GetDescription(byCategory[0].Category) : null,
                ByDrug = byDrug
                    .Take(15)
                    .Select(x => new DrugConsumptionItem
                    {
                        DrugId = x.DrugId,
                        Description = x.Description,
                        CategoryLabel = EnumExtensions.GetDescription(x.Category),
                        Count = x.Count,
                        Percentage = Percentage(x.Count, total)
                    })
                    .ToList(),
                ByCategory = byCategory
                    .Select(x => new NamedCountItem { Name = EnumExtensions.GetDescription(x.Category), Count = x.Count })
                    .ToList()
            };
        }

        public async Task<SurgeriesReportResponse> GetSurgeriesAsync(ReportFilterQuery filter)
        {
            var baseQuery = BuildAnesthesiaRecordQuery(filter);

            var total = await baseQuery.CountAsync();

            var surgeryDates = await baseQuery.Select(a => a.SurgeryDate.Date).ToListAsync();

            var byDay = surgeryDates
                .GroupBy(d => d)
                .Select(g => new DateCountItem { Date = DateOnly.FromDateTime(g.Key), Count = g.Count() })
                .OrderBy(x => x.Date)
                .ToList();

            var durations = await baseQuery
                .Where(a => a.MonitoringRecord != null
                    && a.MonitoringRecord.SurgeryStartedAt != null
                    && a.MonitoringRecord.SurgeryEndedAt != null)
                .Select(a => new
                {
                    StartedAt = a.MonitoringRecord!.SurgeryStartedAt!.Value,
                    EndedAt = a.MonitoringRecord!.SurgeryEndedAt!.Value
                })
                .ToListAsync();

            var minutesList = durations
                .Select(x => (x.EndedAt - x.StartedAt).TotalMinutes)
                .Where(m => m > 0)
                .ToList();

            var byShift = durations
                .Select(x => new { Shift = ShiftLabel(ToBrazilLocal(x.StartedAt)), Minutes = (x.EndedAt - x.StartedAt).TotalMinutes })
                .Where(x => x.Minutes > 0)
                .GroupBy(x => x.Shift)
                .Select(g => new ShiftBreakdownItem
                {
                    Shift = g.Key,
                    Count = g.Count(),
                    AverageDurationMinutes = Math.Round(g.Average(x => x.Minutes), 1)
                })
                .OrderBy(x => x.Shift)
                .ToList();

            var byProcedure = await baseQuery
                .SelectMany(a => a.Surgeries.Where(s => s.IsPrimary))
                .GroupBy(s => new { s.ProcedureId, s.Procedure.Description })
                .Select(g => new { g.Key.Description, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .Take(10)
                .ToListAsync();

            return new SurgeriesReportResponse
            {
                TotalSurgeries = total,
                SurgeriesWithDurationData = minutesList.Count,
                AverageDurationMinutes = minutesList.Count > 0 ? Math.Round(minutesList.Average(), 1) : null,
                MinDurationMinutes = minutesList.Count > 0 ? Math.Round(minutesList.Min(), 1) : null,
                MaxDurationMinutes = minutesList.Count > 0 ? Math.Round(minutesList.Max(), 1) : null,
                ByDay = byDay,
                ByShift = byShift,
                ByProcedure = byProcedure
                    .Select(x => new NamedCountItem { Name = x.Description, Count = x.Count })
                    .ToList()
            };
        }

        public async Task<AnesthetistsReportResponse> GetAnesthetistsAsync(ReportFilterQuery filter)
        {
            var baseQuery = BuildAnesthesiaRecordQuery(filter);

            var raw = await baseQuery
                .Where(a => a.FirstAnesthesiologistId != null)
                .Select(a => new
                {
                    a.FirstAnesthesiologistId,
                    Name = a.FirstAnesthesiologist!.Name,
                    a.SignatureDate,
                    StartedAt = a.MonitoringRecord != null ? a.MonitoringRecord.SurgeryStartedAt : null,
                    EndedAt = a.MonitoringRecord != null ? a.MonitoringRecord.SurgeryEndedAt : null
                })
                .ToListAsync();

            var anesthetists = raw
                .GroupBy(x => new { x.FirstAnesthesiologistId, x.Name })
                .Select(g =>
                {
                    var minutes = g
                        .Where(x => x.StartedAt != null && x.EndedAt != null)
                        .Select(x => (x.EndedAt!.Value - x.StartedAt!.Value).TotalMinutes)
                        .Where(m => m > 0)
                        .ToList();

                    return new AnesthetistProductivityItem
                    {
                        AnesthesiologistId = g.Key.FirstAnesthesiologistId!.Value,
                        Name = g.Key.Name,
                        SurgeriesCount = g.Count(),
                        SignedRecordsCount = g.Count(x => x.SignatureDate != null),
                        AverageDurationMinutes = minutes.Count > 0 ? Math.Round(minutes.Average(), 1) : null
                    };
                })
                .OrderByDescending(x => x.SurgeriesCount)
                .ToList();

            return new AnesthetistsReportResponse { Anesthetists = anesthetists };
        }

        public async Task<CancellationsReportResponse> GetCancellationsAsync(ReportFilterQuery filter)
        {
            var baseQuery = BuildAnesthesiaRecordQuery(filter);

            var total = await baseQuery.CountAsync();
            var canceled = await baseQuery.CountAsync(a => a.Status == SurgeryStatusEnum.Canceled);

            var byAnesthetist = await baseQuery
                .Where(a => a.Status == SurgeryStatusEnum.Canceled && a.FirstAnesthesiologistId != null)
                .GroupBy(a => new { a.FirstAnesthesiologistId, a.FirstAnesthesiologist!.Name })
                .Select(g => new { g.Key.FirstAnesthesiologistId, g.Key.Name, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .ToListAsync();

            var canceledDates = await baseQuery
                .Where(a => a.Status == SurgeryStatusEnum.Canceled)
                .Select(a => a.SurgeryDate)
                .ToListAsync();

            var byWeekday = canceledDates
                .GroupBy(d => d.DayOfWeek)
                .Select(g => new NamedCountItem { Name = WeekdayLabel(g.Key), Count = g.Count() })
                .OrderBy(x => x.Name)
                .ToList();

            return new CancellationsReportResponse
            {
                TotalSurgeries = total,
                CanceledSurgeries = canceled,
                CanceledPercentage = Percentage(canceled, total),
                ByAnesthetist = byAnesthetist
                    .Select(x => new NamedCountItem { Id = x.FirstAnesthesiologistId, Name = x.Name, Count = x.Count })
                    .ToList(),
                ByWeekday = byWeekday
            };
        }

        public async Task<AsaReportResponse> GetAsaAsync(ReportFilterQuery filter)
        {
            var query = BuildPreAnesthesiaRecordQuery(filter)
                .Where(p => p.AsaClassification != null);

            var total = await query.CountAsync();

            var byClassification = await query
                .GroupBy(p => p.AsaClassification!.Value)
                .Select(g => new { Classification = g.Key, Count = g.Count() })
                .OrderBy(x => x.Classification)
                .ToListAsync();

            var byAnesthetist = await query
                .Where(p => p.AnesthesiaRecord.FirstAnesthesiologistId != null)
                .GroupBy(p => new { p.AnesthesiaRecord.FirstAnesthesiologistId, p.AnesthesiaRecord.FirstAnesthesiologist!.Name })
                .Select(g => new { g.Key.FirstAnesthesiologistId, g.Key.Name, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .ToListAsync();

            var surgeryDates = await query.Select(p => p.AnesthesiaRecord.SurgeryDate).ToListAsync();

            var byWeek = surgeryDates
                .GroupBy(d => WeekLabel(d))
                .Select(g => new NamedCountItem { Name = g.Key, Count = g.Count() })
                .OrderBy(x => x.Name)
                .ToList();

            return new AsaReportResponse
            {
                TotalEvaluated = total,
                Distribution = byClassification
                    .Select(x => new AsaDistributionItem
                    {
                        Classification = x.Classification,
                        Label = AsaLabel(x.Classification),
                        Count = x.Count,
                        Percentage = Percentage(x.Count, total)
                    })
                    .ToList(),
                ByAnesthetist = byAnesthetist
                    .Select(x => new NamedCountItem { Id = x.FirstAnesthesiologistId, Name = x.Name, Count = x.Count })
                    .ToList(),
                ByWeek = byWeek
            };
        }

        public async Task<RecoveryReportResponse> GetRecoveryAsync(ReportFilterQuery filter)
        {
            var raw = await BuildAnesthesiaRecordQuery(filter)
                .Where(a => a.TotalAldreteKroulikScore != null)
                .Select(a => new
                {
                    a.TotalAldreteKroulikScore,
                    a.ClinicalDischargeCondition,
                    a.Destination,
                    a.AnesthesiaEndTime,
                    a.AldreteEvaluationTime
                })
                .ToListAsync();

            var byScore = raw
                .GroupBy(x => x.TotalAldreteKroulikScore!.Value)
                .Select(g => new ScoreCountItem { Score = g.Key, Count = g.Count() })
                .OrderBy(x => x.Score)
                .ToList();

            var byDestination = raw
                .Where(x => x.Destination != null)
                .GroupBy(x => x.Destination!.Value)
                .Select(g => new NamedCountItem { Name = EnumExtensions.GetDescription(g.Key), Count = g.Count() })
                .ToList();

            var byDischargeCondition = raw
                .Where(x => x.ClinicalDischargeCondition != null)
                .GroupBy(x => x.ClinicalDischargeCondition!.Value)
                .Select(g => new NamedCountItem { Name = EnumExtensions.GetDescription(g.Key), Count = g.Count() })
                .ToList();

            // Não existe timestamp de entrada/alta da RPA no sistema. Métrica adaptada:
            // tempo entre o fim da anestesia e a avaliação de Aldrete (ambos TimeOnly).
            // Casos que cruzam a meia-noite são descartados (TimeOnly não carrega a data).
            var evaluationMinutes = raw
                .Where(x => x.AnesthesiaEndTime != null && x.AldreteEvaluationTime != null)
                .Select(x => (x.AldreteEvaluationTime!.Value.ToTimeSpan() - x.AnesthesiaEndTime!.Value.ToTimeSpan()).TotalMinutes)
                .Where(m => m >= 0)
                .ToList();

            return new RecoveryReportResponse
            {
                PatientsEvaluated = raw.Count,
                ScoreDistribution = byScore,
                DestinationDistribution = byDestination,
                DischargeConditionDistribution = byDischargeCondition,
                AverageMinutesToAldreteEvaluation = evaluationMinutes.Count > 0 ? Math.Round(evaluationMinutes.Average(), 1) : null,
                EvaluationsConsideredForTiming = evaluationMinutes.Count
            };
        }

        public async Task<AntibioticProphylaxisReportResponse> GetAntibioticProphylaxisAsync(ReportFilterQuery filter)
        {
            var baseQuery = BuildAnesthesiaRecordQuery(filter);

            var total = await baseQuery.CountAsync();
            var withProphylaxis = await baseQuery.CountAsync(a => a.Antibiotics.Any());

            var topMedications = await baseQuery
                .SelectMany(a => a.Antibiotics)
                .GroupBy(ab => ab.MedicationName)
                .Select(g => new { Name = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .Take(10)
                .ToListAsync();

            return new AntibioticProphylaxisReportResponse
            {
                TotalSurgeries = total,
                SurgeriesWithProphylaxis = withProphylaxis,
                SurgeriesWithoutProphylaxis = total - withProphylaxis,
                AdherencePercentage = Percentage(withProphylaxis, total),
                TopMedications = topMedications
                    .Select(x => new NamedCountItem { Name = x.Name, Count = x.Count })
                    .ToList()
            };
        }

        public async Task<FluidBalanceReportResponse> GetFluidBalanceAsync(ReportFilterQuery filter)
        {
            var query = BuildFluidBalanceQuery(filter);

            var totalGain = await query.Where(f => f.Type == FluidBalanceTypeEnum.Gain).SumAsync(f => (decimal?)f.VolumeMl) ?? 0;
            var totalLoss = await query.Where(f => f.Type == FluidBalanceTypeEnum.Loss).SumAsync(f => (decimal?)f.VolumeMl) ?? 0;
            var bleeding = await query.Where(f => f.Category == FluidCategoryEnum.Bleeding).SumAsync(f => (decimal?)f.VolumeMl) ?? 0;
            var bloodProduct = await query.Where(f => f.Category == FluidCategoryEnum.BloodProduct).SumAsync(f => (decimal?)f.VolumeMl) ?? 0;

            var byCategory = await query
                .GroupBy(f => f.Category)
                .Select(g => new { Category = g.Key, TotalMl = g.Sum(x => x.VolumeMl) })
                .ToListAsync();

            var withProcedure = await query
                .Select(f => new
                {
                    f.VolumeMl,
                    Procedure = f.MonitoringRecord.AnesthesiaRecord.Surgeries
                        .Where(s => s.IsPrimary)
                        .Select(s => s.Procedure.Description)
                        .FirstOrDefault()
                })
                .ToListAsync();

            var byProcedure = withProcedure
                .Where(x => x.Procedure != null)
                .GroupBy(x => x.Procedure)
                .Select(g => new NamedVolumeItem { Name = g.Key!, TotalMl = g.Sum(x => x.VolumeMl) })
                .OrderByDescending(x => x.TotalMl)
                .Take(10)
                .ToList();

            return new FluidBalanceReportResponse
            {
                TotalGainMl = totalGain,
                TotalLossMl = totalLoss,
                Balance = totalGain - totalLoss,
                BleedingMl = bleeding,
                BloodProductMl = bloodProduct,
                ByCategory = byCategory
                    .Select(x => new NamedVolumeItem { Name = EnumExtensions.GetDescription(x.Category), TotalMl = x.TotalMl })
                    .ToList(),
                ByProcedure = byProcedure
            };
        }

        public async Task<List<AnesthetistOptionResponse>> GetAnesthetistOptionsAsync()
        {
            var firstIds = _context.AnesthesiaRecords.AsNoTracking()
                .Where(a => a.FirstAnesthesiologistId != null)
                .Select(a => a.FirstAnesthesiologistId!.Value);

            var secondIds = _context.AnesthesiaRecords.AsNoTracking()
                .Where(a => a.SecondAnesthesiologistId != null)
                .Select(a => a.SecondAnesthesiologistId!.Value);

            var ids = await firstIds.Union(secondIds).Distinct().ToListAsync();

            return await _context.Users.AsNoTracking()
                .Where(u => ids.Contains(u.Id))
                .OrderBy(u => u.Name)
                .Select(u => new AnesthetistOptionResponse { Id = u.Id, Name = u.Name })
                .ToListAsync();
        }

        // --- Consultas base compartilhadas ---

        private IQueryable<AnesthesiaRecord> BuildAnesthesiaRecordQuery(ReportFilterQuery filter)
        {
            var (start, end) = DateRange(filter);

            var query = _context.AnesthesiaRecords.AsNoTracking()
                .Where(a => a.SurgeryDate >= start && a.SurgeryDate < end);

            if (filter.AnesthesiologistId.HasValue)
                query = query.Where(a => a.FirstAnesthesiologistId == filter.AnesthesiologistId || a.SecondAnesthesiologistId == filter.AnesthesiologistId);

            if (filter.Status.HasValue)
                query = query.Where(a => a.Status == filter.Status);

            return query;
        }

        private IQueryable<PreAnesthesiaRecord> BuildPreAnesthesiaRecordQuery(ReportFilterQuery filter)
        {
            var (start, end) = DateRange(filter);

            var query = _context.PreAnesthesiaRecords.AsNoTracking()
                .Where(p => p.AnesthesiaRecord.SurgeryDate >= start && p.AnesthesiaRecord.SurgeryDate < end);

            if (filter.AnesthesiologistId.HasValue)
                query = query.Where(p => p.AnesthesiaRecord.FirstAnesthesiologistId == filter.AnesthesiologistId || p.AnesthesiaRecord.SecondAnesthesiologistId == filter.AnesthesiologistId);

            if (filter.Status.HasValue)
                query = query.Where(p => p.AnesthesiaRecord.Status == filter.Status);

            return query;
        }

        private IQueryable<ClinicalEvent> BuildClinicalEventQuery(ReportFilterQuery filter)
        {
            var (start, end) = DateRange(filter);

            var query = _context.ClinicalEvents.AsNoTracking()
                .Where(ce => ce.MonitoringRecord.AnesthesiaRecord.SurgeryDate >= start && ce.MonitoringRecord.AnesthesiaRecord.SurgeryDate < end);

            if (filter.AnesthesiologistId.HasValue)
                query = query.Where(ce => ce.MonitoringRecord.AnesthesiaRecord.FirstAnesthesiologistId == filter.AnesthesiologistId || ce.MonitoringRecord.AnesthesiaRecord.SecondAnesthesiologistId == filter.AnesthesiologistId);

            if (filter.Status.HasValue)
                query = query.Where(ce => ce.MonitoringRecord.AnesthesiaRecord.Status == filter.Status);

            return query;
        }

        private IQueryable<AdministeredAgent> BuildAdministeredAgentQuery(ReportFilterQuery filter)
        {
            var (start, end) = DateRange(filter);

            var query = _context.AdministeredAgents.AsNoTracking()
                .Where(a => a.MonitoringRecord.AnesthesiaRecord.SurgeryDate >= start && a.MonitoringRecord.AnesthesiaRecord.SurgeryDate < end);

            if (filter.AnesthesiologistId.HasValue)
                query = query.Where(a => a.MonitoringRecord.AnesthesiaRecord.FirstAnesthesiologistId == filter.AnesthesiologistId || a.MonitoringRecord.AnesthesiaRecord.SecondAnesthesiologistId == filter.AnesthesiologistId);

            if (filter.Status.HasValue)
                query = query.Where(a => a.MonitoringRecord.AnesthesiaRecord.Status == filter.Status);

            return query;
        }

        private IQueryable<FluidBalance> BuildFluidBalanceQuery(ReportFilterQuery filter)
        {
            var (start, end) = DateRange(filter);

            var query = _context.FluidBalances.AsNoTracking()
                .Where(f => f.MonitoringRecord.AnesthesiaRecord.SurgeryDate >= start && f.MonitoringRecord.AnesthesiaRecord.SurgeryDate < end);

            if (filter.AnesthesiologistId.HasValue)
                query = query.Where(f => f.MonitoringRecord.AnesthesiaRecord.FirstAnesthesiologistId == filter.AnesthesiologistId || f.MonitoringRecord.AnesthesiaRecord.SecondAnesthesiologistId == filter.AnesthesiologistId);

            if (filter.Status.HasValue)
                query = query.Where(f => f.MonitoringRecord.AnesthesiaRecord.Status == filter.Status);

            return query;
        }

        // SurgeryDate é uma coluna `date` (sem timezone) — usamos Kind Unspecified para
        // não sofrer a conversão que o Npgsql aplica a colunas `timestamptz`.
        private static (DateTime start, DateTime end) DateRange(ReportFilterQuery filter)
        {
            var start = DateTime.SpecifyKind(filter.StartDate.Date, DateTimeKind.Unspecified);
            var end = DateTime.SpecifyKind(filter.EndDate.Date.AddDays(1), DateTimeKind.Unspecified);
            return (start, end);
        }

        private static decimal Percentage(int part, int total) => total == 0 ? 0 : Math.Round(part * 100m / total, 1);

        // O Brasil não observa horário de verão desde 2019: America/Sao_Paulo é UTC-3 fixo.
        private static DateTime ToBrazilLocal(DateTime utc) => DateTime.SpecifyKind(utc, DateTimeKind.Utc).AddHours(-3);

        private static string ShiftLabel(DateTime brazilLocalTime) => brazilLocalTime.Hour switch
        {
            >= 6 and < 12 => "Manhã",
            >= 12 and < 18 => "Tarde",
            _ => "Noite"
        };

        private static string WeekdayLabel(DayOfWeek day) => day switch
        {
            DayOfWeek.Sunday => "Domingo",
            DayOfWeek.Monday => "Segunda-feira",
            DayOfWeek.Tuesday => "Terça-feira",
            DayOfWeek.Wednesday => "Quarta-feira",
            DayOfWeek.Thursday => "Quinta-feira",
            DayOfWeek.Friday => "Sexta-feira",
            DayOfWeek.Saturday => "Sábado",
            _ => day.ToString()
        };

        private static string WeekLabel(DateTime date) => $"{ISOWeek.GetYear(date)}-S{ISOWeek.GetWeekOfYear(date):00}";

        private static string AsaLabel(AsaClassificationEnum classification) => classification.ToString().Replace("_", " ");
    }
}
