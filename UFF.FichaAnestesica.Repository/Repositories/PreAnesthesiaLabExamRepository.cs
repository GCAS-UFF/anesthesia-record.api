using Microsoft.EntityFrameworkCore;
using Npgsql;
using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Repositories;
using UFF.FichaAnestesica.Infra.Context;

namespace UFF.FichaAnestesica.Infra.Repositories
{
    public class PreAnesthesiaLabExamRepository : RepositoryBase<PreAnesthesiaLabExam>, IPreAnesthesiaLabExamRepository
    {
        private readonly SigaDbCtx _context;

        public PreAnesthesiaLabExamRepository(SigaDbCtx context)
            : base(context)
        {
            _context = context;
        }

        public async Task<PreAnesthesiaLabExam?> GetByAnesthesiaRecordIdAsync(int anesthesiaRecordId)
        {
            return await _context.PreAnesthesiaLabExams
                .Include(x => x.Results)
                .FirstOrDefaultAsync(x => x.AnesthesiaRecordId == anesthesiaRecordId);
        }

        public async Task<bool> TrySaveChangesAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                _context.ChangeTracker.Clear();
                return false;
            }
            catch (DbUpdateException ex) when (ex.InnerException is PostgresException { SqlState: PostgresErrorCodes.UniqueViolation })
            {
                _context.ChangeTracker.Clear();
                return false;
            }
        }
    }
}
