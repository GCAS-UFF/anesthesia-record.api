using Moq;
using UFF.FichaAnestesica.Domain.Commands;
using UFF.FichaAnestesica.Domain.Commands.AnesthesiaRecord;
using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Repositories;
using UFF.FichaAnestesica.Domain.Repositories.ReadOnly;
using UFF.FichaAnestesica.Domain.Response;

namespace UFF.FichaAnestesica.Test.Services
{
    public class AnesthesiaRecordServiceTest
    {
        private readonly Mock<IAnesthesiaRecordRepository> _anesthesiaRepoMock;
        private readonly Mock<IMonitoringRecordRepository> _monitoringRepoMock;
        private readonly Mock<IPatientReadOnlyRepository> _aghuRepoMock;
        private readonly Mock<IProcedureRepository> _procedureRepoMock;
        private readonly AnesthesiaRecordService _service;

        public AnesthesiaRecordServiceTest()
        {
            _procedureRepoMock = new Mock<IProcedureRepository>();
            _anesthesiaRepoMock = new Mock<IAnesthesiaRecordRepository>();
            _monitoringRepoMock = new Mock<IMonitoringRecordRepository>();
            _aghuRepoMock = new Mock<IPatientReadOnlyRepository>();
            _service = new AnesthesiaRecordService(
                _anesthesiaRepoMock.Object,
                _monitoringRepoMock.Object,
                _aghuRepoMock.Object,
                _procedureRepoMock.Object);
        }

        private static AnesthesiaRecordResponse GetData(CommandResult result)
        {
            return (AnesthesiaRecordResponse)result.Data!;
        }

        // ========== GetByIdAsync ==========
        [Fact]
        public async Task GetByIdAsync_Should_Return_Response_When_Record_Found()
        {
            var record = AnesthesiaRecord.Create(new AnesthesiaRecordCommand
            {
                PatientId = "P1",
                BloodPressure = "120/80"
            }, DateTime.MinValue);
            _anesthesiaRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(record);

            var result = await _service.GetByIdAsync(1, "xpto");
            var data = GetData(result);

            Assert.True(result.Valid);
            Assert.Equal("P1", data.ExternalPatientId);
            Assert.Equal("120/80", data.BloodPressure);
        }

        [Fact]
        public async Task GetByIdAsync_Should_Throw_Exception_When_Record_Not_Found()
        {
            _anesthesiaRepoMock.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((AnesthesiaRecord?)null);

            await Assert.ThrowsAsync<Exception>(() => _service.GetByIdAsync(99, "xpto"));
        }

        // ========== Create ==========
        [Fact]
        public async Task Create_Should_Save_Record_And_Monitoring()
        {
            var command = new AnesthesiaRecordCommand
            {
                PatientId = "P2",
                BloodPressure = "130/85"
            };

            var result = await _service.Create(command);
            var data = GetData(result);

            Assert.True(result.Valid);
            Assert.Equal("P2", data.ExternalPatientId);
            _anesthesiaRepoMock.Verify(r => r.AddAsync(It.IsAny<AnesthesiaRecord>()), Times.Once);
            _monitoringRepoMock.Verify(r => r.AddAsync(It.IsAny<MonitoringRecord>()), Times.Once);
            _anesthesiaRepoMock.Verify(r => r.SaveChangesAsync(), Times.Exactly(2));
        }

        [Fact]
        public async Task Create_Should_Return_Fail_On_Exception()
        {
            _anesthesiaRepoMock.Setup(r => r.AddAsync(It.IsAny<AnesthesiaRecord>()))
                            .ThrowsAsync(new Exception("Erro de conexão"));

            var result = await _service.Create(new AnesthesiaRecordCommand());

            Assert.False(result.Valid);
            Assert.Equal("Erro de conexão", result.Data);
        }
        // ========== Update ==========
        [Fact]
        public async Task Update_Should_Update_Record_And_Save()
        {
            var existing = AnesthesiaRecord.Create(new AnesthesiaRecordCommand { PatientId = "P3" }, DateTime.MinValue);
            _anesthesiaRepoMock.Setup(r => r.GetByIdAsync(3)).ReturnsAsync(existing);

            var command = new AnesthesiaRecordCommand
            {
                PatientId = "P3",
                BloodPressure = "110/70"
            };

            var result = await _service.Update(3, command);
            var data = GetData(result);

            Assert.True(result.Valid);
            Assert.Equal("110/70", data.BloodPressure);
            _anesthesiaRepoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task Update_Should_Throw_When_Record_Not_Found()
        {
            _anesthesiaRepoMock.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((AnesthesiaRecord?)null);

            await Assert.ThrowsAsync<Exception>(() => _service.Update(99, new AnesthesiaRecordCommand()));
        }

        [Fact]
        public async Task Update_Should_Return_Fail_On_Exception()
        {
            var existing = AnesthesiaRecord.Create(new AnesthesiaRecordCommand { PatientId = "P4" }, DateTime.MinValue);
            _anesthesiaRepoMock.Setup(r => r.GetByIdAsync(4)).ReturnsAsync(existing);
            _anesthesiaRepoMock.Setup(r => r.SaveChangesAsync()).ThrowsAsync(new Exception("Erro ao salvar"));

            var result = await _service.Update(4, new AnesthesiaRecordCommand());

            Assert.False(result.Valid);
            Assert.Equal("Erro ao salvar", result.Data);
        }
    }
}