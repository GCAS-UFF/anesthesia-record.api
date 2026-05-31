using UFF.FichaAnestesica.Domain.Dto;
using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Enums;
using UFF.FichaAnestesica.Domain.Response;

namespace UFF.FichaAnestesica.Service.Mappers
{
    public static class PatientResponseMapper
    {
        public static PatientSurgeryResponse Map(PatientListDto patient, User? user)
        {
            if (patient == null)
                return null;

            return new PatientSurgeryResponse
            {
                SurgeryId = patient.SurgeryId,
                PatientId = patient.PatientId,
                MedicalRecordNumber = patient.MedicalRecordNumber,
                FullName = patient.FullName,
                BirthDate = patient.BirthDate,
                Age = CalculateAge(patient.BirthDate),
                //SurgeryStatus = ParseStatus(patient.SurgeryStatus),
                ExpectedAt = patient.ExpectedAt,
                Room = patient.Room,
                Status = user != null ? AnesthesiaRecordStatus.InProgress : AnesthesiaRecordStatus.Scheduled,
                Procedures = patient.Procedures?
                    .Select(p => new ProcedureResponse
                    {
                        Id = p.Id,
                        Description = p.Description
                    })
                    .ToList() ?? new List<ProcedureResponse>(),
                Allergies = patient.Allergies?
                    .Select(MapAllergy)
                    .ToList() ?? new List<AllergyResponse>(),
                FirstAnesthesiologist =  null,
                SecondAnesthesiologist = null
            };
        }

        public static List<PatientSurgeryResponse> Map(IEnumerable<PatientListDto> patients)
        {
            if (patients == null || !patients.Any())
                return null;

            var patientsList = new List<PatientSurgeryResponse>();

            foreach (var patient in patients)
            {
                patientsList.Add(new PatientSurgeryResponse
                {
                    SurgeryId = patient.SurgeryId,
                    PatientId = patient.PatientId,
                    MedicalRecordNumber = patient.MedicalRecordNumber,
                    FullName = patient.FullName,                    
                    BirthDate = patient.BirthDate,
                    Age = CalculateAge(patient.BirthDate),
                    //SurgeryStatus = ParseStatus(patient.SurgeryStatus),
                    ExpectedAt = patient.ExpectedAt,
                    Room = patient.Room,                    
                    Procedures = patient.Procedures?
                   .Select(p => new ProcedureResponse
                   {
                       Id = p.Id,
                       Description = p.Description,
                       IsPrimary = p.IsPrimary,
                       Cid = p.Cid
                   }).ToList() ?? new List<ProcedureResponse>(),
                    Allergies = patient.Allergies?
                   .Select(MapAllergy).ToList() ?? new List<AllergyResponse>(),
                    FirstAnesthesiologist = null,
                    SecondAnesthesiologist = null
                });
            }

            return patientsList;
        }

        public static PatientSurgeryResponse MapDetail(PatientDto patient, User? firstAnesthesiologist, User? secondAnesthesiologist, User? surgeon, User? assistant, AnesthesiaRecordStatus status)
        {
            if (patient == null)
                return null;

            return new PatientSurgeryResponse
            {
                SurgeryId = patient.Id,
                Status = status,

                PatientId = patient.PatientCode,
                MedicalRecordNumber = patient.MedicalRecordNumber,
                FullName = patient.FullName,
                BirthDate = patient.BirthDate,
                Age = CalculateAge(patient.BirthDate),
                Gender = patient.Gender,

                WeightKg = patient.WeightKg,
                HeightCm = patient.HeightCm,

                CurrentLocation = MapLocation(patient.CurrentLocation),

                Allergies = patient.Allergies?
                    .Select(MapAllergy)
                    .ToList() ?? new List<AllergyResponse>(),

                Surgeries = patient.Surgeries?
                    .Select(MapSurgery)
                    .ToList() ?? new List<SurgeryResponse>(),

                FirstAnesthesiologist = MapResponsible(firstAnesthesiologist),
                SecondAnesthesiologist = MapResponsible(secondAnesthesiologist),
                Surgeon = MapResponsible(surgeon),
                Assistant = MapResponsible(assistant)
            };
        }

        public static List<PatientSurgeryResponse> Map(IEnumerable<PatientListDto> patients, User user)
            => patients.Select(p => Map(p, user)).ToList();

        private static int CalculateAge(DateTime birthDate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;

            if (birthDate.Date > today.AddYears(-age))
                age--;

            return age;
        }

        private static ResponsibleResponse? MapResponsible(User? responsible)
        {
            if (responsible == null)
                return null;

            return new ResponsibleResponse
            {
                Id = responsible.Id,
                FullName = responsible.Name,
                Registration = responsible.Registration
            };
        }

        private static PatientLocationResponse? MapLocation(CurrentLocationDto? location)
        {
            if (location == null)
                return null;

            return new PatientLocationResponse
            {
                Unit = location.Unit == null
                    ? null
                    : new UnitResponse
                    {
                        Code = location.Unit.Code,
                        Description = location.Unit.Description
                    },
                Bed = location.Bed,
                Floor = location.Floor,
                Room = location.Room
            };
        }

        private static SurgeryResponse MapSurgery(SurgeryDto surgery)
        {
            return new SurgeryResponse
            {
                Id = surgery.Id,

                SurgeryDate = surgery.SurgeryDate,
                Status = ParseStatus(surgery.SurgeryStatus),
                Specialty = surgery.Specialty == null
                    ? null
                    : new SpecialtyResponse
                    {
                        Code = surgery.Specialty.Code,
                        Description = surgery.Specialty.Description
                    },
                Location = surgery.Location == null
                    ? null
                    : new SurgeryLocationResponse
                    {
                        Room = surgery.Location.Room,
                        SurgicalCenter = surgery.Location.SurgicalCenter == null
                            ? null
                            : new SurgicalCenterResponse
                            {
                                Code = surgery.Location.SurgicalCenter.Code,
                                Description = surgery.Location.SurgicalCenter.Description
                            }
                    },

                Procedures = surgery.Procedures?
                    .Select(p => new ProcedureResponse
                    {
                        Id = p.Id,
                        Description = p.Description,
                        Cid = p.Cid,
                        IsPrimary = p.IsPrimary
                    })
                    .ToList() ?? new List<ProcedureResponse>()
            };
        }

        private static AllergyResponse MapAllergy(AllergyDto allergy)
        {
            return new AllergyResponse
            {
                RegisterDate = allergy.RegisterDate,
                Description = allergy.Description,
                Reason = allergy.Reason,
                AllergyCriticality = allergy.AllergyCriticality,
                CertaintyLevel = allergy.CertaintyLevel,
                CausativeAgent = allergy.CausativeAgent,
                Medication = allergy.Medication == null
                    ? null
                    : new MedicationResponse
                    {
                        Description = allergy.Medication.Description
                    }
            };
        }

        private static SurgeryStatusEnum ParseStatus(string status)
        {
            return status?.ToLower() switch
            {
                "agendada" => SurgeryStatusEnum.Scheduled,
                "em_andamento" => SurgeryStatusEnum.InProgress,
                "finalizada" => SurgeryStatusEnum.Completed,
                _ => SurgeryStatusEnum.Scheduled
            };
        }
    }
}