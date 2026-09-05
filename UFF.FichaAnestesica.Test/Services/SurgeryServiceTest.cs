using Moq;
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
        private readonly Mock<IPreAnesthesiaRecordRepository> _preAnesthesiaRepoMock;
        private readonly SurgeryService _service;

        public SurgeryServiceTest()
        {
            _userRepoMock = new Mock<IUserRepository>();
            _hospitalApiRepoMock = new Mock<IPatientReadOnlyRepository>();
            _anesthesiaRepoMock = new Mock<IAnesthesiaRecordRepository>();
            _monitoringRepoMock = new Mock<IMonitoringRecordRepository>();
            _preAnesthesiaRepoMock = new Mock<IPreAnesthesiaRecordRepository>();
            _service = new SurgeryService(
                _userRepoMock.Object,
                _hospitalApiRepoMock.Object,
                _anesthesiaRepoMock.Object,
                _monitoringRepoMock.Object,
                _preAnesthesiaRepoMock.Object);
        }

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
                Status = "agendado",
                ExpectedAt = new DateTime(2026, 1, 1),
                SurgeryDate = new DateTime(2026, 1, 1),
            };
            _hospitalApiRepoMock
                .Setup(h => h.GetPatientsFromHospitalAsync(It.IsAny<DateTime?>(), string.Empty, It.IsAny<SurgeryStatusEnum?>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new PagedResponse<PatientDetailDto> { Data = new List<PatientDetailDto> { patientDto }, TotalItems = 1 });
            _anesthesiaRepoMock.Setup(a => a.GetByIdsAsync(It.IsAny<string[]>())).ReturnsAsync(new List<AnesthesiaRecord>());
            _preAnesthesiaRepoMock
                .Setup(p => p.GetCompletedAnesthesiaRecordIds(It.IsAny<IEnumerable<int>>()))
                .Returns(new HashSet<int>());

            var result = await _service.GetPatientsWithSurgeriesAsync(1, null, string.Empty, null);
            var paged = Assert.IsType<PagedResponse<PatientSurgeryResponse>>(result.Data);
            var patient = Assert.Single(paged.Data);
            Assert.Null(patient.FirstAnesthesiologist);
        }

        [Fact]
        public async Task GetPatientsWithSurgeriesAsync_Should_Report_InProgress_When_FirstAnesthesiologist_Assigned()
        {
           
            var patientDto = new PatientDetailDto
            {
                PatientId = "P1",
                FullName = "João",
                SurgeryId = 1,
                Status = "agendado",               
                SurgeryDate = new DateTime(2026, 1, 1),
            };
            _hospitalApiRepoMock
                .Setup(h => h.GetPatientsFromHospitalAsync(It.IsAny<DateTime?>(), string.Empty, It.IsAny<SurgeryStatusEnum?>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new PagedResponse<PatientDetailDto> { Data = new List<PatientDetailDto> { patientDto }, TotalItems = 1 });

            var record = AnesthesiaRecord.Create(new Domain.Commands.AnesthesiaRecord.AnesthesiaRecordCommand { PatientId = "P1", SurgeryId = 1 }, DateTime.MinValue);
            record.SetStatus(SurgeryStatusEnum.Preparing);
            var anesthesiologist = User.Create(1, "Dr. João", "joao@teste.com", "jsilva", "123", MedicalSpecialtyEnum.Anesthesiology, SectorEnum.SurgicalCenter);
            typeof(AnesthesiaRecord).GetProperty("FirstAnesthesiologist")!.SetValue(record, anesthesiologist);
            _anesthesiaRepoMock.Setup(a => a.GetByIdsAsync(It.IsAny<string[]>())).ReturnsAsync(new List<AnesthesiaRecord> { record });
            _preAnesthesiaRepoMock
                .Setup(p => p.GetCompletedAnesthesiaRecordIds(It.IsAny<IEnumerable<int>>()))
                .Returns(new HashSet<int>());

            var result = await _service.GetPatientsWithSurgeriesAsync(1, null, string.Empty, null);
            var paged = Assert.IsType<PagedResponse<PatientSurgeryResponse>>(result.Data);
            var patient = Assert.Single(paged.Data);
            Assert.Equal(SurgeryStatusEnum.InProgress, patient.Status);
        }

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
            var record = AnesthesiaRecord.Create(new Domain.Commands.AnesthesiaRecord.AnesthesiaRecordCommand { PatientId = "P1" }, DateTime.MinValue);
            var anesthesiologist = User.Create(1, "Dr. João", "joao@teste.com", "jsilva", "123", MedicalSpecialtyEnum.Anesthesiology, SectorEnum.SurgicalCenter);
            typeof(AnesthesiaRecord).GetProperty("FirstAnesthesiologist")!.SetValue(record, anesthesiologist);
            _anesthesiaRepoMock.Setup(a => a.GetByIdAsync(1)).ReturnsAsync(record);

            var result = await _service.GetPatientAnesthesiaRecordByIdAsync("P1", 1);
            Assert.NotNull(result);
            Assert.True(result.Valid);
        }

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

        private static AnesthesiaRecord CreateRecord(int surgeryId, string patientId, SurgeryStatusEnum status, DateTime surgeryDate)
        {
            var record = AnesthesiaRecord.Create(new Domain.Commands.AnesthesiaRecord.AnesthesiaRecordCommand
            {
                SurgeryId = surgeryId,
                PatientId = patientId
            }, surgeryDate);

            record.SetStatus(status);

            return record;
        }

        [Fact]
        public async Task GetMyPatientsAsync_SemFiltroDeData_UsaPaginacaoPriorizadaLocal()
        {
            var inProgressRecord = CreateRecord(2, "P2", SurgeryStatusEnum.InProgress, new DateTime(2026, 1, 1));
            var scheduledRecord = CreateRecord(3, "P3", SurgeryStatusEnum.Scheduled, new DateTime(2026, 8, 20));

            
            _anesthesiaRepoMock
                .Setup(a => a.GetPagedByDoctorPrioritizedAsync(1, null, 1, 10))
                .ReturnsAsync(((IEnumerable<AnesthesiaRecord>)new[] { inProgressRecord, scheduledRecord }, 2));

            _anesthesiaRepoMock
                .Setup(a => a.CanAssumePatientsAsync(1))
                .ReturnsAsync(true);

            _preAnesthesiaRepoMock
                .Setup(p => p.GetCompletedAnesthesiaRecordIds(It.Is<IEnumerable<int>>(ids => ids.SequenceEqual(new[] { 2, 3 }))))
                .Returns(new HashSet<int> { 2, 3 });

           
            _hospitalApiRepoMock
                .Setup(h => h.GetMyPatientsFromHospitalAsync(
                    It.Is<IEnumerable<int>>(ids => ids.SequenceEqual(new[] { 2, 3 })),
                    null, 1, 2))
                .ReturnsAsync(new PagedResponse<PatientDetailDto>
                {
                    Data = new List<PatientDetailDto>
                    {
                        new() { SurgeryId = 3, PatientId = "P3", FullName = "Ana", Status = "agendada", ExpectedAt = new DateTime(2026, 8, 20) },
                        new() { SurgeryId = 2, PatientId = "P2", FullName = "Zeca", Status = "em_progresso", ExpectedAt = new DateTime(2026, 1, 1) }
                    },
                    Page = 1,
                    PageSize = 2,
                    TotalItems = 2
                });

            var result = await _service.GetMyPatientsAsync(1, null, string.Empty, null, 1, 10);

            var paged = Assert.IsType<PagedResponse<PatientSurgeryResponse>>(result.Data);
            Assert.Equal(2, paged.TotalItems);
            Assert.Equal(2, paged.Data.Count());
            Assert.Equal(2, paged.Data.First().SurgeryId);
            Assert.Equal(3, paged.Data.Last().SurgeryId);
            Assert.All(paged.Data, p => Assert.True(p.IsPreAnesthesiaRecordDone));

            _anesthesiaRepoMock.Verify(a => a.GetByDoctorAndDateAsync(It.IsAny<int>(), It.IsAny<DateTime?>()), Times.Never);
        }

        [Fact]
        public async Task GetMyPatientsAsync_SemFiltroDeData_SemRegistros_RetornaVazio()
        {
            _anesthesiaRepoMock
                .Setup(a => a.GetPagedByDoctorPrioritizedAsync(1, null, 1, 10))
                .ReturnsAsync(((IEnumerable<AnesthesiaRecord>)new List<AnesthesiaRecord>(), 0));

            var result = await _service.GetMyPatientsAsync(1, null, string.Empty, null, 1, 10);

            var paged = Assert.IsType<PagedResponse<PatientSurgeryResponse>>(result.Data);
            Assert.Empty(paged.Data);
            Assert.Equal(0, paged.TotalItems);
        }

        [Fact]
        public async Task GetMyPatientsAsync_ComFiltroDeData_SemTermo_TambemUsaPaginacaoPriorizada()
        {
            var date = new DateTime(2026, 8, 29, 0, 0, 0, DateTimeKind.Utc);
            var record = CreateRecord(5, "P5", SurgeryStatusEnum.Scheduled, date);

           
            _anesthesiaRepoMock
                .Setup(a => a.GetPagedByDoctorPrioritizedAsync(1, date, 1, 10))
                .ReturnsAsync(((IEnumerable<AnesthesiaRecord>)new List<AnesthesiaRecord> { record }, 1));

            _anesthesiaRepoMock
                .Setup(a => a.CanAssumePatientsAsync(1))
                .ReturnsAsync(false);

            _preAnesthesiaRepoMock
                .Setup(p => p.GetCompletedAnesthesiaRecordIds(It.IsAny<IEnumerable<int>>()))
                .Returns(new HashSet<int>());

            _hospitalApiRepoMock
                .Setup(h => h.GetMyPatientsFromHospitalAsync(
                    It.Is<IEnumerable<int>>(ids => ids.SequenceEqual(new[] { 5 })),
                    null, 1, 1))
                .ReturnsAsync(new PagedResponse<PatientDetailDto>
                {
                    Data = new List<PatientDetailDto>
                    {
                        new() { SurgeryId = 5, PatientId = "P5", FullName = "Carlos", Status = "agendada", ExpectedAt = date }
                    },
                    Page = 1,
                    PageSize = 1,
                    TotalItems = 1
                });

            var result = await _service.GetMyPatientsAsync(1, date, string.Empty, null, 1, 10);

            var paged = Assert.IsType<PagedResponse<PatientSurgeryResponse>>(result.Data);
            Assert.Single(paged.Data);
            Assert.Equal(5, paged.Data.Single().SurgeryId);

            _anesthesiaRepoMock.Verify(a => a.GetByDoctorAndDateAsync(It.IsAny<int>(), It.IsAny<DateTime?>()), Times.Never);
        }

        [Fact]
        public async Task GetMyPatientsAsync_ComTermoDeBusca_UsaFluxoDelegadoAoAghu()
        {
            var date = new DateTime(2026, 8, 29, 0, 0, 0, DateTimeKind.Utc);
            var record = CreateRecord(5, "P5", SurgeryStatusEnum.Scheduled, date);

          
            _anesthesiaRepoMock
                .Setup(a => a.GetByDoctorAndDateAsync(1, date))
                .ReturnsAsync(new List<AnesthesiaRecord> { record });

            _anesthesiaRepoMock
                .Setup(a => a.CanAssumePatientsAsync(1))
                .ReturnsAsync(false);

            _preAnesthesiaRepoMock
                .Setup(p => p.GetCompletedAnesthesiaRecordIds(It.IsAny<IEnumerable<int>>()))
                .Returns(new HashSet<int> { 5 });

            _hospitalApiRepoMock
                .Setup(h => h.GetMyPatientsFromHospitalAsync(
                    It.Is<IEnumerable<int>>(ids => ids.SequenceEqual(new[] { 5 })),
                    "Carlos", 1, 10))
                .ReturnsAsync(new PagedResponse<PatientDetailDto>
                {
                    Data = new List<PatientDetailDto>
                    {
                        new() { SurgeryId = 5, PatientId = "P5", FullName = "Carlos", Status = "agendada", ExpectedAt = date }
                    },
                    Page = 1,
                    PageSize = 10,
                    TotalItems = 1
                });

            var result = await _service.GetMyPatientsAsync(1, date, "Carlos", null, 1, 10);

            var paged = Assert.IsType<PagedResponse<PatientSurgeryResponse>>(result.Data);
            Assert.Single(paged.Data);
            Assert.Equal(5, paged.Data.Single().SurgeryId);
            Assert.True(paged.Data.Single().IsPreAnesthesiaRecordDone);

            _anesthesiaRepoMock.Verify(a => a.GetPagedByDoctorPrioritizedAsync(It.IsAny<int>(), It.IsAny<DateTime?>(), It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        }
    }
}