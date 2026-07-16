using UFF.FichaAnestesica.Domain.Entities;

namespace UFF.FichaAnestesica.Domain.Repositories
{
    public interface IProcedureRepository : IRepositoryBase<Procedure>
    {
        Task<Procedure?> GetByNameAsync(string name);

        Task<List<Procedure>> SearchByNameAsync(string search);

        Task<bool> ExistsByNameAsync(string name);

        Task<List<Procedure>> GetActivesOnlyAsync();
    }
}