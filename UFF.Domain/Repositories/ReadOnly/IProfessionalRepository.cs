using UFF.FichaAnestesica.Domain.Dto;

namespace UFF.FichaAnestesica.Domain.Repositories.ReadOnly
{
    public interface IProfessionalRepository
    {
        Task<List<UserDto>> GetProfessionalsForAnethesiaRecord(string name);
    }
}
