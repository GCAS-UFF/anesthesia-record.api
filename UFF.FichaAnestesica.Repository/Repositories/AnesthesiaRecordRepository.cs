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


        public async Task RemoveProceduresAsync(int anesthesiaRecordId)
        {
           var procedures =  await _context.AnesthesiaRecordProcedures
                .Where(x => x.AnesthesiaRecordId == anesthesiaRecordId)
                .ToArrayAsync();

            _context.RemoveRange(procedures);
            _context.SaveChanges();
        }

        public async Task<AnesthesiaRecord> GetByIdAsync(int id)
        {
            return await _context.AnesthesiaRecords
                            .Include(x => x.FirstAnesthesiologist)
                            .Include(x => x.SecondAnesthesiologist)
                            .Include(x => x.Surgeon)
                            .Include(x => x.Assistant)
                            .Include(x => x.Surgeries)
                                .ThenInclude(x => x.Procedure)
                            .Include(x => x.Antibiotics)
                              .ThenInclude(x => x.Boosters)
                            .Include(x => x.MonitoringRecord)
                            .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<AnesthesiaRecord>> GetByIdsAsync(IEnumerable<string> ids)
        {
            return await _context.AnesthesiaRecords
                .Include(x => x.FirstAnesthesiologist)
                .Include(x => x.SecondAnesthesiologist)
                .Include(x => x.Surgeries)
                .Where(x => ids.Contains(x.PatientId))
                .ToListAsync();
        }

        public async Task<bool> CanAssumePatientsAsync(int id)
        {
            return await _context.AnesthesiaRecords
                .AnyAsync(x => x.FirstAnesthesiologistId == id
                && (x.Status == SurgeryStatusEnum.InProgress || x.Status == SurgeryStatusEnum.Scheduled
                || x.Status ==SurgeryStatusEnum.Preparing));
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
                .FirstOrDefaultAsync(x => x.PatientId.ToLower() == id.ToLower());
        }
    }
}