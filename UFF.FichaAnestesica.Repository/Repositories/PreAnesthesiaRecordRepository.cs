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

        public async Task<bool> ExistsByAnesthesiaRecordIdAsync(int anesthesiaRecordId)
        {
            return await _context.PreAnesthesiaRecords
                .AnyAsync(x => x.AnesthesiaRecordId == anesthesiaRecordId);
        }

        public HashSet<int> GetCompletedAnesthesiaRecordIds(IEnumerable<int> anesthesiaRecordIds)
        {
            return _context.PreAnesthesiaRecords
                .Where(x =>
                    anesthesiaRecordIds.Contains(x.AnesthesiaRecordId) &&
                    x.SignedAt != null)
                .Select(x => x.AnesthesiaRecordId)
                .ToHashSet();
        }

        public async Task<PreAnesthesiaRecord?> GetCompleteByIdAsync(int id)
        {
            return await _context.PreAnesthesiaRecords
                .Include(x => x.AnesthesiaRecord)
                .Include(x => x.SignedByProfessional)
                .Include(x => x.Surgeries)
                .Include(x => x.Comorbidities)
                .Include(x => x.Medications)
                .Include(x => x.PhysicalExamAreas)
                .Include(x => x.Reports)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<PreAnesthesiaRecord?> GetByAnesthesiaRecordIdAsync(int anesthesiaRecordId)
        {
            return await _context.PreAnesthesiaRecords
                .AsNoTracking()
                .Include(x => x.AnesthesiaRecord)
                .Include(x => x.SignedByProfessional)
                .Include(x => x.Surgeries)
                .Include(x => x.Comorbidities)
                .Include(x => x.Medications)
                .Include(x => x.PhysicalExamAreas)
                .Include(x => x.Reports)
                .FirstOrDefaultAsync(x => x.AnesthesiaRecordId == anesthesiaRecordId);
        }
    }
}
