using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using UFF.FichaAnestesica.Domain.Commands;
using UFF.FichaAnestesica.Domain.Commands.AnesthesiaRecord;
using UFF.FichaAnestesica.Domain.Commands.PreAnesthesiaRecord;
using UFF.FichaAnestesica.Domain.Dto;
using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Enums;
using UFF.FichaAnestesica.Domain.Repositories;
using UFF.FichaAnestesica.Domain.Repositories.Aghu;
using UFF.FichaAnestesica.Domain.Response;
using UFF.FichaAnestesica.Domain.Services;

namespace UFF.FichaAnestesica.Test.Services
{
    public class PreAnesthesiaLabExamServiceTest
    {
        private const int AnesthesiaRecordId = 10;
        private const string PatientId = "PAC-000001";
        private const int ResponsibleDoctorId = 1;

        private readonly Mock<IPreAnesthesiaLabExamRepository> _labRepoMock = new();
        private readonly Mock<IPreAnesthesiaRecordRepository> _preRepoMock = new();
        private readonly Mock<IAnesthesiaRecordRepository> _anesthesiaRepoMock = new();
        private readonly Mock<ILabExamReadOnlyRepository> _aghuMock = new();
        private readonly Mock<ICurrentUserService> _currentUserMock = new();
        private readonly PreAnesthesiaLabExamService _service;

        private PreAnesthesiaLabExam? _stored;
        private PreAnesthesiaLabExam? _pendingAdd;

        public PreAnesthesiaLabExamServiceTest()
        {
            _currentUserMock.Setup(x => x.UserId).Returns(ResponsibleDoctorId);

            _anesthesiaRepoMock.Setup(r => r.GetByIdAsync(AnesthesiaRecordId)).ReturnsAsync(CreateAnesthesiaRecord());

            _labRepoMock.Setup(r => r.GetByAnesthesiaRecordIdAsync(AnesthesiaRecordId)).ReturnsAsync(() => _stored);
            _labRepoMock.Setup(r => r.AddAsync(It.IsAny<PreAnesthesiaLabExam>()))
                .Callback<PreAnesthesiaLabExam>(x => _pendingAdd = x)
                .Returns(Task.CompletedTask);
            _labRepoMock.Setup(r => r.TrySaveChangesAsync()).ReturnsAsync(() =>
            {
                if (_pendingAdd != null)
                {
                    _stored = _pendingAdd;
                    _pendingAdd = null;
                }
                return true;
            });

            _service = new PreAnesthesiaLabExamService(
                _labRepoMock.Object,
                _preRepoMock.Object,
                _anesthesiaRepoMock.Object,
                _aghuMock.Object,
                _currentUserMock.Object,
                NullLogger<PreAnesthesiaLabExamService>.Instance);
        }

        private static AnesthesiaRecord CreateAnesthesiaRecord(int? firstAnesthesiologistId = ResponsibleDoctorId)
        {
            var record = AnesthesiaRecord.Create(new AnesthesiaRecordCommand { SurgeryId = AnesthesiaRecordId, PatientId = PatientId }, DateTime.MinValue);
            record.AssignFirstAnesthesiologistId(firstAnesthesiologistId);
            return record;
        }

        private static AghuLabResultDto Result(string analyte, string code, decimal value, string unit = "g/dL")
            => new() { Analyte = analyte, Code = code, Value = value, Unit = unit, ReferenceRange = "ref" };

