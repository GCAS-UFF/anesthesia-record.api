using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using UFF.FichaAnestesica.Domain.Commands;
using UFF.FichaAnestesica.Domain.Repositories;
using UFF.FichaAnestesica.Domain.Repositories.ReadOnly;
using UFF.FichaAnestesica.Domain.Services;

namespace UFF.FichaAnestesica.Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly ILdapAuthReadOnlyRepository _ldapAuthRepository;

        public AuthService(
            IHospitalReadOnlyRepository hospitalReadRepository,
            ISurgeryRepository surgeryRepository,
            IConfiguration configuration,
            IUserRepository userRepository,
            ILdapAuthReadOnlyRepository ldapAuthRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _ldapAuthRepository = ldapAuthRepository;
        }

        public async Task<CommandResult> AuthSync(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password)) 
                return new CommandResult(false, "Usuário e senha precisam ser preenchidos");

            var user = await _userRepository.GetUserByLogin(email);

            if (user is null)
                return new CommandResult(false, "Usuário não encontrado");

            if (user.Status != Domain.Enums.UserStatusEnum.Enabled)
                return new CommandResult(false, "Usuário sem permissão");

            var logged = _ldapAuthRepository.ValidateCredentials(email, password);

            if (!logged)
                return new CommandResult(false, "Senha inválida");

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddHours(12),

                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var accessToken = tokenHandler.WriteToken(token);

            return new CommandResult(true, new
            {
                Token = accessToken
            });
        }     
    }
}