using UFF.FichaAnestesica.Domain.Dto;
using UFF.FichaAnestesica.Domain.Entities;

namespace UFF.FichaAnestesica.Domain.Repositories
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<User> GetUserByLoginAsync(string login);
        Task<User> GetUserByIdAsync(int id);
        Task<UserDto?> GetUserFromApiByLoginAsync(string login);
    }
}