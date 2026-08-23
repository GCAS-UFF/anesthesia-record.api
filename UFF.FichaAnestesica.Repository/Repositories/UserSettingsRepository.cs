using Microsoft.EntityFrameworkCore;
using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Repositories;
using UFF.FichaAnestesica.Infra.Context;

namespace UFF.FichaAnestesica.Infra.Repositories
{
    public class UserSettingsRepository : RepositoryBase<UserSettings>, IUserSettingsRepository
    {
        private readonly SigaDbCtx _context;

        public UserSettingsRepository(SigaDbCtx context)
            : base(context)
        {
            _context = context;
        }

        public async Task<UserSettings> GetByUserIdAsync(int userId)
        {
            return await _context.UserSettings
                .FirstOrDefaultAsync(x => x.UserId == userId);
        }
    }
}
