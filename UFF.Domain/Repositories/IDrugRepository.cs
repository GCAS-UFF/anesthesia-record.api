using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Domain.Repositories
{
    public interface IDrugRepository : IRepositoryBase<Drug>
    {
        Task<Drug?> GetByNameAsync(string name);

        Task<List<Drug>> SearchByNameAsync(string search);

        Task<bool> ExistsByNameAsync(string name);

        Task<List<Drug>> GetActiveAsync();
        Task<DateTime?> GetLastTimeIntegration();

        Task<Drug?> GetByIdAsync(int id);

        Task<(List<Drug> Items, int TotalItems)> GetPagedAsync(string? term, DrugCategoryEnum? category, int page, int pageSize);
    }
}