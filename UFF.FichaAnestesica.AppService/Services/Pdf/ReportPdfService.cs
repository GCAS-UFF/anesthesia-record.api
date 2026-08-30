using PuppeteerSharp;
using PuppeteerSharp.Media;
using UFF.FichaAnestesica.Application.Interfaces;
using UFF.FichaAnestesica.Domain.Commands.Reports;
using UFF.FichaAnestesica.Domain.Enums;
using UFF.FichaAnestesica.Domain.Repositories;
using UFF.FichaAnestesica.Domain.Response.Reports;
using UFF.FichaAnestesica.Domain.Services;

namespace UFF.FichaAnestesica.Service.Services.Pdf
{
    public class ReportPdfService : IReportPdfService
    {
        private readonly IReportService _reportService;
        private readonly IReportRepository _reportRepository;
        private readonly IRazorViewRenderer _viewRenderer;
        private readonly IPdfBrowserProvider _browserProvider;

        private const string ViewPath = "~/Views/Reports/ReportPdf.cshtml";

        public ReportPdfService(
            IReportService reportService,
            IReportRepository reportRepository,
            IRazorViewRenderer viewRenderer,
            IPdfBrowserProvider browserProvider)
        {
            _reportService = reportService;
            _reportRepository = reportRepository;
            _viewRenderer = viewRenderer;
            _browserProvider = browserProvider;
        }

        public async Task<(byte[]? Bytes, string? Error)> GenerateAsync(string reportKey, ReportFilterQuery filter, DrugCategoryEnum? category)
        {
            var validationError = filter.Validate();
            if (validationError != null)
                return (null, validationError);

            var viewModel = await BuildViewModelAsync(reportKey, filter, category);
            if (viewModel == null)
                return (null, "Relatório não encontrado.");

            var html = await _viewRenderer.RenderAsync(ViewPath, viewModel);
            var bytes = await RenderPdfAsync(html);

            return (bytes, null);
        }

        private async Task<byte[]> RenderPdfAsync(string html)
        {
            var browser = await _browserProvider.GetBrowserAsync();
            await using var page = await browser.NewPageAsync();

            await page.SetContentAsync(html);
            await page.WaitForFunctionAsync("() => window.__chartsReady === true", new WaitForFunctionOptions { Timeout = 8000 });

            return await page.PdfDataAsync(new PdfOptions
            {
                Format = PaperFormat.A4,
                PrintBackground = true,
                DisplayHeaderFooter = true,
                HeaderTemplate = "<span></span>",
                FooterTemplate = "<div style=\"width:100%;font-size:8px;color:#94a3b8;text-align:center;\">Página <span class=\"pageNumber\"></span> de <span class=\"totalPages\"></span></div>",
                MarginOptions = new MarginOptions { Top = "14mm", Bottom = "14mm", Left = "12mm", Right = "12mm" }
            });
        }

