using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using UFF.FichaAnestesica.Domain.Commands.AnesthesiaRecord;
using UFF.FichaAnestesica.Domain.Commands.PreAnesthesiaRecord;
using UFF.FichaAnestesica.Domain.Dto;
using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Enums;
using UFF.FichaAnestesica.Domain.Repositories;
using UFF.FichaAnestesica.Domain.Repositories.ReadOnly;
using UFF.FichaAnestesica.Domain.Response;
using UFF.FichaAnestesica.Domain.Services;
using UFF.FichaAnestesica.Service.Services;

namespace UFF.FichaAnestesica.Test.Services
{
    /// <summary>
    /// Procedimento cirúrgico oficial: o AGHU fornece o valor inicial; a última escolha do médico
    /// (pré-anestésica ou ficha anestésica) prevalece em todas as telas, inclusive após novas
    /// sincronizações com o AGHU. Os cenários passam pelos serviços reais que alimentam a
    /// pré-anestésica, a ficha anestésica, "Meus pacientes" e "Todos os pacientes".
    /// </summary>
    public class SurgeryProcedureConsistencyTest
    {
        private const int DoctorId = 1;
        private const string PatientId = "P1";
        private const int SurgeryId = 10;
        private const int OtherSurgeryId = 11;

        private readonly Procedure _procA = CreateProcedure(1, "100", "Colecistectomia");
        private readonly Procedure _procB = CreateProcedure(2, "200", "Herniorrafia inguinal");
        private readonly Procedure _procC = CreateProcedure(3, "300", "Laparotomia exploradora");

        private readonly AnesthesiaRecord _record;
        private readonly AnesthesiaRecord _otherRecord;
        private PreAnesthesiaRecord? _preRecord;

        private readonly SurgeryService _surgeryService;
        private readonly AnesthesiaRecordService _anesthesiaService;
        private readonly PreAnesthesiaRecordService _preAnesthesiaService;

