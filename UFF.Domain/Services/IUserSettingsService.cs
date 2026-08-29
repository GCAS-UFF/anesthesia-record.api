using UFF.FichaAnestesica.Domain.Commands;
using UFF.FichaAnestesica.Domain.Commands.UserSettings;

namespace UFF.FichaAnestesica.Domain.Services
{
    public interface IUserSettingsService
    {
        Task<CommandResult> GetForCurrentUserAsync();
        Task<CommandResult> UpdateUserSettingsAsync(UserSettingsCommand command);
        Task<CommandResult> UpdateInstitutionSettingsAsync(InstitutionSettingsCommand command);
        Task<CommandResult> ChangeAdminPasswordAsync(ChangeAdminPasswordCommand command);
        Task<CommandResult> TestAghuConnectionAsync(TestAghuConnectionCommand command);
    }
}
