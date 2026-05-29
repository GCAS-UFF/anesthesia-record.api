using UFF.FichaAnestesica.Domain.Entities;

namespace UFF.FichaAnestesica.Domain.Repositories
{
    public interface IVitalSignRecordRepository : IRepositoryBase<VitalSignRecord>
    {
        Task<List<VitalSignRecord>> GetByMonitoringRecordIdAsync(int monitoringRecordId);

        Task<List<VitalSignRecord>> GetByPeriodAsync(DateTime start, DateTime end);

        Task<VitalSignRecord?> GetLatestAsync(int monitoringRecordId);

        Task<List<VitalSignRecord>> GetLatestAsync(int monitoringRecordId, int quantity);

        Task<List<VitalSignRecord>> GetWithCustomFieldsAsync(int monitoringRecordId);
    }
}