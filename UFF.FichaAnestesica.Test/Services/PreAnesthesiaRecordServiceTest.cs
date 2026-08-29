using Moq;
using UFF.FichaAnestesica.Domain.Commands;
using UFF.FichaAnestesica.Domain.Commands.AnesthesiaRecord;
using UFF.FichaAnestesica.Domain.Commands.PreAnesthesiaRecord;
using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Enums;
using UFF.FichaAnestesica.Domain.Repositories;
using UFF.FichaAnestesica.Domain.Response;
using UFF.FichaAnestesica.Domain.Services;

namespace UFF.FichaAnestesica.Test.Services
{
    public class PreAnesthesiaRecordServiceTest
    {
        private const int ResponsibleDoctorId = 1;
        private const int OtherDoctorId = 2;

        private readonly Mock<IPreAnesthesiaRecordRepository> _preAnesthesiaRepoMock;
        private readonly Mock<IAnesthesiaRecordRepository> _anesthesiaRepoMock;
        private readonly Mock<ICurrentUserService> _currentUserServiceMock;
        private readonly PreAnesthesiaRecordService _service;

        public PreAnesthesiaRecordServiceTest()
        {
            _preAnesthesiaRepoMock = new Mock<IPreAnesthesiaRecordRepository>();
            _anesthesiaRepoMock = new Mock<IAnesthesiaRecordRepository>();
            _currentUserServiceMock = new Mock<ICurrentUserService>();
            _currentUserServiceMock.Setup(x => x.UserId).Returns(ResponsibleDoctorId);
            _service = new PreAnesthesiaRecordService(
                _preAnesthesiaRepoMock.Object,
                _anesthesiaRepoMock.Object,
                _currentUserServiceMock.Object);
        }

        private static PreAnesthesiaRecordCommand BaseCommand(int anesthesiaRecordId = 10)
        {
            return new PreAnesthesiaRecordCommand
            {
                AnesthesiaRecordId = anesthesiaRecordId,
                AsaClassification = AsaClassificationEnum.ASA_II,
                PreOperativeDiagnosis = "Colelitíase"
            };
        }

        private static AnesthesiaRecord CreateAnesthesiaRecord(int? firstAnesthesiologistId = ResponsibleDoctorId)
        {
            var anesthesiaRecord = AnesthesiaRecord.Create(new AnesthesiaRecordCommand(), DateTime.MinValue);
            anesthesiaRecord.AssignFirstAnesthesiologistId(firstAnesthesiologistId);
            return anesthesiaRecord;
        }

        private static PreAnesthesiaRecord CreateBaseRecord(int anesthesiaRecordId = 10, int? firstAnesthesiologistId = ResponsibleDoctorId)
        {
            var record = PreAnesthesiaRecord.Create(BaseCommand(anesthesiaRecordId));
            record.SetAnesthesiaRecord(CreateAnesthesiaRecord(firstAnesthesiologistId));
            return record;
        }

        private static PreAnesthesiaRecordResponse GetData(CommandResult result) =>
            (PreAnesthesiaRecordResponse)result.Data!;


        [Fact]
        public async Task GetByIdAsync_Should_Return_Success_When_Record_Found()
        {
            var record = CreateBaseRecord();
            _preAnesthesiaRepoMock.Setup(r => r.GetCompleteByIdAsync(1)).ReturnsAsync(record);

            var result = await _service.GetByIdAsync(1);
            var data = GetData(result);

            Assert.True(result.Valid);
            Assert.Equal(record.AnesthesiaRecordId, data.AnesthesiaRecordId);
        }

        [Fact]
        public async Task GetByIdAsync_Should_Return_Fail_When_Record_Not_Found()
        {
            _preAnesthesiaRepoMock.Setup(r => r.GetCompleteByIdAsync(99)).ReturnsAsync((PreAnesthesiaRecord?)null);

            var result = await _service.GetByIdAsync(99);

            Assert.False(result.Valid);
        }

        // ========== GetByAnesthesiaRecordIdAsync ==========

        [Fact]
        public async Task GetByAnesthesiaRecordIdAsync_Should_Return_Success_When_Record_Found()
        {
            var record = CreateBaseRecord(20);
            _preAnesthesiaRepoMock.Setup(r => r.GetByAnesthesiaRecordIdAsync(20)).ReturnsAsync(record);

            var result = await _service.GetByAnesthesiaRecordIdAsync(20);
            var data = GetData(result);

            Assert.True(result.Valid);
            Assert.Equal(20, data.AnesthesiaRecordId);
        }

