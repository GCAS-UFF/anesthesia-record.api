using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Enums;
using UFF.FichaAnestesica.Domain.Extensions;
using UFF.FichaAnestesica.Domain.Repositories;
using UFF.FichaAnestesica.Domain.Repositories.ReadOnly;
using UFF.FichaAnestesica.Service.Services.Aghu;

namespace UFF.FichaAnestesica.Infra.Repositories.Aghu
{
    public class ProfessionalApiService : IProfessionalApiService
    {
        private readonly IProfessionalReadOnlyRepository _professionalReadOnlyRepository;
        private readonly IUserRepository _userRepository;

        public ProfessionalApiService(IProfessionalReadOnlyRepository professionalReadOnlyRepository, IUserRepository userRepository)
        {
            _professionalReadOnlyRepository = professionalReadOnlyRepository;
            _userRepository = userRepository;
        }

        public async Task<int> SyncProfessionals()
        {
            var aghuResponse = await _professionalReadOnlyRepository.GetProfessionalsFromAGHU();

            if (aghuResponse?.Professionals == null || !aghuResponse.Professionals.Any())
                return 0;

            var professionalsFromApi = aghuResponse.Professionals;
            var usersDatabase = await _userRepository.GetAllAsync();

            var usersByExternalId = usersDatabase
                .Where(x => x.Registration != string.Empty)
                .ToDictionary(x => x.Registration);

            var externalIdsFromApi = new HashSet<string>();

            foreach (var professional in professionalsFromApi)
            {
                if (professional.Registration == string.Empty)
                    continue;

                externalIdsFromApi.Add(professional.Registration);

                var specialty = MedicalSpecialtyExtensions.ParseToEnum(professional.MedicalSpecialty);
                var sector = SectorExtensions.ParseToEnum(professional.Sector);

                if (usersByExternalId.TryGetValue(professional.Registration, out var existingUser))
                {
                    existingUser.Update(professional.Name, professional.Email, professional.Login, professional.Registration, specialty, sector);
                    _userRepository.Update(existingUser);
                }
                else
                {
                    var newUser = User.Create(professional.Id, professional.Name, professional.Email, professional.Login, professional.Registration, specialty, sector);
                    await _userRepository.AddAsync(newUser);
                }
            }

            var usersToDisable = usersDatabase
                .Where(x =>
                    x.ExternalId > 0 &&
                    !externalIdsFromApi.Contains(x.Registration) &&
                    x.Status != UserStatusEnum.Disabled)
                .ToList();

            foreach (var user in usersToDisable)
            {
                user.Disable();
                _userRepository.Update(user);
            }

            await _userRepository.SaveChangesAsync();

            return aghuResponse.Professionals.Count();
        }
    }
}