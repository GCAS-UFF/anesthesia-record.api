using UFF.FichaAnestesica.CrossCutting.Mappings;
using UFF.FichaAnestesica.Domain.Commands;
using UFF.FichaAnestesica.Domain.Commands.AnesthesiaRecord;
using UFF.FichaAnestesica.Domain.Dto;
using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Enums;
using UFF.FichaAnestesica.Domain.Extensions;
using UFF.FichaAnestesica.Domain.Repositories;
using UFF.FichaAnestesica.Domain.Repositories.ReadOnly;
using UFF.FichaAnestesica.Domain.Response;
using UFF.FichaAnestesica.Domain.Services;
using UFF.FichaAnestesica.Service.Mappers;

namespace UFF.FichaAnestesica.Service.Services
{
    public class SurgeryService : ISurgeryService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPatientReadOnlyRepository _hospitalApiRepository;
        private readonly IAnesthesiaRecordRepository _anesthesiaRecordRepository;
        private readonly IMonitoringRecordRepository _monitoringRecordRepository;
        private readonly IPreAnesthesiaRecordRepository _preAnesthesiaRecordRepository;

        public SurgeryService(IUserRepository userRepository, IPatientReadOnlyRepository hospitalApiRepository, IAnesthesiaRecordRepository anesthesiaRecordRepository, IMonitoringRecordRepository monitoringRecordRepository, IPreAnesthesiaRecordRepository preAnesthesiaRecordRepository)
        {
            _userRepository = userRepository;
            _hospitalApiRepository = hospitalApiRepository;
            _anesthesiaRecordRepository = anesthesiaRecordRepository;
            _monitoringRecordRepository = monitoringRecordRepository;
            _preAnesthesiaRecordRepository = preAnesthesiaRecordRepository;
        }

        public async Task<CommandResult> GetPatientsWithSurgeriesAsync(int doctorId, DateTime? date, string term, SurgeryStatusEnum? status, int page = 1, int size = 10)
        {
            if (date.HasValue)
                date = DateTime.SpecifyKind(date.Value, DateTimeKind.Utc);

            PagedResponse<PatientDetailDto> hospitalData;

            if (status == SurgeryStatusEnum.Completed)
            {
                var completedRecords = await _anesthesiaRecordRepository.GetByStatusAndDateAsync(SurgeryStatusEnum.Completed, date);
                var completedSurgeryIds = completedRecords.Select(x => x.Id).Distinct().ToList();

                hospitalData = await _hospitalApiRepository.GetMyPatientsFromHospitalAsync(completedSurgeryIds, term, page, size);
            }
            else
            {
                hospitalData = await _hospitalApiRepository.GetPatientsFromHospitalAsync(date, term, status, page, size);
            }

            if (hospitalData.Data == null || !hospitalData.Data.Any())
            {
                return CommandResult.Success(new PagedResponse<PatientSurgeryResponse>
                {
                    Data = [],
                    Page = hospitalData.Page,
                    PageSize = hospitalData.PageSize,
                    TotalItems = hospitalData.TotalItems
                });
            }

            var patientIds = hospitalData.Data.Select(x => x.PatientId).ToArray();
            var anesthesiaRecords = await _anesthesiaRecordRepository.GetByIdsAsync(patientIds);
            var anesthesiaRecordIds = anesthesiaRecords.Select(x => x.Id).ToArray();
            var completedPreAnesthesiaRecordIds = _preAnesthesiaRecordRepository.GetCompletedAnesthesiaRecordIds(anesthesiaRecordIds);

            var recordsBySurgeryId = anesthesiaRecords.GroupBy(x => x.Id).ToDictionary(x => x.Key, x => x.First());

            SetSurgeryStatus(hospitalData, recordsBySurgeryId);

            await AssociateSurgeryProcedures(hospitalData, recordsBySurgeryId);
            await _anesthesiaRecordRepository.SaveChangesAsync();


            var responseData = PatientResponseMapper.Map(hospitalData.Data, recordsBySurgeryId);
            var recordsByPatientId = anesthesiaRecords.GroupBy(x => x.Id).ToDictionary(x => x.Key, x => x.First());

            var canAssumePatient = await _anesthesiaRecordRepository.CanAssumePatientsAsync(doctorId);

            foreach (var patient in responseData)
            {
                patient.IsPreAnesthesiaRecordDone = completedPreAnesthesiaRecordIds.Contains(patient.SurgeryId);

                if (!recordsByPatientId.TryGetValue(patient.SurgeryId, out var record))
                {
                    patient.FirstAnesthesiologist = null;
                    patient.SecondAnesthesiologist = null;
                    patient.Surgeon = null;
                    patient.Assistant = null;

                    continue;
                }

                if (record.FirstAnesthesiologist != null)
                {
                    patient.FirstAnesthesiologist = new ResponsibleResponse
                    {
                        Id = record.FirstAnesthesiologist.Id,
                        FullName = record.FirstAnesthesiologist.Name,
                        Registration = record.FirstAnesthesiologist.Registration
                    };
                }

                if (record.SecondAnesthesiologist != null)
                {
                    patient.SecondAnesthesiologist = new ResponsibleResponse
                    {
                        Id = record.SecondAnesthesiologist.Id,
                        FullName = record.SecondAnesthesiologist.Name,
                        Registration = record.SecondAnesthesiologist.Registration
                    };
                }

                if (record.Surgeon != null)
                {
                    patient.Surgeon = new ResponsibleResponse
                    {
                        Id = record.Surgeon.Id,
                        FullName = record.Surgeon.Name,
                        Registration = record.Surgeon.Registration
                    };
                }

                if (record.Assistant != null)
                {
                    patient.Assistant = new ResponsibleResponse
                    {
                        Id = record.Assistant.Id,
                        FullName = record.Assistant.Name,
                        Registration = record.Assistant.Registration
                    };
                }
            }

            return CommandResult.Success(new PagedResponse<PatientSurgeryResponse>
            {
                Data = responseData,
                Page = hospitalData.Page,
                PageSize = hospitalData.PageSize,
                TotalItems = hospitalData.TotalItems,
                CanAssumePatient = !canAssumePatient
            });
        }

