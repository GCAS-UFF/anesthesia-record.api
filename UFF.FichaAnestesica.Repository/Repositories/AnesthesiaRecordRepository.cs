using Microsoft.EntityFrameworkCore;
using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Repositories;
using UFF.FichaAnestesica.Infra.Context;

namespace UFF.FichaAnestesica.Infra.Repositories
{
    public class AnesthesiaRecordRepository : RepositoryBase<AnesthesiaRecord>, IAnesthesiaRecordRepository
    {
        private readonly SigaDbCtx _context;

        public AnesthesiaRecordRepository(SigaDbCtx context)
            : base(context)
        {
            _context = context;
        }

        public async Task<AnesthesiaRecord> GetByIdAsync(int id)
        {
            return await _context.AnesthesiaRecords
                            .AsNoTracking()
                            .Include(x => x.FirstAnesthesiologist)
                            .Include(x => x.SecondAnesthesiologist)
                            .FirstOrDefaultAsync(x => x.Id == id);
        }      
    }
}