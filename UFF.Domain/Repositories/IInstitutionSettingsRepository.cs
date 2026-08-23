using UFF.FichaAnestesica.Domain.Entities;

namespace UFF.FichaAnestesica.Domain.Repositories
{
    public interface IInstitutionSettingsRepository : IRepositoryBase<InstitutionSettings>
    {
        Task<InstitutionSettings> GetSingletonAsync();
    }
}