        public SurgeryProcedureConsistencyTest()
        {
            _record = CreateRecord(SurgeryId);
            _otherRecord = CreateRecord(OtherSurgeryId);
            var records = new List<AnesthesiaRecord> { _record, _otherRecord };

            var anesthesiaRepo = new Mock<IAnesthesiaRecordRepository>();
            anesthesiaRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((int id) => records.FirstOrDefault(x => x.Id == id)!);
            anesthesiaRepo.Setup(r => r.GetByIdsAsync(It.IsAny<IEnumerable<string>>())).ReturnsAsync(() => records.ToList());
            anesthesiaRepo.Setup(r => r.GetPagedByDoctorPrioritizedAsync(DoctorId, It.IsAny<DateTime?>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(() => (records.ToList(), records.Count));
            anesthesiaRepo.Setup(r => r.CanAssumePatientsAsync(It.IsAny<int>())).ReturnsAsync(false);

            // O AGHU sempre agenda o procedimento A para as duas cirurgias do paciente.
            var hospitalRepo = new Mock<IPatientReadOnlyRepository>();
            hospitalRepo.Setup(r => r.GetPatientsFromHospitalAsync(It.IsAny<DateTime?>(), It.IsAny<string>(), It.IsAny<SurgeryStatusEnum?>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(() => AghuPage());
            hospitalRepo.Setup(r => r.GetMyPatientsFromHospitalAsync(It.IsAny<IEnumerable<int>>(), It.IsAny<string?>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(() => AghuPage());
            hospitalRepo.Setup(r => r.GetFromHospitalByPatientIdAndSurgeryIdAsync(PatientId, It.IsAny<int>()))
                .ReturnsAsync(() => AghuPatientDetail());

            var catalog = new List<Procedure> { _procA, _procB, _procC };
            var procedureRepo = new Mock<IProcedureRepository>();
            procedureRepo.Setup(r => r.GetByIdsAsync(It.IsAny<IEnumerable<string>>()))
                .ReturnsAsync((IEnumerable<string> ids) => catalog.Where(x => ids.Contains(x.ExternalId)).ToList());

            var monitoringRepo = new Mock<IMonitoringRecordRepository>();
            monitoringRepo.Setup(r => r.GetStartedAnesthesiaRecordIds(It.IsAny<IEnumerable<int>>())).Returns(new HashSet<int>());

            var preRepo = new Mock<IPreAnesthesiaRecordRepository>();
            preRepo.Setup(r => r.GetCompletedAnesthesiaRecordIds(It.IsAny<IEnumerable<int>>())).Returns(new HashSet<int>());
            preRepo.Setup(r => r.GetByAnesthesiaRecordIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => _preRecord != null && _preRecord.AnesthesiaRecordId == id ? _preRecord : null);
            preRepo.Setup(r => r.GetCompleteByIdAsync(It.IsAny<int>())).ReturnsAsync(() => _preRecord);
            preRepo.Setup(r => r.AddAsync(It.IsAny<PreAnesthesiaRecord>()))
                .Callback((PreAnesthesiaRecord pre) =>
                {
                    pre.SetAnesthesiaRecord(records.First(x => x.Id == pre.AnesthesiaRecordId));
                    _preRecord = pre;
                });

            var currentUser = new Mock<ICurrentUserService>();
            currentUser.Setup(x => x.UserId).Returns(DoctorId);

            _surgeryService = new SurgeryService(new Mock<IUserRepository>().Object, hospitalRepo.Object, anesthesiaRepo.Object,
                monitoringRepo.Object, preRepo.Object, procedureRepo.Object);
            _anesthesiaService = new AnesthesiaRecordService(anesthesiaRepo.Object, monitoringRepo.Object, hospitalRepo.Object,
                procedureRepo.Object, currentUser.Object);
            _preAnesthesiaService = new PreAnesthesiaRecordService(preRepo.Object, anesthesiaRepo.Object, procedureRepo.Object,
                currentUser.Object, NullLogger<PreAnesthesiaRecordService>.Instance);
        }

        [Fact]
        public async Task Scenario1_InitialLoad_Should_Prefill_Aghu_Procedure()
        {
            // A pré-anestésica é pré-preenchida com o procedimento oficial da ficha (AGHU enquanto não houver escolha médica).
            Assert.Equal(_procA.Description, await AnesthesiaFormProcedure(SurgeryId));
            Assert.Equal(_procA.Description, await AllPatientsProcedure(SurgeryId));
            Assert.Equal(_procA.Description, await MyPatientsProcedure(SurgeryId));
            Assert.False(_record.ProceduresCustomized);
        }

        [Fact]
        public async Task Scenario2_ChangeInPreAnesthesia_Should_Reflect_Everywhere()
        {
            await SavePreAnesthesia(current: _procB, baseline: _procA);

            Assert.Equal(_procB.Description, await PreAnesthesiaProcedure());
            Assert.Equal(_procB.Description, await AnesthesiaFormProcedure(SurgeryId));
            Assert.Equal(_procB.Description, await MyPatientsProcedure(SurgeryId));
            Assert.Equal(_procB.Description, await AllPatientsProcedure(SurgeryId));
        }

        [Fact]
        public async Task Scenario3_ChangeInAnesthesiaRecord_Should_Reflect_Everywhere()
        {
            await SavePreAnesthesia(current: _procB, baseline: _procA);

            var result = await SaveAnesthesiaRecord(current: _procC, baseline: _procB);

            Assert.True(result.Valid);
            Assert.Equal(_procC.Description, await AnesthesiaFormProcedure(SurgeryId));
            Assert.Equal(_procC.Description, await PreAnesthesiaProcedure());
            Assert.Equal(_procC.Description, await MyPatientsProcedure(SurgeryId));
            Assert.Equal(_procC.Description, await AllPatientsProcedure(SurgeryId));
        }

        [Fact]
        public async Task Scenario4_NewAghuSync_Should_Not_Override_Doctor_Choice()
        {
            await SavePreAnesthesia(current: _procB, baseline: _procA);
            await SaveAnesthesiaRecord(current: _procC, baseline: _procB);

            // "Todos os pacientes" roda a sincronização do agendamento do AGHU (que continua retornando A).
            await AllPatientsProcedure(SurgeryId);
            await AllPatientsProcedure(SurgeryId);

            Assert.Equal(new[] { _procC.Id }, _record.Surgeries.Select(x => x.ProcedureId));
            Assert.Equal(_procC.Description, await AllPatientsProcedure(SurgeryId));
            Assert.Equal(_procC.Description, await MyPatientsProcedure(SurgeryId));
            Assert.Equal(_procC.Description, await AnesthesiaFormProcedure(SurgeryId));
            Assert.Equal(_procC.Description, await PreAnesthesiaProcedure());
        }

        [Fact]
        public async Task Scenario5_Without_Doctor_Change_Should_Keep_Aghu_Procedure()
        {
            await AllPatientsProcedure(OtherSurgeryId);

            Assert.False(_otherRecord.ProceduresCustomized);
            Assert.Equal(_procA.Description, await AllPatientsProcedure(OtherSurgeryId));
            Assert.Equal(_procA.Description, await MyPatientsProcedure(OtherSurgeryId));
            Assert.Equal(_procA.Description, await AnesthesiaFormProcedure(OtherSurgeryId));
        }

        [Fact]
        public async Task Scenario6_Changing_One_Surgery_Should_Not_Affect_Other_Surgeries_Of_Same_Patient()
        {
            await SavePreAnesthesia(current: _procB, baseline: _procA);
            await SaveAnesthesiaRecord(current: _procC, baseline: _procB);

            Assert.Equal(_procC.Description, await AllPatientsProcedure(SurgeryId));
            Assert.Equal(_procA.Description, await AllPatientsProcedure(OtherSurgeryId));
            Assert.Equal(_procA.Description, await MyPatientsProcedure(OtherSurgeryId));
            Assert.Equal(_procA.Description, await AnesthesiaFormProcedure(OtherSurgeryId));
            Assert.False(_otherRecord.ProceduresCustomized);
            Assert.DoesNotContain(_otherRecord.Surgeries, x => x.ProcedureId == _procB.Id || x.ProcedureId == _procC.Id);
        }

        [Fact]
        public async Task Stale_Form_Without_Procedure_Change_Should_Not_Revert_Newer_Choice()
        {
            await SavePreAnesthesia(current: _procB, baseline: _procA);
            await SaveAnesthesiaRecord(current: _procC, baseline: _procB);

            // Ficha anestésica aberta antes da alteração (ainda com B) e salva sem mexer no procedimento.
            await SaveAnesthesiaRecord(current: _procB, baseline: _procB);

            Assert.Equal(_procC.Description, await AllPatientsProcedure(SurgeryId));
            Assert.Equal(_procC.Description, await PreAnesthesiaProcedure());
        }

        [Fact]
        public async Task Saving_AnesthesiaRecord_Without_Procedures_Should_Keep_Official_Procedure()
        {
            await SavePreAnesthesia(current: _procB, baseline: _procA);

            var result = await _anesthesiaService.Update(SurgeryId, new AnesthesiaRecordCommand { SurgeryId = SurgeryId, PatientId = PatientId });

            Assert.True(result.Valid);
            Assert.Equal(_procB.Description, await AnesthesiaFormProcedure(SurgeryId));
            Assert.Equal(_procB.Description, await MyPatientsProcedure(SurgeryId));
        }

        [Fact]
        public async Task Unknown_Procedure_Should_Fail_Without_Changing_Official_Procedure()
        {
            await SavePreAnesthesia(current: _procB, baseline: _procA);

            var result = await _anesthesiaService.Update(SurgeryId, new AnesthesiaRecordCommand
            {
                SurgeryId = SurgeryId,
                PatientId = PatientId,
                Surgeries = [new SurgeryCommand { Id = "999", IsPrimary = true }],
                BaseSurgeries = [new SurgeryCommand { Id = _procB.ExternalId, IsPrimary = true }]
            });

            Assert.False(result.Valid);
            Assert.Equal(new[] { _procB.Id }, _record.Surgeries.Select(x => x.ProcedureId));
        }

        #region Helpers

        private async Task SavePreAnesthesia(Procedure current, Procedure baseline)
        {
            var result = await _preAnesthesiaService.Create(new PreAnesthesiaRecordCommand
            {
                AnesthesiaRecordId = SurgeryId,
                AsaClassification = AsaClassificationEnum.ASA_II,
                Surgeries = [new PreAnesthesiaSurgeryCommand { ProcedureId = current.ExternalId, Name = current.Description, IsPrimary = true }],
                BaseSurgeries = [new PreAnesthesiaSurgeryCommand { ProcedureId = baseline.ExternalId, Name = baseline.Description, IsPrimary = true }]
            });

            Assert.True(result.Valid, result.Data?.ToString());
        }

        private Task<Domain.Commands.CommandResult> SaveAnesthesiaRecord(Procedure current, Procedure baseline)
        {
            return _anesthesiaService.Update(SurgeryId, new AnesthesiaRecordCommand
            {
                SurgeryId = SurgeryId,
                PatientId = PatientId,
                Surgeries = [new SurgeryCommand { Id = current.ExternalId, Description = current.Description, IsPrimary = true }],
                BaseSurgeries = [new SurgeryCommand { Id = baseline.ExternalId, Description = baseline.Description, IsPrimary = true }]
            });
        }

        private async Task<string?> PreAnesthesiaProcedure()
        {
            var result = await _preAnesthesiaService.GetByAnesthesiaRecordIdAsync(SurgeryId);
            var data = (PreAnesthesiaRecordResponse)result.Data!;
            return data.Surgeries.FirstOrDefault(x => x.IsPrimary)?.Name;
        }

        private async Task<string?> AnesthesiaFormProcedure(int surgeryId)
        {
            var result = await _anesthesiaService.GetByIdAsync(surgeryId, PatientId);
            var data = (AnesthesiaRecordResponse)result.Data!;
            return data.Surgeries.Single().Procedures.FirstOrDefault(x => x.IsPrimary)?.Description;
        }

        private async Task<string?> AllPatientsProcedure(int surgeryId)
        {
            var result = await _surgeryService.GetPatientsWithSurgeriesAsync(DoctorId, null, string.Empty, null);
            return PrimaryProcedure(result.Data, surgeryId);
        }

        private async Task<string?> MyPatientsProcedure(int surgeryId)
        {
            var result = await _surgeryService.GetMyPatientsAsync(DoctorId, null, string.Empty, null);
            return PrimaryProcedure(result.Data, surgeryId);
        }

        private static string? PrimaryProcedure(object? data, int surgeryId)
        {
            var paged = Assert.IsType<PagedResponse<PatientSurgeryResponse>>(data);
            var patient = paged.Data.Single(x => x.SurgeryId == surgeryId);
            return patient.Procedures.FirstOrDefault(x => x.IsPrimary)?.Description;
        }

        private static Procedure CreateProcedure(int id, string externalId, string description)
        {
            var procedure = Procedure.Create(externalId, externalId, description, null);
            procedure.Id = id;
            return procedure;
        }

        private static AnesthesiaRecord CreateRecord(int surgeryId)
        {
            var record = AnesthesiaRecord.Create(new AnesthesiaRecordCommand { SurgeryId = surgeryId, PatientId = PatientId }, new DateTime(2026, 9, 28));
            record.AssignFirstAnesthesiologistId(DoctorId);
            return record;
        }

        private ProcedureDto AghuProcedureA() => new()
        {
            ExternalId = int.Parse(_procA.ExternalId),
            Codigo = _procA.Code,
            Description = _procA.Description,
            IsPrimary = true
        };

        private PatientDetailDto AghuRow(int surgeryId) => new()
        {
            SurgeryId = surgeryId,
            PatientId = PatientId,
            FullName = "Paciente Teste",
            Status = "agendada",
            SurgeryDate = new DateTime(2026, 9, 28),
            BirthDate = new DateTime(1980, 1, 1),
            Procedures = [AghuProcedureA()]
        };

        private PagedResponse<PatientDetailDto> AghuPage() => new()
        {
            Data = [AghuRow(SurgeryId), AghuRow(OtherSurgeryId)],
            Page = 1,
            PageSize = 10,
            TotalItems = 2
        };

        private PatientDetailDto AghuPatientDetail()
        {
            var detail = AghuRow(SurgeryId);
            detail.Surgeries =
            [
                new SurgeryDetailsDto { Id = SurgeryId, SurgeryStatus = "agendada", Procedures = [AghuProcedureA()] },
                new SurgeryDetailsDto { Id = OtherSurgeryId, SurgeryStatus = "agendada", Procedures = [AghuProcedureA()] }
            ];
            return detail;
        }

        #endregion
    }
}
