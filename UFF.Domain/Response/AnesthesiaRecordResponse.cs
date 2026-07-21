using UFF.FichaAnestesica.Domain.Dto;
using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Enums;
using UFF.FichaAnestesica.Domain.Extensions;

namespace UFF.FichaAnestesica.Domain.Response
{
    public class AnesthesiaRecordResponse
    {
        public PatientSurgeryResponse Patient { get; set; }
        public int SurgeryId { get; set; }

        public int? PreAnestheticMedicationId { get; set; }
        public string? PreAnestheticMedicationName { get; set; }
        public string? PreAnestheticMedicationDose { get; set; }
        public string? PreAnestheticMedicationRoute { get; set; }
        public string? PreAnestheticMedicationOtherRoute { get; set; }
        public TimeOnly? PreAnestheticMedicationTime { get; set; }
        public List<SurgeryResponse> Surgeries { get; set; }
        public List<AntibioticResponse>? AntibioticsList { get; set; }
        public string? SurgeonRegistration { get; set; }
        public string? AssistantRegistration { get; set; }
        public bool ProceduresCustomized { get; set; }
        public bool? DorUsouENV { get; set; }
        public int? DorENV { get; set; }
        public bool? DorUsouPAINAD { get; set; }
        public int? DorPAINAD { get; set; }
        public bool? DorUsouBPS { get; set; }
        public int? DorBPS { get; set; }
        public string? Conduta { get; set; }
        public bool? PatientIdentifiedBeforeInduction { get; set; }
        public bool? AnestheticConsentSigned { get; set; }
        public bool? AnesthesiaEquipmentChecked { get; set; }
        public string? SafetyObservations { get; set; }
        public bool? PreAnestheticMedication { get; set; }
        public bool? ProphylacticAntibioticUsed { get; set; }
        public string? BloodPressure { get; set; }
        public int? RespiratoryRate { get; set; }
        public decimal? Temperature { get; set; }
        public int? OxygenSaturation { get; set; }
        public decimal? WeightKg { get; set; }
        public AsaClassificationEnum? AsaClassification { get; set; }
        public TimeOnly? RoomEntryTime { get; set; }
        public TimeOnly? AnesthesiaStartTime { get; set; }
        public TimeOnly? SurgeryEndTime { get; set; }
        public TimeOnly? AnesthesiaEndTime { get; set; }
        public string? PreOperativeDiagnosis { get; set; }
        public SurgicalPositionEnum? SurgicalPosition { get; set; }
        public bool? UsesCushions { get; set; }
        public string CushionsAccessLocation { get; set; }
        public VenousAccessTypeEnum? VenousAccessType { get; set; }
        public string? VenousAccessLocation { get; set; }
        public bool? DifficultVenousPuncture { get; set; }
        public bool? GeneralAnesthesia { get; set; }
        public RespirationModeEnum? RespirationMode { get; set; }
        public ControlledVentilationModeEnum? ControlledVentilationMode { get; set; }
        public bool? Co2AbsorberCircuit { get; set; }
        public AirwayDeviceTypeEnum? AirwayDeviceType { get; set; }
        public string? AirwayDeviceNumber { get; set; }
        public bool? OralTube { get; set; }
        public bool? NasalTube { get; set; }
        public IntubationDifficultyEnum? IntubationDifficulty { get; set; }
        public AirwayTypeEnum? AirwayType { get; set; }
        public string? OtherAirwayTypeDescription { get; set; }
        public bool? Laryngoscopy { get; set; }
        public bool? RetrogradeTechnique { get; set; }
        public bool? VideoLaryngoscopy { get; set; }
        public bool? Bronchofibroscopy { get; set; }
        public bool? Tracheostomy { get; set; }
        public string? OtherAirwayTechnique { get; set; }
        public bool? SpinalBlockPerformed { get; set; }
        public bool? SedationPerformed { get; set; }
        public bool? OxygenSupplementation { get; set; }
        public bool? PlexusBlockPerformed { get; set; }
        public string? SurgeryPerformed { get; set; }
        public string? PostOperativeDiagnosis { get; set; }
        public int? ConsciousnessScore { get; set; }
        public int? ActivityScore { get; set; }
        public int? CirculationScore { get; set; }
        public int? RespirationScore { get; set; }
        public int? OxygenSaturationScore { get; set; }
        public int? TotalAldreteKroulikScore { get; set; }
        public ClinicalDischargeConditionEnum? ClinicalDischargeCondition { get; set; }
        public PatientDestinationEnum? Destination { get; set; }
        public bool? HasPain { get; set; }
        public int? FirstAnesthesiologistId { get; set; }
        public string? FirstAnesthesiologistName { get; set; }
        public int? SecondAnesthesiologistId { get; set; }
        public string? SecondAnesthesiologistName { get; set; }
        public int? SurgeonId { get; set; }
        public string? SurgeonName { get; set; }
        public int? AssistantId { get; set; }
        public string? AssistantName { get; set; }
        public string ExternalPatientId { get; set; } = string.Empty;
        public DateOnly RecordDate { get; set; }
        public DateTime SurgeryDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastUpdate { get; set; }
        public SurgeryStatusEnum Status { get; set; }

