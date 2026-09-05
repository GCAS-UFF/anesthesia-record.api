using UFF.FichaAnestesica.CrossCutting.Mappings;
using UFF.FichaAnestesica.Domain.Dto;
using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Enums;
using UFF.FichaAnestesica.Domain.Response;

namespace UFF.FichaAnestesica.Service.Mappers
{
    public static class PatientResponseMapper
    {
        public static List<PatientSurgeryResponse> Map(IEnumerable<PatientDetailDto> patients, Dictionary<int, AnesthesiaRecord> recordsBySurgeryId)
        {
            if (patients == null || !patients.Any())
                return [];

            var patientsList = new List<PatientSurgeryResponse>();

            foreach (var patient in patients)
            {
                recordsBySurgeryId.TryGetValue(patient.SurgeryId, out var record);

                List<ProcedureResponse> procedures;

                if (record != null && record.Surgeries.Any() && record.ProceduresCustomized)
                {
                    procedures = record.Surgeries
                        .OrderByDescending(x => x.IsPrimary)
                        .Select(s =>
                        {
                            return new ProcedureResponse
                            {
                                Id = s.ProcedureId.ToString(),
                                Description = record.Surgeries.FirstOrDefault(x => x.ProcedureId == s.ProcedureId).Procedure.Description,
                                Cid = record.Surgeries.FirstOrDefault(x => x.ProcedureId == s.ProcedureId).Procedure.Cid,
                                IsPrimary = record.Surgeries.FirstOrDefault(x => x.ProcedureId == s.ProcedureId).IsPrimary
                            };
                        })
                        .ToList();
                }
                else
                {
                    procedures = patient.Procedures?
                        .Select(p => new ProcedureResponse
                        {
                            Id = p.ExternalId.ToString(),
                            Description = p.Description,
                            IsPrimary = p.IsPrimary,
                            Cid = p.Cid
                        })
                        .ToList() ?? [];
                }

                patientsList.Add(new PatientSurgeryResponse
                {
                    SurgeryId = patient.SurgeryId,
                    PatientId = patient.PatientId,
                    SurgeryDate = (patient.ExpectedAt.HasValue && patient.ExpectedAt.Value != DateTime.MinValue) ? patient.ExpectedAt.Value : patient.SurgeryDate,
                    MedicalRecordNumber = patient.MedicalRecordNumber,
                    FullName = patient.FullName,
                    BirthDate = patient.BirthDate,
                    Gender = patient.Gender,
                    WeightKg = patient.WeightKg,
                    HeightCm = patient.HeightCm,
                    Status = SurgeryStatusEnumMapping.Parse(patient.Status),
                    Age = CalculateAge(patient.BirthDate),
                    ExpectedAt = patient.ExpectedAt,
                    Room = patient.Room,
                    Procedures = procedures,
                    Allergies = patient.Allergies?
                        .Select(MapAllergy)
                        .ToList() ?? [],
                    FirstAnesthesiologist = null,
                    SecondAnesthesiologist = null
                });
            }

            return patientsList;
        }

        public static PatientSurgeryResponse MapDetail(PatientDetailDto patient, User? firstAnesthesiologist, User? secondAnesthesiologist, User? surgeon, User? assistant, bool isPreAnesthesiaRecordDone)
        {
            if (patient == null)
                return null;

            return new PatientSurgeryResponse
            {
                SurgeryId = patient.SurgeryId,
                PatientId = patient.PatientId,
                SurgeryDate = patient.SurgeryDate,
                MedicalRecordNumber = patient.MedicalRecordNumber,
                FullName = patient.FullName,
                BirthDate = patient.BirthDate,

                IsPreAnesthesiaRecordDone = isPreAnesthesiaRecordDone,

                Age = CalculateAge(patient.BirthDate),
                Gender = patient.Gender,
                Status = firstAnesthesiologist != null &&
                         SurgeryStatusEnumMapping.Parse(patient.Status) != SurgeryStatusEnum.Completed
                    ? SurgeryStatusEnum.InProgress
                    : SurgeryStatusEnumMapping.Parse(patient.Status),

                WeightKg = patient.WeightKg,
                HeightCm = patient.HeightCm,
                CurrentLocation = MapLocation(patient.CurrentLocation),

                Allergies = patient.Allergies?
                    .Select(MapAllergy)
                    .ToList() ?? new List<Domain.Response.ListAllergyDto>(),

                Surgeries = patient.Surgeries?
                    .Select(MapSurgery)
                    .ToList() ?? new List<SurgeryResponse>(),

                FirstAnesthesiologist = MapResponsible(firstAnesthesiologist),
                SecondAnesthesiologist = MapResponsible(secondAnesthesiologist),
                Surgeon = MapResponsible(surgeon),
                Assistant = MapResponsible(assistant)
            };
        }

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

        private static SurgeryResponse MapSurgery(SurgeryDetailsDto surgery)
        {
            return new SurgeryResponse
            {
                Id = surgery.Id,
                SurgeryDate = surgery.SurgeryDate,
                Status = SurgeryStatusEnumMapping.Parse(surgery.SurgeryStatus),
                Specialty = surgery.Specialty == null
                    ? null
                    : new SpecialtyResponse
                    {
                        Code = surgery.Specialty.Id,
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
                                Code = surgery.Location.SurgicalCenter.Id,
                                Description = surgery.Location.SurgicalCenter.Description
                            }
                    },

                Procedures = surgery.Procedures?
                    .Select(p => new ProcedureResponse
                    {
                        Id = p.ExternalId.ToString(),
                        Description = p.Description,
                        Cid = p.Cid,
                        IsPrimary = p.IsPrimary
                    })
                    .ToList() ?? new List<ProcedureResponse>()
            };
        }

        private static Domain.Response.ListAllergyDto MapAllergy(Domain.Dto.ListAllergyDto allergy)
        {
            return new Domain.Response.ListAllergyDto
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
    }
}