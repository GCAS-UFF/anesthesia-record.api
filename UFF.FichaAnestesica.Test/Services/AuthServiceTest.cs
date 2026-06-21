using Microsoft.Extensions.Configuration;
using Moq;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using UFF.FichaAnestesica.Domain.Commands;
using UFF.FichaAnestesica.Domain.Dto;
using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Enums;
using UFF.FichaAnestesica.Domain.Repositories;
using UFF.FichaAnestesica.Service.Services;

namespace UFF.FichaAnestesica.Test.Services
{
    public class AuthServiceTest
    {
        private readonly Mock<IConfiguration> _configMock;
        private readonly Mock<IUserRepository> _userRepoMock;
        private readonly Mock<IAuthRepository> _authRepoMock;
        private readonly AuthService _service;

        private const string ValidKey = "3f8a6b29d4e17c52a01e9f83b6c7d42e8f01a69b73c5d2e1074f6a8b9c0d3e5f";

        public AuthServiceTest()
        {
            _configMock = new Mock<IConfiguration>();
            _userRepoMock = new Mock<IUserRepository>();
            _authRepoMock = new Mock<IAuthRepository>();

            _configMock.Setup(c => c["Jwt:Key"]).Returns(ValidKey);

            _service = new AuthService(
                _configMock.Object,
                _userRepoMock.Object,
                _authRepoMock.Object);
        }

        private static User CreateEnabledUser()
        {
            return User.Create(1, "Dr. João", "joao@teste.com", "jsilva", "123456",
                MedicalSpecialtyEnum.Anesthesiology, SectorEnum.SurgicalCenter);
        }

        private static UserDto CreateHospitalUserDto()
        {
            return new UserDto
            {
                Id = 1,
                Name = "Dr. João",
                Email = "joao@teste.com",
                Login = "jsilva",
                Registration = "123456",
                MedicalSpecialty = "Anesthesiology",
                Sector = "SurgicalCenter"
            };
        }

        private static JObject GetJsonData(CommandResult result)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(result.Data);
            return JObject.Parse(json);
        }

        // ========== LoginAsync ==========

        [Fact]
        public async Task LoginAsync_Should_Return_Fail_When_Credentials_Are_Empty()
        {
            var result = await _service.LoginAsync("", "");
            Assert.False(result.Valid);
            Assert.Contains("preenchidos", result.Message);
        }

        [Fact]
        public async Task LoginAsync_Should_Return_Fail_When_AGHU_Login_Fails()
        {
            _authRepoMock.Setup(a => a.LoginAGHU("jsilva", "123"))
                         .ReturnsAsync((UserDto?)null);

            var result = await _service.LoginAsync("jsilva", "123");

            Assert.False(result.Valid);
            Assert.Contains("inválidos", result.Message);
        }

        [Fact]
        public async Task LoginAsync_Should_Create_Local_User_When_Not_Exists()
        {
            var hospitalUser = CreateHospitalUserDto();
            _authRepoMock.Setup(a => a.LoginAGHU("jsilva", "123"))
                         .ReturnsAsync(hospitalUser);
            _userRepoMock.Setup(u => u.GetUserByLoginAsync("jsilva"))
                         .ReturnsAsync((User?)null);

            var result = await _service.LoginAsync("jsilva", "123");

            Assert.True(result.Valid);
            _userRepoMock.Verify(u => u.AddAsync(It.IsAny<User>()), Times.Once);
            _userRepoMock.Verify(u => u.SaveChangesAsync(), Times.Once);

            var data = GetJsonData(result);
            Assert.NotNull(data["token"]?.ToString());
            var handler = new JwtSecurityTokenHandler();
            Assert.True(handler.CanReadToken(data["token"]!.ToString()));
        }

        [Fact]
        public async Task LoginAsync_Should_Return_Fail_When_User_Disabled()
        {
            var hospitalUser = CreateHospitalUserDto();
            var user = CreateEnabledUser();
            user.Disable();

            _authRepoMock.Setup(a => a.LoginAGHU("jsilva", "123"))
                         .ReturnsAsync(hospitalUser);
            _userRepoMock.Setup(u => u.GetUserByLoginAsync("jsilva"))
                         .ReturnsAsync(user);

            var result = await _service.LoginAsync("jsilva", "123");

            Assert.False(result.Valid);
            Assert.Contains("permissão", result.Message);
        }

        [Fact]
        public async Task LoginAsync_Should_Return_Success_With_Token_When_Valid_Credentials()
        {
            var hospitalUser = CreateHospitalUserDto();
            var user = CreateEnabledUser();

            _authRepoMock.Setup(a => a.LoginAGHU("jsilva", "123"))
                         .ReturnsAsync(hospitalUser);
            _userRepoMock.Setup(u => u.GetUserByLoginAsync("jsilva"))
                         .ReturnsAsync(user);

            var result = await _service.LoginAsync("jsilva", "123");

            Assert.True(result.Valid);
            var data = GetJsonData(result);
            Assert.NotNull(data["token"]?.ToString());
            Assert.Equal("jsilva", data["usuario"]?["login"]?.ToString());
            Assert.Equal("Dr. João", data["usuario"]?["nome"]?.ToString());
        }
    }
}