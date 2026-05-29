using UFF.FichaAnestesica.Domain.Dto;
using UFF.FichaAnestesica.Domain.Entities;

namespace UFF.FichaAnestesica.Domain.Repositories.ReadOnly
{
    public interface IProfessionalRepository
    {
        Task<List<User>> GetProfessionalsForAnethesiaRecord(string name);
        Task<UserListDto> GetProfessionalsFromAGHU();
    }
}
