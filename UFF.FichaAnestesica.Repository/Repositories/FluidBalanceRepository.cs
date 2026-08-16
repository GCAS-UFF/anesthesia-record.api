using Microsoft.EntityFrameworkCore;
using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Enums;
using UFF.FichaAnestesica.Domain.Repositories;
using UFF.FichaAnestesica.Infra.Context;

namespace UFF.FichaAnestesica.Infra.Repositories
{
    public class FluidBalanceRepository
        : RepositoryBase<FluidBalance>,
          IFluidBalanceRepository
    {
        private readonly SigaDbCtx _context;

        public FluidBalanceRepository(SigaDbCtx context)
            : base(context)
        {
            _context = context;
        }

        public async Task<List<FluidBalance>> GetByMonitoringRecordIdAsync(int monitoringRecordId)
        {
            return await _context.FluidBalances
                .Where(x => x.MonitoringRecordId == monitoringRecordId)
                .OrderBy(x => x.Time)
                .ToListAsync();
        }

        public async Task<List<FluidBalance>> GetByTypeAsync(FluidBalanceTypeEnum type)
        {
            return await _context.FluidBalances
                .Where(x => x.Type == type)
                .OrderByDescending(x => x.Time)
                .ToListAsync();
        }

        public async Task<List<FluidBalance>> GetByCategoryAsync(FluidCategoryEnum category)
        {
            return await _context.FluidBalances
                .Where(x => x.Category == category)
                .OrderByDescending(x => x.Time)
                .ToListAsync();
        }

        public async Task<List<FluidBalance>> GetByPeriodAsync(DateTime start, DateTime end)
        {
            return await _context.FluidBalances
                .Where(x => x.Date >= start && x.Date <= end)
                .OrderBy(x => x.Time)
                .ToListAsync();
        }

        public async Task<decimal> GetTotalVolumeByTypeAsync(
            int monitoringRecordId,
            FluidBalanceTypeEnum type)
        {
            return await _context.FluidBalances
                .Where(x =>
                    x.MonitoringRecordId == monitoringRecordId &&
                    x.Type == type)
                .SumAsync(x => x.VolumeMl);
        }

        public async Task<decimal> GetTotalBalanceAsync(int monitoringRecordId)
        {
            var input = await _context.FluidBalances
                .Where(x =>
                    x.MonitoringRecordId == monitoringRecordId &&
                    x.Type == FluidBalanceTypeEnum.Gain)
                .SumAsync(x => x.VolumeMl);

            var output = await _context.FluidBalances
                .Where(x =>
                    x.MonitoringRecordId == monitoringRecordId &&
                    x.Type == FluidBalanceTypeEnum.Loss)
                .SumAsync(x => x.VolumeMl);

            return input - output;
        }
    }
}