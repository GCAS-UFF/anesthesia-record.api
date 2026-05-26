using UFF.FichaAnestesica.Domain.Dto;
using UFF.FichaAnestesica.Domain.Repositories.ReadOnly;
using UFF.FichaAnestesica.Domain.Services;
using UFF.FichaAnestesica.Service.Mappers;

namespace UFF.FichaAnestesica.Service.Services
{
    public class ProfessionalServices : IProfessionalService
    {
        private readonly IProfessionalRepository _professionalRepository;

        public ProfessionalServices(IProfessionalRepository professionalRepository)
        {
            _professionalRepository = professionalRepository;
        }

        public async Task<List<UserResponse>> GetProfessionalsForAnethesiaRecord(string name)
        {
            var professionals = await _professionalRepository.GetProfessionalsForAnethesiaRecord(name);
            return ProfessionalReponseMapper.Map(professionals);
        }
    }
}