        private static AghuLabExamLookup FullLookup(long examId = 900002, decimal hemoglobin = 13.5m)
        {
            return new AghuLabExamLookup
            {
                PatientFound = true,
                Exam = new AghuLabExamDto { Id = examId, CollectedAt = new DateTime(2026, 9, 20, 8, 0, 0), ReleasedAt = new DateTime(2026, 9, 20, 15, 0, 0), Status = "LIBERADO" },
                Results = new()
                {
                    Result("HEMACIAS", "HEM_HEMACIAS", 4.6m, "milhões/mm³"),
                    Result("HEMOGLOBINA", "HEM_HB", hemoglobin),
                    Result("HEMATOCRITO", "HEM_HT", 40.2m, "%"),
                    Result("VCM", "HEM_VCM", 87.4m, "fL"),
                    Result("HCM", "HEM_HCM", 29.3m, "pg"),
                    Result("CHCM", "HEM_CHCM", 33.6m),
                    Result("RDW", "HEM_RDW", 13.1m, "%"),
                    Result("LEUCOCITOS", "HEM_LEUCOCITOS", 7200m, "/mm³"),
                    Result("PLAQUETAS", "HEM_PLAQUETAS", 245000m, "/mm³"),
                    Result("TP", "COAG_TP", 12.8m, "s"),
                    Result("INR", "COAG_INR", 1.05m, ""),
                    Result("TTPA", "COAG_TTPA", 31.5m, "s"),
                    Result("TGO", "HEP_TGO", 24m, "U/L"),
                    Result("TGP", "HEP_TGP", 28m, "U/L"),
                    Result("GGT", "HEP_GGT", 35m, "U/L"),
                    Result("FOSFATASE_ALCALINA", "HEP_FA", 82m, "U/L"),
                    Result("UREIA", "REN_UREIA", 32m, "mg/dL"),
                    Result("CREATININA", "REN_CREATININA", 0.92m, "mg/dL"),
                }
            };
        }

        private static PreAnesthesiaLabExamResponse Data(CommandResult result) => (PreAnesthesiaLabExamResponse)result.Data;

        private static decimal? ValueOf(PreAnesthesiaLabExamResponse response, LabAnalyteEnum analyte)
            => response.Results.Single(r => r.Analyte == analyte.ToString()).Value;

        private static PreAnesthesiaLabExamCommand Command(params (string analyte, decimal? value)[] values)
            => new() { Results = values.Select(v => new PreAnesthesiaLabResultCommand { Analyte = v.analyte, Value = v.value }).ToList() };

        // Teste 1 — primeira importação
        [Fact]
        public async Task GetOrImport_Should_Import_And_Persist_On_First_Open()
        {
            _aghuMock.Setup(x => x.GetLatestReleasedByPatientIdAsync(PatientId, It.IsAny<CancellationToken>())).ReturnsAsync(FullLookup());

            var result = await _service.GetOrImportAsync(AnesthesiaRecordId);
            var data = Data(result);

            Assert.True(result.Valid);
            Assert.True(data.ImportedNow);
            Assert.Equal("IMPORTED", data.ImportStatus);
            Assert.Equal(900002, data.SourceExamId);
            Assert.Equal(18, data.Results.Count);
            Assert.Empty(data.MissingAnalytes);
            Assert.All(data.Results, r => Assert.Equal("AGHU", r.Source));
            Assert.Equal(13.5m, ValueOf(data, LabAnalyteEnum.HEMOGLOBIN));
            Assert.NotNull(_stored);
            _labRepoMock.Verify(r => r.AddAsync(It.IsAny<PreAnesthesiaLabExam>()), Times.Once);
        }