        private static void SetSurgeryStatus(PagedResponse<PatientDetailDto> hospitalData, Dictionary<int, AnesthesiaRecord> recordsBySurgeryId)
        {
            foreach (var patient in hospitalData.Data)
            {
                if (recordsBySurgeryId.TryGetValue(patient.SurgeryId, out var record))
                {
                    patient.HaveFirstAnesthesist = record.FirstAnesthesiologist != null;
                    patient.Status = record.Status == 0 ? SurgeryStatusEnumMapping.Parse(patient.Status).GetDescription() : record.Status.GetDescription();

                }
            }
        }

        private async Task AssociateSurgeryProcedures(PagedResponse<PatientDetailDto> hospitalData, Dictionary<int, AnesthesiaRecord> recordsBySurgeryId)
        {
            foreach (var surgery in hospitalData.Data)
            {
                if (!recordsBySurgeryId.TryGetValue(surgery.SurgeryId, out var record))
                {
                    record = AnesthesiaRecord.Create(new AnesthesiaRecordCommand
                    {
                        SurgeryId = surgery.SurgeryId,
                        PatientId = surgery.PatientId,
                        SurgeryDate = surgery.SurgeryDate
                    }, surgery.SurgeryDate);

                    await _anesthesiaRecordRepository.AddAsync(record);

                    recordsBySurgeryId.Add(record.Id, record);
                }

                if (record.ProceduresCustomized)
                    continue;

                var aghuProcedures = surgery.Procedures ?? [];
                var aghuById = aghuProcedures.ToDictionary(x => x.ExternalId);
                var relationsToRemove = record.Surgeries.Where(x => !aghuById.ContainsKey(x.ProcedureId)).ToList();

                foreach (var relation in relationsToRemove)
                    record.Surgeries.Remove(relation);

                foreach (var procedure in aghuProcedures)
                {
                    var relation = record.Surgeries.FirstOrDefault(x => x.ProcedureId == procedure.ExternalId);

                    if (relation == null)
                        record.Surgeries.Add(AnesthesiaRecordSurgery.Create(record.Id, procedure.ExternalId, procedure.IsPrimary, procedure.Time));
                    else
                        relation.SetPrimary(procedure.IsPrimary);
                }
            }
        }

