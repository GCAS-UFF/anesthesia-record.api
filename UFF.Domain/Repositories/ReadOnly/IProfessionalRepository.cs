using UFF.FichaAnestesica.Domain.Dto;

namespace UFF.FichaAnestesica.Domain.Repositories.ReadOnly
{
    public interface IProfessionalRepository
    {
        Task<UserListDto> GetProfessionalsForAnethesiaRecord(string name);
    }
}
