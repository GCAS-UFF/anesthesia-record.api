using UFF.FichaAnestesica.Domain.Entities;

namespace UFF.FichaAnestesica.Domain.Repositories
{
    public interface IDrugRepository : IRepositoryBase<Drug>
    {
        Task<Drug?> GetByNameAsync(string name);

        Task<List<Drug>> SearchByNameAsync(string search);

        Task<bool> ExistsByNameAsync(string name);

        Task<List<Drug>> GetActiveAsync();
    }
}