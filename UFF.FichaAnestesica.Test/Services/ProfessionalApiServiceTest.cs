using Moq;
using UFF.FichaAnestesica.Domain.Dto;
using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Enums;
using UFF.FichaAnestesica.Domain.Repositories;
using UFF.FichaAnestesica.Domain.Repositories.ReadOnly;
using UFF.FichaAnestesica.Infra.Repositories.Aghu;

namespace UFF.FichaAnestesica.Test.Services
{
    public class ProfessionalApiServiceTest
    {
        [Fact]
        public async Task SyncProfessionals_Should_Do_Nothing_When_Response_Null()
        {
            var professionalRepoMock = new Mock<IProfessionalReadOnlyRepository>();
            var userRepoMock = new Mock<IUserRepository>();
            professionalRepoMock.Setup(p => p.GetProfessionalsFromAGHU()).ReturnsAsync((UserListDto)null!);
            var service = new ProfessionalApiService(professionalRepoMock.Object, userRepoMock.Object);

            await service.SyncProfessionals();

            userRepoMock.Verify(u => u.GetAllAsync(), Times.Never);
            userRepoMock.Verify(u => u.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task SyncProfessionals_Should_Do_Nothing_When_Professionals_List_Empty()
        {
            var professionalRepoMock = new Mock<IProfessionalReadOnlyRepository>();
            var userRepoMock = new Mock<IUserRepository>();
            professionalRepoMock.Setup(p => p.GetProfessionalsFromAGHU()).ReturnsAsync(new UserListDto { Professionals = new List<UserDto>() });
            var service = new ProfessionalApiService(professionalRepoMock.Object, userRepoMock.Object);

            await service.SyncProfessionals();

            userRepoMock.Verify(u => u.GetAllAsync(), Times.Never);
            userRepoMock.Verify(u => u.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task SyncProfessionals_Should_Add_New_Users_And_Update_Existing()
        {
            var professionalRepoMock = new Mock<IProfessionalReadOnlyRepository>();
            var userRepoMock = new Mock<IUserRepository>();

            var aghuProfessionals = new UserListDto
            {
                Professionals = new List<UserDto>
                {
                    new UserDto { Id = 1, Name = "Dr. João", Email = "joao@teste.com", Login = "jsilva", Registration = "123", MedicalSpecialty = "Anesthesiology", Sector = "SurgicalCenter" },
                    new UserDto { Id = 2, Name = "Dr. Maria", Email = "maria@teste.com", Login = "msantos", Registration = "456", MedicalSpecialty = "Cardiology", Sector = "AdultICU" }
                }
            };
            professionalRepoMock.Setup(p => p.GetProfessionalsFromAGHU()).ReturnsAsync(aghuProfessionals);

            var existingUser = User.Create(1, "João Antigo", "old@teste.com", "old", "111", MedicalSpecialtyEnum.Anesthesiology, SectorEnum.SurgicalCenter);
            userRepoMock.Setup(u => u.GetAllAsync()).ReturnsAsync(new List<User> { existingUser });

            var service = new ProfessionalApiService(professionalRepoMock.Object, userRepoMock.Object);
            await service.SyncProfessionals();

            Assert.Equal("Dr. João", existingUser.Name);
            Assert.Equal("joao@teste.com", existingUser.Email);
            userRepoMock.Verify(u => u.Update(existingUser), Times.Once);
            userRepoMock.Verify(u => u.AddAsync(It.Is<User>(user => user.ExternalId == 2 && user.Name == "Dr. Maria")), Times.Once);
            userRepoMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task SyncProfessionals_Should_Disable_Users_Not_In_AGHU()
        {
            var professionalRepoMock = new Mock<IProfessionalReadOnlyRepository>();
            var userRepoMock = new Mock<IUserRepository>();

            professionalRepoMock.Setup(p => p.GetProfessionalsFromAGHU()).ReturnsAsync(new UserListDto
            {
                Professionals = new List<UserDto>
                {
                    new UserDto { Id = 1, Name = "Dr. João", Email = "joao@teste.com", Login = "jsilva", Registration = "123", MedicalSpecialty = "Anesthesiology", Sector = "SurgicalCenter" }
                }
            });

            var activeUser1 = User.Create(1, "Dr. João", "joao@teste.com", "jsilva", "123", MedicalSpecialtyEnum.Anesthesiology, SectorEnum.SurgicalCenter);
            var activeUser2 = User.Create(2, "Dr. Excluído", "ex@teste.com", "ex", "999", MedicalSpecialtyEnum.Anesthesiology, SectorEnum.SurgicalCenter);
            userRepoMock.Setup(u => u.GetAllAsync()).ReturnsAsync(new List<User> { activeUser1, activeUser2 });

            var service = new ProfessionalApiService(professionalRepoMock.Object, userRepoMock.Object);
            await service.SyncProfessionals();

            Assert.Equal(UserStatusEnum.Disabled, activeUser2.Status);
            userRepoMock.Verify(u => u.Update(activeUser2), Times.Once);
        }
    }
}