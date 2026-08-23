using UFF.FichaAnestesica.Domain.Entities;

namespace UFF.FichaAnestesica.Domain.Repositories
{
    public interface IUserSettingsRepository : IRepositoryBase<UserSettings>
    {
        Task<UserSettings> GetByUserIdAsync(int userId);
    }
}
