using UFF.FichaAnestesica.Domain.Commands;
using UFF.FichaAnestesica.Domain.Commands.AnesthesiaRecord;

namespace UFF.FichaAnestesica.Domain.Services
{
    public interface IAnesthesiaRecordService
    {
        Task<CommandResult> GetByIdAsync(int id, string? extenalPatientId);
        Task<CommandResult> Create(AnesthesiaRecordCommand command);
        Task<CommandResult> Update(int id, AnesthesiaRecordCommand command);
    }
}