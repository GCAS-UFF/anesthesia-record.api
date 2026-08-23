using UFF.FichaAnestesica.Domain.Commands;
using UFF.FichaAnestesica.Domain.Commands.UserSettings;
using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Repositories;
using UFF.FichaAnestesica.Domain.Response;
using UFF.FichaAnestesica.Domain.Services;

namespace UFF.FichaAnestesica.Service.Services
{
    public class UserSettingsService : IUserSettingsService
    {
        private readonly IUserSettingsRepository _userSettingsRepository;
        private readonly IInstitutionSettingsRepository _institutionSettingsRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICurrentUserService _currentUser;

        public UserSettingsService(
            IUserSettingsRepository userSettingsRepository,
            IInstitutionSettingsRepository institutionSettingsRepository,
            IUserRepository userRepository,
            ICurrentUserService currentUser)
        {
            _userSettingsRepository = userSettingsRepository;
            _institutionSettingsRepository = institutionSettingsRepository;
            _userRepository = userRepository;
            _currentUser = currentUser;
        }

        public async Task<CommandResult> GetForCurrentUserAsync()
        {
            if (_currentUser.UserId is not int userId)
                return CommandResult.Fail("Usuário não autenticado");

            var userSettings = await GetOrCreateUserSettingsAsync(userId);
            var institutionSettings = await GetOrCreateInstitutionSettingsAsync();

            return CommandResult.Success(
                UserSettingsResponse.ToResponse(userSettings, institutionSettings, _currentUser.IsAdmin));
        }

        public async Task<CommandResult> UpdateUserSettingsAsync(UserSettingsCommand command)
        {
            if (_currentUser.UserId is not int userId)
                return CommandResult.Fail("Usuário não autenticado");

            if (string.IsNullOrWhiteSpace(command.Language))
                return CommandResult.Fail("Idioma é obrigatório");

            if (command.MonitoringIntervalMinutes < 1 || command.MonitoringIntervalMinutes > 60)
                return CommandResult.Fail("Intervalo de aferição deve estar entre 1 e 60 minutos");

            try
            {
                var userSettings = await GetOrCreateUserSettingsAsync(userId);

                userSettings.Update(command.Language, command.MonitoringIntervalMinutes, command.UseInstitutionalInterval);
                _userSettingsRepository.Update(userSettings);
                await _userSettingsRepository.SaveChangesAsync();

                var institutionSettings = await GetOrCreateInstitutionSettingsAsync();

                return CommandResult.Success(
                    UserSettingsResponse.ToResponse(userSettings, institutionSettings, _currentUser.IsAdmin));
            }
            catch (Exception ex)
            {
                return CommandResult.Fail(ex.Message);
            }
        }

        public async Task<CommandResult> UpdateInstitutionSettingsAsync(InstitutionSettingsCommand command)
        {
            if (_currentUser.UserId is not int userId)
                return CommandResult.Fail("Usuário não autenticado");

            if (command.MonitoringIntervalMinutes < 1 || command.MonitoringIntervalMinutes > 60)
                return CommandResult.Fail("Intervalo institucional deve estar entre 1 e 60 minutos");

            try
            {
                var institutionSettings = await GetOrCreateInstitutionSettingsAsync();

                institutionSettings.Update(
                    command.MonitoringIntervalMinutes,
                    command.SigaApiUrl,
                    command.AghuApiUrl,
                    command.HospitalName,
                    command.HospitalSector,
                    command.HospitalCnpj,
                    command.HospitalCep,
                    command.HospitalStreet,
                    command.HospitalNumber,
                    command.HospitalNeighborhood,
                    command.HospitalCity,
                    command.HospitalState,
                    userId);

                _institutionSettingsRepository.Update(institutionSettings);
                await _institutionSettingsRepository.SaveChangesAsync();

                var userSettings = await GetOrCreateUserSettingsAsync(userId);

                return CommandResult.Success(
                    UserSettingsResponse.ToResponse(userSettings, institutionSettings, _currentUser.IsAdmin));
            }
            catch (Exception ex)
            {
                return CommandResult.Fail(ex.Message);
            }
        }

        public async Task<CommandResult> ChangeAdminPasswordAsync(ChangeAdminPasswordCommand command)
        {
            if (_currentUser.UserId is not int userId)
                return CommandResult.Fail("Usuário não autenticado");

            if (string.IsNullOrWhiteSpace(command.CurrentPassword) || string.IsNullOrWhiteSpace(command.NewPassword))
                return CommandResult.Fail("Preencha a senha atual e a nova senha");

            if (command.NewPassword.Length < 6)
                return CommandResult.Fail("A nova senha deve ter ao menos 6 caracteres");

            var user = await _userRepository.GetUserByIdAsync(userId);

            if (user == null)
                return CommandResult.Fail("Usuário não encontrado");

            if (user.Password != command.CurrentPassword)
                return CommandResult.Fail("Senha atual incorreta");

            try
            {
                user.ChangePassword(command.NewPassword);
                _userRepository.Update(user);
                await _userRepository.SaveChangesAsync();

                return CommandResult.Success(message: "Senha atualizada com sucesso");
            }
            catch (Exception ex)
            {
                return CommandResult.Fail(ex.Message);
            }
        }

        private async Task<UserSettings> GetOrCreateUserSettingsAsync(int userId)
        {
            var userSettings = await _userSettingsRepository.GetByUserIdAsync(userId);

            if (userSettings != null)
                return userSettings;

            userSettings = UserSettings.CreateDefault(userId);
            await _userSettingsRepository.AddAsync(userSettings);
            await _userSettingsRepository.SaveChangesAsync();

            return userSettings;
        }

        private async Task<InstitutionSettings> GetOrCreateInstitutionSettingsAsync()
        {
            var institutionSettings = await _institutionSettingsRepository.GetSingletonAsync();

            if (institutionSettings != null)
                return institutionSettings;

            institutionSettings = InstitutionSettings.CreateDefault();
            await _institutionSettingsRepository.AddAsync(institutionSettings);
            await _institutionSettingsRepository.SaveChangesAsync();

            return institutionSettings;
        }
    }
}
