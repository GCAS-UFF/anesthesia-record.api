using UFF.FichaAnestesica.Domain.Dto;
using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Enums;
using UFF.FichaAnestesica.Domain.Extensions;
using UFF.FichaAnestesica.Domain.Repositories;
using UFF.FichaAnestesica.Domain.Repositories.ReadOnly;
using UFF.FichaAnestesica.Domain.Response;
using UFF.FichaAnestesica.Domain.Services;

namespace UFF.FichaAnestesica.Service.Services
{
    public class SurgeryService : ISurgeryService
    {
        private readonly IHospitalReadRepository _hospitalReadRepository;
        private readonly ISurgeryRepository _surgeryRepository;

        public SurgeryService(
            IHospitalReadRepository hospitalReadRepository,
            ISurgeryRepository surgeryRepository)
        {
            _hospitalReadRepository = hospitalReadRepository;
            _surgeryRepository = surgeryRepository;
        }

        public async Task<PagedResponse<PatientSurgeryResponse>> GetPatientsWithSurgeriesAsync(DateTime? date, SurgeryStatus? status, int page = 1, int size = 10)
        {
            var filterDate = date ?? DateTime.UtcNow;

            var hospitalData = await _hospitalReadRepository.GetSurgeriesFromHospitalAsync(filterDate, status, page, size);

            if (hospitalData.Data == null || !hospitalData.Data.Any())
            {
                return new PagedResponse<PatientSurgeryResponse>
                {
                    Data = new List<PatientSurgeryResponse>(),
                    Page = page,
                    PageSize = size,
                    TotalItems = hospitalData.TotalItems
                };
            }

            var patients = MapToPatientEntities(hospitalData.Data);

            await _surgeryRepository.AddOrUpdatePatientsAsync(patients);

            var savedPatients = await _surgeryRepository.GetPatientsWithSurgeriesAsync(filterDate, status);

            var patientsList = savedPatients.ToList();
            patientsList = ApplyOrdering(patientsList);

            var totalCount = patientsList.Count;
            var totalPages = (int)Math.Ceiling(totalCount / (double)size);

            var pagedPatients = patientsList
                .Skip((page - 1) * size)
                .Take(size)
                .ToList();

            var responseData = MapToResponse(pagedPatients);

            return new PagedResponse<PatientSurgeryResponse>
            {
                Data = responseData,
                Page = page,
                PageSize = size,
                TotalItems = hospitalData.TotalItems
            };
        }

