using Microsoft.EntityFrameworkCore;
using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Enums;
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
                            .Include(x => x.FirstAnesthesiologist)
                            .Include(x => x.SecondAnesthesiologist)
                            .Include(x => x.Surgeon)
                            .Include(x => x.Assistant)
                            .Include(x => x.MonitoringRecord)
                            .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<AnesthesiaRecord>> GetByIdsAsync(IEnumerable<string> ids)
        {
            return await _context.AnesthesiaRecords
                .Include(x => x.FirstAnesthesiologist)
                .Include(x => x.SecondAnesthesiologist)
                .Include(x => x.Procedures)
                .Where(x => ids.Contains(x.ExternalPatientId))
                .ToListAsync();
        }

        public async Task<bool> CanAssumePatientsAsync(int id)
        {
            return await _context.AnesthesiaRecords
                .AnyAsync(x => x.FirstAnesthesiologistId == id
                && (x.Status == Domain.Enums.SurgeryStatusEnum.InProgress || x.Status == Domain.Enums.SurgeryStatusEnum.Scheduled
                || x.Status == Domain.Enums.SurgeryStatusEnum.Preparing));
        }

        public async Task<IEnumerable<AnesthesiaRecord>> GetByDoctorAndDateAsync(int doctorId, DateTime? date)
        {
            return await _context.AnesthesiaRecords
                .Include(x => x.FirstAnesthesiologist)
                .Where(x => x.FirstAnesthesiologistId == doctorId && x.SurgeryDate == date)
                .OrderBy(x => x.Status == SurgeryStatusEnum.InProgress ? 0 :
                              x.Status == SurgeryStatusEnum.Preparing ? 1 :
                              x.Status == SurgeryStatusEnum.Scheduled ? 2 :
                              x.Status == SurgeryStatusEnum.Completed ? 3 :
                              4)
                .ThenBy(x => x.SurgeryDate)
                .ToListAsync();
        }

        public async Task<AnesthesiaRecord> GetByExternalPatientIdAsync(string id)
        {
            return await _context.AnesthesiaRecords
                .Include(x => x.FirstAnesthesiologist)
                .Include(x => x.SecondAnesthesiologist)
                .FirstOrDefaultAsync(x => x.ExternalPatientId.ToLower() == id.ToLower());
        }
    }
}