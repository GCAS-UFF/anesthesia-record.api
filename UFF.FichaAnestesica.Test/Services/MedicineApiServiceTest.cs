using Moq;
using UFF.FichaAnestesica.Domain.Dto;
using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Repositories;
using UFF.FichaAnestesica.Domain.Repositories.ReadOnly;
using UFF.FichaAnestesica.Infra.Repositories.Aghu;

namespace UFF.FichaAnestesica.Test.Services
{
    public class MedicineApiServiceTest
    {
        [Fact]
        public async Task SyncMedicines_Should_Throw_When_Response_Null()
        {
            var medicineRepoMock = new Mock<IMedicineReadOnlyRepository>();
            var drugRepoMock = new Mock<IDrugRepository>();
            medicineRepoMock.Setup(m => m.GetDrugsFromAGHU()).ReturnsAsync((DrugListDto)null!);
            var service = new MedicineApiService(medicineRepoMock.Object, drugRepoMock.Object);

            await Assert.ThrowsAsync<NullReferenceException>(() => service.SyncMedicines());
        }

        [Fact]
        public async Task SyncMedicines_Should_Do_Nothing_When_Drugs_List_Empty()
        {
            var medicineRepoMock = new Mock<IMedicineReadOnlyRepository>();
            var drugRepoMock = new Mock<IDrugRepository>();
            medicineRepoMock.Setup(m => m.GetDrugsFromAGHU()).ReturnsAsync(new DrugListDto { Drugs = new List<DrugDto>() });
            var service = new MedicineApiService(medicineRepoMock.Object, drugRepoMock.Object);

            await service.SyncMedicines();

            drugRepoMock.Verify(d => d.GetAllAsync(), Times.Never);
            drugRepoMock.Verify(d => d.SaveChangesAsync(), Times.Never);
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
            medicineRepoMock.Setup(m => m.GetDrugsFromAGHU()).ReturnsAsync(aghuDrugs);

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

            medicineRepoMock.Setup(m => m.GetDrugsFromAGHU()).ReturnsAsync(new DrugListDto
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
    }
}