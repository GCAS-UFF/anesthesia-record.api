using Microsoft.EntityFrameworkCore;
using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Repositories;
using UFF.FichaAnestesica.Infra.Context;

namespace UFF.FichaAnestesica.Infra.Repositories
{
    public class InstitutionSettingsRepository : RepositoryBase<InstitutionSettings>, IInstitutionSettingsRepository
    {
        private readonly SigaDbCtx _context;

        public InstitutionSettingsRepository(SigaDbCtx context)
            : base(context)
        {
            _context = context;
        }

        public async Task<InstitutionSettings> GetSingletonAsync()
        {
            return await _context.InstitutionSettings.FirstOrDefaultAsync();
        }
    }
}