        public async Task<CommandResult> GetMyPatientsAsync(int doctorId, DateTime? date, string term, SurgeryStatusEnum? status, int page = 1, int size = 10)
        {
            if (date.HasValue)
                date = DateTime.SpecifyKind(date.Value, DateTimeKind.Utc);

            // O paciente atualmente em atendimento (status InProgress) deve sempre ficar na
            // primeira posição, mesmo em páginas seguintes de pacientes mais antigos ou com
            // filtro de data aplicado. Isso só é possível paginando pela tabela local (que já
            // conhece o status "em atendimento") antes de consultar o AGHU. Quando há busca por
            // termo, a filtragem por nome/prontuário só existe no AGHU, então mantemos o fluxo
            // antigo (paginação e ordenação delegadas ao AGHU) para não quebrar a busca.
            if (string.IsNullOrWhiteSpace(term))
                return await GetMyPatientsPrioritizedAsync(doctorId, date, page, size);

            var anesthesiaRecords = await _anesthesiaRecordRepository.GetByDoctorAndDateAsync(doctorId, date);

            if (!anesthesiaRecords.Any())
            {
                return CommandResult.Success(new PagedResponse<PatientSurgeryResponse>
                {
                    Data = [],
                    Page = page,
                    PageSize = size,
                    TotalItems = anesthesiaRecords.Count()
                });
            }

            var surgeryIds = anesthesiaRecords.Select(x => x.Id).Distinct().ToList();

            var hospitalData = await _hospitalApiRepository.GetMyPatientsFromHospitalAsync(surgeryIds, term, page, size);

            if (hospitalData.Data == null || !hospitalData.Data.Any())
            {
                return CommandResult.Success(new PagedResponse<PatientSurgeryResponse>
                {
                    Data = [],
                    Page = hospitalData.Page,
                    PageSize = hospitalData.PageSize,
                    TotalItems = hospitalData.TotalItems
                });
            }

            var recordsBySurgeryId = anesthesiaRecords.ToDictionary(x => x.Id, x => x);

            var canAssumePatient = await _anesthesiaRecordRepository.CanAssumePatientsAsync(doctorId);

            var completedPreAnesthesiaRecordIds = _preAnesthesiaRecordRepository.GetCompletedAnesthesiaRecordIds(surgeryIds);

            SetSurgeryStatus(hospitalData, recordsBySurgeryId);

            var responseData = PatientResponseMapper.Map(hospitalData.Data, recordsBySurgeryId);

            AttachResponsibles(responseData, recordsBySurgeryId);
            ApplyPreAnesthesiaRecordDone(responseData, completedPreAnesthesiaRecordIds);

            return CommandResult.Success(new PagedResponse<PatientSurgeryResponse>
            {
                Data = responseData,
                Page = hospitalData.Page,
                PageSize = hospitalData.PageSize,
                TotalItems = hospitalData.TotalItems,
                CanAssumePatient = !canAssumePatient
            });
        }

        private async Task<CommandResult> GetMyPatientsPrioritizedAsync(int doctorId, DateTime? date, int page, int size)
        {
            var (pagedRecords, totalItems) = await _anesthesiaRecordRepository.GetPagedByDoctorPrioritizedAsync(doctorId, date, page, size);

            var pagedRecordsList = pagedRecords.ToList();

            if (!pagedRecordsList.Any())
            {
                return CommandResult.Success(new PagedResponse<PatientSurgeryResponse>
                {
                    Data = [],
                    Page = page,
                    PageSize = size,
                    TotalItems = totalItems
                });
            }

            // A ordem já vem definida pela consulta local (em atendimento primeiro,
            // depois mais recente para o mais antigo). Buscamos no AGHU somente os
            // detalhes desses ids (já limitados ao tamanho da página).
            var orderedIds = pagedRecordsList.Select(x => x.Id).ToList();

            var hospitalData = await _hospitalApiRepository.GetMyPatientsFromHospitalAsync(orderedIds, null, 1, orderedIds.Count);

            if (hospitalData.Data == null || !hospitalData.Data.Any())
            {
                return CommandResult.Success(new PagedResponse<PatientSurgeryResponse>
                {
                    Data = [],
                    Page = page,
                    PageSize = size,
                    TotalItems = totalItems
                });
            }

            var recordsBySurgeryId = pagedRecordsList.ToDictionary(x => x.Id, x => x);

            var canAssumePatient = await _anesthesiaRecordRepository.CanAssumePatientsAsync(doctorId);

            var completedPreAnesthesiaRecordIds = _preAnesthesiaRecordRepository.GetCompletedAnesthesiaRecordIds(orderedIds);

            SetSurgeryStatus(hospitalData, recordsBySurgeryId);

            var responseData = PatientResponseMapper.Map(hospitalData.Data, recordsBySurgeryId);

            AttachResponsibles(responseData, recordsBySurgeryId);
            ApplyPreAnesthesiaRecordDone(responseData, completedPreAnesthesiaRecordIds);

            // O AGHU retorna os dados ordenados por nome; restauramos a prioridade
            // (em atendimento primeiro, depois mais recente) antes de responder.
            var orderIndex = orderedIds
                .Select((id, index) => (id, index))
                .ToDictionary(x => x.id, x => x.index);

            responseData = responseData
                .OrderBy(x => orderIndex.TryGetValue(x.SurgeryId, out var index) ? index : int.MaxValue)
                .ToList();

            return CommandResult.Success(new PagedResponse<PatientSurgeryResponse>
            {
                Data = responseData,
                Page = page,
                PageSize = size,
                TotalItems = totalItems,
                CanAssumePatient = !canAssumePatient
            });
        }

