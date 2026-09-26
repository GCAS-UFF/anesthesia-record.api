using UFF.FichaAnestesica.Domain.Commands;
using UFF.FichaAnestesica.Domain.Commands.PreAnesthesiaRecord;

namespace UFF.FichaAnestesica.Domain.Services
{
    public interface IPreAnesthesiaLabExamService
    {
        Task<CommandResult> GetOrImportAsync(int anesthesiaRecordId);
        Task<CommandResult> SaveAsync(int anesthesiaRecordId, PreAnesthesiaLabExamCommand command);
    }
}
