using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UFF.FichaAnestesica.CrossCutting.Extensions;
using UFF.FichaAnestesica.Domain.Commands;
using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Enums;
using UFF.FichaAnestesica.Domain.Extensions;
using UFF.FichaAnestesica.Domain.Repositories;
using UFF.FichaAnestesica.Domain.Services;

namespace UFF.FichaAnestesica.Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly IAuthRepository _authRepository;

        public AuthService(
            IConfiguration configuration,
            IUserRepository userRepository,
            IAuthRepository authRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _authRepository = authRepository;
        }

        public async Task<CommandResult> LoginAsync(string login, string password)
        {
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
                return CommandResult.Fail("Usuário e senha precisam ser preenchidos");

            var hospitalUser = await _authRepository.LoginAGHU(login, password);

            if (hospitalUser is null)
                return CommandResult.Fail("Usuário ou senha inválidos");

            var user = await _userRepository.GetUserByLoginAsync(login);

            if (user is null)
            {
                user = User.Create(
                    externalId: hospitalUser.Id,
                    name: hospitalUser.Name,
                    email: hospitalUser.Email,
                    login: hospitalUser.Login,
                    registration: hospitalUser.Registration,
                    medicalSpecialty: MedicalSpecialtyExtensions.ParseToEnum(hospitalUser.MedicalSpecialty),
                    sector: SectorExtensions.ParseToEnum(hospitalUser.Sector)
                );

                await _userRepository.AddAsync(user);
                await _userRepository.SaveChangesAsync();
            }

            if (user.Status != UserStatusEnum.Enabled)
                return CommandResult.Fail("Usuário sem permissão");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

            var claims = new[]
            {
                new Claim("user_id", user.Id.ToString()),
                new Claim("login", user.Login),
                new Claim("name", user.Name),
                new Claim("email", user.Email ?? "")
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

            return CommandResult.Success(new
            {
                token = accessToken,
                usuario = new
                {
                    id = user.Id,
                    nome = user.Name,
                    email = user.Email,
                    login = user.Login,
                    role = user.MedicalSpecialty.GetDescription(),
                    sector = hospitalUser.Sector
                }
            });
        }
    }
}