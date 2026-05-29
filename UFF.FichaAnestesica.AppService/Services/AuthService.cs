using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UFF.FichaAnestesica.Domain.Commands;
using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Enums;
using UFF.FichaAnestesica.Domain.Repositories;
using UFF.FichaAnestesica.Domain.Repositories.ReadOnly;
using UFF.FichaAnestesica.Domain.Services;

namespace UFF.FichaAnestesica.Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;

        public AuthService(IConfiguration configuration, IUserRepository userRepository, IPatientApiReadOnlyRepository hospitalUserRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }

        public async Task<CommandResult> AuthSync(string login, string password)
        {
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                return new CommandResult(false, "Usuário e senha precisam ser preenchidos");
            }

            var authenticated = true;//_ldapAuthRepository.ValidateCredentials(login, password);

            if (!authenticated)
            {
                return new CommandResult(false, "Usuário ou senha inválidos");
            }

            var user = await _userRepository.GetUserByLoginAsync(login);

            if (user is null)
            {
                var hospitalUser = await _userRepository.GetUserFromApiByLoginAsync(login);

                if (hospitalUser is null)
                {
                    return new CommandResult(false, "Usuário não encontrado na base hospitalar");
                }

                user = User.Create(externalId: hospitalUser.Id, name: hospitalUser.Name, email: hospitalUser.Email, login: hospitalUser.Login, registration: hospitalUser.Registration);

                await _userRepository.AddAsync(user);
                await _userRepository.SaveChangesAsync();
            }

            if (user.Status != UserStatusEnum.Enabled)
            {
                return new CommandResult(false, "Usuário sem permissão");
            }

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

            var claims = new[]
            {
                new Claim("user_id", user.Id.ToString()),
                new Claim("login", user.Login),
                new Claim("name", user.Name),
                new Claim("email", user.Email)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),

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
                token = accessToken,
                usuario = new
                {
                    id = user.Id,
                    nome = user.Name,
                    email = user.Email,
                    login = user.Login
                }
            });
        }
    }
}