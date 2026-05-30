using UFF.FichaAnestesica.Domain.Dto;
using UFF.FichaAnestesica.Domain.Enums;
using UFF.FichaAnestesica.Domain.Response;

namespace UFF.FichaAnestesica.Service.Mappers
{
    public static class PatientResponseMapper
    {
        public static PatientSurgeryResponse Map(PatientDto patient)
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
                Gender = patient.Gender == "M"
                    ? "M"
                    : "F",
                WeightKg = patient.WeightKg,
                HeightCm = patient.HeightCm,
                CurrentLocation = MapLocation(patient.CurrentLocation),
                Surgeries = patient.Surgeries?
                    .Select(MapSurgery)
                    .ToList() ?? new List<SurgeryResponse>(),

                FirstAnesthesiologist = MapResponsible(
                    patient.ResponsibleAnesthesiologist
                ),
                Allergies = patient.Allergies?
                    .Select(MapAllergy)
                    .ToList() ?? new List<AllergyResponse>()
            };
        }

        public static List<PatientSurgeryResponse> Map(IEnumerable<PatientDto> patients)
        {
            return patients
                .Select(Map)
                .ToList();
        }

        private static int CalculateAge(DateTime birthDate)
        {
            var today = DateTime.Today;

            var age = today.Year - birthDate.Year;

            if (birthDate.Date > today.AddYears(-age))
                age--;

            return age;
        }

        private static ResponsibleResponse? MapResponsible(UserDto? responsible)
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
                Id = surgery.SurgeryId,

                SurgeryDate = surgery.SurgeryDate,

                Status = ParseStatus(surgery.Status),

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
                        Id = p.ExternalId,
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
                CreatedAt = allergy.CreatedAt,
                Description = allergy.Description,
                Reason = allergy.Reason,
                AllergyCriticality = allergy.AllergyCriticality,
                CertaintyLevel = allergy.CertaintyLevel,
                AllergyManifestation = allergy.AllergyManifestation,
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
                "cancelada" => SurgeryStatusEnum.Cancelled,
                _ => SurgeryStatusEnum.Scheduled
            };
        }
    }
}