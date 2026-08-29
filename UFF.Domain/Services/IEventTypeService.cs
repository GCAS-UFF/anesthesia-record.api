using UFF.FichaAnestesica.Domain.Commands;
using UFF.FichaAnestesica.Domain.Commands.EventTypes;

namespace UFF.FichaAnestesica.Domain.Services
{
    public interface IEventTypeService
    {
        Task<CommandResult> GetPagedForAdminAsync(string? term, int page, int size);
        Task<CommandResult> GetActiveAsync();
        Task<CommandResult> CreateAsync(CreateEventTypeCommand command);
        Task<CommandResult> UpdateAsync(int id, UpdateEventTypeCommand command);
    }
}
