namespace UFF.FichaAnestesica.Domain.Repositories
{
    public interface IAdministeredAgentRepository : IRepositoryBase<AdministeredAgent>
    {
        Task<List<AdministeredAgent>> GetByMonitoringRecordIdAsync(int monitoringRecordId);

        Task<List<AdministeredAgent>> GetByDrugIdAsync(int drugId);

        Task<List<AdministeredAgent>> GetByPeriodAsync(DateTime start, DateTime end);

        Task<AdministeredAgent?> GetDetailedByIdAsync(int id);
    }
}