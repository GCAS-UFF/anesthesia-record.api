using Microsoft.EntityFrameworkCore;
using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Repositories;
using UFF.FichaAnestesica.Infra.Context;

namespace UFF.FichaAnestesica.Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SigaDbCtx _context;

        public UserRepository(SigaDbCtx context)
        {
            _context = context;
        }

        public async Task<User> GetUserByLogin(string email)
            => await _context.Users
               .FirstOrDefaultAsync(p => p.Email == email);
    }
}