        private static void ApplyPreAnesthesiaRecordDone(List<PatientSurgeryResponse> responseData, HashSet<int> completedPreAnesthesiaRecordIds)
        {
            foreach (var patient in responseData)
                patient.IsPreAnesthesiaRecordDone = completedPreAnesthesiaRecordIds.Contains(patient.SurgeryId);
        }

        private static void AttachResponsibles(List<PatientSurgeryResponse> responseData, Dictionary<int, AnesthesiaRecord> recordsBySurgeryId)
        {
            foreach (var patient in responseData)
            {
                if (!recordsBySurgeryId.TryGetValue(patient.SurgeryId, out var record))
                    continue;

                if (record.FirstAnesthesiologist != null)
                {
                    patient.FirstAnesthesiologist = new ResponsibleResponse
                    {
                        Id = record.FirstAnesthesiologist.Id,
                        FullName = record.FirstAnesthesiologist.Name,
                        Registration = record.FirstAnesthesiologist.Registration
                    };
                }

                if (record.SecondAnesthesiologist != null)
                {
                    patient.SecondAnesthesiologist = new ResponsibleResponse
                    {
                        Id = record.SecondAnesthesiologist.Id,
                        FullName = record.SecondAnesthesiologist.Name,
                        Registration = record.SecondAnesthesiologist.Registration
                    };
                }

                if (record.Surgeon != null)
                {
                    patient.Surgeon = new ResponsibleResponse
                    {
                        Id = record.Surgeon.Id,
                        FullName = record.Surgeon.Name,
                        Registration = record.Surgeon.Registration
                    };
                }

                if (record.Assistant != null)
                {
                    patient.Assistant = new ResponsibleResponse
                    {
                        Id = record.Assistant.Id,
                        FullName = record.Assistant.Name,
                        Registration = record.Assistant.Registration
                    };
                }
            }
        }

        public async Task<CommandResult> GetPatientAnesthesiaRecordByIdAsync(string patientId, int surgeryId)
        {
            var patient = await _hospitalApiRepository
                .GetFromHospitalByPatientIdAndSurgeryIdAsync(patientId, surgeryId);

            if (patient == null)
                return null;

            var anesthesiaRecord = await _anesthesiaRecordRepository.GetByIdAsync(surgeryId);

            var isPreAnesthesiaRecordDone =
                await _preAnesthesiaRecordRepository
                    .ExistsByAnesthesiaRecordIdAsync(surgeryId);

            return CommandResult.Success(PatientResponseMapper.MapDetail(patient, anesthesiaRecord?.FirstAnesthesiologist, anesthesiaRecord?.SecondAnesthesiologist,
                 anesthesiaRecord?.Surgeon, anesthesiaRecord?.Assistant, isPreAnesthesiaRecordDone));
        }

        public async Task<CommandResult> AssumePatientAsync(string patientId, int surgeryId, int? responsibleAnesthesiologistId)
        {
            var patient = await _hospitalApiRepository.GetFromHospitalByPatientIdAndSurgeryIdAsync(patientId, surgeryId);

            if (patient == null)
                throw new Exception("Paciente não encontrado");

            User responsibleAnesthesiologist = null;

            if (responsibleAnesthesiologistId > 0)
                responsibleAnesthesiologist = await _userRepository.GetUserByIdAsync(responsibleAnesthesiologistId.Value);

            var anesthesiaRecord = await _anesthesiaRecordRepository.GetByIdAsync(surgeryId);

            try
            {
                if (anesthesiaRecord == null)
                {
                    anesthesiaRecord = AnesthesiaRecord.Create(new AnesthesiaRecordCommand
                    {
                        SurgeryId = surgeryId,
                        Status = SurgeryStatusEnum.Preparing,
                        PatientId = patientId,
                        FirstAnesthesiologistId = responsibleAnesthesiologistId
                    }, patient.SurgeryDate);

                    await _anesthesiaRecordRepository.AddAsync(anesthesiaRecord);
                }
                else
                {
                    anesthesiaRecord.AssignFirstAnesthesiologistId(responsibleAnesthesiologistId);
                    anesthesiaRecord.SetStatus(SurgeryStatusEnum.Preparing);

                    _anesthesiaRecordRepository.Update(anesthesiaRecord);
                }

                var monitoring = MonitoringRecord.Create(new MonitoringRecordCommand(anesthesiaRecord.Id));

                monitoring.SetAnesthesiaRecord(anesthesiaRecord);

                await _monitoringRecordRepository.AddAsync(monitoring);

                await _anesthesiaRecordRepository.SaveChangesAsync();

                return CommandResult.Success(PatientResponseMapper.MapDetail(patient, responsibleAnesthesiologist, null, null, null, true));
            }
            catch (Exception ex)
            {
                return CommandResult.Fail(ex.Message);
            }
        }
    }
}