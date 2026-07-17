using UFF.FichaAnestesica.Domain.Commands;
using UFF.FichaAnestesica.Domain.Repositories;
using UFF.FichaAnestesica.Domain.Services;
using UFF.FichaAnestesica.Service.Mappers;

namespace UFF.FichaAnestesica.Service.Services
{
    public class ProcedureService : IProcedureService
    {
        private readonly IProcedureRepository _procedureRepository;

        public ProcedureService(IProcedureRepository procedureRepository)
        {
            _procedureRepository = procedureRepository;
        }

        public async Task<CommandResult> GetAllProceduresForAnethesiaRecord()
        {
            var procedures = await _procedureRepository.GetAllAsync();
            return CommandResult.Success(ProcedureResponseMapper.Map(procedures));
        }
    }
}