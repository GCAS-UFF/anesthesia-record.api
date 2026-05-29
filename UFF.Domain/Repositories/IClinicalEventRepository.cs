using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Domain.Repositories
{
    public interface IClinicalEventRepository : IRepositoryBase<ClinicalEvent>
    {
        Task<List<ClinicalEvent>> GetByMonitoringRecordIdAsync(int monitoringRecordId);

        Task<List<ClinicalEvent>> GetByTypeAsync(ClinicalEventTypeEnum type);

        Task<List<ClinicalEvent>> GetByPeriodAsync(DateTime start, DateTime end);

        Task<ClinicalEvent?> GetDetailedByIdAsync(int id);
    }
}