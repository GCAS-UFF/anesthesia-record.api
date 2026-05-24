using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Enums;
using UFF.FichaAnestesica.Domain.Response;

namespace UFF.FichaAnestesica.Service.Mappers
{
    public static class PatientResponseMapper
    {
        public static PatientSurgeryResponse Map(Patient patient)
        {
            if (patient == null)
                return null;

            return new PatientSurgeryResponse
            {
                Id = patient.Id,
                PatientId = patient.PatientId,
                MedicalRecordNumber = patient.MedicalRecordNumber,
                FullName = patient.FullName,
                BirthDate = patient.BirthDate,

                Age = CalculateAge(patient.BirthDate),

                Gender = patient.Gender == GenderEnum.Male
                    ? "M"
                    : "F",

                WeightKg = patient.WeightKg,
                HeightCm = patient.HeightCm,

                CurrentLocation = MapLocation(patient),

                Surgeries = patient.Surgeries?
                    .Select(MapSurgery)
                    .ToList() ?? new List<SurgeryResponse>()
            };
        }

        public static List<PatientSurgeryResponse> Map(IEnumerable<Patient> patients)
        {
            return patients.Select(Map).ToList();
        }

        private static int CalculateAge(DateTime birthDate)
        {
            var today = DateTime.Today;

            var age = today.Year - birthDate.Year;

            if (birthDate.Date > today.AddYears(-age))
                age--;

            return age;
        }

        private static PatientLocationResponse? MapLocation(Patient patient)
        {
            if (patient.CurrentLocation == null)
                return null;

            return new PatientLocationResponse
            {
                Unit = patient.CurrentLocation.Unit == null
                    ? null
                    : new UnitResponse
                    {
                        Code = patient.CurrentLocation.Unit.Code,
                        Description = patient.CurrentLocation.Unit.Description
                    },

                Bed = patient.CurrentLocation.Bed,
                Floor = patient.CurrentLocation.Floor,
                Room = patient.CurrentLocation.Room
            };
        }

        private static SurgeryResponse MapSurgery(Surgery surgery)
        {
            return new SurgeryResponse
            {
                Id = surgery.Id,

                SurgeryDate = surgery.SurgeryDate,

                Status = surgery.Status,

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
    }
}