        private async Task<ReportPdfViewModel?> BuildViewModelAsync(string reportKey, ReportFilterQuery filter, DrugCategoryEnum? category)
        {
            var periodLabel = $"{filter.StartDate:dd/MM/yyyy} a {filter.EndDate:dd/MM/yyyy}";
            var filtersLabel = await BuildFiltersLabelAsync(filter, category);

            switch (reportKey)
            {
                case "summary":
                    {
                        var data = (ReportsSummaryResponse)(await _reportService.GetSummaryAsync(filter)).Data;
                        return new ReportPdfViewModel
                        {
                            Title = "Resumo Executivo",
                            Subtitle = "Indicadores consolidados do período selecionado.",
                            PeriodLabel = periodLabel,
                            FiltersLabel = filtersLabel,
                            GeneratedAt = DateTime.Now,
                            Kpis =
                            [
                                new() { Label = "Cirurgias no período", Value = data.TotalSurgeries.ToString() },
                                new() { Label = "Concluídas", Value = $"{data.CompletedSurgeries} ({data.CompletedPercentage}%)" },
                                new() { Label = "Canceladas", Value = $"{data.CanceledSurgeries} ({data.CanceledPercentage}%)" },
                                new() { Label = "Fichas assinadas", Value = data.SignedAnesthesiaRecords.ToString() },
                                new() { Label = "Eventos clínicos", Value = data.ClinicalEventsCount.ToString() },
                                new() { Label = "Medicamentos administrados", Value = data.AdministeredAgentsCount.ToString() }
                            ],
                            Charts =
                            [
                                new ReportPdfChart
                                {
                                    Title = "Cirurgias por status",
                                    Type = "doughnut",
                                    Labels = ["Concluídas", "Canceladas", "Outras"],
                                    Values =
                                    [
                                        data.CompletedSurgeries,
                                        data.CanceledSurgeries,
                                        Math.Max(0, data.TotalSurgeries - data.CompletedSurgeries - data.CanceledSurgeries)
                                    ]
                                }
                            ]
                        };
                    }

                case "clinical-events":
                    {
                        var data = (ClinicalEventsReportResponse)(await _reportService.GetClinicalEventsAsync(filter)).Data;
                        return new ReportPdfViewModel
                        {
                            Title = "Eventos Clínicos",
                            Subtitle = "Eventos registrados durante a monitorização intraoperatória.",
                            PeriodLabel = periodLabel,
                            FiltersLabel = filtersLabel,
                            GeneratedAt = DateTime.Now,
                            Kpis =
                            [
                                new() { Label = "Total de eventos", Value = data.TotalEvents.ToString() },
                                new() { Label = "Tipos diferentes", Value = data.DistinctEventTypes.ToString() },
                                new() { Label = "Cirurgias com evento", Value = data.SurgeriesWithEvents.ToString() }
                            ],
                            Charts = [new ReportPdfChart { Title = "Eventos por tipo", Type = "bar", Labels = data.ByType.Select(x => x.EventTypeLabel).ToList(), Values = data.ByType.Select(x => (decimal)x.Count).ToList() }],
                            Table = new ReportPdfTable
                            {
                                Headers = ["Tipo do evento", "Quantidade", "Percentual"],
                                Rows = data.ByType.Select(x => new List<string> { x.EventTypeLabel, x.Count.ToString(), $"{x.Percentage}%" }).ToList()
                            }
                        };
                    }

                case "drug-consumption":
                    {
                        var data = (DrugConsumptionReportResponse)(await _reportService.GetDrugConsumptionAsync(filter, category)).Data;
                        return new ReportPdfViewModel
                        {
                            Title = "Consumo de Fármacos e Insumos",
                            Subtitle = "Administrações registradas na monitorização intraoperatória.",
                            PeriodLabel = periodLabel,
                            FiltersLabel = filtersLabel,
                            GeneratedAt = DateTime.Now,
                            Kpis =
                            [
                                new() { Label = "Total de administrações", Value = data.TotalAdministrations.ToString() },
                                new() { Label = "Fármacos distintos", Value = data.DistinctDrugs.ToString() },
                                new() { Label = "Fármaco mais usado", Value = data.TopDrug ?? "—" },
                                new() { Label = "Categoria mais usada", Value = data.TopCategory ?? "—" }
                            ],
                            Charts = [new ReportPdfChart { Title = "Top medicamentos utilizados", Type = "bar", Labels = data.ByDrug.Select(x => x.Description).ToList(), Values = data.ByDrug.Select(x => (decimal)x.Count).ToList() }],
                            Table = new ReportPdfTable
                            {
                                Headers = ["Medicamento", "Categoria", "Quantidade", "%"],
                                Rows = data.ByDrug.Select(x => new List<string> { x.Description, x.CategoryLabel, x.Count.ToString(), $"{x.Percentage}%" }).ToList()
                            }
                        };
                    }

                case "surgeries":
                    {
                        var data = (SurgeriesReportResponse)(await _reportService.GetSurgeriesAsync(filter)).Data;
                        return new ReportPdfViewModel
                        {
                            Title = "Volume e Tempo de Cirurgias",
                            Subtitle = "Duração calculada apenas para cirurgias com registro de início e fim na monitorização.",
                            PeriodLabel = periodLabel,
                            FiltersLabel = filtersLabel,
                            GeneratedAt = DateTime.Now,
                            Kpis =
                            [
                                new() { Label = "Total de cirurgias", Value = data.TotalSurgeries.ToString() },
                                new() { Label = "Duração média", Value = data.AverageDurationMinutes != null ? $"{data.AverageDurationMinutes} min" : "—" },
                                new() { Label = "Duração mínima", Value = data.MinDurationMinutes != null ? $"{data.MinDurationMinutes} min" : "—" },
                                new() { Label = "Duração máxima", Value = data.MaxDurationMinutes != null ? $"{data.MaxDurationMinutes} min" : "—" }
                            ],
                            Charts = [new ReportPdfChart { Title = "Cirurgias por dia", Type = "line", Labels = data.ByDay.Select(x => x.Date.ToString("dd/MM")).ToList(), Values = data.ByDay.Select(x => (decimal)x.Count).ToList() }],
                            Table = new ReportPdfTable
                            {
                                Headers = ["Turno", "Cirurgias", "Duração média (min)"],
                                Rows = data.ByShift.Select(x => new List<string> { x.Shift, x.Count.ToString(), x.AverageDurationMinutes?.ToString() ?? "—" }).ToList()
                            }
                        };
                    }

                case "anesthetists":
                    {
                        var data = (AnesthetistsReportResponse)(await _reportService.GetAnesthetistsAsync(filter)).Data;
                        return new ReportPdfViewModel
                        {
                            Title = "Produtividade por Anestesista",
                            Subtitle = "Indicador operacional de volume de trabalho — não representa avaliação de desempenho individual.",
                            PeriodLabel = periodLabel,
                            FiltersLabel = filtersLabel,
                            GeneratedAt = DateTime.Now,
                            Kpis = [new() { Label = "Anestesistas no período", Value = data.Anesthetists.Count.ToString() }],
                            Charts = [new ReportPdfChart { Title = "Cirurgias por anestesista", Type = "bar", Labels = data.Anesthetists.Select(x => x.Name).ToList(), Values = data.Anesthetists.Select(x => (decimal)x.SurgeriesCount).ToList() }],
                            Table = new ReportPdfTable
                            {
                                Headers = ["Anestesista", "Cirurgias", "Fichas assinadas", "Duração média (min)"],
                                Rows = data.Anesthetists.Select(x => new List<string> { x.Name, x.SurgeriesCount.ToString(), x.SignedRecordsCount.ToString(), x.AverageDurationMinutes?.ToString() ?? "—" }).ToList()
                            }
                        };
                    }

                case "cancellations":
                    {
                        var data = (CancellationsReportResponse)(await _reportService.GetCancellationsAsync(filter)).Data;
                        return new ReportPdfViewModel
                        {
                            Title = "Cancelamentos",
                            Subtitle = "Cirurgias com status Cancelada no período selecionado.",
                            PeriodLabel = periodLabel,
                            FiltersLabel = filtersLabel,
                            GeneratedAt = DateTime.Now,
                            Kpis =
                            [
                                new() { Label = "Total de cirurgias", Value = data.TotalSurgeries.ToString() },
                                new() { Label = "Canceladas", Value = $"{data.CanceledSurgeries} ({data.CanceledPercentage}%)" }
                            ],
                            Charts = [new ReportPdfChart { Title = "Cancelamentos por dia da semana", Type = "bar", Labels = data.ByWeekday.Select(x => x.Name).ToList(), Values = data.ByWeekday.Select(x => (decimal)x.Count).ToList() }],
                            Table = new ReportPdfTable
                            {
                                Headers = ["Anestesista", "Cancelamentos"],
                                Rows = data.ByAnesthetist.Select(x => new List<string> { x.Name, x.Count.ToString() }).ToList()
                            }
                        };
                    }

                case "asa":
                    {
                        var data = (AsaReportResponse)(await _reportService.GetAsaAsync(filter)).Data;
                        return new ReportPdfViewModel
                        {
                            Title = "Classificação ASA",
                            Subtitle = "Distribuição da classificação ASA registrada na avaliação pré-anestésica.",
                            PeriodLabel = periodLabel,
                            FiltersLabel = filtersLabel,
                            GeneratedAt = DateTime.Now,
                            Kpis = [new() { Label = "Total avaliado", Value = data.TotalEvaluated.ToString() }],
                            Charts = [new ReportPdfChart { Title = "Distribuição ASA", Type = "doughnut", Labels = data.Distribution.Select(x => x.Label).ToList(), Values = data.Distribution.Select(x => (decimal)x.Count).ToList() }],
                            Table = new ReportPdfTable
                            {
                                Headers = ["Classificação", "Quantidade", "Percentual"],
                                Rows = data.Distribution.Select(x => new List<string> { x.Label, x.Count.ToString(), $"{x.Percentage}%" }).ToList()
                            }
                        };
                    }

                case "recovery":
                    {
                        var data = (RecoveryReportResponse)(await _reportService.GetRecoveryAsync(filter)).Data;
                        return new ReportPdfViewModel
                        {
                            Title = "Recuperação / Aldrete",
                            Subtitle = "Não há registro de horário de entrada/alta da RPA no sistema. O tempo abaixo é entre o fim da anestesia e a avaliação de Aldrete.",
                            PeriodLabel = periodLabel,
                            FiltersLabel = filtersLabel,
                            GeneratedAt = DateTime.Now,
                            Kpis =
                            [
                                new() { Label = "Pacientes avaliados", Value = data.PatientsEvaluated.ToString() },
                                new() { Label = "Tempo médio até avaliação de Aldrete", Value = data.AverageMinutesToAldreteEvaluation != null ? $"{data.AverageMinutesToAldreteEvaluation} min (n={data.EvaluationsConsideredForTiming})" : "—" }
                            ],
                            Charts = [new ReportPdfChart { Title = "Distribuição do score de Aldrete", Type = "bar", Labels = data.ScoreDistribution.Select(x => $"Score {x.Score}").ToList(), Values = data.ScoreDistribution.Select(x => (decimal)x.Count).ToList() }],
                            Table = new ReportPdfTable
                            {
                                Headers = ["Score", "Quantidade"],
                                Rows = data.ScoreDistribution.Select(x => new List<string> { x.Score.ToString(), x.Count.ToString() }).ToList()
                            }
                        };
                    }

                case "antibiotic-prophylaxis":
                    {
                        var data = (AntibioticProphylaxisReportResponse)(await _reportService.GetAntibioticProphylaxisAsync(filter)).Data;
                        return new ReportPdfViewModel
                        {
                            Title = "Antibioticoprofilaxia",
                            Subtitle = "Considera todas as fichas do período — o sistema não registra critério de elegibilidade nem horário de incisão cirúrgica.",
                            PeriodLabel = periodLabel,
                            FiltersLabel = filtersLabel,
                            GeneratedAt = DateTime.Now,
                            Kpis =
                            [
                                new() { Label = "Total de cirurgias", Value = data.TotalSurgeries.ToString() },
                                new() { Label = "Com profilaxia registrada", Value = data.SurgeriesWithProphylaxis.ToString() },
                                new() { Label = "Sem registro", Value = data.SurgeriesWithoutProphylaxis.ToString() },
                                new() { Label = "Adesão", Value = $"{data.AdherencePercentage}%" }
                            ],
                            Charts = [new ReportPdfChart { Title = "Medicamentos mais usados", Type = "bar", Labels = data.TopMedications.Select(x => x.Name).ToList(), Values = data.TopMedications.Select(x => (decimal)x.Count).ToList() }],
                            Table = new ReportPdfTable
                            {
                                Headers = ["Medicamento", "Quantidade"],
                                Rows = data.TopMedications.Select(x => new List<string> { x.Name, x.Count.ToString() }).ToList()
                            }
                        };
                    }

                case "fluid-balance":
                    {
                        var data = (FluidBalanceReportResponse)(await _reportService.GetFluidBalanceAsync(filter)).Data;
                        return new ReportPdfViewModel
                        {
                            Title = "Balanço Hídrico",
                            Subtitle = "Volumes registrados na monitorização intraoperatória.",
                            PeriodLabel = periodLabel,
                            FiltersLabel = filtersLabel,
                            GeneratedAt = DateTime.Now,
                            Kpis =
                            [
                                new() { Label = "Volume ganho", Value = $"{data.TotalGainMl} mL" },
                                new() { Label = "Volume perdido", Value = $"{data.TotalLossMl} mL" },
                                new() { Label = "Balanço", Value = $"{data.Balance} mL" },
                                new() { Label = "Sangramento", Value = $"{data.BleedingMl} mL" },
                                new() { Label = "Hemocomponentes", Value = $"{data.BloodProductMl} mL" }
                            ],
                            Charts = [new ReportPdfChart { Title = "Por categoria", Type = "doughnut", Labels = data.ByCategory.Select(x => x.Name).ToList(), Values = data.ByCategory.Select(x => x.TotalMl).ToList() }],
                            Table = new ReportPdfTable
                            {
                                Headers = ["Categoria", "Volume (mL)"],
                                Rows = data.ByCategory.Select(x => new List<string> { x.Name, x.TotalMl.ToString() }).ToList()
                            }
                        };
                    }

                case "integration-status":
                    {
                        var data = (IntegrationStatusReportResponse)(await _reportService.GetIntegrationStatusAsync()).Data;
                        return new ReportPdfViewModel
                        {
                            Title = "Saúde da Integração AGHU/SIGA",
                            Subtitle = "Estado atual da sincronização — não há histórico armazenado, apenas o instante mais recente de cada sincronização.",
                            PeriodLabel = "Não se aplica",
                            FiltersLabel = "Não se aplica",
                            GeneratedAt = DateTime.Now,
                            Kpis =
                            [
                                new() { Label = "Banco de dados", Value = data.DatabaseHealthy ? "Disponível" : "Indisponível" },
                                new() { Label = "AGHU", Value = data.AghuHealthy ? "Disponível" : "Indisponível" }
                            ],
                            Table = new ReportPdfTable
                            {
                                Headers = ["Sincronização", "Última execução", "Status"],
                                Rows =
                                [
                                    ["Medicamentos", FormatSync(data.Medicines.LastSyncAt), data.Medicines.IsStale ? "Desatualizada" : "Em dia"],
                                    ["Procedimentos", FormatSync(data.Procedures.LastSyncAt), data.Procedures.IsStale ? "Desatualizada" : "Em dia"],
                                    ["Profissionais", FormatSync(data.Professionals.LastSyncAt), data.Professionals.IsStale ? "Desatualizada" : "Em dia"]
                                ]
                            }
                        };
                    }

                default:
                    return null;
            }
        }

        private static string FormatSync(DateTime? value) => value == null ? "Nunca sincronizado" : value.Value.ToLocalTime().ToString("dd/MM/yyyy HH:mm");

        private async Task<string> BuildFiltersLabelAsync(ReportFilterQuery filter, DrugCategoryEnum? category)
        {
            var parts = new List<string>();

            if (filter.AnesthesiologistId.HasValue)
            {
                var options = await _reportRepository.GetAnesthetistOptionsAsync();
                var name = options.FirstOrDefault(x => x.Id == filter.AnesthesiologistId.Value)?.Name;
                parts.Add($"Anestesista: {name ?? $"#{filter.AnesthesiologistId}"}");
            }

            if (filter.Status.HasValue)
                parts.Add($"Status: {filter.Status}");

            if (category.HasValue)
                parts.Add($"Categoria: {category}");

            return parts.Count > 0 ? string.Join(" · ", parts) : "Todos";
        }
    }
}
