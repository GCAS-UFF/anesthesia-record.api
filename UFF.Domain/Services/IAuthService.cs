using UFF.FichaAnestesica.Domain.Commands;

namespace UFF.FichaAnestesica.Domain.Services
{
    public interface IAuthService
    {
        Task<CommandResult> LoginAsync(string login, string password);
    }
}
