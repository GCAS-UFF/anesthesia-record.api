using Moq;
using UFF.FichaAnestesica.Domain.Commands;
using UFF.FichaAnestesica.Domain.Dto;
using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Enums;
using UFF.FichaAnestesica.Domain.Repositories.ReadOnly;
using UFF.FichaAnestesica.Service.Services;

namespace UFF.FichaAnestesica.Test.Services
{
    public class ProfessionalServicesTest
    {
        private readonly Mock<IProfessionalReadOnlyRepository> _repoMock;
        private readonly ProfessionalServices _service;

        public ProfessionalServicesTest()
        {
            _repoMock = new Mock<IProfessionalReadOnlyRepository>();
            _service = new ProfessionalServices(_repoMock.Object);
        }

        [Fact]
        public async Task GetProfessionalsForAnethesiaRecord_Should_Return_Success_With_Mapped_Users()
        {
            var users = new List<User>
            {
                User.Create(1, "Dr. João", "joao@email.com", "joao.login", "CRM123", MedicalSpecialtyEnum.Anesthesiology, SectorEnum.SurgicalCenter),
                User.Create(2, "Dra. Maria", "maria@email.com", "maria.login", "CRM456", MedicalSpecialtyEnum.Cardiology, SectorEnum.AdultICU)
            };
            _repoMock.Setup(r => r.GetProfessionalsForAnethesiaRecord(It.IsAny<string>()))
                     .ReturnsAsync(users);

            var result = await _service.GetProfessionalsForAnethesiaRecord("João");

            Assert.True(result.Valid);
            var mapped = Assert.IsType<List<UserResponse>>(result.Data);
            Assert.Equal(2, mapped.Count);
            Assert.Equal(users[0].Id, mapped[0].Id);
            Assert.Equal(users[0].Name, mapped[0].Name);
        }

        [Fact]
        public async Task GetProfessionalsForAnethesiaRecord_Should_Return_Success_With_Empty_List()
        {
            _repoMock.Setup(r => r.GetProfessionalsForAnethesiaRecord(It.IsAny<string>()))
                     .ReturnsAsync(new List<User>());

            var result = await _service.GetProfessionalsForAnethesiaRecord("");

            Assert.True(result.Valid);
            var mapped = Assert.IsType<List<UserResponse>>(result.Data);
            Assert.Empty(mapped);
        }

        [Fact]
        public async Task GetProfessionalsForAnethesiaRecord_Should_Return_Success_With_Null_Data()
        {
            _repoMock.Setup(r => r.GetProfessionalsForAnethesiaRecord(It.IsAny<string>()))
                     .ReturnsAsync((List<User>)null);

            var result = await _service.GetProfessionalsForAnethesiaRecord("abc");

            Assert.True(result.Valid);
            Assert.Null(result.Data);
        }

        [Fact]
        public async Task GetProfessionalsForAnethesiaRecord_Should_Propagate_Exception()
        {
            var expectedException = new InvalidOperationException("Erro de conexão");
            _repoMock.Setup(r => r.GetProfessionalsForAnethesiaRecord(It.IsAny<string>()))
                     .ThrowsAsync(expectedException);

            var ex = await Assert.ThrowsAsync<InvalidOperationException>(
                () => _service.GetProfessionalsForAnethesiaRecord("nome"));
            Assert.Equal(expectedException.Message, ex.Message);
        }
    }
}