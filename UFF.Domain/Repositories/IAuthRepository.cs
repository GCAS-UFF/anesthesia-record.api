using UFF.FichaAnestesica.Domain.Dto;

namespace UFF.FichaAnestesica.Domain.Repositories
{
    public interface IAuthRepository 
    {
        Task<UserDto?> LoginAGHU(string login, string password);
    }
}