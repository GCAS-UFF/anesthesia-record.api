using Moq;
using UFF.FichaAnestesica.Domain.Dto;
using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Enums;
using UFF.FichaAnestesica.Domain.Repositories;
using UFF.FichaAnestesica.Domain.Repositories.ReadOnly;
using UFF.FichaAnestesica.Domain.Response;
using UFF.FichaAnestesica.Service.Services;

public class SurgeryServiceTests
{
    private readonly Mock<IUserRepository> _userRepoMock;
    private readonly Mock<IPatientReadOnlyRepository> _hospitalRepoMock;
    private readonly SurgeryService _service;

    private const SurgeryStatusEnum STATUS_PENDING = SurgeryStatusEnum.Scheduled;
    private const SurgeryStatusEnum STATUS_DONE = SurgeryStatusEnum.Completed;

    public SurgeryServiceTests()
    {
        _userRepoMock = new Mock<IUserRepository>();
        _hospitalRepoMock = new Mock<IPatientReadOnlyRepository>();
        _service = new SurgeryService(_userRepoMock.Object, _hospitalRepoMock.Object);
    }

    // ========== GetPatientsWithSurgeriesAsync ==========

    [Fact]
    public async Task GetPatientsWithSurgeriesAsync_ReturnsEmptyResponse_WhenHospitalDataIsNull()
    {
        _hospitalRepoMock
            .Setup(x => x.GetPatientsFromHospitalAsync(It.IsAny<DateTime?>(), It.IsAny<SurgeryStatusEnum?>(), It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(new PagedResponse<PatientDto> { Data = null!, TotalItems = 0 });

        var result = await _service.GetPatientsWithSurgeriesAsync(null, null);

        Assert.NotNull(result);
        Assert.Empty(result.Data);
        Assert.Equal(0, result.TotalItems);
    }

    [Fact]
    public async Task GetPatientsWithSurgeriesAsync_ReturnsEmptyResponse_WhenHospitalDataIsEmptyList()
    {
        _hospitalRepoMock
            .Setup(x => x.GetPatientsFromHospitalAsync(It.IsAny<DateTime?>(), It.IsAny<SurgeryStatusEnum?>(), It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(new PagedResponse<PatientDto> { Data = new List<PatientDto>(), TotalItems = 0 });

        var result = await _service.GetPatientsWithSurgeriesAsync(null, null);

        Assert.Empty(result.Data);
        Assert.Equal(0, result.TotalItems);
    }

    [Fact]
    public async Task GetPatientsWithSurgeriesAsync_ConvertsDateToUtc_BeforeCallingRepository()
    {
        var localDate = new DateTime(2025, 6, 10, 14, 30, 0, DateTimeKind.Local);
        DateTime? capturedDate = null;

        _hospitalRepoMock
            .Setup(x => x.GetPatientsFromHospitalAsync(It.IsAny<DateTime?>(), It.IsAny<SurgeryStatusEnum?>(), It.IsAny<int>(), It.IsAny<int>()))
            .Callback<DateTime?, SurgeryStatusEnum?, int, int>((d, _, _, _) => capturedDate = d)
            .ReturnsAsync(new PagedResponse<PatientDto> { Data = new List<PatientDto>(), TotalItems = 0 });

        await _service.GetPatientsWithSurgeriesAsync(localDate, null);

        Assert.NotNull(capturedDate);
        Assert.Equal(DateTimeKind.Utc, capturedDate!.Value.Kind);
    }

    [Fact]
    public async Task GetPatientsWithSurgeriesAsync_MapsPatientDtoCorrectly_WithNestedObjects()
    {
        var patientDto = new PatientDto
        {
            PatientId = "1",
            FullName = "João",
            MedicalRecordNumber = "MR001",
            BirthDate = new DateTime(2000, 1, 1),
            Gender = "M",
            WeightKg = 70,
            HeightCm = 180,
            CurrentLocation = new CurrentLocationDto
            {
                Unit = new UnitDto { Code = "UTI", Description = "Unidade de Terapia Intensiva" },
                Bed = "101",
                Floor = "1",
                Room = "A"
            },
            Surgeries = new List<SurgeryDto>
            {
                new SurgeryDto
                {
                    SurgeryId = "1",
                    SurgeryDate = new DateTime(2025, 6, 10, 8, 0, 0),
                    Status = "agendada",
                    Specialty = new SpecialtyDto { Code = "CARD", Description = "Cardiologia" },
                    Location = new SurgeryLocationDto
                    {
                        SurgicalCenter = new SurgicalCenterDto { Code = "SC1", Description = "Centro Cirúrgico 1" },
                        Room = "Sala 1"
                    },
                    Procedures = new List<ProcedureDto>
                    {
                        new ProcedureDto
                        {
                            ExternalId = "P1",
                            Description = "Cateterismo",
                            Cid = "I25.1",
                            IsPrimary = true
                        }
                    }
                }
            }
        };

        _hospitalRepoMock
            .Setup(x => x.GetPatientsFromHospitalAsync(It.IsAny<DateTime?>(), It.IsAny<SurgeryStatusEnum?>(), It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(new PagedResponse<PatientDto>
            {
                Data = new List<PatientDto> { patientDto },
                TotalItems = 1,
                Page = 1,
                PageSize = 10
            });

        var result = await _service.GetPatientsWithSurgeriesAsync(null, null);

        Assert.Equal(1, result.TotalItems);
        var response = result.Data.First();

        Assert.Equal("João", response.FullName);
        Assert.Equal("MR001", response.MedicalRecordNumber);
        Assert.Equal("M", response.Gender);
        Assert.Equal(70, response.WeightKg);
        Assert.Equal(180, response.HeightCm);
        Assert.NotNull(response.CurrentLocation);
        Assert.Equal("UTI", response.CurrentLocation!.Unit!.Code);
        Assert.Equal("101", response.CurrentLocation.Bed);
        Assert.Equal("1", response.CurrentLocation.Floor);
        Assert.Equal("A", response.CurrentLocation.Room);

        Assert.Single(response.Surgeries);
        var surgery = response.Surgeries.First();
        Assert.Equal("1", surgery.Id);
        Assert.Equal(STATUS_PENDING, surgery.Status);
        Assert.NotNull(surgery.Specialty);
        Assert.Equal("CARD", surgery.Specialty.Code);
        Assert.NotNull(surgery.Location);
        Assert.NotNull(surgery.Location.SurgicalCenter);
        Assert.Equal("SC1", surgery.Location.SurgicalCenter.Code);
        Assert.Equal("Sala 1", surgery.Location.Room);

        Assert.Single(surgery.Procedures);
        var procedure = surgery.Procedures.First();
        Assert.Equal("P1", procedure.Id);
        Assert.Equal("Cateterismo", procedure.Description);
        Assert.Equal("I25.1", procedure.Cid);
        Assert.True(procedure.IsPrimary);
    }

    [Fact]
    public async Task GetPatientsWithSurgeriesAsync_MergesMultipleSurgeriesForSamePatient()
    {
        var patientDto = new PatientDto
        {
            PatientId = "1",
            FullName = "Maria",
            MedicalRecordNumber = "MR002",
            BirthDate = new DateTime(1990, 3, 20),
            Gender = "F",
            WeightKg = 65,
            HeightCm = 165,
            Surgeries = new List<SurgeryDto>
            {
                new SurgeryDto
                {
                    SurgeryId = "1",
                    SurgeryDate = new DateTime(2025, 6, 10),
                    Status = "agendada",
                    Specialty = new SpecialtyDto { Code = "ORT", Description = "Ortopedia" },
                    Location = new SurgeryLocationDto
                    {
                        SurgicalCenter = new SurgicalCenterDto { Code = "SC2", Description = "Centro 2" },
                        Room = "Sala 3"
                    },
                    Procedures = new List<ProcedureDto>
                    {
                        new ProcedureDto { ExternalId = "P1", Description = "Proc1", IsPrimary = true },
                        new ProcedureDto { ExternalId = "P2", Description = "Proc2", IsPrimary = false }
                    }
                },
                new SurgeryDto
                {
                    SurgeryId = "2",
                    SurgeryDate = new DateTime(2025, 6, 11),
                    Status = "agendada",
                    Specialty = new SpecialtyDto { Code = "NEU", Description = "Neurologia" },
                    Location = new SurgeryLocationDto
                    {
                        SurgicalCenter = new SurgicalCenterDto { Code = "SC3", Description = "Centro 3" },
                        Room = "Sala 7"
                    },
                    Procedures = new List<ProcedureDto>
                    {
                        new ProcedureDto { ExternalId = "P3", Description = "Proc3", IsPrimary = true }
                    }
                }
            }
        };

        _hospitalRepoMock
            .Setup(x => x.GetPatientsFromHospitalAsync(It.IsAny<DateTime?>(), It.IsAny<SurgeryStatusEnum?>(), It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(new PagedResponse<PatientDto> { Data = new List<PatientDto> { patientDto }, TotalItems = 1 });

        var result = await _service.GetPatientsWithSurgeriesAsync(null, null);

        var response = result.Data.First();
        Assert.Equal(2, response.Surgeries.Count);
        var surgery1 = response.Surgeries.First(s => s.Id == "1");
        Assert.Equal(2, surgery1.Procedures.Count);
        Assert.Contains(surgery1.Procedures, p => p.Id == "P1");
        Assert.Contains(surgery1.Procedures, p => p.Id == "P2");
    }

    [Fact]
    public async Task GetPatientsWithSurgeriesAsync_PreservesOrderFromHospital()
    {
        var patient1 = new PatientDto { PatientId = "1", FullName = "Ana", Surgeries = new List<SurgeryDto>() };
        var patient2 = new PatientDto { PatientId = "2", FullName = "Beto", Surgeries = new List<SurgeryDto>() };
        var patient3 = new PatientDto { PatientId = "3", FullName = "Carlos", Surgeries = new List<SurgeryDto>() };

        _hospitalRepoMock
            .Setup(x => x.GetPatientsFromHospitalAsync(It.IsAny<DateTime?>(), It.IsAny<SurgeryStatusEnum?>(), It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(new PagedResponse<PatientDto> { Data = new List<PatientDto> { patient1, patient2, patient3 }, TotalItems = 3 });

        var result = await _service.GetPatientsWithSurgeriesAsync(null, null);
        var ordered = result.Data.ToList();

        Assert.Equal(3, ordered.Count);
        Assert.Equal("Ana", ordered[0].FullName);
        Assert.Equal("Beto", ordered[1].FullName);
        Assert.Equal("Carlos", ordered[2].FullName);
    }

    [Fact]
    public async Task GetPatientsWithSurgeriesAsync_UsesHospitalTotalItems_NotMappedCount()
    {
        var hospitalData = new PagedResponse<PatientDto>
        {
            Data = new List<PatientDto> { new PatientDto { PatientId = "1", FullName = "Fulano" } },
            TotalItems = 100
        };

        _hospitalRepoMock
            .Setup(x => x.GetPatientsFromHospitalAsync(It.IsAny<DateTime?>(), It.IsAny<SurgeryStatusEnum?>(), It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(hospitalData);

        var result = await _service.GetPatientsWithSurgeriesAsync(null, null);

        Assert.Equal(100, result.TotalItems);
        Assert.Single(result.Data);
    }

    [Fact]
    public async Task GetPatientsWithSurgeriesAsync_AgeCalculation_ReflectsBirthDateCorrectly()
    {
        var birthDate = new DateTime(2000, 5, 15);
        var patientDto = new PatientDto
        {
            PatientId = "1",
            FullName = "Jovem",
            BirthDate = birthDate,
            Gender = "F",
            WeightKg = 55,
            HeightCm = 165,
            Surgeries = new List<SurgeryDto>()
        };

        _hospitalRepoMock
            .Setup(x => x.GetPatientsFromHospitalAsync(It.IsAny<DateTime?>(), It.IsAny<SurgeryStatusEnum?>(), It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(new PagedResponse<PatientDto> { Data = new List<PatientDto> { patientDto }, TotalItems = 1 });

        var result = await _service.GetPatientsWithSurgeriesAsync(null, null);
        var response = result.Data.First();

        var expectedAge = DateTime.Today.Year - birthDate.Year;
        if (birthDate.Date > DateTime.Today.AddYears(-expectedAge)) expectedAge--;
        Assert.Equal(expectedAge, response.Age);
    }

    [Fact]
    public async Task GetPatientsWithSurgeriesAsync_GenderMapping_MaleAndFemale()
    {
        var male = new PatientDto { PatientId = "1", FullName = "Homem", Gender = "M", Surgeries = new List<SurgeryDto>() };
        var female = new PatientDto { PatientId = "2", FullName = "Mulher", Gender = "F", Surgeries = new List<SurgeryDto>() };

        _hospitalRepoMock
            .Setup(x => x.GetPatientsFromHospitalAsync(It.IsAny<DateTime?>(), It.IsAny<SurgeryStatusEnum?>(), It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(new PagedResponse<PatientDto> { Data = new List<PatientDto> { male, female }, TotalItems = 2 });

        var result = await _service.GetPatientsWithSurgeriesAsync(null, null);
        var patients = result.Data.ToList();

        Assert.Equal("M", patients.First(p => p.FullName == "Homem").Gender);
        Assert.Equal("F", patients.First(p => p.FullName == "Mulher").Gender);
    }

    [Fact]
    public async Task GetPatientsWithSurgeriesAsync_NullCurrentLocation_MapsToNullInResponse()
    {
        var patientDto = new PatientDto
        {
            PatientId = "1",
            FullName = "Sem Local",
            Gender = "M",
            WeightKg = 70,
            HeightCm = 170,
            CurrentLocation = null,
            Surgeries = new List<SurgeryDto>()
        };

        _hospitalRepoMock
            .Setup(x => x.GetPatientsFromHospitalAsync(It.IsAny<DateTime?>(), It.IsAny<SurgeryStatusEnum?>(), It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(new PagedResponse<PatientDto> { Data = new List<PatientDto> { patientDto }, TotalItems = 1 });

        var result = await _service.GetPatientsWithSurgeriesAsync(null, null);
        Assert.Null(result.Data.First().CurrentLocation);
    }

    [Fact]
    public async Task GetPatientsWithSurgeriesAsync_NullSpecialtyOrLocation_MapsToNullProperties()
    {
        var patientDto = new PatientDto
        {
            PatientId = "1",
            FullName = "Paciente",
            Gender = "M",
            WeightKg = 70,
            HeightCm = 170,
            Surgeries = new List<SurgeryDto>
            {
                new SurgeryDto
                {
                    SurgeryId = "1",
                    SurgeryDate = DateTime.UtcNow,
                    Status = "agendada",
                    Specialty = null,
                    Location = null,
                    Procedures = new List<ProcedureDto>()
                }
            }
        };

        _hospitalRepoMock
            .Setup(x => x.GetPatientsFromHospitalAsync(It.IsAny<DateTime?>(), It.IsAny<SurgeryStatusEnum?>(), It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(new PagedResponse<PatientDto> { Data = new List<PatientDto> { patientDto }, TotalItems = 1 });

        var result = await _service.GetPatientsWithSurgeriesAsync(null, null);
        var surgeryResponse = result.Data.First().Surgeries.First();

        // Se o mapper retornar objetos com campos nulos, checamos null-safe
        Assert.Null(surgeryResponse.Specialty?.Code);
        Assert.Null(surgeryResponse.Specialty?.Description);
        Assert.Null(surgeryResponse.Location?.SurgicalCenter?.Code);
        Assert.Null(surgeryResponse.Location?.SurgicalCenter?.Description);
        Assert.Null(surgeryResponse.Location?.Room);
    }

    [Fact]
    public async Task GetPatientsWithSurgeriesAsync_PaginationParameters_ArePassedCorrectly()
    {
        DateTime? capturedDate = null;
        SurgeryStatusEnum? capturedStatus = null;
        int capturedPage = 0, capturedSize = 0;

        _hospitalRepoMock
            .Setup(x => x.GetPatientsFromHospitalAsync(It.IsAny<DateTime?>(), It.IsAny<SurgeryStatusEnum?>(), It.IsAny<int>(), It.IsAny<int>()))
            .Callback<DateTime?, SurgeryStatusEnum?, int, int>((d, s, p, sz) =>
            {
                capturedDate = d;
                capturedStatus = s;
                capturedPage = p;
                capturedSize = sz;
            })
            .ReturnsAsync(new PagedResponse<PatientDto> { Data = new List<PatientDto>(), TotalItems = 0 });

        var testDate = new DateTime(2025, 6, 10, 0, 0, 0, DateTimeKind.Utc);
        await _service.GetPatientsWithSurgeriesAsync(testDate, STATUS_PENDING, 2, 25);

        Assert.Equal(testDate, capturedDate);
        Assert.Equal(STATUS_PENDING, capturedStatus);
        Assert.Equal(2, capturedPage);
        Assert.Equal(25, capturedSize);
    }

    // ========== GetPatientByIdAsync ==========

    [Fact]
    public async Task GetPatientByIdAsync_ReturnsCompletePatient_WhenFound()
    {
        var patientDto = new PatientDto
        {
            PatientId = "1",
            FullName = "Ricardo",
            MedicalRecordNumber = "MR001",
            BirthDate = new DateTime(1985, 8, 20),
            Gender = "M",
            WeightKg = 85,
            HeightCm = 178,
            CurrentLocation = new CurrentLocationDto
            {
                Unit = new UnitDto { Code = "U1", Description = "Unidade A" },
                Bed = "5",
                Floor = "2",
                Room = "B"
            },
            Surgeries = new List<SurgeryDto>
            {
                new SurgeryDto
                {
                    SurgeryId = "100",
                    SurgeryDate = new DateTime(2025, 5, 1),
                    Status = "finalizada",   // <-- CORREÇÃO AQUI
                    Specialty = new SpecialtyDto { Code = "CARD", Description = "Cardiologia" },
                    Location = new SurgeryLocationDto
                    {
                        SurgicalCenter = new SurgicalCenterDto { Code = "SC1", Description = "Centro 1" },
                        Room = "OR1"
                    },
                    Procedures = new List<ProcedureDto>
                    {
                        new ProcedureDto
                        {
                            ExternalId = "P100",
                            Description = "Angioplastia",
                            Cid = "I25.2",
                            IsPrimary = true
                        }
                    }
                }
            }
        };

        _hospitalRepoMock
            .Setup(x => x.GetPatientFromHospitalByIdAsync("1"))
            .ReturnsAsync(patientDto);

        var result = await _service.GetPatientByIdAsync("1");

        Assert.NotNull(result);
        Assert.Equal("Ricardo", result.FullName);
        Assert.Equal("M", result.Gender);
        Assert.Equal(85, result.WeightKg);
        Assert.Equal(178, result.HeightCm);
        Assert.NotNull(result.CurrentLocation);
        Assert.Equal("U1", result.CurrentLocation!.Unit!.Code);
        Assert.Equal("5", result.CurrentLocation.Bed);

        var surgery = Assert.Single(result.Surgeries);
        Assert.Equal(STATUS_DONE, surgery.Status);
        Assert.Equal("CARD", surgery.Specialty!.Code);
        Assert.Equal("SC1", surgery.Location!.SurgicalCenter!.Code);
        Assert.Equal("OR1", surgery.Location.Room);

        var proc = Assert.Single(surgery.Procedures);
        Assert.Equal("P100", proc.Id);
        Assert.Equal("Angioplastia", proc.Description);
        Assert.True(proc.IsPrimary);
    }

    [Fact]
    public async Task GetPatientByIdAsync_ReturnsNull_WhenNotFound()
    {
        _hospitalRepoMock
            .Setup(x => x.GetPatientFromHospitalByIdAsync("99"))
            .ReturnsAsync((PatientDto?)null);

        var result = await _service.GetPatientByIdAsync("99");
        Assert.Null(result);
    }

    [Fact]
    public async Task GetPatientByIdAsync_AgeCalculation_WorksCorrectly()
    {
        var birthDate = new DateTime(1990, 12, 31);
        var patientDto = new PatientDto
        {
            PatientId = "1",
            FullName = "Idade",
            BirthDate = birthDate,
            Gender = "F",
            WeightKg = 60,
            HeightCm = 160,
            Surgeries = new List<SurgeryDto>()
        };

        _hospitalRepoMock
            .Setup(x => x.GetPatientFromHospitalByIdAsync("1"))
            .ReturnsAsync(patientDto);

        var result = await _service.GetPatientByIdAsync("1");
        var expectedAge = DateTime.Today.Year - birthDate.Year;
        if (birthDate.Date > DateTime.Today.AddYears(-expectedAge)) expectedAge--;
        Assert.Equal(expectedAge, result!.Age);
    }

    // ========== AssumePatientAsync ==========

    [Fact]
    public async Task AssumePatientAsync_AssignsResponsibleAnesthesiologist_WhenPatientExists()
    {
        var patientDto = new PatientDto
        {
            PatientId = "P1",
            FullName = "Paciente Teste",
            Surgeries = new List<SurgeryDto>()
        };

        _hospitalRepoMock
            .Setup(x => x.GetPatientsFromHospitalAsync(null, null, 1, int.MaxValue))
            .ReturnsAsync(new PagedResponse<PatientDto> { Data = new List<PatientDto> { patientDto }, TotalItems = 1 });

        // Cria instância real de User e define propriedades com setters protegidos
        var anesthesiologist = (User)Activator.CreateInstance(typeof(User), nonPublic: true)!;
        
        typeof(User).GetProperty("ExternalId")!
            .SetMethod!.Invoke(anesthesiologist, new object[] { "U1" });
        typeof(User).GetProperty("Name")!
            .SetMethod!.Invoke(anesthesiologist, new object[] { "Dr. João" });
        typeof(User).GetProperty("Registration")!
            .SetMethod!.Invoke(anesthesiologist, new object[] { "CRM123" });

        _userRepoMock.Setup(x => x.GetUserByIdAsync(10)).ReturnsAsync(anesthesiologist);

        var result = await _service.AssumePatientAsync("P1", 10);

        Assert.NotNull(result.ResponsibleAnesthesiologist);
        Assert.Equal("U1", result.ResponsibleAnesthesiologist.Id);
        Assert.Equal("Dr. João", result.ResponsibleAnesthesiologist.FullName);
        Assert.Equal("CRM123", result.ResponsibleAnesthesiologist.Registration);
    }

    [Fact]
    public async Task AssumePatientAsync_ThrowsException_WhenPatientNotFound()
    {
        _hospitalRepoMock
            .Setup(x => x.GetPatientsFromHospitalAsync(null, null, 1, int.MaxValue))
            .ReturnsAsync(new PagedResponse<PatientDto> { Data = new List<PatientDto>(), TotalItems = 0 });

        await Assert.ThrowsAsync<Exception>(() => _service.AssumePatientAsync("NONEXISTENT", 10));
    }

    [Fact]
    public async Task AssumePatientAsync_ThrowsException_WhenAnesthesiologistNotFound()
    {
        var patientDto = new PatientDto
        {
            PatientId = "P1",
            FullName = "Paciente",
            Surgeries = new List<SurgeryDto>()
        };

        _hospitalRepoMock
            .Setup(x => x.GetPatientsFromHospitalAsync(null, null, 1, int.MaxValue))
            .ReturnsAsync(new PagedResponse<PatientDto> { Data = new List<PatientDto> { patientDto }, TotalItems = 1 });

        _userRepoMock.Setup(x => x.GetUserByIdAsync(It.IsAny<int>())).ReturnsAsync((User?)null);

        await Assert.ThrowsAsync<Exception>(() => _service.AssumePatientAsync("P1", 999));
    }
}