        public static AnesthesiaRecordResponse ToResponse(AnesthesiaRecord anesthesiaRecord, PatientDetailDto patientDetail)
        {
            var surgeries = anesthesiaRecord.ProceduresCustomized && anesthesiaRecord.Surgeries.Any() ?
             BuildProceduresFromRecord(anesthesiaRecord) : patientDetail.Surgeries?.Select(MapSurgery).ToList() ?? [];

            return new AnesthesiaRecordResponse
            {
                Patient = new PatientSurgeryResponse()
                {
                    FullName = patientDetail.FullName,
                    BirthDate = patientDetail.BirthDate,
                    Age = patientDetail.BirthDate.Date.GetAge(),
                    Status = patientDetail.HaveFirstAnesthesist && ParseStatus(patientDetail.Status) != SurgeryStatusEnum.Completed ? SurgeryStatusEnum.InProgress : ParseStatus(patientDetail.Status),
                    Gender = patientDetail.Gender,
                    WeightKg = patientDetail.WeightKg,
                    MedicalRecordNumber = patientDetail.MedicalRecordNumber,

                    Allergies = patientDetail.Allergies?.Select(MapAllergy).ToList() ?? new List<ListAllergyDto>(),
                    CurrentLocation = MapLocation(patientDetail.CurrentLocation)
                },
                SurgeryId = anesthesiaRecord.Id,
                ExternalPatientId = anesthesiaRecord.PatientId,
                Surgeries = surgeries,
                PreAnestheticMedicationId = anesthesiaRecord.PreAnestheticMedicationId,
                PreAnestheticMedicationName = anesthesiaRecord.PreAnestheticMedicationName,
                PreAnestheticMedicationDose = anesthesiaRecord.PreAnestheticMedicationDose,
                PreAnestheticMedicationRoute = anesthesiaRecord.PreAnestheticMedicationRoute,
                PreAnestheticMedicationOtherRoute = anesthesiaRecord.PreAnestheticMedicationOtherRoute,
                PreAnestheticMedicationTime = anesthesiaRecord.PreAnestheticMedicationTime,
                AntibioticsList = anesthesiaRecord.Antibiotics?.Select(MapAntibiotic).ToList() ?? new List<AntibioticResponse>(),
                SurgeonRegistration = anesthesiaRecord.Surgeon?.Registration,
                AssistantRegistration = anesthesiaRecord.Assistant?.Registration,
                ProceduresCustomized = anesthesiaRecord.ProceduresCustomized,
                DorUsouENV = anesthesiaRecord.DorUsouENV,
                DorENV = anesthesiaRecord.DorENV,
                DorUsouPAINAD = anesthesiaRecord.DorUsouPAINAD,
                DorPAINAD = anesthesiaRecord.DorPAINAD,
                DorUsouBPS = anesthesiaRecord.DorUsouBPS,
                DorBPS = anesthesiaRecord.DorBPS,
                Conduta = anesthesiaRecord.Conduta,
                PatientIdentifiedBeforeInduction = anesthesiaRecord.PatientIdentifiedBeforeInduction,
                AnestheticConsentSigned = anesthesiaRecord.AnestheticConsentSigned,
                AnesthesiaEquipmentChecked = anesthesiaRecord.AnesthesiaEquipmentChecked,
                SafetyObservations = anesthesiaRecord.SafetyObservations,
                PreAnestheticMedication = anesthesiaRecord.PreAnestheticMedication,
                ProphylacticAntibioticUsed = anesthesiaRecord.ProphylacticAntibioticUsed,
                BloodPressure = anesthesiaRecord.BloodPressure,
                RespiratoryRate = anesthesiaRecord.RespiratoryRate,
                Temperature = anesthesiaRecord.Temperature,
                OxygenSaturation = anesthesiaRecord.OxygenSaturation,
                WeightKg = anesthesiaRecord.WeightKg,
                AsaClassification = anesthesiaRecord.AsaClassification,
                RoomEntryTime = anesthesiaRecord.RoomEntryTime,
                AnesthesiaStartTime = anesthesiaRecord.AnesthesiaStartTime,
                SurgeryEndTime = anesthesiaRecord.SurgeryEndTime,
                AnesthesiaEndTime = anesthesiaRecord.AnesthesiaEndTime,
                PreOperativeDiagnosis = anesthesiaRecord.PreOperativeDiagnosis,
                SurgicalPosition = anesthesiaRecord.SurgicalPosition,
                UsesCushions = anesthesiaRecord.UsesCushions,
                VenousAccessType = anesthesiaRecord.VenousAccessType,
                VenousAccessLocation = anesthesiaRecord.VenousAccessLocation,
                CushionsAccessLocation = anesthesiaRecord.CushionsAccessLocation,
                DifficultVenousPuncture = anesthesiaRecord.DifficultVenousPuncture,
                GeneralAnesthesia = anesthesiaRecord.GeneralAnesthesia,
                RespirationMode = anesthesiaRecord.RespirationMode,
                ControlledVentilationMode = anesthesiaRecord.ControlledVentilationMode,
                Co2AbsorberCircuit = anesthesiaRecord.Co2AbsorberCircuit,
                AirwayDeviceType = anesthesiaRecord.AirwayDeviceType,
                AirwayDeviceNumber = anesthesiaRecord.AirwayDeviceNumber,
                OralTube = anesthesiaRecord.OralTube,
                NasalTube = anesthesiaRecord.NasalTube,
                IntubationDifficulty = anesthesiaRecord.IntubationDifficulty,
                AirwayType = anesthesiaRecord.AirwayType,
                OtherAirwayTypeDescription = anesthesiaRecord.OtherAirwayTypeDescription,
                Laryngoscopy = anesthesiaRecord.Laryngoscopy,
                RetrogradeTechnique = anesthesiaRecord.RetrogradeTechnique,
                VideoLaryngoscopy = anesthesiaRecord.VideoLaryngoscopy,
                Bronchofibroscopy = anesthesiaRecord.Bronchofibroscopy,
                Tracheostomy = anesthesiaRecord.Tracheostomy,
                OtherAirwayTechnique = anesthesiaRecord.OtherAirwayTechnique,
                SpinalBlockPerformed = anesthesiaRecord.SpinalBlockPerformed,
                SedationPerformed = anesthesiaRecord.SedationPerformed,
                OxygenSupplementation = anesthesiaRecord.OxygenSupplementation,
                PlexusBlockPerformed = anesthesiaRecord.PlexusBlockPerformed,
                SurgeryPerformed = anesthesiaRecord.SurgeryPerformed,
                PostOperativeDiagnosis = anesthesiaRecord.PostOperativeDiagnosis,
                ConsciousnessScore = anesthesiaRecord.ConsciousnessScore,
                ActivityScore = anesthesiaRecord.ActivityScore,
                CirculationScore = anesthesiaRecord.CirculationScore,
                RespirationScore = anesthesiaRecord.RespirationScore,
                OxygenSaturationScore = anesthesiaRecord.OxygenSaturationScore,
                TotalAldreteKroulikScore = anesthesiaRecord.TotalAldreteKroulikScore,
                ClinicalDischargeCondition = anesthesiaRecord.ClinicalDischargeCondition,
                Destination = anesthesiaRecord.Destination,
                HasPain = anesthesiaRecord.HasPain,
                Status = anesthesiaRecord.Status,
                FirstAnesthesiologistId = anesthesiaRecord.FirstAnesthesiologistId,
                FirstAnesthesiologistName = anesthesiaRecord.FirstAnesthesiologist?.Name,
                SecondAnesthesiologistId = anesthesiaRecord.SecondAnesthesiologistId,
                SecondAnesthesiologistName = anesthesiaRecord.SecondAnesthesiologist?.Name,
                SurgeonId = anesthesiaRecord.SurgeonId,
                SurgeonName = anesthesiaRecord.Surgeon?.Name,
                AssistantId = anesthesiaRecord.AssistantId,
                AssistantName = anesthesiaRecord.Assistant?.Name,
                SurgeryDate = anesthesiaRecord.SurgeryDate,
                CreatedAt = anesthesiaRecord.CreatedAt,
                LastUpdate = anesthesiaRecord.LastUpdate,

            };
        }

