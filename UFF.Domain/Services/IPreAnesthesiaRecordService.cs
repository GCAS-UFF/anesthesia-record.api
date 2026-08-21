using UFF.FichaAnestesica.Domain.Commands;
using UFF.FichaAnestesica.Domain.Commands.PreAnesthesiaRecord;

namespace UFF.FichaAnestesica.Domain.Services
{
    public interface IPreAnesthesiaRecordService
    {
        Task<CommandResult> GetByIdAsync(int id);
        Task<CommandResult> GetByAnesthesiaRecordIdAsync(int anesthesiaRecordId);
        Task<CommandResult> Create(PreAnesthesiaRecordCommand command);
        Task<CommandResult> Update(int id, PreAnesthesiaRecordCommand command);
    }
}
