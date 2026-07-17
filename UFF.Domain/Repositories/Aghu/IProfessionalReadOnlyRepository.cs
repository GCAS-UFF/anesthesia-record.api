using UFF.FichaAnestesica.Domain.Dto;
using UFF.FichaAnestesica.Domain.Entities;

namespace UFF.FichaAnestesica.Domain.Repositories.ReadOnly
{
    public interface IProfessionalReadOnlyRepository
    {
        Task<List<User>> GetProfessionalsForAnethesiaRecord(string term);
        Task<List<User>> GetAllProfessionalsForAnethesiaRecord();
        Task<UserListDto> GetProfessionalsFromAGHU();
        Task<DateTime?> GetLastTimeIntegration();
    }
}
