using Moq;
using UFF.FichaAnestesica.Domain.Commands;
using UFF.FichaAnestesica.Domain.Commands.AnesthesiaRecord;
using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Repositories;
using UFF.FichaAnestesica.Domain.Response;
using UFF.FichaAnestesica.Service.Services;

namespace UFF.FichaAnestesica.Test.Services
{
    public class AnesthesiaRecordServiceTest
    {
        private readonly Mock<IAnesthesiaRecordRepository> _anesthesiaRepoMock;
        private readonly Mock<IMonitoringRecordRepository> _monitoringRepoMock;
        private readonly AnesthesiaRecordService _service;

        public AnesthesiaRecordServiceTest()
        {
            _anesthesiaRepoMock = new Mock<IAnesthesiaRecordRepository>();
            _monitoringRepoMock = new Mock<IMonitoringRecordRepository>();
            _service = new AnesthesiaRecordService(
                _anesthesiaRepoMock.Object,
                _monitoringRepoMock.Object);
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
                ExternalPatientId = "P1",
                BloodPressure = "120/80"
            });
            _anesthesiaRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(record);

            var result = await _service.GetByIdAsync(1);
            var data = GetData(result);

            Assert.True(result.Valid);
            Assert.Equal("P1", data.ExternalPatientId);
            Assert.Equal("120/80", data.BloodPressure);
        }

        [Fact]
        public async Task GetByIdAsync_Should_Throw_Exception_When_Record_Not_Found()
        {
            _anesthesiaRepoMock.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((AnesthesiaRecord?)null);

            await Assert.ThrowsAsync<Exception>(() => _service.GetByIdAsync(99));
        }

        // ========== Create ==========
        [Fact]
        public async Task Create_Should_Save_Record_And_Monitoring()
        {
            var command = new AnesthesiaRecordCommand
            {
                ExternalPatientId = "P2",
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
            var existing = AnesthesiaRecord.Create(new AnesthesiaRecordCommand { ExternalPatientId = "P3" });
            _anesthesiaRepoMock.Setup(r => r.GetByIdAsync(3)).ReturnsAsync(existing);

            var command = new AnesthesiaRecordCommand
            {
                ExternalPatientId = "P3",
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
            var existing = AnesthesiaRecord.Create(new AnesthesiaRecordCommand { ExternalPatientId = "P4" });
            _anesthesiaRepoMock.Setup(r => r.GetByIdAsync(4)).ReturnsAsync(existing);
            _anesthesiaRepoMock.Setup(r => r.SaveChangesAsync()).ThrowsAsync(new Exception("Erro ao salvar"));

            var result = await _service.Update(4, new AnesthesiaRecordCommand());

            Assert.False(result.Valid);
            Assert.Equal("Erro ao salvar", result.Data);
        }
    }
}