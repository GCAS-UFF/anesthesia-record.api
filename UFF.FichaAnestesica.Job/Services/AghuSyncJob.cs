using UFF.FichaAnestesica.Infra.Repositories.Aghu;
using UFF.FichaAnestesica.Service.Services.Aghu;

namespace UFF.FichaAnestesica.Job.Services
{
    public class AghuSyncJob : BackgroundService
    {
        private readonly IMedicineApiService _medicineApiService;
        private readonly IProfessionalApiService _professionalService;
        private readonly IPatientApiService _petientService;
        private readonly ILogger<AghuSyncJob> _logger;

        public AghuSyncJob(IMedicineApiService medicineApiService, IProfessionalApiService professionalApiService, IPatientApiService patientApiService, ILogger<AghuSyncJob> logger)
        {
            _medicineApiService = medicineApiService;
            _professionalService = professionalApiService;
            _petientService = patientApiService;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Executando job em: {time}", DateTime.Now);

                await Sync();

                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
            }
        }

        private async Task Sync()
        {
            await _medicineApiService.SyncMedicines();
            await _professionalService.SyncProfessionals();

            await Task.CompletedTask;
        }
    }
}
