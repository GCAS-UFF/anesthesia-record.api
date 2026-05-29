using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Enums;
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

        public async Task SyncProfessionals()
        {
            var aghuResponse = await _professionalReadOnlyRepository.GetProfessionalsFromAGHU();

            if (aghuResponse?.Professionals == null || !aghuResponse.Professionals.Any())
                return;

            var professionalsFromApi = aghuResponse.Professionals;
            var usersDatabase = await _userRepository.GetAllAsync();
            var usersByExternalId = usersDatabase.Where(x => !string.IsNullOrWhiteSpace(x.ExternalId)).ToDictionary(x => x.ExternalId);
            var externalIdsFromApi = new HashSet<string>();

            foreach (var professional in professionalsFromApi)
            {
                if (string.IsNullOrWhiteSpace(professional.Id))
                    continue;

                externalIdsFromApi.Add(professional.Id);

                if (usersByExternalId.TryGetValue(professional.Id, out var existingUser))
                {
                    existingUser.Update(professional.Name, professional.Email, professional.Login, professional.Registration);
                    _userRepository.Update(existingUser);
                }
                else
                {
                    var newUser = User.Create(professional.Id, professional.Name, professional.Email, professional.Login, professional.Registration);
                    await _userRepository.AddAsync(newUser);
                }
            }

            var usersToDisable = usersDatabase
                .Where(x => !string.IsNullOrWhiteSpace(x.ExternalId) && !externalIdsFromApi.Contains(x.ExternalId) && x.Status != UserStatusEnum.Disabled).ToList();

            foreach (var user in usersToDisable)
            {
                user.Disable();
                _userRepository.Update(user);
            }

            await _userRepository.SaveChangesAsync();
        }
    }
}