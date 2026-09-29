using UFF.FichaAnestesica.CrossCutting.Mappings;
using UFF.FichaAnestesica.Domain.Commands;
using UFF.FichaAnestesica.Domain.Commands.AnesthesiaRecord;
using UFF.FichaAnestesica.Domain.Dto;
using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Enums;
using UFF.FichaAnestesica.Domain.Extensions;
using UFF.FichaAnestesica.Domain.Helpers;
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
        private readonly IProcedureRepository _procedureRepository;

        public SurgeryService(IUserRepository userRepository, IPatientReadOnlyRepository hospitalApiRepository, IAnesthesiaRecordRepository anesthesiaRecordRepository, IMonitoringRecordRepository monitoringRecordRepository, IPreAnesthesiaRecordRepository preAnesthesiaRecordRepository, IProcedureRepository procedureRepository)
        {
            _userRepository = userRepository;
            _hospitalApiRepository = hospitalApiRepository;
            _anesthesiaRecordRepository = anesthesiaRecordRepository;
            _monitoringRecordRepository = monitoringRecordRepository;
            _preAnesthesiaRecordRepository = preAnesthesiaRecordRepository;
            _procedureRepository = procedureRepository;
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
            var startedMonitoringIds = GetStartedMonitoringIds(anesthesiaRecordIds);

            var recordsBySurgeryId = anesthesiaRecords.GroupBy(x => x.Id).ToDictionary(x => x.Key, x => x.First());

            var effectiveStatusBySurgeryId = SetSurgeryStatus(hospitalData, recordsBySurgeryId);

            if (status.HasValue && status != SurgeryStatusEnum.Completed)
            {
                hospitalData.Data = hospitalData.Data
                    .Where(x => effectiveStatusBySurgeryId.TryGetValue(x.SurgeryId, out var effectiveStatus) && effectiveStatus == status.Value)
                    .ToList();
                hospitalData.TotalItems = hospitalData.Data.Count();
            }

            await AssociateSurgeryProcedures(hospitalData, recordsBySurgeryId);
            await _anesthesiaRecordRepository.SaveChangesAsync();


            var responseData = PatientResponseMapper.Map(hospitalData.Data, recordsBySurgeryId);
            var recordsByPatientId = anesthesiaRecords.GroupBy(x => x.Id).ToDictionary(x => x.Key, x => x.First());

            var canAssumePatient = await _anesthesiaRecordRepository.CanAssumePatientsAsync(doctorId);

            foreach (var patient in responseData)
            {
                patient.IsPreAnesthesiaRecordDone = completedPreAnesthesiaRecordIds.Contains(patient.SurgeryId);
                patient.IsMonitoringStarted = startedMonitoringIds.Contains(patient.SurgeryId);

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

        private static Dictionary<int, SurgeryStatusEnum> SetSurgeryStatus(PagedResponse<PatientDetailDto> hospitalData, Dictionary<int, AnesthesiaRecord> recordsBySurgeryId)
        {
            var effectiveStatusBySurgeryId = new Dictionary<int, SurgeryStatusEnum>();

            foreach (var patient in hospitalData.Data)
            {
                recordsBySurgeryId.TryGetValue(patient.SurgeryId, out var record);

                patient.HaveFirstAnesthesist = record?.FirstAnesthesiologist != null;

                var rawStatus = record == null || record.Status == 0
                    ? SurgeryStatusEnumMapping.Parse(patient.Status)
                    : record.Status;

                var effectiveStatus = SurgeryStatusDerivation.DeriveEffectiveStatus(rawStatus, patient.HaveFirstAnesthesist);

                patient.Status = effectiveStatus.GetDescription();
                effectiveStatusBySurgeryId[patient.SurgeryId] = effectiveStatus;
            }

            return effectiveStatusBySurgeryId;
        }

    
        private async Task AssociateSurgeryProcedures(PagedResponse<PatientDetailDto> hospitalData, Dictionary<int, AnesthesiaRecord> recordsBySurgeryId)
        {
            var externalIds = hospitalData.Data
                .SelectMany(x => x.Procedures ?? [])
                .Select(x => x.ExternalId.ToString())
                .Distinct()
                .ToList();

            var catalog = (await _procedureRepository.GetByIdsAsync(externalIds) ?? [])
                .GroupBy(x => x.ExternalId)
                .ToDictionary(x => x.Key, x => x.First());

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

                // O AGHU identifica o procedimento pelo id externo; a relação usa o id local do catálogo.
                // Procedimento ainda não importado no catálogo fica de fora até a próxima sincronização.
                var choices = (surgery.Procedures ?? [])
                    .Where(x => catalog.ContainsKey(x.ExternalId.ToString()))
                    .Select(x => new ProcedureChoice(catalog[x.ExternalId.ToString()], x.IsPrimary, x.Time))
                    .ToList();

                if (!choices.Any() && surgery.Procedures?.Any() == true)
                    continue;

                record.SyncProceduresFromAghu(choices);
            }
        }

        public async Task<CommandResult> GetMyPatientsAsync(int doctorId, DateTime? date, string term, SurgeryStatusEnum? status, int page = 1, int size = 10)
        {
            if (date.HasValue)
                date = DateTime.SpecifyKind(date.Value, DateTimeKind.Utc);

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
            ApplyMonitoringStarted(responseData, GetStartedMonitoringIds(responseData.Select(x => x.SurgeryId)));

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
            ApplyMonitoringStarted(responseData, GetStartedMonitoringIds(responseData.Select(x => x.SurgeryId)));
            
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

        private HashSet<int> GetStartedMonitoringIds(IEnumerable<int> anesthesiaRecordIds)
        {
            return _monitoringRecordRepository.GetStartedAnesthesiaRecordIds(anesthesiaRecordIds.ToList()) ?? new HashSet<int>();
        }

        private static void ApplyMonitoringStarted(List<PatientSurgeryResponse> responseData, HashSet<int> startedMonitoringIds)
        {
            foreach (var patient in responseData)
                patient.IsMonitoringStarted = startedMonitoringIds.Contains(patient.SurgeryId);
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
                 anesthesiaRecord?.Surgeon, anesthesiaRecord?.Assistant, isPreAnesthesiaRecordDone, anesthesiaRecord));
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
            var isNewAnesthesiaRecord = anesthesiaRecord == null;

            
            var isRemovingResponsible = !(responsibleAnesthesiologistId > 0);
            if (isRemovingResponsible && !isNewAnesthesiaRecord)
            {
                if (anesthesiaRecord.Status == SurgeryStatusEnum.Completed || anesthesiaRecord.Status == SurgeryStatusEnum.Canceled)
                    return CommandResult.Fail("Não é possível remover o médico responsável de uma cirurgia finalizada.");

                var currentMonitoring = await _monitoringRecordRepository.GetByAnesthesiaRecordIdAsync(anesthesiaRecord.Id);
                if (currentMonitoring?.StartedAt != null)
                    return CommandResult.Fail("Não é possível remover o médico responsável: o monitoramento já começou.");
            }

            try
            {
                if (isNewAnesthesiaRecord)
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
                else if (anesthesiaRecord.Status != SurgeryStatusEnum.Completed && anesthesiaRecord.Status != SurgeryStatusEnum.Canceled)
                {
                    if (responsibleAnesthesiologistId > 0)
                    {
                        anesthesiaRecord.AssignFirstAnesthesiologistId(responsibleAnesthesiologistId);
                        anesthesiaRecord.SetStatus(SurgeryStatusEnum.Preparing);
                    }
                    else
                    {                      
                        anesthesiaRecord.AssignFirstAnesthesiologistId(null);
                        anesthesiaRecord.SetStatus(SurgeryStatusEnum.Scheduled);
                    }

                    _anesthesiaRecordRepository.Update(anesthesiaRecord);
                }
                var hasMonitoring = await _monitoringRecordRepository.GetByAnesthesiaRecordIdAsync(anesthesiaRecord.Id) != null;

                if (!hasMonitoring)
                {                   
                    var monitoring = MonitoringRecord.Create(new MonitoringRecordCommand(anesthesiaRecord.Id)
                    {
                        RecordedByProfessionalId = responsibleAnesthesiologistId ?? 0
                    });

                    monitoring.SetAnesthesiaRecord(anesthesiaRecord);

                    await _monitoringRecordRepository.AddAsync(monitoring);
                }

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