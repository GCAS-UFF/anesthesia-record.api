using Microsoft.EntityFrameworkCore;
using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Repositories;
using UFF.FichaAnestesica.Infra.Context;

namespace UFF.FichaAnestesica.Infra.Repositories
{
    public class PreAnesthesiaRecordRepository : RepositoryBase<PreAnesthesiaRecord>, IPreAnesthesiaRecordRepository
    {
        private readonly SigaDbCtx _context;

        public PreAnesthesiaRecordRepository(SigaDbCtx context)
            : base(context)
        {
            _context = context;
        }

        public async Task<PreAnesthesiaRecord> GetByIdAsync(int id)
        {
            return await _context.PreAnesthesiaRecords
                            .AsNoTracking()
                            .Include(x => x.FirstAnesthesiologist)
                            .Include(x => x.SecondAnesthesiologist)
                            .FirstOrDefaultAsync(x => x.Id == id);
        }      
    }
}