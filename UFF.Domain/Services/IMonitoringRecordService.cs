using UFF.FichaAnestesica.Domain.Commands;
using UFF.FichaAnestesica.Domain.Commands.AnesthesiaRecord;

namespace UFF.FichaAnestesica.Domain.Services
{
    public interface IMonitoringRecordService
    {
        Task<CommandResult> GetByIdAsync(int id);
        Task<CommandResult> Create(MonitoringRecordCommand command);
        Task<CommandResult> Update(int id, MonitoringRecordCommand command);
        Task<CommandResult> FinalizePatientAsync(int anesthesiaRecordId);
    }
}