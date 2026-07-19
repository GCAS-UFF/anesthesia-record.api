using Moq;
using UFF.FichaAnestesica.Domain.Commands;
using UFF.FichaAnestesica.Domain.Dto;
using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Enums;
using UFF.FichaAnestesica.Domain.Repositories;
using UFF.FichaAnestesica.Domain.Repositories.ReadOnly;
using UFF.FichaAnestesica.Domain.Response;
using UFF.FichaAnestesica.Service.Services;

namespace UFF.FichaAnestesica.Test.Services
{
    public class SurgeryServiceTest
    {
        private readonly Mock<IUserRepository> _userRepoMock;
        private readonly Mock<IPatientReadOnlyRepository> _hospitalApiRepoMock;
        private readonly Mock<IAnesthesiaRecordRepository> _anesthesiaRepoMock;
        private readonly Mock<IMonitoringRecordRepository> _monitoringRepoMock;
        private readonly SurgeryService _service;

        public SurgeryServiceTest()
        {
            _userRepoMock = new Mock<IUserRepository>();
            _hospitalApiRepoMock = new Mock<IPatientReadOnlyRepository>();
            _anesthesiaRepoMock = new Mock<IAnesthesiaRecordRepository>();
            _monitoringRepoMock = new Mock<IMonitoringRecordRepository>();
            _service = new SurgeryService(
                _userRepoMock.Object,
                _hospitalApiRepoMock.Object,
                _anesthesiaRepoMock.Object,
                _monitoringRepoMock.Object);
        }