        private List<Patient> ApplyOrdering(List<Patient> patients, bool ascending = true, string orderBy = "surgerydate")
        {
            return orderBy?.ToLower() switch
            {
                "fullname" => ascending
                    ? patients.OrderBy(p => p.FullName).ToList()
                    : patients.OrderByDescending(p => p.FullName).ToList(),
                "medicalrecordnumber" => ascending
                    ? patients.OrderBy(p => p.MedicalRecordNumber).ToList()
                    : patients.OrderByDescending(p => p.MedicalRecordNumber).ToList(),
                "birthdate" => ascending
                    ? patients.OrderBy(p => p.BirthDate).ToList()
                    : patients.OrderByDescending(p => p.BirthDate).ToList(),
                "surgeriescount" => ascending
                    ? patients.OrderBy(p => p.Surgeries.Count).ToList()
                    : patients.OrderByDescending(p => p.Surgeries.Count).ToList(),
                "surgerydate" => ascending
                    ? patients.OrderBy(p => p.Surgeries.Any() ? p.Surgeries.Min(s => s.SurgeryDate) : DateTime.MaxValue).ToList()
                    : patients.OrderByDescending(p => p.Surgeries.Any() ? p.Surgeries.Max(s => s.SurgeryDate) : DateTime.MinValue).ToList(),
                _ => ascending
                    ? patients.OrderBy(p => p.FullName).ToList()
                    : patients.OrderByDescending(p => p.FullName).ToList()
            };
        }
        private List<Patient> MapToPatientEntities(IEnumerable<PatientViewDto> viewData)
        {
            var patientsDict = new Dictionary<string, Patient>();

            foreach (var item in viewData)
            {
                if (string.IsNullOrEmpty(item.Id))
                    continue;

                if (!patientsDict.TryGetValue(item.Id, out var patient))
                {
                    var unit = string.IsNullOrEmpty(item.UnitCode)
                        ? null
                        : Unit.Create(item.UnitCode, item.UnitDescription);

                    var currentLocation = CurrentLocation.Create(
                        item.Bed,
                        item.Floor,
                        item.Room,
                        unit
                    );

                    patient = Patient.Create(
                        item.Id,
                        item.MedicalRecordNumber,
                        item.FullName,
                        item.BirthDate,
                        item.Gender == "M" ? GenderEnum.Male : GenderEnum.Female,
                        item.WeightKg,
                        item.HeightCm,
                        currentLocation
                    );

                    patientsDict[item.Id] = patient;
                }

                if (!string.IsNullOrEmpty(item.SurgeryId))
                {
                    var surgery = patient.Surgeries
                        .FirstOrDefault(s => s.PatientId == item.Id);

                    if (surgery == null)
                    {
                        var specialty = string.IsNullOrEmpty(item.SpecialtyCode)
                            ? null
                            : Specialty.Create(item.SpecialtyCode, item.SpecialtyDescription);

                        var surgicalCenter = string.IsNullOrEmpty(item.SurgicalCenterCode)
                            ? null
                            : SurgicalCenter.Create(item.SurgicalCenterCode, item.SurgicalCenterDescription);

                        var surgeryLocation = SurgeryLocation.Create(
                            item.SurgeryRoom,
                            surgicalCenter
                        );

                        surgery = Surgery.Create(
                            item.SurgeryId,
                            item.SurgeryDate,
                            item.SurgeryStatus.ToSurgeryStatus(),
                            patient.PatientId,
                            specialty,
                            surgeryLocation
                        );

                        patient.SyncSurgery(surgery);
                    }

                    if (!string.IsNullOrEmpty(item.ProcedureId))
                    {
                        var procedure = surgery.Procedures
                            .FirstOrDefault(p => p.ExternalId == item.ProcedureId);

                        if (procedure == null)
                        {
                            procedure = Procedure.Create(
                                item.ProcedureId,
                                item.ProcedureDescription,
                                item.ProcedureCid,
                                item.IsPrimaryProcedure
                            );

                            surgery.AddProcedure(procedure);
                        }
                    }
                }
            }

            return patientsDict.Values.ToList();
        }

        private List<PatientSurgeryResponse> MapToResponse(IEnumerable<Patient> patients)
        {
            return patients.Select(p => new PatientSurgeryResponse
            {
                PatientId = p.PatientId,
                MedicalRecordNumber = p.MedicalRecordNumber,
                FullName = p.FullName,
                BirthDate = p.BirthDate,
                Age = CalculateAge(p.BirthDate),
                Gender = p.Gender == GenderEnum.Male ? "M" : "F",
                WeightKg = p.WeightKg,
                HeightCm = p.HeightCm,
                CurrentLocation = p.CurrentLocation != null ? new PatientLocationResponse
                {
                    Unit = new UnitResponse
                    {
                        Code = p.CurrentLocation.Unit?.Code,
                        Description = p.CurrentLocation.Unit?.Description
                    },
                    Bed = p.CurrentLocation.Bed,
                    Floor = p.CurrentLocation.Floor,
                    Room = p.CurrentLocation.Room
                } : null,
                Surgeries = p.Surgeries?.Select(s => new SurgeryResponse
                {
                    Id = s.Id,
                    SurgeryDate = s.SurgeryDate,
                    Status = s.Status,
                    Specialty = new SpecialtyResponse
                    {
                        Code = s.Specialty?.Code,
                        Description = s.Specialty?.Description
                    },
                    Location = new SurgeryLocationResponse
                    {
                        SurgicalCenter = new SurgicalCenterResponse
                        {
                            Code = s.Location?.SurgicalCenter?.Code,
                            Description = s.Location?.SurgicalCenter?.Description
                        },
                        Room = s.Location?.Room
                    },
                    Procedures = s.Procedures?.Select(pr => new ProcedureResponse
                    {
                        Id = pr.ExternalId,
                        Description = pr.Description,
                        Cid = pr.Cid,
                        IsPrimary = pr.IsPrimary
                    }).ToList() ?? new List<ProcedureResponse>()
                }).ToList() ?? new List<SurgeryResponse>()
            }).ToList();
        }
        private int CalculateAge(DateTime birthDate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;
            if (birthDate.Date > today.AddYears(-age)) age--;
            return age;
        }
    }
}