        [Fact]
        public async Task GetByAnesthesiaRecordIdAsync_Should_Return_Fail_When_No_Record_Yet()
        {
            _preAnesthesiaRepoMock.Setup(r => r.GetByAnesthesiaRecordIdAsync(30)).ReturnsAsync((PreAnesthesiaRecord?)null);

            var result = await _service.GetByAnesthesiaRecordIdAsync(30);

            Assert.False(result.Valid);
        }

        // ========== Create ==========

        [Fact]
        public async Task Create_Should_Return_Success_And_Save_When_AnesthesiaRecord_Exists_And_No_Duplicate()
        {
            var command = BaseCommand(40);
            _anesthesiaRepoMock.Setup(r => r.GetByIdAsync(40)).ReturnsAsync(CreateAnesthesiaRecord());
            _preAnesthesiaRepoMock.Setup(r => r.GetByAnesthesiaRecordIdAsync(40)).ReturnsAsync((PreAnesthesiaRecord?)null);
            _preAnesthesiaRepoMock.Setup(r => r.AddAsync(It.IsAny<PreAnesthesiaRecord>()));
            _preAnesthesiaRepoMock.Setup(r => r.SaveChangesAsync());
            _preAnesthesiaRepoMock.Setup(r => r.GetCompleteByIdAsync(It.IsAny<int>()))
                                   .ReturnsAsync(CreateBaseRecord(40));

            var result = await _service.Create(command);
            var data = GetData(result);

            Assert.True(result.Valid);
            Assert.Equal(40, data.AnesthesiaRecordId);
            _preAnesthesiaRepoMock.Verify(r => r.AddAsync(It.IsAny<PreAnesthesiaRecord>()), Times.Once);
            _preAnesthesiaRepoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task Create_Should_Return_Fail_When_AsaClassification_Missing()
        {
            var command = BaseCommand(41);
            command.AsaClassification = null;

            var result = await _service.Create(command);

            Assert.False(result.Valid);
            Assert.Equal("Classificação ASA é obrigatória", result.Message);
            _preAnesthesiaRepoMock.Verify(r => r.AddAsync(It.IsAny<PreAnesthesiaRecord>()), Times.Never);
        }

        [Fact]
        public async Task Create_Should_Return_Fail_When_AnesthesiaRecord_Not_Found()
        {
            var command = BaseCommand(42);
            _anesthesiaRepoMock.Setup(r => r.GetByIdAsync(42)).ReturnsAsync((AnesthesiaRecord)null!);

            var result = await _service.Create(command);

            Assert.False(result.Valid);
            Assert.Equal("Cirurgia/ficha anestésica não encontrada", result.Message);
        }

        [Fact]
        public async Task Create_Should_Return_Fail_When_Record_Already_Exists_For_AnesthesiaRecord()
        {
            var command = BaseCommand(43);
            _anesthesiaRepoMock.Setup(r => r.GetByIdAsync(43)).ReturnsAsync(CreateAnesthesiaRecord());
            _preAnesthesiaRepoMock.Setup(r => r.GetByAnesthesiaRecordIdAsync(43)).ReturnsAsync(CreateBaseRecord(43));

            var result = await _service.Create(command);

            Assert.False(result.Valid);
            Assert.Contains("Já existe uma avaliação", result.Message);
            _preAnesthesiaRepoMock.Verify(r => r.AddAsync(It.IsAny<PreAnesthesiaRecord>()), Times.Never);
        }

        [Fact]
        public async Task Create_Should_Return_Fail_When_Exception_Occurs()
        {
            var command = BaseCommand(44);
            _anesthesiaRepoMock.Setup(r => r.GetByIdAsync(44)).ReturnsAsync(CreateAnesthesiaRecord());
            _preAnesthesiaRepoMock.Setup(r => r.GetByAnesthesiaRecordIdAsync(44)).ReturnsAsync((PreAnesthesiaRecord?)null);
            _preAnesthesiaRepoMock.Setup(r => r.AddAsync(It.IsAny<PreAnesthesiaRecord>()))
                                   .ThrowsAsync(new Exception("Erro ao salvar"));

            var result = await _service.Create(command);

            Assert.False(result.Valid);
            Assert.Contains("Erro ao salvar", result.Message);
        }

        // ========== Update ==========

        [Fact]
        public async Task Update_Should_Return_Success_When_Record_Found()
        {
            var existing = CreateBaseRecord(50);
            _preAnesthesiaRepoMock.Setup(r => r.GetCompleteByIdAsync(50)).ReturnsAsync(existing);
            _preAnesthesiaRepoMock.Setup(r => r.Update(existing));
            _preAnesthesiaRepoMock.Setup(r => r.SaveChangesAsync());

            var updateCommand = BaseCommand(50);
            updateCommand.PreOperativeDiagnosis = "Apendicite aguda";

            var result = await _service.Update(50, updateCommand);
            var data = GetData(result);

            Assert.True(result.Valid);
            Assert.Equal("Apendicite aguda", data.PreOperativeDiagnosis);
            _preAnesthesiaRepoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task Update_Should_Return_Fail_When_AsaClassification_Missing()
        {
            var updateCommand = BaseCommand(51);
            updateCommand.AsaClassification = null;

            var result = await _service.Update(51, updateCommand);

            Assert.False(result.Valid);
            Assert.Equal("Classificação ASA é obrigatória", result.Message);
            _preAnesthesiaRepoMock.Verify(r => r.GetCompleteByIdAsync(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task Update_Should_Return_Fail_When_Record_Not_Found()
        {
            _preAnesthesiaRepoMock.Setup(r => r.GetCompleteByIdAsync(99)).ReturnsAsync((PreAnesthesiaRecord?)null);

            var result = await _service.Update(99, BaseCommand(99));

            Assert.False(result.Valid);
            Assert.Equal("Avaliação pré-anestésica não encontrada", result.Message);
        }

        [Fact]
        public async Task Update_Should_Return_Fail_When_Exception_Occurs()
        {
            var existing = CreateBaseRecord(52);
            _preAnesthesiaRepoMock.Setup(r => r.GetCompleteByIdAsync(52)).ReturnsAsync(existing);
            _preAnesthesiaRepoMock.Setup(r => r.SaveChangesAsync()).ThrowsAsync(new Exception("Erro de atualização"));

            var result = await _service.Update(52, BaseCommand(52));

            Assert.False(result.Valid);
            Assert.Contains("Erro de atualização", result.Message);
        }

     

        [Fact]
        public async Task Create_Should_Return_Forbidden_When_Not_Responsible()
        {
            _currentUserServiceMock.Setup(x => x.UserId).Returns(OtherDoctorId);
            var command = BaseCommand(45);
            _anesthesiaRepoMock.Setup(r => r.GetByIdAsync(45)).ReturnsAsync(CreateAnesthesiaRecord(ResponsibleDoctorId));

            var result = await _service.Create(command);

            Assert.False(result.Valid);
            Assert.True(result.Forbidden);
            _preAnesthesiaRepoMock.Verify(r => r.AddAsync(It.IsAny<PreAnesthesiaRecord>()), Times.Never);
        }

        [Fact]
        public async Task Update_Should_Return_Forbidden_When_Not_Responsible()
        {
            _currentUserServiceMock.Setup(x => x.UserId).Returns(OtherDoctorId);
            var existing = CreateBaseRecord(53, ResponsibleDoctorId);
            _preAnesthesiaRepoMock.Setup(r => r.GetCompleteByIdAsync(53)).ReturnsAsync(existing);

            var result = await _service.Update(53, BaseCommand(53));

            Assert.False(result.Valid);
            Assert.True(result.Forbidden);
            _preAnesthesiaRepoMock.Verify(r => r.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task GetByIdAsync_Should_Return_Success_When_Not_Responsible()
        {
            _currentUserServiceMock.Setup(x => x.UserId).Returns(OtherDoctorId);
            var record = CreateBaseRecord(54, ResponsibleDoctorId);
            _preAnesthesiaRepoMock.Setup(r => r.GetCompleteByIdAsync(54)).ReturnsAsync(record);

            var result = await _service.GetByIdAsync(54);

            Assert.True(result.Valid);
        }
    }
}
