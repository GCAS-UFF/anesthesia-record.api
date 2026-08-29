using UFF.FichaAnestesica.Domain.Commands;
using UFF.FichaAnestesica.Domain.Commands.Drugs;
using UFF.FichaAnestesica.Domain.Enums;
using UFF.FichaAnestesica.Domain.Repositories;
using UFF.FichaAnestesica.Domain.Response;
using UFF.FichaAnestesica.Domain.Services;
using UFF.FichaAnestesica.Service.Mappers;

namespace UFF.FichaAnestesica.Service.Services
{
    public class DrugService : IDrugService
    {
        private readonly IDrugRepository _drugRepository;

        public DrugService(IDrugRepository drugRepository)
        {
            _drugRepository = drugRepository;
        }

        public async Task<CommandResult> GetAllDrugsForAnethesiaRecord()
        {
            var drugs = await _drugRepository.GetAllAsync();
            return CommandResult.Success(DrugResponseMapper.Map(drugs));
        }

        public async Task<DateTime?> GetLasIntegrationTime()
        {
            return await _drugRepository.GetLastTimeIntegration();
        }

        public async Task<CommandResult> GetPagedForAdminAsync(string? term, DrugCategoryEnum? category, int page, int size)
        {
            var (items, totalItems) = await _drugRepository.GetPagedAsync(term, category, page, size);

            return CommandResult.Success(new PagedResponse<DrugAdminResponse>
            {
                Data = DrugResponseMapper.MapAdmin(items),
                TotalItems = totalItems,
                Page = page,
                PageSize = size,
                HasNext = page * size < totalItems
            });
        }

        public async Task<CommandResult> UpdateCategoryAsync(int id, UpdateDrugCategoryCommand command)
        {
            var drug = await _drugRepository.GetByIdAsync(id);

            if (drug == null)
                return CommandResult.Fail("Item não encontrado");

            drug.UpdateCategory(command.Category);
            _drugRepository.Update(drug);
            await _drugRepository.SaveChangesAsync();

            return CommandResult.Success(DrugResponseMapper.MapAdmin(drug));
        }
    }
}