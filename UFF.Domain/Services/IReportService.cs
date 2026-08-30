using UFF.FichaAnestesica.Domain.Commands;
using UFF.FichaAnestesica.Domain.Commands.Reports;
using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Domain.Services
{
    public interface IReportService
    {
        Task<CommandResult> GetSummaryAsync(ReportFilterQuery filter);
        Task<CommandResult> GetClinicalEventsAsync(ReportFilterQuery filter);
        Task<CommandResult> GetDrugConsumptionAsync(ReportFilterQuery filter, DrugCategoryEnum? category);
        Task<CommandResult> GetSurgeriesAsync(ReportFilterQuery filter);
        Task<CommandResult> GetAnesthetistsAsync(ReportFilterQuery filter);
        Task<CommandResult> GetCancellationsAsync(ReportFilterQuery filter);
        Task<CommandResult> GetAsaAsync(ReportFilterQuery filter);
        Task<CommandResult> GetRecoveryAsync(ReportFilterQuery filter);
        Task<CommandResult> GetAntibioticProphylaxisAsync(ReportFilterQuery filter);
        Task<CommandResult> GetFluidBalanceAsync(ReportFilterQuery filter);
        Task<CommandResult> GetIntegrationStatusAsync();
        Task<CommandResult> GetAnesthetistOptionsAsync();
    }
}
