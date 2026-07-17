using UFF.FichaAnestesica.Domain.Commands;

namespace UFF.FichaAnestesica.Domain.Services
{
    public interface IProcedureService
    {
        Task<CommandResult> GetAllProceduresForAnethesiaRecord();
        Task<DateTime?> GetLasIntegrationTime();
    }
}