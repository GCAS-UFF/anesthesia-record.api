using Microsoft.EntityFrameworkCore;
using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Enums;
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

        public async Task<DateTime?> GetLastTimeIntegration()
        {
            var drug = await _context.Drugs.FirstOrDefaultAsync();
            return drug == null ? null : drug.LastSyncAt;
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
                .AnyAsync(x => x.Description.ToLower() == name.ToLower());
        }

        public async Task<List<Drug>> GetActiveAsync()
        {
            return await _context.Drugs
                .OrderBy(x => x.Description)
                .ToListAsync();
        }

        public async Task<Drug?> GetByIdAsync(int id)
        {
            return await _context.Drugs.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<(List<Drug> Items, int TotalItems)> GetPagedAsync(string? term, DrugCategoryEnum? category, int page, int pageSize)
        {
            var query = _context.Drugs.AsQueryable();

            if (!string.IsNullOrWhiteSpace(term))
                query = query.Where(x => x.Description.ToLower().Contains(term.ToLower()));

            if (category.HasValue)
                query = query.Where(x => x.Category == category.Value);

            var totalItems = await query.CountAsync();

            var items = await query
                .OrderBy(x => x.Description)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalItems);
        }
    }
}