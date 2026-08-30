using UFF.FichaAnestesica.Domain.Commands.Reports;
using UFF.FichaAnestesica.Domain.Enums;
using UFF.FichaAnestesica.Domain.Response.Reports;

namespace UFF.FichaAnestesica.Domain.Repositories
{
    public interface IReportRepository
    {
        Task<ReportsSummaryResponse> GetSummaryAsync(ReportFilterQuery filter);
        Task<ClinicalEventsReportResponse> GetClinicalEventsAsync(ReportFilterQuery filter);
        Task<DrugConsumptionReportResponse> GetDrugConsumptionAsync(ReportFilterQuery filter, DrugCategoryEnum? category);
        Task<SurgeriesReportResponse> GetSurgeriesAsync(ReportFilterQuery filter);
        Task<AnesthetistsReportResponse> GetAnesthetistsAsync(ReportFilterQuery filter);
        Task<CancellationsReportResponse> GetCancellationsAsync(ReportFilterQuery filter);
        Task<AsaReportResponse> GetAsaAsync(ReportFilterQuery filter);
        Task<RecoveryReportResponse> GetRecoveryAsync(ReportFilterQuery filter);
        Task<AntibioticProphylaxisReportResponse> GetAntibioticProphylaxisAsync(ReportFilterQuery filter);
        Task<FluidBalanceReportResponse> GetFluidBalanceAsync(ReportFilterQuery filter);
        Task<List<AnesthetistOptionResponse>> GetAnesthetistOptionsAsync();
    }
}
