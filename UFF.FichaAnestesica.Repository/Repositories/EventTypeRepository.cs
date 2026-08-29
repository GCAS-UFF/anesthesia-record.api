using Microsoft.EntityFrameworkCore;
using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Repositories;
using UFF.FichaAnestesica.Infra.Context;

namespace UFF.FichaAnestesica.Infra.Repositories
{
    public class EventTypeRepository : RepositoryBase<EventType>, IEventTypeRepository
    {
        private readonly SigaDbCtx _context;

        public EventTypeRepository(SigaDbCtx context)
            : base(context)
        {
            _context = context;
        }

        public async Task<EventType?> GetByIdAsync(int id)
        {
            return await _context.EventTypes.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> ExistsByNameAsync(string name, int? excludeId = null)
        {
            return await _context.EventTypes
                .AnyAsync(x => x.Name.ToLower() == name.ToLower() && (!excludeId.HasValue || x.Id != excludeId.Value));
        }

        public async Task<List<EventType>> GetActiveAsync()
        {
            return await _context.EventTypes
                .Where(x => x.Active)
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<(List<EventType> Items, int TotalItems)> GetPagedAsync(string? term, int page, int pageSize)
        {
            var query = _context.EventTypes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(term))
                query = query.Where(x => x.Name.ToLower().Contains(term.ToLower()));

            var totalItems = await query.CountAsync();

            var items = await query
                .OrderBy(x => x.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalItems);
        }
    }
}