        // ========== GetPatientsWithSurgeriesAsync ==========
        [Fact]
        public async Task GetPatientsWithSurgeriesAsync_Should_Return_Empty_When_No_Hospital_Data()
        {
            _hospitalApiRepoMock
                .Setup(h => h.GetPatientsFromHospitalAsync(It.IsAny<DateTime?>(), string.Empty, It.IsAny<SurgeryStatusEnum?>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new PagedResponse<PatientDetailDto> { Data = new List<PatientDetailDto>(), TotalItems = 0, Page = 1, PageSize = 10 });

            var result = await _service.GetPatientsWithSurgeriesAsync(1, null, string.Empty, null);
            var paged = Assert.IsType<PagedResponse<PatientSurgeryResponse>>(result.Data);
            Assert.True(result.Valid);
            Assert.Empty(paged.Data);
        }

        [Fact]
        public async Task GetPatientsWithSurgeriesAsync_Should_Set_Anesthesiologists_Null_When_No_Record()
        {
            var patientDto = new PatientDetailDto
            {
                PatientId = "P1",
                FullName = "João",
                SurgeryId = 1,
                Status = "agendado"
            };
            _hospitalApiRepoMock
                .Setup(h => h.GetPatientsFromHospitalAsync(It.IsAny<DateTime?>(), string.Empty, It.IsAny<SurgeryStatusEnum?>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new PagedResponse<PatientDetailDto> { Data = new List<PatientDetailDto> { patientDto }, TotalItems = 1 });
            _anesthesiaRepoMock.Setup(a => a.GetByIdsAsync(It.IsAny<string[]>())).ReturnsAsync(new List<AnesthesiaRecord>());

            var result = await _service.GetPatientsWithSurgeriesAsync(1, null, string.Empty, null);
            var paged = Assert.IsType<PagedResponse<PatientSurgeryResponse>>(result.Data);
            var patient = Assert.Single(paged.Data);
            Assert.Null(patient.FirstAnesthesiologist);
        }

        // ========== GetPatientAnesthesiaRecordByIdAsync ==========
        [Fact]
        public async Task GetPatientAnesthesiaRecordByIdAsync_Should_Return_Null_When_Patient_Not_Found()
        {
            _hospitalApiRepoMock
                .Setup(h => h.GetFromHospitalByPatientIdAndSurgeryIdAsync("P99", 99))
                .ReturnsAsync((PatientDetailDto?)null);

            var result = await _service.GetPatientAnesthesiaRecordByIdAsync("P99", 99);
            Assert.Null(result);
        }

        [Fact]
        public async Task GetPatientAnesthesiaRecordByIdAsync_Should_Return_Detail_When_Found()
        {
            var patientDto = new PatientDetailDto
            {
                PatientId = "P1",
                FullName = "Maria",
                Status = "agendado"
            };
            _hospitalApiRepoMock
                .Setup(h => h.GetFromHospitalByPatientIdAndSurgeryIdAsync("P1", 1))
                .ReturnsAsync(patientDto);
            var record = AnesthesiaRecord.Create(new Domain.Commands.AnesthesiaRecord.AnesthesiaRecordCommand { PatientId = "P1" });
            var anesthesiologist = User.Create(1, "Dr. João", "joao@teste.com", "jsilva", "123", MedicalSpecialtyEnum.Anesthesiology, SectorEnum.SurgicalCenter);
            typeof(AnesthesiaRecord).GetProperty("FirstAnesthesiologist")!.SetValue(record, anesthesiologist);
            _anesthesiaRepoMock.Setup(a => a.GetByIdAsync(1)).ReturnsAsync(record);

            var result = await _service.GetPatientAnesthesiaRecordByIdAsync("P1", 1);
            Assert.NotNull(result);
            Assert.True(result.Valid);
        }

        // ========== AssumePatientAsync ==========
        [Fact]
        public async Task AssumePatientAsync_Should_Throw_When_Patient_Not_Found()
        {
            _hospitalApiRepoMock
                .Setup(h => h.GetFromHospitalByPatientIdAndSurgeryIdAsync("P1", 1))
                .ReturnsAsync((PatientDetailDto?)null);

            await Assert.ThrowsAsync<Exception>(() => _service.AssumePatientAsync("P1", 1, 10));
        }

        [Fact]
        public async Task AssumePatientAsync_Should_Create_Record_And_Monitoring_When_New()
        {
            var patientDto = new PatientDetailDto
            {
                PatientId = "P1",
                FullName = "João",
                Status = "agendado"
            };
            _hospitalApiRepoMock
                .Setup(h => h.GetFromHospitalByPatientIdAndSurgeryIdAsync("P1", 1))
                .ReturnsAsync(patientDto);
            var anesthesiologist = User.Create(10, "Dr. Ana", "ana@teste.com", "ana", "CRM10", MedicalSpecialtyEnum.Anesthesiology, SectorEnum.SurgicalCenter);
            _userRepoMock.Setup(u => u.GetUserByIdAsync(10)).ReturnsAsync(anesthesiologist);
            _anesthesiaRepoMock.Setup(a => a.GetByIdAsync(1)).ReturnsAsync((AnesthesiaRecord?)null);

            var result = await _service.AssumePatientAsync("P1", 1, 10);
            Assert.True(result.Valid);
            _anesthesiaRepoMock.Verify(a => a.AddAsync(It.IsAny<AnesthesiaRecord>()), Times.Once);
            _monitoringRepoMock.Verify(m => m.AddAsync(It.IsAny<MonitoringRecord>()), Times.Once);
            _anesthesiaRepoMock.Verify(a => a.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task AssumePatientAsync_Should_Return_Fail_On_Exception()
        {
            _hospitalApiRepoMock
                .Setup(h => h.GetFromHospitalByPatientIdAndSurgeryIdAsync("P1", 1))
                .ReturnsAsync(new PatientDetailDto
                {
                    PatientId = "P1",
                    FullName = "João",
                    Status = "agendado"
                });
            _anesthesiaRepoMock.Setup(a => a.GetByIdAsync(1)).ReturnsAsync((AnesthesiaRecord?)null);
            _anesthesiaRepoMock.Setup(a => a.AddAsync(It.IsAny<AnesthesiaRecord>())).ThrowsAsync(new Exception("Erro DB"));

            var result = await _service.AssumePatientAsync("P1", 1, null);
            Assert.False(result.Valid);
            Assert.Contains("Erro DB", result.Message);
        }
    }
}