using Microsoft.EntityFrameworkCore;
using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Enums;
using UFF.FichaAnestesica.Domain.Repositories;
using UFF.FichaAnestesica.Infra.Context;

namespace UFF.FichaAnestesica.Infra.Repositories
{
    public class ClinicalEventRepository : RepositoryBase<ClinicalEvent>, IClinicalEventRepository
    {
        private readonly SigaDbCtx _context;

        public ClinicalEventRepository(SigaDbCtx context)
            : base(context)
        {
            _context = context;
        }

        public async Task<List<ClinicalEvent>> GetByMonitoringRecordIdAsync(int monitoringRecordId)
        {
            return await _context.ClinicalEvents
                .Where(x => x.MonitoringRecordId == monitoringRecordId)
                .OrderBy(x => x.Time)
                .ToListAsync();
        }

        public async Task<List<ClinicalEvent>> GetByTypeAsync(ClinicalEventTypeEnum type)
        {
            return await _context.ClinicalEvents
                .Where(x => x.EventType == type)
                .OrderByDescending(x => x.Time)
                .ToListAsync();
        }

        public async Task<List<ClinicalEvent>> GetByPeriodAsync(DateTime start, DateTime end)
        {
            return await _context.ClinicalEvents
                .Where(x => x.Date >= start && x.Date <= end)
                .OrderBy(x => x.Time)
                .ToListAsync();
        }

        public async Task<ClinicalEvent?> GetDetailedByIdAsync(int id)
        {
            return await _context.ClinicalEvents
                .Include(x => x.MonitoringRecord)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}