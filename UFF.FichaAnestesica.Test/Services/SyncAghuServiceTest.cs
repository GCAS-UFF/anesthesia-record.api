using Moq;
using UFF.FichaAnestesica.Domain.Dto;
using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Enums;
using UFF.FichaAnestesica.Domain.Repositories;
using UFF.FichaAnestesica.Domain.Repositories.ReadOnly;
using UFF.FichaAnestesica.Infra.Repositories.Aghu;

namespace UFF.FichaAnestesica.Test.Services
{
    public class SyncAghuServiceTest
    {
        // ========== MedicineApiService ==========
        [Fact]
        public async Task SyncMedicines_Should_Throw_When_Response_Null()
        {
            var medicineRepoMock = new Mock<IMedicineReadOnlyRepository>();
            var drugRepoMock = new Mock<IDrugRepository>();
            medicineRepoMock.Setup(m => m.GetDrugssFromAGHU()).ReturnsAsync((DrugListDto)null!);
            var service = new MedicineApiService(medicineRepoMock.Object, drugRepoMock.Object);

            await Assert.ThrowsAsync<NullReferenceException>(() => service.SyncMedicines());
        }

        [Fact]
        public async Task SyncMedicines_Should_Do_Nothing_When_Drugs_List_Empty()
        {
            var medicineRepoMock = new Mock<IMedicineReadOnlyRepository>();
            var drugRepoMock = new Mock<IDrugRepository>();
            medicineRepoMock.Setup(m => m.GetDrugssFromAGHU()).ReturnsAsync(new DrugListDto { Drugs = new List<DrugDto>() });
            var service = new MedicineApiService(medicineRepoMock.Object, drugRepoMock.Object);

            await service.SyncMedicines();

            drugRepoMock.Verify(d => d.GetAllAsync(), Times.Never);
        }

        [Fact]
        public async Task SyncMedicines_Should_Add_New_Drugs_And_Update_Existing()
        {
            var medicineRepoMock = new Mock<IMedicineReadOnlyRepository>();
            var drugRepoMock = new Mock<IDrugRepository>();

            var aghuDrugs = new DrugListDto
            {
                Drugs = new List<DrugDto>
                {
                    new DrugDto { Codigo = "D1", Description = "Dipirona", Unity = "mg" },
                    new DrugDto { Codigo = "D2", Description = "Paracetamol", Unity = "mg" }
                }
            };
            medicineRepoMock.Setup(m => m.GetDrugssFromAGHU()).ReturnsAsync(aghuDrugs);

            var existingDrug = Drug.Create("D1", "Dipirona antiga", "ml");
            drugRepoMock.Setup(d => d.GetAllAsync()).ReturnsAsync(new List<Drug> { existingDrug });

            var service = new MedicineApiService(medicineRepoMock.Object, drugRepoMock.Object);
            await service.SyncMedicines();

            Assert.Equal("Dipirona", existingDrug.Description);
            Assert.Equal("mg", existingDrug.DefaultUnit);
            drugRepoMock.Verify(d => d.Update(existingDrug), Times.Once);

            drugRepoMock.Verify(d => d.AddAsync(It.Is<Drug>(drug => drug.ExternalId == "D2" && drug.Description == "Paracetamol")), Times.Once);
            drugRepoMock.Verify(d => d.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task SyncMedicines_Should_Disable_Drugs_Not_In_AGHU()
        {
            var medicineRepoMock = new Mock<IMedicineReadOnlyRepository>();
            var drugRepoMock = new Mock<IDrugRepository>();

            medicineRepoMock.Setup(m => m.GetDrugssFromAGHU()).ReturnsAsync(new DrugListDto
            {
                Drugs = new List<DrugDto>
                {
                    new DrugDto { Codigo = "D1", Description = "Dipirona", Unity = "mg" }
                }
            });

            var activeDrug1 = Drug.Create("D1", "Dipirona", "mg");
            var activeDrug2 = Drug.Create("D2", "Morfina", "ampola");
            drugRepoMock.Setup(d => d.GetAllAsync()).ReturnsAsync(new List<Drug> { activeDrug1, activeDrug2 });

            var service = new MedicineApiService(medicineRepoMock.Object, drugRepoMock.Object);
            await service.SyncMedicines();

            Assert.False(activeDrug2.Active);
            drugRepoMock.Verify(d => d.Update(activeDrug2), Times.Once);
        }

        // ========== ProfessionalApiService ==========
        [Fact]
        public async Task SyncProfessionals_Should_Do_Nothing_When_Response_Null()
        {
            var professionalRepoMock = new Mock<IProfessionalReadOnlyRepository>();
            var userRepoMock = new Mock<IUserRepository>();
            professionalRepoMock.Setup(p => p.GetProfessionalsFromAGHU()).ReturnsAsync((UserListDto)null!);
            var service = new ProfessionalApiService(professionalRepoMock.Object, userRepoMock.Object);

            await service.SyncProfessionals();

            userRepoMock.Verify(u => u.GetAllAsync(), Times.Never);
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