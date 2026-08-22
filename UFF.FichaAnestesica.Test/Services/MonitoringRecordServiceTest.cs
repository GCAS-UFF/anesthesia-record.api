using Moq;
using UFF.FichaAnestesica.Domain.Commands;
using UFF.FichaAnestesica.Domain.Commands.AnesthesiaRecord;
using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Enums;
using UFF.FichaAnestesica.Domain.Repositories;
using UFF.FichaAnestesica.Domain.Response;

namespace UFF.FichaAnestesica.Test.Services
{
    public class MonitoringRecordServiceTest
    {
        private readonly Mock<IMonitoringRecordRepository> _monitoringRepoMock;
        private readonly Mock<IAnesthesiaRecordRepository> _anesthesiaRepoMock;
        private readonly MonitoringRecordService _service;

        public MonitoringRecordServiceTest()
        {
            _monitoringRepoMock = new Mock<IMonitoringRecordRepository>();
            _anesthesiaRepoMock = new Mock<IAnesthesiaRecordRepository>();
            _service = new MonitoringRecordService(
                _monitoringRepoMock.Object,
                _anesthesiaRepoMock.Object);
        }

        private static MonitoringRecord CreateBaseRecord(int anesthesiaRecordId = 10, int professionalId = 5)
        {
            var cmd = new MonitoringRecordCommand(1)
            {
                AnesthesiaRecordId = anesthesiaRecordId,
                RecordedByProfessionalId = professionalId,
                StartedAt = DateTime.UtcNow
            };
            return MonitoringRecord.Create(cmd);
        }

        private static MonitoringRecordResponse GetData(CommandResult result) =>
            (MonitoringRecordResponse)result.Data!;

        // ========== GetByIdAsync ==========

        [Fact]
        public async Task GetByIdAsync_Should_Return_Success_When_Record_Found()
        {
            var record = CreateBaseRecord();
            _monitoringRepoMock.Setup(r => r.GetCompleteByIdAsync(1))
                               .ReturnsAsync(record);

            var result = await _service.GetByIdAsync(1);
            var data = GetData(result);

            Assert.True(result.Valid);
            Assert.Equal(record.AnesthesiaRecordId, data.AnesthesiaRecordId);
            Assert.Equal(record.RecordedByProfessionalId, data.RecordedByProfessionalId);
        }

        [Fact]
        public async Task GetByIdAsync_Should_Return_Fail_When_Record_Not_Found()
        {
            _monitoringRepoMock.Setup(r => r.GetCompleteByIdAsync(99))
                            .ReturnsAsync((MonitoringRecord?)null);

            var result = await _service.GetByIdAsync(99);

            Assert.False(result.Valid);
            Assert.Equal("Monitorização não encontrada", result.Data);
        }

        // ========== Create ==========

