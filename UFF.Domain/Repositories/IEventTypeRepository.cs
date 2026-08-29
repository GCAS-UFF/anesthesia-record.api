using UFF.FichaAnestesica.Domain.Entities;

namespace UFF.FichaAnestesica.Domain.Repositories
{
    public interface IEventTypeRepository : IRepositoryBase<EventType>
    {
        Task<EventType?> GetByIdAsync(int id);
        Task<bool> ExistsByNameAsync(string name, int? excludeId = null);
        Task<List<EventType>> GetActiveAsync();
        Task<(List<EventType> Items, int TotalItems)> GetPagedAsync(string? term, int page, int pageSize);
    }
}
