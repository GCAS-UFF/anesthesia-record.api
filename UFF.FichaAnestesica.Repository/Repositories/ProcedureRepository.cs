using Microsoft.EntityFrameworkCore;
using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Repositories;
using UFF.FichaAnestesica.Infra.Context;

namespace UFF.FichaAnestesica.Infra.Repositories
{
    public class ProcedureRepository : RepositoryBase<Procedure>, IProcedureRepository
    {
        private readonly SigaDbCtx _context;

        public ProcedureRepository(SigaDbCtx context)
            : base(context)
        {
            _context = context;
        }

        public async Task<bool> ExistsByNameAsync(string name)
        {
            return await _context.Procedures
                 .AnyAsync(x => x.Description.ToLower() == name.ToLower());
        }

        public async Task<List<Procedure>> GetActivesOnlyAsync()
        {
            return await _context.Procedures
                .Where(x => x.Active)
                .OrderBy(x => x.Description)
                .ToListAsync();
        }

        public async Task<Procedure?> GetByNameAsync(string description)
        {
            return await _context.Procedures
              .FirstOrDefaultAsync(x => x.Description.ToLower() == description.ToLower());
        }

        public async Task<List<Procedure>> SearchByNameAsync(string search)
        {
            return await _context.Procedures
                .Where(x => x.Description.ToLower().Contains(search.ToLower()))
                .OrderBy(x => x.Description)
                .ToListAsync();
        }      
    }
}