        [Fact]
        public async Task Create_Should_Return_Success_And_Save()
        {
            var command = new MonitoringRecordCommand(1)
            {
                AnesthesiaRecordId = 20,
                RecordedByProfessionalId = 7
            };
            _monitoringRepoMock.Setup(r => r.AddAsync(It.IsAny<MonitoringRecord>()));
            _monitoringRepoMock.Setup(r => r.SaveChangesAsync());

            var result = await _service.Create(command);
            var data = GetData(result);

            Assert.True(result.Valid);
            Assert.Equal(20, data.AnesthesiaRecordId);
            Assert.Equal(7, data.RecordedByProfessionalId);
            _monitoringRepoMock.Verify(r => r.AddAsync(It.IsAny<MonitoringRecord>()), Times.Once);
            _monitoringRepoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task Create_Should_Return_Fail_When_Exception_Occurs()
        {
            _monitoringRepoMock.Setup(r => r.AddAsync(It.IsAny<MonitoringRecord>()))
                               .ThrowsAsync(new Exception("Erro ao salvar"));

            var result = await _service.Create(new MonitoringRecordCommand(1)
            {
                AnesthesiaRecordId = 30,
                RecordedByProfessionalId = 8
            });

            Assert.False(result.Valid);
            Assert.Contains("Erro ao salvar", result.Message);
        }

        // ========== Update ==========

        [Fact]
        public async Task Update_Should_Return_Success_When_Record_Found()
        {
            var existing = CreateBaseRecord(40, 9);
            _monitoringRepoMock.Setup(r => r.GetCompleteByIdAsync(40))
                               .ReturnsAsync(existing);
            _monitoringRepoMock.Setup(r => r.Update(existing));
            _monitoringRepoMock.Setup(r => r.SaveChangesAsync());

            var updateCmd = new MonitoringRecordCommand(2)
            {
                AnesthesiaRecordId = 41,
                RecordedByProfessionalId = 10,
                StartedAt = new DateTime(2025, 6, 1, 10, 0, 0)
            };

            var result = await _service.Update(40, updateCmd);
            var data = GetData(result);

            Assert.True(result.Valid);
            Assert.Equal(41, data.AnesthesiaRecordId);
            Assert.Equal(10, data.RecordedByProfessionalId);
            _monitoringRepoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task Update_Should_Return_Fail_When_Record_Not_Found()
        {
            _monitoringRepoMock.Setup(r => r.GetCompleteByIdAsync(99))
                               .ReturnsAsync((MonitoringRecord?)null);

            var result = await _service.Update(99, new MonitoringRecordCommand(99));

            Assert.False(result.Valid);
            Assert.Null(result.Data);
        }

        [Fact]
        public async Task Update_Should_Return_Fail_When_Exception_Occurs()
        {
            var existing = CreateBaseRecord(50, 11);
            _monitoringRepoMock.Setup(r => r.GetCompleteByIdAsync(50))
                               .ReturnsAsync(existing);
            _monitoringRepoMock.Setup(r => r.SaveChangesAsync())
                               .ThrowsAsync(new Exception("Erro de atualização"));

            var result = await _service.Update(50, new MonitoringRecordCommand(50)
            {
                AnesthesiaRecordId = 51,
                RecordedByProfessionalId = 12
            });

            Assert.False(result.Valid);
            Assert.Contains("Erro de atualização", result.Message);
        }

        [Fact]
        public async Task FinalizePatientAsync_Should_Set_Only_Monitoring_Status_Completed_And_Save()
        {
            var anesthesiaRecord = AnesthesiaRecord.Create(new AnesthesiaRecordCommand(), DateTime.MinValue);
            var monitoringRecord = CreateBaseRecord(60, 13);
            monitoringRecord.SetAnesthesiaRecord(anesthesiaRecord);

            _monitoringRepoMock.Setup(r => r.GetCompleteByIdAsync(60))
                               .ReturnsAsync(monitoringRecord);
            _monitoringRepoMock.Setup(r => r.SaveChangesAsync());

            var result = await _service.FinalizePatientAsync(60, null);

            Assert.True(result.Valid);
            Assert.Equal(SurgeryStatusEnum.Completed, monitoringRecord.Status);
            // Finalizar o MONITORAMENTO não pode finalizar a FICHA anestésica.
            Assert.NotEqual(SurgeryStatusEnum.Completed, anesthesiaRecord.Status);
            _monitoringRepoMock.Verify(r => r.Update(monitoringRecord), Times.Once);
            _monitoringRepoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task FinalizePatientAsync_Should_Return_Fail_When_Record_Not_Found()
        {
            _monitoringRepoMock.Setup(r => r.GetCompleteByIdAsync(99))
                               .ReturnsAsync((MonitoringRecord?)null);

            var result = await _service.FinalizePatientAsync(99, null);

            Assert.False(result.Valid);
            Assert.Null(result.Data);
        }

        [Fact]
        public async Task FinalizePatientAsync_Should_Return_Fail_When_Exception_Occurs()
        {
            var monitoringRecord = CreateBaseRecord(70, 14);
            var anesthesiaRecord = AnesthesiaRecord.Create(new AnesthesiaRecordCommand(), DateTime.MinValue);
            monitoringRecord.SetAnesthesiaRecord(anesthesiaRecord);

            _monitoringRepoMock.Setup(r => r.GetCompleteByIdAsync(70))
                               .ReturnsAsync(monitoringRecord);
            _monitoringRepoMock.Setup(r => r.SaveChangesAsync())
                               .ThrowsAsync(new Exception("Falha ao salvar"));

            var result = await _service.FinalizePatientAsync(70, null);

            Assert.False(result.Valid);
            Assert.Contains("Falha ao salvar", result.Message);
        }
    }
}