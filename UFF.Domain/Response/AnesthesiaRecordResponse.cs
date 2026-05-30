using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Domain.Response
{
    public class AnesthesiaRecordResponse
    {
        public int Id { get; set; }
        public bool PatientIdentifiedBeforeInduction { get; set; }
        public bool AnestheticConsentSigned { get; set; }
        public bool AnesthesiaEquipmentChecked { get; set; }
        public string? SafetyObservations { get; set; }
        public bool PreAnestheticMedication { get; set; }
        public bool ProphylacticAntibioticUsed { get; set; }
        public string BloodPressure { get; set; } = default!;
        public int RespiratoryRate { get; set; }
        public decimal Temperature { get; set; }
        public int OxygenSaturation { get; set; }
        public decimal WeightKg { get; set; }
        public AsaClassificationEnum AsaClassification { get; set; }
        public TimeOnly RoomEntryTime { get; set; }
        public TimeOnly AnesthesiaStartTime { get; set; }
        public TimeOnly SurgeryEndTime { get; set; }
        public TimeOnly AnesthesiaEndTime { get; set; }
        public string PreOperativeDiagnosis { get; set; } = default!;
        public SurgicalPositionEnum SurgicalPosition { get; set; }
        public bool UsesCushions { get; set; }
        public VenousAccessTypeEnum VenousAccessType { get; set; }
        public string VenousAccessLocation { get; set; } = default!;
        public bool DifficultVenousPuncture { get; set; }
        public bool GeneralAnesthesia { get; set; }
        public RespirationModeEnum RespirationMode { get; set; }
        public ControlledVentilationModeEnum? ControlledVentilationMode { get; set; }
        public bool Co2AbsorberCircuit { get; set; }
        public AirwayDeviceTypeEnum? AirwayDeviceType { get; set; }
        public string? AirwayDeviceNumber { get; set; }
        public bool OralTube { get; set; }
        public bool NasalTube { get; set; }
        public IntubationDifficultyEnum? IntubationDifficulty { get; set; }
        public AirwayTypeEnum? AirwayType { get; set; }
        public string? OtherAirwayTypeDescription { get; set; }
        public bool Laryngoscopy { get; set; }
        public bool RetrogradeTechnique { get; set; }
        public bool VideoLaryngoscopy { get; set; }
        public bool Bronchofibroscopy { get; set; }
        public bool Tracheostomy { get; set; }
        public string? OtherAirwayTechnique { get; set; }
        public bool SpinalBlockPerformed { get; set; }
        public bool SedationPerformed { get; set; }
        public bool OxygenSupplementation { get; set; }
        public bool PlexusBlockPerformed { get; set; }
        public string SurgeryPerformed { get; set; } = default!;
        public string PostOperativeDiagnosis { get; set; } = default!;
        public int ConsciousnessScore { get; set; }
        public int ActivityScore { get; set; }
        public int CirculationScore { get; set; }
        public int RespirationScore { get; set; }
        public int OxygenSaturationScore { get; set; }
        public int TotalAldreteKroulikScore { get; set; }
        public ClinicalDischargeConditionEnum ClinicalDischargeCondition { get; set; }
        public PatientDestinationEnum Destination { get; set; }
        public bool HasPain { get; set; }
        public int? FirstAnesthesiologistId { get; set; }
        public string? FirstAnesthesiologistName { get; set; } = default!;
        public int? SecondAnesthesiologistId { get; set; }
        public string? SecondAnesthesiologistName { get; set; }
        public DateOnly RecordDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public int SurgeryId { get; set; }
        public string ExternalPatientId { get; set; } = default!;
        public int? SurgeonId { get; set; }
        public string? SurgeonName { get; set; }
        public int? AssistantId { get; set; }
        public string? AssistantName { get; set; }
        public DateTime? LastUpdate { get; set; }

        public static AnesthesiaRecordResponse ToResponse(AnesthesiaRecord anesthesiaRecord)
        {
            return new AnesthesiaRecordResponse
            {
                Id = anesthesiaRecord.Id,
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
                FirstAnesthesiologistId = anesthesiaRecord.FirstAnesthesiologistId,
                FirstAnesthesiologistName = anesthesiaRecord.FirstAnesthesiologist?.Name,
                SecondAnesthesiologistId = anesthesiaRecord.SecondAnesthesiologistId,
                SecondAnesthesiologistName = anesthesiaRecord.SecondAnesthesiologist?.Name,
                SurgeonId = anesthesiaRecord.SurgeonId,
                SurgeonName = anesthesiaRecord.Surgeon?.Name,
                AssistantId = anesthesiaRecord.AssistantId,
                AssistantName = anesthesiaRecord.Assistant?.Name,
                RecordDate = anesthesiaRecord.RecordDate,
                CreatedAt = anesthesiaRecord.CreatedAt,
                LastUpdate = anesthesiaRecord.LastUpdate               
            };
        }
    }
}