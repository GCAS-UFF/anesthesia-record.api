using Microsoft.EntityFrameworkCore;
using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Repositories;
using UFF.FichaAnestesica.Infra.Context;

namespace UFF.FichaAnestesica.Infra.Repositories
{
    public class CustomFieldRepository : RepositoryBase<CustomField>, ICustomFieldRepository
    {
        private readonly SigaDbCtx _context;

        public CustomFieldRepository(SigaDbCtx context)
            : base(context)
        {
            _context = context;
        }

        public async Task<List<CustomField>> GetByNameAsync(string name)
        {
            return await _context.CustomFields
                .Where(x => x.Name == name)
                .ToListAsync();
        }

        public async Task<List<CustomField>> GetByValueAsync(string value)
        {
            return await _context.CustomFields
                .Where(x => x.Value.Contains(value))
                .ToListAsync();
        }
    }
}