        private static AntibioticResponse MapAntibiotic(AnesthesiaRecordAntibiotic antibiotic)
        {
            return new AntibioticResponse
            {
                MedicationId = antibiotic.MedicationId,
                MedicationName = antibiotic.MedicationName,
                Name = antibiotic.MedicationName,
                Dose = antibiotic.Dose,
                Route = antibiotic.Route,
                Time = antibiotic.Time,
                HasBooster = antibiotic.HasBooster,
                Boosters = antibiotic.Boosters?.Select(MapBooster).ToList() ?? new List<BoosterResponse>()
            };
        }

        private static BoosterResponse MapBooster(AnesthesiaRecordAntibioticBooster booster)
        {
            return new BoosterResponse
            {
                MedicationId = booster.MedicationId,
                MedicationName = booster.MedicationName,
                Name = booster.MedicationName,
                Dose = booster.Dose,
                Route = booster.Route,
                Time = booster.Time
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

        private static List<SurgeryResponse> BuildProceduresFromRecord(AnesthesiaRecord record)
        {
            return new List<SurgeryResponse>
            {
                new SurgeryResponse
                {
                    Id = record.Id,
                    SurgeryDate = record.SurgeryDate,
                    Status = record.Status,
                    Procedures = record.Surgeries
                        .Select(p => new ProcedureResponse
                        {
                            Id = p.ProcedureId.ToString(),
                            Description = p.Procedure?.Description,
                            Cid = p.Procedure?.Cid,
                            IsPrimary = p.IsPrimary,
                            Time = p.Time
                        })
                        .ToList()
                }
            };
        }

        private static SurgeryResponse MapSurgery(SurgeryDetailsDto surgery)
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
                        IsPrimary = p.IsPrimary,
                        Time = p.Time
                    })
                    .ToList() ?? new List<ProcedureResponse>()
            };
        }

        private static ListAllergyDto MapAllergy(Dto.ListAllergyDto allergy)
        {
            return new ListAllergyDto
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
                "em_progresso" => SurgeryStatusEnum.InProgress,
                "cancelada" => SurgeryStatusEnum.Canceled,
                "em_preparacao" => SurgeryStatusEnum.Preparing,
                "em_andamento" => SurgeryStatusEnum.InProgress,
                "finalizada" => SurgeryStatusEnum.Completed,
                _ => SurgeryStatusEnum.Scheduled
            };
        }
    }
}