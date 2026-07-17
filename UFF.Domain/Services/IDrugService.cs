using UFF.FichaAnestesica.Domain.Commands;

namespace UFF.FichaAnestesica.Domain.Services
{
    public interface IDrugService
    {
        Task<CommandResult> GetAllDrugsForAnethesiaRecord();
    }
}