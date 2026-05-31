using UFF.FichaAnestesica.Domain.Commands;
using UFF.FichaAnestesica.Domain.Commands.AnesthesiaRecord;
using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Enums;
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

        public SurgeryService(IUserRepository userRepository, IPatientReadOnlyRepository hospitalApiRepository, IAnesthesiaRecordRepository anesthesiaRecordRepository, IMonitoringRecordRepository monitoringRecordRepository)
        {
            _userRepository = userRepository;
            _hospitalApiRepository = hospitalApiRepository;
            _anesthesiaRecordRepository = anesthesiaRecordRepository;
            _monitoringRecordRepository = monitoringRecordRepository;
        }

        public async Task<CommandResult> GetPatientsWithSurgeriesAsync(DateTime? date, SurgeryStatusEnum? status, int page = 1, int size = 10)
        {
            if (date.HasValue)
                date = DateTime.SpecifyKind(date.Value, DateTimeKind.Utc);

            var hospitalData = await _hospitalApiRepository.GetPatientsFromHospitalAsync(date, status, page, size);

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
            var recordsBySurgeryId = anesthesiaRecords.GroupBy(x => x.Id).ToDictionary(x => x.Key, x => x.First());
            SetSurgeryStatus(hospitalData, recordsBySurgeryId);

            var responseData = PatientResponseMapper.Map(hospitalData.Data);
            var recordsByPatientId = anesthesiaRecords.GroupBy(x => x.Id).ToDictionary(x => x.Key, x => x.First());

            foreach (var patient in responseData)
            {
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
                TotalItems = hospitalData.TotalItems
            });
        }

        private static void SetSurgeryStatus(PagedResponse<Domain.Dto.PatientListDto> hospitalData, Dictionary<int, AnesthesiaRecord> recordsBySurgeryId)
        {
            foreach (var patient in hospitalData.Data)
            {
                if (recordsBySurgeryId.TryGetValue(patient.SurgeryId, out var record))
                {
                    patient.HaveFirstAnesthesist = record.FirstAnesthesiologist != null;
                }
            }
        }

        public async Task<CommandResult> GetPatientAnesthesiaRecordByIdAsync(string patientId, int surgeryId)
        {
            var patient = await _hospitalApiRepository.GetFromHospitalByPatientIdAndSurgeryIdAsync(patientId, surgeryId);

            if (patient == null)
                return null;

            var anesthesiaRecord = await _anesthesiaRecordRepository.GetByIdAsync(surgeryId);

            return CommandResult.Success(PatientResponseMapper.MapDetail(patient, anesthesiaRecord?.FirstAnesthesiologist, anesthesiaRecord?.SecondAnesthesiologist, anesthesiaRecord?.Surgeon, anesthesiaRecord?.Assistant));
        }

        public async Task<CommandResult> AssumePatientAsync(string patientId, int surgeryId, int responsibleAnesthesiologistId)
        {
            var patient = await _hospitalApiRepository.GetFromHospitalByPatientIdAndSurgeryIdAsync(patientId, surgeryId);

            if (patient == null)
                throw new Exception("Paciente não encontrado");

            var responsibleAnesthesiologist = await _userRepository.GetUserByIdAsync(responsibleAnesthesiologistId);

            if (responsibleAnesthesiologist == null)
                throw new Exception("Médico não encontrado");

            var anesthesiaRecord = await _anesthesiaRecordRepository.GetByIdAsync(surgeryId);

            try
            {
                if (anesthesiaRecord == null)
                {
                    anesthesiaRecord = AnesthesiaRecord.Create(new AnesthesiaRecordCommand
                    {
                        Status = SurgeryStatusEnum.InProgress,
                        ExternalPatientId = patientId,
                        FirstAnesthesiologistId = responsibleAnesthesiologistId,
                        RecordDate = DateOnly.FromDateTime(DateTime.Today)
                    });

                    await _anesthesiaRecordRepository.AddAsync(anesthesiaRecord);
                }
                else
                {
                    anesthesiaRecord.AssignFirstAnesthesiologistId(responsibleAnesthesiologistId);
                    anesthesiaRecord.SetStatus(SurgeryStatusEnum.InProgress);

                    _anesthesiaRecordRepository.Update(anesthesiaRecord);
                }

                var monitoring = MonitoringRecord.Create(new MonitoringRecordCommand(anesthesiaRecord.Id));

                monitoring.SetAnesthesiaRecord(anesthesiaRecord);

                await _monitoringRecordRepository.AddAsync(monitoring);

                await _anesthesiaRecordRepository.SaveChangesAsync();

                return CommandResult.Success(PatientResponseMapper.MapDetail(patient, responsibleAnesthesiologist, null, null, null));
            }
            catch (Exception ex)
            {
                return CommandResult.Fail(ex.Message);
            }
        }
    }
}