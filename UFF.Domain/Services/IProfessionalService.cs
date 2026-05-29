using UFF.FichaAnestesica.Domain.Commands;

namespace UFF.FichaAnestesica.Domain.Services
{
    public interface IProfessionalService
    {
        Task<CommandResult> GetProfessionalsForAnethesiaRecord(string name);
    }
}