using UFF.FichaAnestesica.Domain.Entities;

namespace UFF.FichaAnestesica.Domain.Repositories
{
    public interface IMonitoringRecordRepository : IRepositoryBase<MonitoringRecord>
    {
        Task<MonitoringRecord?> GetCompleteByIdAsync(int id);

        Task<MonitoringRecord?> GetByAnesthesiaRecordIdAsync(int anesthesiaRecordId);

        Task<List<MonitoringRecord>> GetBySurgeryIdAsync(int surgeryId);

        Task<List<MonitoringRecord>> GetByProfessionalIdAsync(int professionalId);

        Task<List<MonitoringRecord>> GetByPeriodAsync(DateTime start, DateTime end);

        Task<MonitoringRecord?> GetActiveBySurgeryIdAsync(int surgeryId);
    }
}