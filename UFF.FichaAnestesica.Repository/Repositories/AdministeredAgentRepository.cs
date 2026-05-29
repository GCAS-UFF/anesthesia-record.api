using Microsoft.EntityFrameworkCore;
using UFF.FichaAnestesica.Domain.Repositories;
using UFF.FichaAnestesica.Infra.Context;

namespace UFF.FichaAnestesica.Infra.Repositories
{
    public class AdministeredAgentRepository
        : RepositoryBase<AdministeredAgent>,
          IAdministeredAgentRepository
    {
        private readonly SigaDbCtx _context;

        public AdministeredAgentRepository(SigaDbCtx context)
            : base(context)
        {
            _context = context;
        }

        public async Task<List<AdministeredAgent>> GetByMonitoringRecordIdAsync(int monitoringRecordId)
        {
            return await _context.AdministeredAgents
                .Include(x => x.Drug)
                .Where(x => x.MonitoringRecordId == monitoringRecordId)
                .OrderBy(x => x.Timestamp)
                .ToListAsync();
        }

        public async Task<List<AdministeredAgent>> GetByDrugIdAsync(int drugId)
        {
            return await _context.AdministeredAgents
                .Include(x => x.Drug)
                .Where(x => x.DrugId == drugId)
                .OrderByDescending(x => x.Timestamp)
                .ToListAsync();
        }

        public async Task<List<AdministeredAgent>> GetByPeriodAsync(DateTime start, DateTime end)
        {
            return await _context.AdministeredAgents
                .Include(x => x.Drug)
                .Where(x => x.Timestamp >= start && x.Timestamp <= end)
                .OrderBy(x => x.Timestamp)
                .ToListAsync();
        }

        public async Task<AdministeredAgent?> GetDetailedByIdAsync(int id)
        {
            return await _context.AdministeredAgents
                .Include(x => x.Drug)
                .Include(x => x.MonitoringRecord)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}