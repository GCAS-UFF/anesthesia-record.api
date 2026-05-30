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

        public SurgeryService(IUserRepository userRepository, IPatientReadOnlyRepository hospitalApiRepository, IAnesthesiaRecordRepository anesthesiaRecordRepository)
        {
            _userRepository = userRepository;
            _hospitalApiRepository = hospitalApiRepository;
            _anesthesiaRecordRepository = anesthesiaRecordRepository;
        }
        public async Task<PagedResponse<PatientSurgeryResponse>> GetPatientsWithSurgeriesAsync(DateTime? date, SurgeryStatusEnum? status, int page = 1, int size = 10)
        {
            if (date.HasValue)
                date = DateTime.SpecifyKind(date.Value, DateTimeKind.Utc);

            var hospitalData = await _hospitalApiRepository.GetPatientsFromHospitalAsync(date, status, page, size);

            if (hospitalData.Data == null || !hospitalData.Data.Any())
            {
                return new PagedResponse<PatientSurgeryResponse>
                {
                    Data = [],
                    Page = hospitalData.Page,
                    PageSize = hospitalData.PageSize,
                    TotalItems = hospitalData.TotalItems
                };
            }

            var responseData = PatientResponseMapper.Map(hospitalData.Data);
            var patientIds = responseData.Select(x => x.PatientId).Distinct().ToArray();
            var anesthesiaRecords = await _anesthesiaRecordRepository.GetByIdsAsync(patientIds);
            var recordsByPatientId = anesthesiaRecords.GroupBy(x => x.ExternalPatientId).ToDictionary(x => x.Key, x => x.First());

            foreach (var patient in responseData)
            {
                if (!recordsByPatientId.TryGetValue(patient.PatientId, out var record))
                    continue;

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

                patient.FirstAnesthesiologist = patient.FirstAnesthesiologist;
            }

            return new PagedResponse<PatientSurgeryResponse>
            {
                Data = responseData,
                Page = hospitalData.Page,
                PageSize = hospitalData.PageSize,
                TotalItems = hospitalData.TotalItems
            };
        }

        public async Task<PatientSurgeryResponse?> GetPatientByIdAsync(string id)
        {
            var surgery = await _hospitalApiRepository.GetPatientFromHospitalByIdAsync(id);

            if (surgery == null)
                return null;

            return PatientResponseMapper.Map(surgery);
        }

        public async Task<PatientSurgeryResponse> AssumePatientAsync(string patientId, int responsibleAnesthesiologistId)
        {
            var hospitalData = await _hospitalApiRepository.GetPatientsFromHospitalAsync(null, null, 1, int.MaxValue);
            var patient = hospitalData.Data.FirstOrDefault(x => x.PatientId == patientId);

            if (patient == null)
                throw new Exception("Paciente não encontrado");

            //var anesthesiaRecord = _anesthesiaRecordRepository.GetByIdAsync()


            var responsibleAnesthesiologist = await _userRepository.GetUserByIdAsync(responsibleAnesthesiologistId);

            if (responsibleAnesthesiologist == null)
                throw new Exception("Médico não encontrado");

            patient.ResponsibleAnesthesiologist = new Domain.Dto.UserDto
            {
                Id = responsibleAnesthesiologist.ExternalId,
                Name = responsibleAnesthesiologist.Name,
                Registration = responsibleAnesthesiologist.Registration
            };

            return PatientResponseMapper.Map(patient);
        }
    }
}