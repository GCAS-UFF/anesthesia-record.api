using Microsoft.EntityFrameworkCore;
using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Repositories;
using UFF.FichaAnestesica.Infra.Context;

namespace UFF.FichaAnestesica.Infra.Repositories
{
    public class DrugRepository : RepositoryBase<Drug>, IDrugRepository
    {
        private readonly SigaDbCtx _context;

        public DrugRepository(SigaDbCtx context)
            : base(context)
        {
            _context = context;
        }

        public async Task<Drug?> GetByNameAsync(string name)
        {
            return await _context.Drugs
                .FirstOrDefaultAsync(x => x.Description == name);
        }

        public async Task<List<Drug>> SearchByNameAsync(string search)
        {
            return await _context.Drugs
                .Where(x => x.Description.Contains(search))
                .OrderBy(x => x.Description)
                .ToListAsync();
        }

        public async Task<bool> ExistsByNameAsync(string name)
        {
            return await _context.Drugs
                .AnyAsync(x => x.Description == name);
        }

        public async Task<List<Drug>> GetActiveAsync()
        {
            return await _context.Drugs
                .OrderBy(x => x.Description)
                .ToListAsync();
        }
    }
}