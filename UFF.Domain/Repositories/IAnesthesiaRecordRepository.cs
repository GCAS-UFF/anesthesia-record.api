using UFF.FichaAnestesica.Domain.Entities;

namespace UFF.FichaAnestesica.Domain.Repositories
{
    public interface IAnesthesiaRecordRepository : IRepositoryBase<AnesthesiaRecord>
    {
        Task<AnesthesiaRecord> GetByIdAsync(int id);
    }
}