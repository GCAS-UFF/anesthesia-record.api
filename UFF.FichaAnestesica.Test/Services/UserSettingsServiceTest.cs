using Moq;
using UFF.FichaAnestesica.Domain.Commands.UserSettings;
using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Enums;
using UFF.FichaAnestesica.Domain.Repositories;
using UFF.FichaAnestesica.Domain.Security;
using UFF.FichaAnestesica.Domain.Services;
using UFF.FichaAnestesica.Service.Services;

namespace UFF.FichaAnestesica.Test.Services
{
    public class UserSettingsServiceTest
    {
        private readonly Mock<IUserSettingsRepository> _userSettingsRepoMock;
        private readonly Mock<IInstitutionSettingsRepository> _institutionSettingsRepoMock;
        private readonly Mock<IUserRepository> _userRepoMock;
        private readonly Mock<ICurrentUserService> _currentUserMock;
        private readonly Mock<IHttpClientFactory> _httpClientFactoryMock;
        private readonly UserSettingsService _service;

        public UserSettingsServiceTest()
        {
            _userSettingsRepoMock = new Mock<IUserSettingsRepository>();
            _institutionSettingsRepoMock = new Mock<IInstitutionSettingsRepository>();
            _userRepoMock = new Mock<IUserRepository>();
            _currentUserMock = new Mock<ICurrentUserService>();
            _httpClientFactoryMock = new Mock<IHttpClientFactory>();

            _service = new UserSettingsService(
                _userSettingsRepoMock.Object,
                _institutionSettingsRepoMock.Object,
                _userRepoMock.Object,
                _currentUserMock.Object,
                _httpClientFactoryMock.Object);
        }

        private static User CreateUser() =>
            User.Create(1, "Admin", "admin@teste.com", "admin", "000",
                MedicalSpecialtyEnum.Anesthesiology, SectorEnum.SurgicalCenter);

        // ========== ChangeAdminPasswordAsync ==========

        [Fact]
        public async Task ChangeAdminPasswordAsync_Should_Fail_When_Current_Password_Is_Wrong()
        {
            var user = CreateUser();
            user.ChangePassword(PasswordHasher.Hash("senhaCorreta"));

            _currentUserMock.Setup(c => c.UserId).Returns(1);
            _userRepoMock.Setup(u => u.GetUserByIdAsync(1)).ReturnsAsync(user);

            var result = await _service.ChangeAdminPasswordAsync(new ChangeAdminPasswordCommand
            {
                CurrentPassword = "senhaErrada",
                NewPassword = "novaSenha123"
            });

            Assert.False(result.Valid);
            Assert.Contains("incorreta", result.Message);
        }

        [Fact]
        public async Task ChangeAdminPasswordAsync_Should_Update_To_A_Hashed_Password_When_Current_Password_Matches_A_Hash()
        {
            var user = CreateUser();
            user.ChangePassword(PasswordHasher.Hash("senhaCorreta"));

            _currentUserMock.Setup(c => c.UserId).Returns(1);
            _userRepoMock.Setup(u => u.GetUserByIdAsync(1)).ReturnsAsync(user);

            var result = await _service.ChangeAdminPasswordAsync(new ChangeAdminPasswordCommand
            {
                CurrentPassword = "senhaCorreta",
                NewPassword = "novaSenha123"
            });

            Assert.True(result.Valid);
            Assert.NotEqual("novaSenha123", user.Password);
            Assert.True(PasswordHasher.IsHashed(user.Password));
            Assert.True(PasswordHasher.Verify("novaSenha123", user.Password));
            _userRepoMock.Verify(u => u.Update(user), Times.Once);
            _userRepoMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task ChangeAdminPasswordAsync_Should_Accept_Current_Password_Stored_As_Legacy_Plain_Text()
        {
            var user = CreateUser();
            user.ChangePassword("senhaEmTextoPuro");

            _currentUserMock.Setup(c => c.UserId).Returns(1);
            _userRepoMock.Setup(u => u.GetUserByIdAsync(1)).ReturnsAsync(user);

            var result = await _service.ChangeAdminPasswordAsync(new ChangeAdminPasswordCommand
            {
                CurrentPassword = "senhaEmTextoPuro",
                NewPassword = "novaSenha123"
            });

            Assert.True(result.Valid);
            Assert.True(PasswordHasher.IsHashed(user.Password)); 
        }

        [Fact]
        public async Task ChangeAdminPasswordAsync_Should_Fail_When_New_Password_Is_Too_Short()
        {
            _currentUserMock.Setup(c => c.UserId).Returns(1);

            var result = await _service.ChangeAdminPasswordAsync(new ChangeAdminPasswordCommand
            {
                CurrentPassword = "qualquer",
                NewPassword = "123"
            });

            Assert.False(result.Valid);
            _userRepoMock.Verify(u => u.GetUserByIdAsync(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task TestAghuConnectionAsync_Should_Fail_When_Url_Is_Empty()
        {
            var result = await _service.TestAghuConnectionAsync(new TestAghuConnectionCommand
            {
                AghuBaseUrl = ""
            });

            Assert.False(result.Valid);
        }

        [Fact]
        public async Task TestAghuConnectionAsync_Should_Fail_When_Url_Is_Not_Well_Formed()
        {
            var result = await _service.TestAghuConnectionAsync(new TestAghuConnectionCommand
            {
                AghuBaseUrl = "not-a-url"
            });

            Assert.False(result.Valid);
        }
    }
}
