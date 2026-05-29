using UFF.FichaAnestesica.Domain.Entities;

namespace UFF.FichaAnestesica.Domain.Repositories
{
    public interface ICustomFieldRepository : IRepositoryBase<CustomField>
    {
        Task<List<CustomField>> GetByNameAsync(string name);

        Task<List<CustomField>> GetByValueAsync(string value);
    }
}