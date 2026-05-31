using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Test.Entities.UserTest
{
    public class UserTest
    {
        [Fact]
        public void Create_Should_Create_User_With_Expected_Values()
        {
            const int externalId = 123;
            const string name = "João Silva";
            const string email = "joao@teste.com";
            const string login = "jsilva";
            const string registration = "123456";

            var user = User.Create(
                externalId,
                name,
                email,
                login,
                registration,
                MedicalSpecialtyEnum.Anesthesiology,
                SectorEnum.SurgicalCenter);

            Assert.Equal(externalId, user.ExternalId);
            Assert.Equal(name, user.Name);
            Assert.Equal(email, user.Email);
            Assert.Equal(login, user.Login);
            Assert.Equal(registration, user.Registration);

            Assert.Equal(MedicalSpecialtyEnum.Anesthesiology, user.MedicalSpecialty);
            Assert.Equal(SectorEnum.SurgicalCenter, user.Sector);

            Assert.True(user.CanLogIn);
            Assert.Equal(UserStatusEnum.Enabled, user.Status);

            Assert.NotEqual(default, user.CreatedAt);
            Assert.NotNull(user.LastLoginAt);
        }

        [Fact]
        public void Update_Should_Update_All_Fields()
        {
            var user = User.Create(
                123,
                "Nome Antigo",
                "antigo@teste.com",
                "login_antigo",
                "111",
                MedicalSpecialtyEnum.Anesthesiology,
                SectorEnum.SurgicalCenter);

            user.Update(
                "Nome Novo",
                "novo@teste.com",
                "login_novo",
                "222",
                MedicalSpecialtyEnum.Cardiology,
                SectorEnum.AdultICU);

            Assert.Equal("Nome Novo", user.Name);
            Assert.Equal("novo@teste.com", user.Email);
            Assert.Equal("login_novo", user.Login);
            Assert.Equal("222", user.Registration);

            Assert.Equal(MedicalSpecialtyEnum.Cardiology, user.MedicalSpecialty);
            Assert.Equal(SectorEnum.AdultICU, user.Sector);

            Assert.Equal(UserStatusEnum.Enabled, user.Status);
            Assert.NotNull(user.LastSyncAt);
        }

        [Fact]
        public void Update_Should_Keep_Existing_String_Values_When_New_Values_Are_Null_Or_Whitespace()
        {
            var user = User.Create(
                123,
                "Nome Original",
                "original@teste.com",
                "login_original",
                "123",
                MedicalSpecialtyEnum.Anesthesiology,
                SectorEnum.SurgicalCenter);

            user.Update(
                "",
                null!,
                " ",
                null!,
                MedicalSpecialtyEnum.Cardiology,
                SectorEnum.AdultICU);

            Assert.Equal("Nome Original", user.Name);
            Assert.Equal("original@teste.com", user.Email);
            Assert.Equal("login_original", user.Login);
            Assert.Equal("123", user.Registration);

            Assert.Equal(MedicalSpecialtyEnum.Cardiology, user.MedicalSpecialty);
            Assert.Equal(SectorEnum.AdultICU, user.Sector);
        }

        [Fact]
        public void Update_Should_Set_LastSyncAt()
        {
            var user = User.Create(
                123,
                "João",
                "joao@teste.com",
                "joao",
                "123",
                MedicalSpecialtyEnum.Anesthesiology,
                SectorEnum.SurgicalCenter);

            user.Update(
                "João Atualizado",
                "joao@teste.com",
                "joao",
                "123",
                MedicalSpecialtyEnum.Anesthesiology,
                SectorEnum.SurgicalCenter);

            Assert.NotNull(user.LastSyncAt);
        }

        [Fact]
        public void Disable_Should_Set_Status_To_Disabled()
        {
            var user = User.Create(
                123,
                "João",
                "joao@teste.com",
                "joao",
                "123",
                MedicalSpecialtyEnum.Anesthesiology,
                SectorEnum.SurgicalCenter);

            user.Disable();

            Assert.Equal(UserStatusEnum.Disabled, user.Status);
        }

        [Fact]
        public void Disable_Should_Set_LastSyncAt()
        {
            var user = User.Create(
                123,
                "João",
                "joao@teste.com",
                "joao",
                "123",
                MedicalSpecialtyEnum.Anesthesiology,
                SectorEnum.SurgicalCenter);

            user.Disable();

            Assert.NotNull(user.LastSyncAt);
        }
    }
}