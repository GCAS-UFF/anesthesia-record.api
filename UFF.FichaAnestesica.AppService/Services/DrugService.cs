using UFF.FichaAnestesica.Domain.Commands;
using UFF.FichaAnestesica.Domain.Repositories;
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
    }
}