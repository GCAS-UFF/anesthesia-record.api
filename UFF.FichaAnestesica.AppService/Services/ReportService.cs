using UFF.FichaAnestesica.Domain.Commands;
using UFF.FichaAnestesica.Domain.Commands.Reports;
using UFF.FichaAnestesica.Domain.Enums;
using UFF.FichaAnestesica.Domain.Repositories;
using UFF.FichaAnestesica.Domain.Repositories.ReadOnly;
using UFF.FichaAnestesica.Domain.Response.Reports;
using UFF.FichaAnestesica.Domain.Services;

namespace UFF.FichaAnestesica.Service.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;
        private readonly IDrugService _drugService;
        private readonly IProcedureService _procedureService;
        private readonly IProfessionalService _professionalService;
        private readonly IHealthReadOnlyRepository _healthRepository;

        private const int SyncIntervalMinutes = 1440;

        public ReportService(
            IReportRepository reportRepository,
            IDrugService drugService,
            IProcedureService procedureService,
            IProfessionalService professionalService,
            IHealthReadOnlyRepository healthRepository)
        {
            _reportRepository = reportRepository;
            _drugService = drugService;
            _procedureService = procedureService;
            _professionalService = professionalService;
            _healthRepository = healthRepository;
        }

        public async Task<CommandResult> GetSummaryAsync(ReportFilterQuery filter)
        {
            var validationError = filter.Validate();
            if (validationError != null)
                return CommandResult.Fail(validationError);

            var response = await _reportRepository.GetSummaryAsync(filter);

            response.LastMedicineSyncAt = await _drugService.GetLasIntegrationTime();
            response.LastProcedureSyncAt = await _procedureService.GetLasIntegrationTime();
            response.LastProfessionalSyncAt = await _professionalService.GetLastIntegrationTime();

            return CommandResult.Success(response);
        }

        public async Task<CommandResult> GetClinicalEventsAsync(ReportFilterQuery filter)
        {
            var validationError = filter.Validate();
            if (validationError != null)
                return CommandResult.Fail(validationError);

            var response = await _reportRepository.GetClinicalEventsAsync(filter);
            return CommandResult.Success(response);
        }

        public async Task<CommandResult> GetDrugConsumptionAsync(ReportFilterQuery filter, DrugCategoryEnum? category)
        {
            var validationError = filter.Validate();
            if (validationError != null)
                return CommandResult.Fail(validationError);

            var response = await _reportRepository.GetDrugConsumptionAsync(filter, category);
            return CommandResult.Success(response);
        }

        public async Task<CommandResult> GetSurgeriesAsync(ReportFilterQuery filter)
        {
            var validationError = filter.Validate();
            if (validationError != null)
                return CommandResult.Fail(validationError);

            var response = await _reportRepository.GetSurgeriesAsync(filter);
            return CommandResult.Success(response);
        }

        public async Task<CommandResult> GetAnesthetistsAsync(ReportFilterQuery filter)
        {
            var validationError = filter.Validate();
            if (validationError != null)
                return CommandResult.Fail(validationError);

            var response = await _reportRepository.GetAnesthetistsAsync(filter);
            return CommandResult.Success(response);
        }

        public async Task<CommandResult> GetCancellationsAsync(ReportFilterQuery filter)
        {
            var validationError = filter.Validate();
            if (validationError != null)
                return CommandResult.Fail(validationError);

            var response = await _reportRepository.GetCancellationsAsync(filter);
            return CommandResult.Success(response);
        }

        public async Task<CommandResult> GetAsaAsync(ReportFilterQuery filter)
        {
            var validationError = filter.Validate();
            if (validationError != null)
                return CommandResult.Fail(validationError);

            var response = await _reportRepository.GetAsaAsync(filter);
            return CommandResult.Success(response);
        }

        public async Task<CommandResult> GetRecoveryAsync(ReportFilterQuery filter)
        {
            var validationError = filter.Validate();
            if (validationError != null)
                return CommandResult.Fail(validationError);

            var response = await _reportRepository.GetRecoveryAsync(filter);
            return CommandResult.Success(response);
        }

        public async Task<CommandResult> GetAntibioticProphylaxisAsync(ReportFilterQuery filter)
        {
            var validationError = filter.Validate();
            if (validationError != null)
                return CommandResult.Fail(validationError);

            var response = await _reportRepository.GetAntibioticProphylaxisAsync(filter);
            return CommandResult.Success(response);
        }

        public async Task<CommandResult> GetFluidBalanceAsync(ReportFilterQuery filter)
        {
            var validationError = filter.Validate();
            if (validationError != null)
                return CommandResult.Fail(validationError);

            var response = await _reportRepository.GetFluidBalanceAsync(filter);
            return CommandResult.Success(response);
        }

        public async Task<CommandResult> GetIntegrationStatusAsync()
        {
            var lastDrugSync = await _drugService.GetLasIntegrationTime();
            var lastProcedureSync = await _procedureService.GetLasIntegrationTime();
            var lastProfessionalSync = await _professionalService.GetLastIntegrationTime();

            var health = await _healthRepository.CheckHealth();

            var response = new IntegrationStatusReportResponse
            {
                DatabaseHealthy = health.bd,
                AghuHealthy = health.aghu,
                Medicines = BuildSyncStatus(lastDrugSync),
                Procedures = BuildSyncStatus(lastProcedureSync),
                Professionals = BuildSyncStatus(lastProfessionalSync),
                CheckedAt = DateTime.UtcNow
            };

            return CommandResult.Success(response);
        }

        public async Task<CommandResult> GetAnesthetistOptionsAsync()
        {
            var options = await _reportRepository.GetAnesthetistOptionsAsync();
            return CommandResult.Success(options);
        }

        private static SyncStatusItem BuildSyncStatus(DateTime? lastSyncAt)
        {
            var isStale = !lastSyncAt.HasValue || (DateTime.UtcNow - lastSyncAt.Value).TotalMinutes > SyncIntervalMinutes * 1.5;
            return new SyncStatusItem { LastSyncAt = lastSyncAt, IsStale = isStale };
        }
    }
}
