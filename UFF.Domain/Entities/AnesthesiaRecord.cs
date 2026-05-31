using UFF.FichaAnestesica.Domain.Commands.AnesthesiaRecord;
using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Domain.Entities
{
    public class AnesthesiaRecord
    {
        private AnesthesiaRecord() { }

        public int Id { get; private set; }

        public bool? PatientIdentifiedBeforeInduction { get; private set; }
        public bool? AnestheticConsentSigned { get; private set; }
        public bool? AnesthesiaEquipmentChecked { get; private set; }

        public string? SafetyObservations { get; private set; }

        public bool? PreAnestheticMedication { get; private set; }
        public bool? ProphylacticAntibioticUsed { get; private set; }

        public string? BloodPressure { get; private set; }
        public int? RespiratoryRate { get; private set; }
        public decimal? Temperature { get; private set; }
        public int? OxygenSaturation { get; private set; }
        public decimal? WeightKg { get; private set; }

        public AsaClassificationEnum? AsaClassification { get; private set; }

        public TimeOnly? RoomEntryTime { get; private set; }
        public TimeOnly? AnesthesiaStartTime { get; private set; }
        public TimeOnly? SurgeryEndTime { get; private set; }
        public TimeOnly? AnesthesiaEndTime { get; private set; }

        public string? PreOperativeDiagnosis { get; private set; }

        public SurgicalPositionEnum? SurgicalPosition { get; private set; }
        public bool? UsesCushions { get; private set; }

        public VenousAccessTypeEnum? VenousAccessType { get; private set; }
        public string? VenousAccessLocation { get; private set; }
        public bool? DifficultVenousPuncture { get; private set; }

        public bool? GeneralAnesthesia { get; private set; }

        public RespirationModeEnum? RespirationMode { get; private set; }
        public ControlledVentilationModeEnum? ControlledVentilationMode { get; private set; }

        public bool? Co2AbsorberCircuit { get; private set; }

        public AirwayDeviceTypeEnum? AirwayDeviceType { get; private set; }
        public string? AirwayDeviceNumber { get; private set; }

        public bool? OralTube { get; private set; }
        public bool? NasalTube { get; private set; }

        public IntubationDifficultyEnum? IntubationDifficulty { get; private set; }
        public AirwayTypeEnum? AirwayType { get; private set; }

        public string? OtherAirwayTypeDescription { get; private set; }

        public bool? Laryngoscopy { get; private set; }
        public bool? RetrogradeTechnique { get; private set; }
        public bool? VideoLaryngoscopy { get; private set; }
        public bool? Bronchofibroscopy { get; private set; }
        public bool? Tracheostomy { get; private set; }

        public string? OtherAirwayTechnique { get; private set; }

        public bool? SpinalBlockPerformed { get; private set; }
        public bool? SedationPerformed { get; private set; }
        public bool? OxygenSupplementation { get; private set; }
        public bool? PlexusBlockPerformed { get; private set; }

        public string? SurgeryPerformed { get; private set; }
        public string? PostOperativeDiagnosis { get; private set; }

        public int? ConsciousnessScore { get; private set; }
        public int? ActivityScore { get; private set; }
        public int? CirculationScore { get; private set; }
        public int? RespirationScore { get; private set; }
        public int? OxygenSaturationScore { get; private set; }

        public int? TotalAldreteKroulikScore { get; private set; }

        public ClinicalDischargeConditionEnum? ClinicalDischargeCondition { get; private set; }
        public PatientDestinationEnum? Destination { get; private set; }

        public bool? HasPain { get; private set; }

        public int? FirstAnesthesiologistId { get; private set; }
        public User? FirstAnesthesiologist { get; private set; }

        public int? SecondAnesthesiologistId { get; private set; }
        public User? SecondAnesthesiologist { get; private set; }

        public int? SurgeonId { get; private set; }
        public User? Surgeon { get; private set; }

        public int? AssistantId { get; private set; }
        public User? Assistant { get; private set; }

        public MonitoringRecord? MonitoringRecord { get; private set; }

        public string ExternalPatientId { get; private set; } = string.Empty;

        public DateOnly RecordDate { get; private set; }

        public SurgeryStatusEnum AnesthesiaRecordStatus { get; private set; }

        public DateTime CreatedAt { get; protected set; }
        public DateTime LastUpdate { get; protected set; }

        public static AnesthesiaRecord Create(AnesthesiaRecordCommand command)
        {
            var entity = new AnesthesiaRecord();

            entity.SetValues(command);
            entity.CreatedAt = DateTime.UtcNow;
            entity.LastUpdate = DateTime.UtcNow;
            entity.AnesthesiaRecordStatus = SurgeryStatusEnum.Scheduled;

            return entity;
        }

        public void AssignFirstAnesthesiologistId(int? id)
        {
            FirstAnesthesiologistId = id > 0 ? id : null;
            LastUpdate = DateTime.UtcNow;
        }

        public void SetStatus(SurgeryStatusEnum status)
        {
            AnesthesiaRecordStatus = status;
            LastUpdate = DateTime.UtcNow;
        }

        public void Update(AnesthesiaRecordCommand command)
        {
            SetValues(command);
            AnesthesiaRecordStatus = SurgeryStatusEnum.Completed;
            LastUpdate = DateTime.UtcNow;
        }

        private void SetValues(AnesthesiaRecordCommand command)
        {
            PatientIdentifiedBeforeInduction = command.PatientIdentifiedBeforeInduction;
            AnestheticConsentSigned = command.AnestheticConsentSigned;
            AnesthesiaEquipmentChecked = command.AnesthesiaEquipmentChecked;
            SafetyObservations = command.SafetyObservations;

            PreAnestheticMedication = command.PreAnestheticMedication;
            ProphylacticAntibioticUsed = command.ProphylacticAntibioticUsed;

            BloodPressure = command.BloodPressure;
            RespiratoryRate = command.RespiratoryRate;
            Temperature = command.Temperature;
            OxygenSaturation = command.OxygenSaturation;
            WeightKg = command.WeightKg;

            AsaClassification = command.AsaClassification;

            RoomEntryTime = command.RoomEntryTime;
            AnesthesiaStartTime = command.AnesthesiaStartTime;
            SurgeryEndTime = command.SurgeryEndTime;
            AnesthesiaEndTime = command.AnesthesiaEndTime;

            PreOperativeDiagnosis = command.PreOperativeDiagnosis;

            SurgicalPosition = command.SurgicalPosition;
            UsesCushions = command.UsesCushions;

            VenousAccessType = command.VenousAccessType;
            VenousAccessLocation = command.VenousAccessLocation;

            DifficultVenousPuncture = command.DifficultVenousPuncture;

            GeneralAnesthesia = command.GeneralAnesthesia;

            RespirationMode = command.RespirationMode;
            ControlledVentilationMode = command.ControlledVentilationMode;

            Co2AbsorberCircuit = command.Co2AbsorberCircuit;

            AirwayDeviceType = command.AirwayDeviceType;
            AirwayDeviceNumber = command.AirwayDeviceNumber;

            OralTube = command.OralTube;
            NasalTube = command.NasalTube;

            IntubationDifficulty = command.IntubationDifficulty;
            AirwayType = command.AirwayType;

            OtherAirwayTypeDescription = command.OtherAirwayTypeDescription;

            Laryngoscopy = command.Laryngoscopy;
            RetrogradeTechnique = command.RetrogradeTechnique;
            VideoLaryngoscopy = command.VideoLaryngoscopy;
            Bronchofibroscopy = command.Bronchofibroscopy;
            Tracheostomy = command.Tracheostomy;

            OtherAirwayTechnique = command.OtherAirwayTechnique;

            SpinalBlockPerformed = command.SpinalBlockPerformed;
            SedationPerformed = command.SedationPerformed;
            OxygenSupplementation = command.OxygenSupplementation;
            PlexusBlockPerformed = command.PlexusBlockPerformed;

            SurgeryPerformed = command.SurgeryPerformed;
            PostOperativeDiagnosis = command.PostOperativeDiagnosis;

            ConsciousnessScore = command.ConsciousnessScore;
            ActivityScore = command.ActivityScore;
            CirculationScore = command.CirculationScore;
            RespirationScore = command.RespirationScore;
            OxygenSaturationScore = command.OxygenSaturationScore;

            TotalAldreteKroulikScore = command.TotalAldreteKroulikScore;

            ClinicalDischargeCondition = command.ClinicalDischargeCondition;
            Destination = command.Destination;

            HasPain = command.HasPain;

            SurgeonId = command.SurgeonId;
            AssistantId = command.AssistantId;
            FirstAnesthesiologistId = command.FirstAnesthesiologistId;
            SecondAnesthesiologistId = command.SecondAnesthesiologistId;

            ExternalPatientId = command.ExternalPatientId;
            RecordDate = command.RecordDate;
        }
    }
}