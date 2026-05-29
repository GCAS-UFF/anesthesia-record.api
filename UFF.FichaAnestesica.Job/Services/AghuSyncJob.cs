using Quartz;
using UFF.FichaAnestesica.Infra.Repositories.Aghu;
using UFF.FichaAnestesica.Service.Services.Aghu;

namespace UFF.FichaAnestesica.Job.Services
{
    public class AghuSyncJob : IJob
    {
        private readonly IMedicineApiService _medicineApiService;
        private readonly IProfessionalApiService _professionalService;
        private readonly ILogger<AghuSyncJob> _logger;

        public AghuSyncJob(IMedicineApiService medicineApiService, IProfessionalApiService professionalApiService, ILogger<AghuSyncJob> logger)
        {
            _medicineApiService = medicineApiService;
            _professionalService = professionalApiService;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Job executed at: {time}", DateTime.Now);
            await Sync();
        }
      
        private async Task Sync()
        {
            await _medicineApiService.SyncMedicines();
            await _professionalService.SyncProfessionals();

            await Task.CompletedTask;
        }
    }
}
