using Microsoft.EntityFrameworkCore;
using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Repositories;
using UFF.FichaAnestesica.Infra.Context;

namespace UFF.FichaAnestesica.Infra.Repositories
{
    public class VitalSignRecordRepository
        : RepositoryBase<VitalSignRecord>,
          IVitalSignRecordRepository
    {
        private readonly SigaDbCtx _context;

        public VitalSignRecordRepository(SigaDbCtx context)
            : base(context)
        {
            _context = context;
        }

        public async Task<List<VitalSignRecord>> GetByMonitoringRecordIdAsync(int monitoringRecordId)
        {
            return await _context.VitalSignRecords
                .Where(x => x.MonitoringRecordId == monitoringRecordId)
                .OrderBy(x => x.Timestamp)
                .ToListAsync();
        }

        public async Task<List<VitalSignRecord>> GetByPeriodAsync(DateTime start, DateTime end)
        {
            return await _context.VitalSignRecords
                .Where(x => x.Timestamp >= start &&
                            x.Timestamp <= end)
                .OrderBy(x => x.Timestamp)
                .ToListAsync();
        }

        public async Task<VitalSignRecord?> GetLatestAsync(int monitoringRecordId)
        {
            return await _context.VitalSignRecords
                .Where(x => x.MonitoringRecordId == monitoringRecordId)
                .OrderByDescending(x => x.Timestamp)
                .FirstOrDefaultAsync();
        }

        public async Task<List<VitalSignRecord>> GetLatestAsync(int monitoringRecordId, int quantity)
        {
            return await _context.VitalSignRecords
                .Where(x => x.MonitoringRecordId == monitoringRecordId)
                .OrderByDescending(x => x.Timestamp)
                .Take(quantity)
                .ToListAsync();
        }

        public async Task<List<VitalSignRecord>> GetWithCustomFieldsAsync(int monitoringRecordId)

        {
            return await _context.VitalSignRecords
                .Include(x => x.CustomFields)
                .Where(x => x.MonitoringRecordId == monitoringRecordId)
                .OrderBy(x => x.Timestamp)
                .ToListAsync();
        }
    }
}