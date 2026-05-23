using UFF.FichaAnestesica.Domain.Entities;

namespace UFF.FichaAnestesica.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByLogin(string login);
    }
}