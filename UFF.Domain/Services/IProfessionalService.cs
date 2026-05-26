using UFF.FichaAnestesica.Domain.Dto;

namespace UFF.FichaAnestesica.Domain.Services
{
    public interface IProfessionalService
    {
        Task<List<UserResponse>> GetProfessionalsForAnethesiaRecord(string name);      
    }
}