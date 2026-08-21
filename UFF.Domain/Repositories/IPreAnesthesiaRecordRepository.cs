using UFF.FichaAnestesica.Domain.Entities;

namespace UFF.FichaAnestesica.Domain.Repositories
{
    public interface IPreAnesthesiaRecordRepository : IRepositoryBase<PreAnesthesiaRecord>
    {
        Task<PreAnesthesiaRecord?> GetCompleteByIdAsync(int id);
        Task<PreAnesthesiaRecord?> GetByAnesthesiaRecordIdAsync(int anesthesiaRecordId);
        Task<bool> ExistsByAnesthesiaRecordIdAsync(int anesthesiaRecordId);
        HashSet<int> GetCompletedAnesthesiaRecordIds(IEnumerable<int> anesthesiaRecordIds);
    }
}
