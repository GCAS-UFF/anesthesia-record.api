using Microsoft.EntityFrameworkCore;
using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Repositories;
using UFF.FichaAnestesica.Infra.Context;

namespace UFF.FichaAnestesica.Infra.Repositories
{
    public class MonitoringRecordRepository
        : RepositoryBase<MonitoringRecord>,
          IMonitoringRecordRepository
    {
        private readonly SigaDbCtx _context;

        public MonitoringRecordRepository(SigaDbCtx context)
            : base(context)
        {
            _context = context;
        }

        public async Task<MonitoringRecord?> GetCompleteByIdAsync(int id)
        {
            return await _context.MonitoringRecords
                .Include(x => x.VitalSigns)
                    .ThenInclude(x => x.CustomFields)
                .Include(x => x.AdministeredAgents)
                    .ThenInclude(x => x.Drug)
                .Include(x => x.ClinicalEvents)
                .Include(x => x.FluidBalances)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<MonitoringRecord?> GetByAnesthesiaRecordIdAsync(
            int anesthesiaRecordId)
        {
            return await _context.MonitoringRecords
                .FirstOrDefaultAsync(x =>
                    x.AnesthesiaRecordId == anesthesiaRecordId);
        }

        public async Task<List<MonitoringRecord>> GetBySurgeryIdAsync(int surgeryId)
        {
            return await _context.MonitoringRecords
                .Where(x => x.AnesthesiaRecordId == surgeryId)
                .OrderByDescending(x => x.StartedAt)
                .ToListAsync();
        }

        public async Task<MonitoringRecord> GetByIdAsync(int id)
        {
            return await _context.MonitoringRecords
                .Include(x => x.AnesthesiaRecord)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);                
        }

        public async Task<List<MonitoringRecord>> GetByProfessionalIdAsync(
            int professionalId)
        {
            return await _context.MonitoringRecords
                .Where(x => x.RecordedByProfessionalId == professionalId)
                .OrderByDescending(x => x.StartedAt)
                .ToListAsync();
        }

        public async Task<List<MonitoringRecord>> GetByPeriodAsync(
            DateTime start,
            DateTime end)
        {
            return await _context.MonitoringRecords
                .Where(x =>
                    x.StartedAt >= start &&
                    x.StartedAt <= end)
                .OrderByDescending(x => x.StartedAt)
                .ToListAsync();
        }

        public async Task<MonitoringRecord?> GetActiveBySurgeryIdAsync(
            int surgeryId)
        {
            return await _context.MonitoringRecords
                .FirstOrDefaultAsync(x =>
                    x.AnesthesiaRecordId == surgeryId &&
                    x.EndedAt == null);
        }
    }
}