using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Domain.Repositories
{
    public interface IAnesthesiaRecordRepository : IRepositoryBase<AnesthesiaRecord>
    {
        Task<AnesthesiaRecord> GetByIdAsync(int id);
        Task<List<AnesthesiaRecord>> GetByIdsAsync(IEnumerable<string> ids);
        Task<AnesthesiaRecord> GetByExternalPatientIdAsync(string id);
        Task<IEnumerable<AnesthesiaRecord>> GetByDoctorAndDateAsync(int doctorId, DateTime? date);
        Task<(IEnumerable<AnesthesiaRecord> Items, int TotalItems)> GetPagedByDoctorPrioritizedAsync(int doctorId, DateTime? date, int page, int pageSize);
        Task<IEnumerable<AnesthesiaRecord>> GetByStatusAndDateAsync(SurgeryStatusEnum status, DateTime? date);
        Task<bool> CanAssumePatientsAsync(int id);
        Task RemoveProceduresAsync(int anesthesiaRecordId);
    }
}