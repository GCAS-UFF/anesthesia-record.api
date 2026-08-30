using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Domain.Response.Reports
{
    public class NamedCountItem
    {
        public int? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Count { get; set; }
    }

    public class NamedVolumeItem
    {
        public string Name { get; set; } = string.Empty;
        public decimal TotalMl { get; set; }
    }

    public class DateCountItem
    {
        public DateOnly Date { get; set; }
        public int Count { get; set; }
    }

    public class ScoreCountItem
    {
        public int Score { get; set; }
        public int Count { get; set; }
    }

    public class AnesthetistOptionResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    // Resumo executivo
    public class ReportsSummaryResponse
    {
        public int TotalSurgeries { get; set; }
        public int CompletedSurgeries { get; set; }
        public decimal CompletedPercentage { get; set; }
        public int CanceledSurgeries { get; set; }
        public decimal CanceledPercentage { get; set; }
        public int SignedAnesthesiaRecords { get; set; }
        public int ClinicalEventsCount { get; set; }
        public int AdministeredAgentsCount { get; set; }
        public DateTime? LastMedicineSyncAt { get; set; }
        public DateTime? LastProcedureSyncAt { get; set; }
        public DateTime? LastProfessionalSyncAt { get; set; }
    }

    // Eventos clínicos
    public class ClinicalEventTypeBreakdown
    {
        public ClinicalEventTypeEnum EventType { get; set; }
        public string EventTypeLabel { get; set; } = string.Empty;
        public int Count { get; set; }
        public decimal Percentage { get; set; }
    }

    public class ClinicalEventsReportResponse
    {
        public int TotalEvents { get; set; }
        public int DistinctEventTypes { get; set; }
        public int SurgeriesWithEvents { get; set; }
        public List<ClinicalEventTypeBreakdown> ByType { get; set; } = [];
        public List<NamedCountItem> ByAnesthetist { get; set; } = [];
        public List<DateCountItem> ByDay { get; set; } = [];
    }

    // Consumo de fármacos
    public class DrugConsumptionItem
    {
        public int DrugId { get; set; }
        public string Description { get; set; } = string.Empty;
        public string CategoryLabel { get; set; } = string.Empty;
        public int Count { get; set; }
        public decimal Percentage { get; set; }
    }

    public class DrugConsumptionReportResponse
    {
        public int TotalAdministrations { get; set; }
        public int DistinctDrugs { get; set; }
        public string? TopDrug { get; set; }
        public string? TopCategory { get; set; }
        public List<DrugConsumptionItem> ByDrug { get; set; } = [];
        public List<NamedCountItem> ByCategory { get; set; } = [];
    }

    // Volume e tempo de cirurgias
    public class ShiftBreakdownItem
    {
        public string Shift { get; set; } = string.Empty;
        public int Count { get; set; }
        public double? AverageDurationMinutes { get; set; }
    }

    public class SurgeriesReportResponse
    {
        public int TotalSurgeries { get; set; }
        public int SurgeriesWithDurationData { get; set; }
        public double? AverageDurationMinutes { get; set; }
        public double? MinDurationMinutes { get; set; }
        public double? MaxDurationMinutes { get; set; }
        public List<DateCountItem> ByDay { get; set; } = [];
        public List<ShiftBreakdownItem> ByShift { get; set; } = [];
        public List<NamedCountItem> ByProcedure { get; set; } = [];
    }

    // Produtividade por anestesista
    public class AnesthetistProductivityItem
    {
        public int AnesthesiologistId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int SurgeriesCount { get; set; }
        public int SignedRecordsCount { get; set; }
        public double? AverageDurationMinutes { get; set; }
    }

    public class AnesthetistsReportResponse
    {
        public List<AnesthetistProductivityItem> Anesthetists { get; set; } = [];
    }

    // Cancelamentos
    public class CancellationsReportResponse
    {
        public int TotalSurgeries { get; set; }
        public int CanceledSurgeries { get; set; }
        public decimal CanceledPercentage { get; set; }
        public List<NamedCountItem> ByAnesthetist { get; set; } = [];
        public List<NamedCountItem> ByWeekday { get; set; } = [];
    }

    // Classificação ASA
    public class AsaDistributionItem
    {
        public AsaClassificationEnum Classification { get; set; }
        public string Label { get; set; } = string.Empty;
        public int Count { get; set; }
        public decimal Percentage { get; set; }
    }

    public class AsaReportResponse
    {
        public int TotalEvaluated { get; set; }
        public List<AsaDistributionItem> Distribution { get; set; } = [];
        public List<NamedCountItem> ByAnesthetist { get; set; } = [];
        public List<NamedCountItem> ByWeek { get; set; } = [];
    }

    // Recuperação / Aldrete
    public class RecoveryReportResponse
    {
        public int PatientsEvaluated { get; set; }
        public List<ScoreCountItem> ScoreDistribution { get; set; } = [];
        public List<NamedCountItem> DestinationDistribution { get; set; } = [];
        public List<NamedCountItem> DischargeConditionDistribution { get; set; } = [];
        public double? AverageMinutesToAldreteEvaluation { get; set; }
        public int EvaluationsConsideredForTiming { get; set; }
    }

    // Antibioticoprofilaxia
    public class AntibioticProphylaxisReportResponse
    {
        public int TotalSurgeries { get; set; }
        public int SurgeriesWithProphylaxis { get; set; }
        public int SurgeriesWithoutProphylaxis { get; set; }
        public decimal AdherencePercentage { get; set; }
        public List<NamedCountItem> TopMedications { get; set; } = [];
    }

    // Balanço hídrico
    public class FluidBalanceReportResponse
    {
        public decimal TotalGainMl { get; set; }
        public decimal TotalLossMl { get; set; }
        public decimal Balance { get; set; }
        public decimal BleedingMl { get; set; }
        public decimal BloodProductMl { get; set; }
        public List<NamedVolumeItem> ByCategory { get; set; } = [];
        public List<NamedVolumeItem> ByProcedure { get; set; } = [];
    }

    // Saúde da integração AGHU/SIGA
    public class SyncStatusItem
    {
        public DateTime? LastSyncAt { get; set; }
        public bool IsStale { get; set; }
    }

    public class IntegrationStatusReportResponse
    {
        public bool DatabaseHealthy { get; set; }
        public bool AghuHealthy { get; set; }
        public SyncStatusItem Medicines { get; set; } = new();
        public SyncStatusItem Procedures { get; set; } = new();
        public SyncStatusItem Professionals { get; set; } = new();
        public DateTime CheckedAt { get; set; }
    }
}
