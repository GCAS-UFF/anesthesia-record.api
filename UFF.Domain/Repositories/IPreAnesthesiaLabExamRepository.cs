using UFF.FichaAnestesica.Domain.Entities;

namespace UFF.FichaAnestesica.Domain.Repositories
{
    public interface IPreAnesthesiaLabExamRepository : IRepositoryBase<PreAnesthesiaLabExam>
    {
        Task<PreAnesthesiaLabExam?> GetByAnesthesiaRecordIdAsync(int anesthesiaRecordId);

        Task<bool> TrySaveChangesAsync();
    }
}
