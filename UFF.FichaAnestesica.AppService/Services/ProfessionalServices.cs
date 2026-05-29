using UFF.FichaAnestesica.Domain.Commands;
using UFF.FichaAnestesica.Domain.Repositories.ReadOnly;
using UFF.FichaAnestesica.Domain.Services;
using UFF.FichaAnestesica.Service.Mappers;

namespace UFF.FichaAnestesica.Service.Services
{
    public class ProfessionalServices : IProfessionalService
    {
        private readonly IProfessionalReadOnlyRepository _professionalRepository;

        public ProfessionalServices(IProfessionalReadOnlyRepository professionalRepository)
        {
            _professionalRepository = professionalRepository;
        }

        public async Task<CommandResult> GetProfessionalsForAnethesiaRecord(string name)
        {
            var professionals = await _professionalRepository.GetProfessionalsForAnethesiaRecord(name);
            return new CommandResult(true, ProfessionalReponseMapper.Map(professionals));
        }
    }
}