using UFF.FichaAnestesica.Domain.Entities;

namespace UFF.FichaAnestesica.Domain.Repositories
{
    public interface IAnesthesiaRecordRepository : IRepositoryBase<AnesthesiaRecord>
    {
        Task<AnesthesiaRecord> GetByIdAsync(int id);
        Task<List<AnesthesiaRecord>> GetByIdsAsync(IEnumerable<string> ids);
        Task<AnesthesiaRecord> GetByExternalPatientIdAsync(string id);
        Task<IEnumerable<AnesthesiaRecord>> GetByDoctorAndDateAsync(int doctorId, DateTime? date);
    }
}