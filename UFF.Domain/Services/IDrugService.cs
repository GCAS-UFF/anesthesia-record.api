using UFF.FichaAnestesica.Domain.Commands;
using UFF.FichaAnestesica.Domain.Commands.Drugs;
using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Domain.Services
{
    public interface IDrugService
    {
        Task<CommandResult> GetAllDrugsForAnethesiaRecord();
        Task<DateTime?> GetLasIntegrationTime();
        Task<CommandResult> GetPagedForAdminAsync(string? term, DrugCategoryEnum? category, int page, int size);
        Task<CommandResult> UpdateCategoryAsync(int id, UpdateDrugCategoryCommand command);
    }
}