using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Domain.Repositories
{
    public interface IFluidBalanceRepository : IRepositoryBase<FluidBalance>
    {
        Task<List<FluidBalance>> GetByMonitoringRecordIdAsync(int monitoringRecordId);

        Task<List<FluidBalance>> GetByTypeAsync(FluidBalanceTypeEnum type);

        Task<List<FluidBalance>> GetByCategoryAsync(FluidCategoryEnum category);

        Task<List<FluidBalance>> GetByPeriodAsync(DateTime start, DateTime end);

        Task<decimal> GetTotalVolumeByTypeAsync(int monitoringRecordId, FluidBalanceTypeEnum type);

        Task<decimal> GetTotalBalanceAsync(int monitoringRecordId);
    }
}