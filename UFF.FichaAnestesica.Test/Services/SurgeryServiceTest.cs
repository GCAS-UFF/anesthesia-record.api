using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using SurgeryService = UFF.FichaAnestesica.Service.Services.SurgeryService;
using UFF.FichaAnestesica.Domain.Dto;
using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Enums;
using UFF.FichaAnestesica.Domain.Repositories;
using UFF.FichaAnestesica.Domain.Repositories.ReadOnly;
using UFF.FichaAnestesica.Domain.Response;

public class SurgeryServiceTests
{
    private readonly Mock<IHospitalReadRepository> _hospitalRepoMock;
    private readonly Mock<ISurgeryRepository> _surgeryRepoMock;
    private readonly SurgeryService _service;

    private const SurgeryStatus STATUS_PENDING = SurgeryStatus.Scheduled;
    private const SurgeryStatus STATUS_DONE = SurgeryStatus.Completed;

    public SurgeryServiceTests()
    {
        _hospitalRepoMock = new Mock<IHospitalReadRepository>();
        _surgeryRepoMock = new Mock<ISurgeryRepository>();
        _service = new SurgeryService(_hospitalRepoMock.Object, _surgeryRepoMock.Object);
    }

    #region GetPatientsWithSurgeriesAsync

    [Fact]
    public async Task GetPatientsWithSurgeriesAsync_ReturnsEmptyResponse_WhenHospitalDataIsNull()
    {
        _hospitalRepoMock
            .Setup(x => x.GetSurgeriesFromHospitalAsync(It.IsAny<DateTime?>(), It.IsAny<SurgeryStatus?>(), It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(new PagedResponse<PatientViewDto> { Data = null!, TotalItems = 0 });

        var result = await _service.GetPatientsWithSurgeriesAsync(null, null);

        Assert.NotNull(result);
        Assert.Empty(result.Data);
        Assert.Equal(0, result.TotalItems);
        _surgeryRepoMock.Verify(x => x.AddOrUpdatePatientsAsync(It.IsAny<List<Patient>>()), Times.Never);
    }

    [Fact]
    public async Task GetPatientsWithSurgeriesAsync_ReturnsEmptyResponse_WhenHospitalDataIsEmptyList()
    {
        _hospitalRepoMock
            .Setup(x => x.GetSurgeriesFromHospitalAsync(It.IsAny<DateTime?>(), It.IsAny<SurgeryStatus?>(), It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(new PagedResponse<PatientViewDto> { Data = new List<PatientViewDto>(), TotalItems = 0 });

        var result = await _service.GetPatientsWithSurgeriesAsync(null, null);

        Assert.Empty(result.Data);
        Assert.Equal(0, result.TotalItems);
        _surgeryRepoMock.Verify(x => x.AddOrUpdatePatientsAsync(It.IsAny<List<Patient>>()), Times.Never);
    }

    [Fact]
    public async Task GetPatientsWithSurgeriesAsync_ConvertsDateToUtc_BeforeCallingRepositories()
    {
        var localDate = new DateTime(2025, 6, 10, 14, 30, 0, DateTimeKind.Local);
        DateTime? capturedHospitalDate = null;

        _hospitalRepoMock
            .Setup(x => x.GetSurgeriesFromHospitalAsync(It.IsAny<DateTime?>(), It.IsAny<SurgeryStatus?>(), It.IsAny<int>(), It.IsAny<int>()))
            .Callback<DateTime?, SurgeryStatus?, int, int>((d, _, _, _) => capturedHospitalDate = d)
            .ReturnsAsync(new PagedResponse<PatientViewDto> { Data = new List<PatientViewDto>(), TotalItems = 0 });

        await _service.GetPatientsWithSurgeriesAsync(localDate, null);

        Assert.NotNull(capturedHospitalDate);
        Assert.Equal(DateTimeKind.Utc, capturedHospitalDate!.Value.Kind);
    }

    [Fact]
    public async Task GetPatientsWithSurgeriesAsync_CallsAddOrUpdatePatients_WithMappedPatients()
    {
        // Arrange
        var hospitalData = new PagedResponse<PatientViewDto>
        {
            Data = new List<PatientViewDto>
            {
                new PatientViewDto
                {
                    Id = "1",
                    FullName = "João",
                    MedicalRecordNumber = "MR001",
                    BirthDate = new DateTime(2000, 1, 1),
                    Gender = "M",
                    WeightKg = 70,
                    HeightCm = 180,
                    Bed = "101",
                    Floor = "1",
                    Room = "A",
                    UnitCode = "UTI",
                    UnitDescription = "Unidade de Terapia Intensiva",
                    SurgeryId = "1",
                    SurgeryDate = new DateTime(2025, 6, 10, 8, 0, 0),
                    SurgeryStatus = "agendada",
                    SpecialtyCode = "CARD",
                    SpecialtyDescription = "Cardiologia",
                    SurgicalCenterCode = "SC1",
                    SurgicalCenterDescription = "Centro Cirúrgico 1",
                    SurgeryRoom = "Sala 1",
                    ProcedureId = "P1",
                    ProcedureDescription = "Cateterismo",
                    ProcedureCid = "I25.1",
                    IsPrimaryProcedure = true
                }
            },
            TotalItems = 1
        };

        List<Patient>? capturedPatients = null;

        _hospitalRepoMock
            .Setup(x => x.GetSurgeriesFromHospitalAsync(It.IsAny<DateTime?>(), It.IsAny<SurgeryStatus?>(), It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(hospitalData);

        _surgeryRepoMock
            .Setup(x => x.AddOrUpdatePatientsAsync(It.IsAny<IList<Patient>>()))
            .Callback<IList<Patient>>(list => capturedPatients = list.ToList())
            .Returns(Task.CompletedTask);

        _surgeryRepoMock
            .Setup(x => x.GetPatientsWithSurgeriesAsync(It.IsAny<DateTime?>(), It.IsAny<SurgeryStatus?>(), It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(new List<Patient>());

        // Act
        await _service.GetPatientsWithSurgeriesAsync(null, null);

        // Assert
        Assert.NotNull(capturedPatients);
        var patient = Assert.Single(capturedPatients!);

        // Dados do paciente
        Assert.Equal("João", patient.FullName);
        Assert.Equal("MR001", patient.MedicalRecordNumber);
        Assert.Equal(70, patient.WeightKg);
        Assert.Equal(180, patient.HeightCm);
        Assert.NotNull(patient.CurrentLocation);
        Assert.Equal("UTI", patient.CurrentLocation!.Unit!.Code);
        Assert.Equal("101", patient.CurrentLocation.Bed);
        Assert.Equal("1", patient.CurrentLocation.Floor);
        Assert.Equal("A", patient.CurrentLocation.Room);

        // Cirurgia
        var surgery = Assert.Single(patient.Surgeries);
        Assert.Equal("1", surgery.SurgeryId);      
        Assert.Equal(SurgeryStatus.Scheduled, surgery.Status);
        Assert.NotNull(surgery.Specialty);
        Assert.Equal("CARD", surgery.Specialty!.Code);
        Assert.NotNull(surgery.Location);
        Assert.NotNull(surgery.Location!.SurgicalCenter);
        Assert.Equal("SC1", surgery.Location.SurgicalCenter!.Code);
        Assert.Equal("Sala 1", surgery.Location.Room);

        // Procedimento
        var procedure = Assert.Single(surgery.Procedures);
        Assert.Equal("P1", procedure.ExternalId);
        Assert.Equal("Cateterismo", procedure.Description);
        Assert.Equal("I25.1", procedure.Cid);
        Assert.True(procedure.IsPrimary);
    }

    [Fact]
    public async Task GetPatientsWithSurgeriesAsync_MergesMultipleRowsForSamePatient()
    {
        var hospitalData = new List<PatientViewDto>
        {
            new PatientViewDto { Id = "1", FullName = "Maria", SurgeryId = "1", SurgeryDate = new DateTime(2025, 6, 10), ProcedureId = "P1", ProcedureDescription = "Proc1", IsPrimaryProcedure = true },
            new PatientViewDto { Id = "1", FullName = "Maria", SurgeryId = "1", SurgeryDate = new DateTime(2025, 6, 10), ProcedureId = "P2", ProcedureDescription = "Proc2", IsPrimaryProcedure = false },
            new PatientViewDto { Id = "1", FullName = "Maria", SurgeryId = "2", SurgeryDate = new DateTime(2025, 6, 11), ProcedureId = "P3", ProcedureDescription = "Proc3" }
        };

        _hospitalRepoMock.Setup(x => x.GetSurgeriesFromHospitalAsync(It.IsAny<DateTime?>(), It.IsAny<SurgeryStatus?>(), It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(new PagedResponse<PatientViewDto> { Data = hospitalData, TotalItems = 3 });

        List<Patient>? capturedPatients = null;
        _surgeryRepoMock.Setup(x => x.AddOrUpdatePatientsAsync(It.IsAny<IList<Patient>>()))
            .Callback<IList<Patient>>(list => capturedPatients = list.ToList())
            .Returns(Task.CompletedTask);
        _surgeryRepoMock.Setup(x => x.GetPatientsWithSurgeriesAsync(It.IsAny<DateTime?>(), It.IsAny<SurgeryStatus?>(), It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(() => capturedPatients ?? new List<Patient>());

        await _service.GetPatientsWithSurgeriesAsync(null, null);

        Assert.NotNull(capturedPatients);
        var patient = Assert.Single(capturedPatients!);

        var surgery = Assert.Single(patient.Surgeries);
        Assert.Equal("1", surgery.SurgeryId);
        Assert.Equal(3, surgery.Procedures.Count);
    }

    [Fact]
    public async Task GetPatientsWithSurgeriesAsync_OrdersBySurgeryDateAscending_ByDefault()
    {
        var patient1 = Patient.Create("1", "MR1", "Ana", DateTime.Now.AddYears(-30), GenderEnum.Female, 60, 160, null);
        var patient2 = Patient.Create("2", "MR2", "Beto", DateTime.Now.AddYears(-40), GenderEnum.Male, 80, 175, null);
        var patient3 = Patient.Create("3", "MR3", "Carlos", DateTime.Now.AddYears(-50), GenderEnum.Male, 90, 180, null);

        var surgery1 = Surgery.Create("1", new DateTime(2025, 6, 12), STATUS_PENDING, "1", null, null);
        var surgery2 = Surgery.Create("2", new DateTime(2025, 6, 10), STATUS_PENDING, "2", null, null);
        var surgery3 = Surgery.Create("3", new DateTime(2025, 6, 11), STATUS_PENDING, "3", null, null);

        patient1.SyncSurgery(surgery1);
        patient2.SyncSurgery(surgery2);
        patient3.SyncSurgery(surgery3);

        var unorderedPatients = new List<Patient> { patient1, patient2, patient3 };

        _hospitalRepoMock.Setup(x => x.GetSurgeriesFromHospitalAsync(It.IsAny<DateTime?>(), It.IsAny<SurgeryStatus?>(), It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(new PagedResponse<PatientViewDto> { Data = new List<PatientViewDto> { new PatientViewDto { Id = "1" } }, TotalItems = 3 });
        _surgeryRepoMock.Setup(x => x.AddOrUpdatePatientsAsync(It.IsAny<List<Patient>>())).Returns(Task.CompletedTask);
        _surgeryRepoMock.Setup(x => x.GetPatientsWithSurgeriesAsync(It.IsAny<DateTime?>(), It.IsAny<SurgeryStatus?>(), It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(unorderedPatients);

        var result = await _service.GetPatientsWithSurgeriesAsync(null, null);

        var ordered = result.Data.ToList();
        Assert.Equal(3, ordered.Count);
        Assert.Equal("Beto", ordered[0].FullName);   
        Assert.Equal("Carlos", ordered[1].FullName); 
        Assert.Equal("Ana", ordered[2].FullName);
    }

    [Fact]
    public async Task GetPatientsWithSurgeriesAsync_OrderingPatientWithoutSurgeries_PlacesAtEnd()
    {
        var patientWith = Patient.Create("1", "MR1", "Com Cirurgia", DateTime.Now, GenderEnum.Female, 60, 160, null);
        patientWith.SyncSurgery(Surgery.Create("1", new DateTime(2025, 6, 10), STATUS_PENDING, "1", null, null));
        var patientWithout = Patient.Create("2", "MR2", "Sem Cirurgia", DateTime.Now, GenderEnum.Male, 80, 170, null);

        var patients = new List<Patient> { patientWithout, patientWith };

        _hospitalRepoMock.Setup(x => x.GetSurgeriesFromHospitalAsync(It.IsAny<DateTime?>(), It.IsAny<SurgeryStatus?>(), It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(new PagedResponse<PatientViewDto> { Data = new List<PatientViewDto> { new PatientViewDto { Id = "1" } }, TotalItems = 2 });
        _surgeryRepoMock.Setup(x => x.AddOrUpdatePatientsAsync(It.IsAny<List<Patient>>())).Returns(Task.CompletedTask);
        _surgeryRepoMock.Setup(x => x.GetPatientsWithSurgeriesAsync(It.IsAny<DateTime?>(), It.IsAny<SurgeryStatus?>(), It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(patients);

        var result = await _service.GetPatientsWithSurgeriesAsync(null, null);
        var ordered = result.Data.ToList();
        Assert.Equal(2, ordered.Count);
        Assert.Equal("Com Cirurgia", ordered[0].FullName);
        Assert.Equal("Sem Cirurgia", ordered[1].FullName);
    }

    [Fact]
    public async Task GetPatientsWithSurgeriesAsync_UsesHospitalTotalItems_NotSavedCount()
    {
        var hospitalData = new PagedResponse<PatientViewDto>
        {
            Data = new List<PatientViewDto> { new PatientViewDto { Id = "1" } },
            TotalItems = 100
        };

        _hospitalRepoMock.Setup(x => x.GetSurgeriesFromHospitalAsync(It.IsAny<DateTime?>(), It.IsAny<SurgeryStatus?>(), It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(hospitalData);

        var savedPatient = Patient.Create("1", "MR1", "Fulano", DateTime.Now, GenderEnum.Male, 70, 170, null);
        _surgeryRepoMock.Setup(x => x.AddOrUpdatePatientsAsync(It.IsAny<List<Patient>>())).Returns(Task.CompletedTask);
        _surgeryRepoMock.Setup(x => x.GetPatientsWithSurgeriesAsync(It.IsAny<DateTime?>(), It.IsAny<SurgeryStatus?>(), It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(new List<Patient> { savedPatient });

        var result = await _service.GetPatientsWithSurgeriesAsync(null, null);

        Assert.Equal(100, result.TotalItems);
        Assert.Single(result.Data);
    }

    [Fact]
    public async Task GetPatientsWithSurgeriesAsync_AgeCalculation_ReflectsBirthDateCorrectly()
    {
        var birthDate = new DateTime(2000, 5, 15);
        var patient = Patient.Create("1", "MR1", "Jovem", birthDate, GenderEnum.Female, 55, 165, null);

        _hospitalRepoMock.Setup(x => x.GetSurgeriesFromHospitalAsync(It.IsAny<DateTime?>(), It.IsAny<SurgeryStatus?>(), It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(new PagedResponse<PatientViewDto> { Data = new List<PatientViewDto> { new PatientViewDto { Id = "1" } }, TotalItems = 1 });
        _surgeryRepoMock.Setup(x => x.AddOrUpdatePatientsAsync(It.IsAny<List<Patient>>())).Returns(Task.CompletedTask);
        _surgeryRepoMock.Setup(x => x.GetPatientsWithSurgeriesAsync(It.IsAny<DateTime?>(), It.IsAny<SurgeryStatus?>(), It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(new List<Patient> { patient });

        var result = await _service.GetPatientsWithSurgeriesAsync(null, null);
        var response = result.Data.First();

        var expectedAge = DateTime.Today.Year - birthDate.Year;
        if (birthDate.Date > DateTime.Today.AddYears(-expectedAge)) expectedAge--;
        Assert.Equal(expectedAge, response.Age);
    }

    [Fact]
    public async Task GetPatientsWithSurgeriesAsync_GenderMapping_MaleAndFemale()
    {
        var male = Patient.Create("1", "M1", "Homem", DateTime.Now, GenderEnum.Male, 80, 180, null);
        var female = Patient.Create("2", "M2", "Mulher", DateTime.Now, GenderEnum.Female, 60, 160, null);

        _hospitalRepoMock.Setup(x => x.GetSurgeriesFromHospitalAsync(It.IsAny<DateTime?>(), It.IsAny<SurgeryStatus?>(), It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(new PagedResponse<PatientViewDto> { Data = new List<PatientViewDto> { new PatientViewDto { Id = "1" }, new PatientViewDto { Id = "2" } }, TotalItems = 2 });
        _surgeryRepoMock.Setup(x => x.AddOrUpdatePatientsAsync(It.IsAny<List<Patient>>())).Returns(Task.CompletedTask);
        _surgeryRepoMock.Setup(x => x.GetPatientsWithSurgeriesAsync(It.IsAny<DateTime?>(), It.IsAny<SurgeryStatus?>(), It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(new List<Patient> { male, female });

        var result = await _service.GetPatientsWithSurgeriesAsync(null, null);
        var patients = result.Data.ToList();
        Assert.Equal("M", patients.First(p => p.FullName == "Homem").Gender);
        Assert.Equal("F", patients.First(p => p.FullName == "Mulher").Gender);
    }

    [Fact]
    public async Task GetPatientsWithSurgeriesAsync_NullCurrentLocation_MapsToNullInResponse()
    {
        var patient = Patient.Create("1", "MR1", "Sem Local", DateTime.Now, GenderEnum.Male, 70, 170, null);

        _hospitalRepoMock.Setup(x => x.GetSurgeriesFromHospitalAsync(It.IsAny<DateTime?>(), It.IsAny<SurgeryStatus?>(), It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(new PagedResponse<PatientViewDto> { Data = new List<PatientViewDto> { new PatientViewDto { Id = "1" } }, TotalItems = 1 });
        _surgeryRepoMock.Setup(x => x.AddOrUpdatePatientsAsync(It.IsAny<List<Patient>>())).Returns(Task.CompletedTask);
        _surgeryRepoMock.Setup(x => x.GetPatientsWithSurgeriesAsync(It.IsAny<DateTime?>(), It.IsAny<SurgeryStatus?>(), It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(new List<Patient> { patient });

        var result = await _service.GetPatientsWithSurgeriesAsync(null, null);
        Assert.Null(result.Data.First().CurrentLocation);
    }

    [Fact]
    public async Task GetPatientsWithSurgeriesAsync_NullSpecialtyOrLocation_MapsToEmptyPropertiesNotFullNull()
    {
        var patient = Patient.Create("1", "MR1", "Paciente", DateTime.Now, GenderEnum.Male, 70, 170, null);
        var surgery = Surgery.Create("1", DateTime.Now, SurgeryStatus.Scheduled, "1", null, null);
        patient.SyncSurgery(surgery);

        _hospitalRepoMock.Setup(x => x.GetSurgeriesFromHospitalAsync(It.IsAny<DateTime?>(), It.IsAny<SurgeryStatus?>(), It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(new PagedResponse<PatientViewDto> { Data = new List<PatientViewDto> { new PatientViewDto { Id = "1" } }, TotalItems = 1 });
        _surgeryRepoMock.Setup(x => x.AddOrUpdatePatientsAsync(It.IsAny<IList<Patient>>())).Returns(Task.CompletedTask);
        _surgeryRepoMock.Setup(x => x.GetPatientsWithSurgeriesAsync(It.IsAny<DateTime?>(), It.IsAny<SurgeryStatus?>(), It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(new List<Patient> { patient });

        var result = await _service.GetPatientsWithSurgeriesAsync(null, null);
        var surgeryResponse = result.Data.First().Surgeries.First();

        Assert.NotNull(surgeryResponse.Specialty);
        Assert.Null(surgeryResponse.Specialty.Code);
        Assert.Null(surgeryResponse.Specialty.Description);

        Assert.NotNull(surgeryResponse.Location);
        Assert.NotNull(surgeryResponse.Location.SurgicalCenter);
        Assert.Null(surgeryResponse.Location.SurgicalCenter.Code);
        Assert.Null(surgeryResponse.Location.SurgicalCenter.Description);
        Assert.Null(surgeryResponse.Location.Room);
    }

    [Fact]
    public async Task GetPatientsWithSurgeriesAsync_PaginationParameters_ArePassedCorrectly()
    {
        DateTime? capturedDate = null;
        SurgeryStatus? capturedStatus = null;
        int hospPage = 0, hospSize = 0;

        _hospitalRepoMock.Setup(x => x.GetSurgeriesFromHospitalAsync(It.IsAny<DateTime?>(), It.IsAny<SurgeryStatus?>(), It.IsAny<int>(), It.IsAny<int>()))
            .Callback<DateTime?, SurgeryStatus?, int, int>((d, s, p, sz) => { capturedDate = d; capturedStatus = s; hospPage = p; hospSize = sz; })
            .ReturnsAsync(new PagedResponse<PatientViewDto> { Data = new List<PatientViewDto>(), TotalItems = 0 });

        _surgeryRepoMock.Setup(x => x.GetPatientsWithSurgeriesAsync(It.IsAny<DateTime?>(), It.IsAny<SurgeryStatus?>(), It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(new List<Patient>());

        var testDate = new DateTime(2025, 6, 10, 0, 0, 0, DateTimeKind.Utc);
        await _service.GetPatientsWithSurgeriesAsync(testDate, STATUS_PENDING, 2, 25);

        Assert.Equal(testDate, capturedDate);
        Assert.Equal(STATUS_PENDING, capturedStatus);
        Assert.Equal(2, hospPage);
        Assert.Equal(25, hospSize);
    }

    #endregion

    #region GetPatientByIdAsync

    [Fact]
    public async Task GetPatientByIdAsync_ReturnsCompletePatient()
    {
        var location = CurrentLocation.Create("5", "2", "B", Unit.Create("U1", "Unidade A"));
        var specialty = Specialty.Create("CARD", "Cardiologia");
        var surgicalCenter = SurgicalCenter.Create("SC1", "Centro 1");
        var surgeryLocation = SurgeryLocation.Create("OR1", surgicalCenter);
        var patient = Patient.Create("1", "MR001", "Ricardo", new DateTime(1985, 8, 20), GenderEnum.Male, 85, 178, location);

        var surgery = Surgery.Create("100", new DateTime(2025, 5, 1), STATUS_DONE, "1", specialty, surgeryLocation);
        var procedure = Procedure.Create("P100", "Angioplastia", "I25.2", true);
        surgery.AddProcedure(procedure);
        patient.SyncSurgery(surgery);

        _surgeryRepoMock.Setup(x => x.GetPatientByIdAsync(1)).ReturnsAsync(patient);

        var result = await _service.GetPatientByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal("Ricardo", result.FullName);
        Assert.Equal("MR001", result.MedicalRecordNumber);
        Assert.Equal("M", result.Gender);
        Assert.Equal(85, result.WeightKg);
        Assert.Equal(178, result.HeightCm);
        Assert.NotNull(result.CurrentLocation);
        Assert.Equal("5", result.CurrentLocation!.Bed);
        Assert.Equal("U1", result.CurrentLocation.Unit!.Code);

        var surgeryResp = Assert.Single(result.Surgeries);
        Assert.Equal(STATUS_DONE, surgeryResp.Status);
        Assert.Equal("CARD", surgeryResp.Specialty!.Code);
        Assert.Equal("SC1", surgeryResp.Location!.SurgicalCenter!.Code);
        Assert.Equal("OR1", surgeryResp.Location.Room);

        var procResp = Assert.Single(surgeryResp.Procedures);
        Assert.Equal("P100", procResp.Id);
        Assert.Equal("Angioplastia", procResp.Description);
        Assert.True(procResp.IsPrimary);
    }

    [Fact]
    public async Task GetPatientByIdAsync_ReturnsNull_WhenPatientNotFound()
    {
        _surgeryRepoMock.Setup(x => x.GetPatientByIdAsync(It.IsAny<int>()))!.ReturnsAsync((Patient?)null);
        var result = await _service.GetPatientByIdAsync(99);
        Assert.Null(result);
    }

    [Fact]
    public async Task GetPatientByIdAsync_AgeCalculation_WorksCorrectly()
    {
        var birthDate = new DateTime(1990, 12, 31);
        var patient = Patient.Create("1", "MR", "Idade", birthDate, GenderEnum.Female, 60, 160, null);
        _surgeryRepoMock.Setup(x => x.GetPatientByIdAsync(1)).ReturnsAsync(patient);

        var result = await _service.GetPatientByIdAsync(1);
        var expectedAge = DateTime.Today.Year - birthDate.Year;
        if (birthDate.Date > DateTime.Today.AddYears(-expectedAge)) expectedAge--;
        Assert.Equal(expectedAge, result!.Age);
    }

    
    #endregion
}