        [Fact]
        public async Task GetOrImport_Should_Query_Aghu_With_Patient_Linked_To_The_Record()
        {
            _aghuMock.Setup(x => x.GetLatestReleasedByPatientIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(FullLookup());

            await _service.GetOrImportAsync(AnesthesiaRecordId);

            _aghuMock.Verify(x => x.GetLatestReleasedByPatientIdAsync(PatientId, It.IsAny<CancellationToken>()), Times.Once);
        }

        // Teste 2 — reabertura
        [Fact]
        public async Task GetOrImport_Should_Not_Call_Aghu_Again_After_Import()
        {
            _aghuMock.Setup(x => x.GetLatestReleasedByPatientIdAsync(PatientId, It.IsAny<CancellationToken>())).ReturnsAsync(FullLookup());
            await _service.GetOrImportAsync(AnesthesiaRecordId);

            var reopened = Data(await _service.GetOrImportAsync(AnesthesiaRecordId));

            Assert.False(reopened.ImportedNow);
            Assert.Equal(18, reopened.Results.Count);
            _aghuMock.Verify(x => x.GetLatestReleasedByPatientIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Save_Should_Persist_Manual_Value_And_Keep_It_On_Reopen()
        {
            _aghuMock.Setup(x => x.GetLatestReleasedByPatientIdAsync(PatientId, It.IsAny<CancellationToken>())).ReturnsAsync(FullLookup());
            await _service.GetOrImportAsync(AnesthesiaRecordId);

            var saved = await _service.SaveAsync(AnesthesiaRecordId, Command(("HEMOGLOBIN", 12.8m)));
            var reopened = Data(await _service.GetOrImportAsync(AnesthesiaRecordId));
            var hemoglobin = reopened.Results.Single(r => r.Analyte == "HEMOGLOBIN");

            Assert.True(saved.Valid);
            Assert.Equal(12.8m, hemoglobin.Value);
            Assert.Equal("MANUAL", hemoglobin.Source);
            Assert.Equal(13.5m, hemoglobin.ImportedValue);
            Assert.Equal("AGHU", reopened.Results.Single(r => r.Analyte == "HEMATOCRIT").Source);
            _aghuMock.Verify(x => x.GetLatestReleasedByPatientIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        
        [Fact]
        public async Task GetOrImport_Should_Ignore_Newer_Aghu_Exam_After_Import()
        {
            _aghuMock.Setup(x => x.GetLatestReleasedByPatientIdAsync(PatientId, It.IsAny<CancellationToken>())).ReturnsAsync(FullLookup());
            await _service.GetOrImportAsync(AnesthesiaRecordId);

            _aghuMock.Setup(x => x.GetLatestReleasedByPatientIdAsync(PatientId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(FullLookup(examId: 900099, hemoglobin: 9.1m));

            var reopened = Data(await _service.GetOrImportAsync(AnesthesiaRecordId));

            Assert.Equal(900002, reopened.SourceExamId);
            Assert.Equal(13.5m, ValueOf(reopened, LabAnalyteEnum.HEMOGLOBIN));
        }

     
        [Fact]
        public async Task GetOrImport_Should_Not_Create_Values_When_No_Released_Exam_And_Allow_Retry()
        {
            _aghuMock.Setup(x => x.GetLatestReleasedByPatientIdAsync(PatientId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new AghuLabExamLookup { PatientFound = true, Exam = null });

            var first = await _service.GetOrImportAsync(AnesthesiaRecordId);
            var data = Data(first);

            Assert.True(first.Valid);
            Assert.Equal("NO_EXAMS_AVAILABLE", data.ImportStatus);
            Assert.Empty(data.Results);
            Assert.False(data.HasSigaData);
            Assert.Equal(18, data.MissingAnalytes.Count);

            _aghuMock.Setup(x => x.GetLatestReleasedByPatientIdAsync(PatientId, It.IsAny<CancellationToken>())).ReturnsAsync(FullLookup());
            var retry = Data(await _service.GetOrImportAsync(AnesthesiaRecordId));

            Assert.True(retry.ImportedNow);
            Assert.Equal("IMPORTED", retry.ImportStatus);
        }

        [Fact]
        public async Task GetOrImport_Should_Treat_Exam_Without_Results_As_No_Exams()
        {
            var lookup = FullLookup();
            lookup.Results.Clear();
            _aghuMock.Setup(x => x.GetLatestReleasedByPatientIdAsync(PatientId, It.IsAny<CancellationToken>())).ReturnsAsync(lookup);

            var data = Data(await _service.GetOrImportAsync(AnesthesiaRecordId));

            Assert.Equal("NO_EXAMS_AVAILABLE", data.ImportStatus);
            Assert.Empty(data.Results);
        }

        [Fact]
        public async Task GetOrImport_Should_Import_Partial_Exam_Without_Inventing_Missing_Values()
        {
            var lookup = FullLookup();
            lookup.Results.RemoveAll(r => r.Analyte is "RDW" or "GGT");
            _aghuMock.Setup(x => x.GetLatestReleasedByPatientIdAsync(PatientId, It.IsAny<CancellationToken>())).ReturnsAsync(lookup);

            var data = Data(await _service.GetOrImportAsync(AnesthesiaRecordId));

            Assert.Equal(16, data.Results.Count);
            Assert.Equal(new[] { "RDW", "GGT" }, data.MissingAnalytes);
            Assert.DoesNotContain(data.Results, r => r.Analyte is "RDW" or "GGT");
        }

      
        [Theory]
        [InlineData(typeof(HttpRequestException))]
        [InlineData(typeof(TaskCanceledException))]
        [InlineData(typeof(InvalidOperationException))]
        public async Task GetOrImport_Should_Not_Mark_As_Imported_When_Integrator_Fails(Type exceptionType)
        {
            _aghuMock.Setup(x => x.GetLatestReleasedByPatientIdAsync(PatientId, It.IsAny<CancellationToken>()))
                .ThrowsAsync((Exception)Activator.CreateInstance(exceptionType)!);

            var result = await _service.GetOrImportAsync(AnesthesiaRecordId);
            var data = Data(result);

            Assert.True(result.Valid);
            Assert.Equal("FAILED", data.ImportStatus);
            Assert.Empty(data.Results);
            Assert.False(data.HasSigaData);
            Assert.NotNull(data.Message);
        }

        [Fact]
        public async Task GetOrImport_Should_Retry_After_Failure_Without_Duplicating()
        {
            _aghuMock.Setup(x => x.GetLatestReleasedByPatientIdAsync(PatientId, It.IsAny<CancellationToken>())).ThrowsAsync(new HttpRequestException());
            await _service.GetOrImportAsync(AnesthesiaRecordId);

            _aghuMock.Setup(x => x.GetLatestReleasedByPatientIdAsync(PatientId, It.IsAny<CancellationToken>())).ReturnsAsync(FullLookup());
            var retry = Data(await _service.GetOrImportAsync(AnesthesiaRecordId));

            Assert.Equal("IMPORTED", retry.ImportStatus);
            Assert.Equal(18, retry.Results.Count);
            Assert.Equal(2, _stored!.ImportAttempts);
            _labRepoMock.Verify(r => r.AddAsync(It.IsAny<PreAnesthesiaLabExam>()), Times.Once);
        }

        [Fact]
        public async Task GetOrImport_Should_Report_Failure_When_Patient_Not_Found_In_Aghu()
        {
            _aghuMock.Setup(x => x.GetLatestReleasedByPatientIdAsync(PatientId, It.IsAny<CancellationToken>())).ReturnsAsync(AghuLabExamLookup.PatientNotFound());

            var data = Data(await _service.GetOrImportAsync(AnesthesiaRecordId));

            Assert.Equal("FAILED", data.ImportStatus);
            Assert.Empty(data.Results);
        }

        [Fact]
        public async Task GetOrImport_Should_Preserve_Existing_Data_When_Integrator_Fails()
        {
            _aghuMock.Setup(x => x.GetLatestReleasedByPatientIdAsync(PatientId, It.IsAny<CancellationToken>())).ReturnsAsync(FullLookup());
            await _service.GetOrImportAsync(AnesthesiaRecordId);
            _aghuMock.Setup(x => x.GetLatestReleasedByPatientIdAsync(PatientId, It.IsAny<CancellationToken>())).ThrowsAsync(new HttpRequestException());

            var data = Data(await _service.GetOrImportAsync(AnesthesiaRecordId));

            Assert.Equal("IMPORTED", data.ImportStatus);
            Assert.Equal(18, data.Results.Count);
        }

        [Fact]
        public async Task GetOrImport_Should_Return_Persisted_State_When_Concurrent_Import_Wins()
        {
            var winner = PreAnesthesiaLabExam.Create(AnesthesiaRecordId);
            winner.RegisterImport(FullLookup(examId: 111).Exam!, FullLookup().Results, DateTime.UtcNow);

            _aghuMock.Setup(x => x.GetLatestReleasedByPatientIdAsync(PatientId, It.IsAny<CancellationToken>())).ReturnsAsync(FullLookup(examId: 222));
            _labRepoMock.Setup(r => r.TrySaveChangesAsync()).ReturnsAsync(() =>
            {
               
                _pendingAdd = null;
                _stored = winner;
                return false;
            });

            var data = Data(await _service.GetOrImportAsync(AnesthesiaRecordId));

            Assert.False(data.ImportedNow);
            Assert.Equal(111, data.SourceExamId);
        }

        [Fact]
        public async Task Save_Should_Reapply_Manual_Values_When_Import_Happened_Concurrently()
        {
            var imported = PreAnesthesiaLabExam.Create(AnesthesiaRecordId);
            imported.RegisterImport(FullLookup().Exam!, FullLookup().Results, DateTime.UtcNow);

            var calls = 0;
            _labRepoMock.Setup(r => r.TrySaveChangesAsync()).ReturnsAsync(() =>
            {
                calls++;
                if (calls == 1)
                {
                    
                    _pendingAdd = null;
                    _stored = imported;
                    return false;
                }
                return true;
            });

            var result = await _service.SaveAsync(AnesthesiaRecordId, Command(("HEMOGLOBIN", 12.8m)));
            var data = Data(result);

            Assert.True(result.Valid);
            Assert.Equal(12.8m, ValueOf(data, LabAnalyteEnum.HEMOGLOBIN));
            Assert.Equal("MANUAL", data.Results.Single(r => r.Analyte == "HEMOGLOBIN").Source);
            Assert.Equal(18, data.Results.Count);
        }

        [Fact]
        public async Task Save_Before_Import_Should_Prevent_Later_Automatic_Import()
        {
            await _service.SaveAsync(AnesthesiaRecordId, Command(("UREA", 40m)));
            _aghuMock.Setup(x => x.GetLatestReleasedByPatientIdAsync(PatientId, It.IsAny<CancellationToken>())).ReturnsAsync(FullLookup());

            var data = Data(await _service.GetOrImportAsync(AnesthesiaRecordId));

            Assert.Single(data.Results);
            Assert.Equal(40m, ValueOf(data, LabAnalyteEnum.UREA));
            _aghuMock.Verify(x => x.GetLatestReleasedByPatientIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Save_Clearing_Value_Should_Not_Trigger_Reimport()
        {
            _aghuMock.Setup(x => x.GetLatestReleasedByPatientIdAsync(PatientId, It.IsAny<CancellationToken>())).ReturnsAsync(FullLookup());
            await _service.GetOrImportAsync(AnesthesiaRecordId);

            await _service.SaveAsync(AnesthesiaRecordId, Command(("HEMOGLOBIN", null)));
            var data = Data(await _service.GetOrImportAsync(AnesthesiaRecordId));

            Assert.Null(ValueOf(data, LabAnalyteEnum.HEMOGLOBIN));
            Assert.Contains("HEMOGLOBIN", data.MissingAnalytes);
            _aghuMock.Verify(x => x.GetLatestReleasedByPatientIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Save_Should_Only_Change_Sent_Analytes()
        {
            _aghuMock.Setup(x => x.GetLatestReleasedByPatientIdAsync(PatientId, It.IsAny<CancellationToken>())).ReturnsAsync(FullLookup());
            await _service.GetOrImportAsync(AnesthesiaRecordId);

            var data = Data(await _service.SaveAsync(AnesthesiaRecordId, Command(("CREATININE", 1.1m))));

            Assert.Equal(1.1m, ValueOf(data, LabAnalyteEnum.CREATININE));
            Assert.Equal(13.5m, ValueOf(data, LabAnalyteEnum.HEMOGLOBIN));
            Assert.Single(data.Results, r => r.Source == "MANUAL");
        }

        
        [Fact]
        public async Task GetOrImport_Should_Not_Import_For_Finalized_Record()
        {
            var record = PreAnesthesiaRecord.Create(new PreAnesthesiaRecordCommand
            {
                AnesthesiaRecordId = AnesthesiaRecordId,
                AsaClassification = AsaClassificationEnum.ASA_II,
                SignedByProfessionalId = ResponsibleDoctorId,
                SignedAt = DateTime.UtcNow
            });
            _preRepoMock.Setup(r => r.GetByAnesthesiaRecordIdAsync(AnesthesiaRecordId)).ReturnsAsync(record);

            var data = Data(await _service.GetOrImportAsync(AnesthesiaRecordId));

            Assert.Equal(PreAnesthesiaLabExamService.SkippedFinalized, data.SkippedReason);
            Assert.Empty(data.Results);
            _aghuMock.Verify(x => x.GetLatestReleasedByPatientIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);
            _labRepoMock.Verify(r => r.TrySaveChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task GetOrImport_Should_Not_Import_When_Record_Already_Has_Lab_Values()
        {
            var record = PreAnesthesiaRecord.Create(new PreAnesthesiaRecordCommand
            {
                AnesthesiaRecordId = AnesthesiaRecordId,
                AsaClassification = AsaClassificationEnum.ASA_II,
                Hemoglobin = 11.0m
            });
            _preRepoMock.Setup(r => r.GetByAnesthesiaRecordIdAsync(AnesthesiaRecordId)).ReturnsAsync(record);

            var data = Data(await _service.GetOrImportAsync(AnesthesiaRecordId));

            Assert.Equal(PreAnesthesiaLabExamService.SkippedExistingRecordValues, data.SkippedReason);
            _aghuMock.Verify(x => x.GetLatestReleasedByPatientIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task GetOrImport_Should_Fail_When_Anesthesia_Record_Not_Found()
        {
            var result = await _service.GetOrImportAsync(999);

            Assert.False(result.Valid);
            _aghuMock.Verify(x => x.GetLatestReleasedByPatientIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Save_Should_Forbid_When_User_Is_Not_Responsible_Doctor()
        {
            _currentUserMock.Setup(x => x.UserId).Returns(99);

            var result = await _service.SaveAsync(AnesthesiaRecordId, Command(("HEMOGLOBIN", 12.8m)));

            Assert.False(result.Valid);
            Assert.True(result.Forbidden);
            _labRepoMock.Verify(r => r.TrySaveChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task Save_Should_Fail_When_Record_Is_Finalized()
        {
            var record = PreAnesthesiaRecord.Create(new PreAnesthesiaRecordCommand
            {
                AnesthesiaRecordId = AnesthesiaRecordId,
                AsaClassification = AsaClassificationEnum.ASA_II,
                SignedByProfessionalId = ResponsibleDoctorId,
                SignedAt = DateTime.UtcNow
            });
            _preRepoMock.Setup(r => r.GetByAnesthesiaRecordIdAsync(AnesthesiaRecordId)).ReturnsAsync(record);

            var result = await _service.SaveAsync(AnesthesiaRecordId, Command(("HEMOGLOBIN", 12.8m)));

            Assert.False(result.Valid);
            _labRepoMock.Verify(r => r.TrySaveChangesAsync(), Times.Never);
        }

        [Theory]
        [InlineData("GLUCOSE")]
        [InlineData("2")]
        [InlineData("")]
        public async Task Save_Should_Reject_Invalid_Analyte(string analyte)
        {
            var result = await _service.SaveAsync(AnesthesiaRecordId, Command((analyte, 1m)));

            Assert.False(result.Valid);
        }

        [Fact]
        public async Task Save_Should_Reject_Duplicated_Analyte_And_Negative_Value()
        {
            var duplicated = await _service.SaveAsync(AnesthesiaRecordId, Command(("HEMOGLOBIN", 1m), ("hemoglobin", 2m)));
            var negative = await _service.SaveAsync(AnesthesiaRecordId, Command(("HEMOGLOBIN", -1m)));

            Assert.False(duplicated.Valid);
            Assert.False(negative.Valid);
        }
    }
}
