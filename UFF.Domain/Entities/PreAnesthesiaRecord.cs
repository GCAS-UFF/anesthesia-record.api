using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Domain.Entities
{
    public class PreAnesthesiaRecord : Base
    {
        private PreAnesthesiaRecord() { }

        public bool PatientIdentifiedBeforeInduction { get; private set; }
        public bool AnestheticConsentSigned { get; private set; }
        public bool AnesthesiaEquipmentChecked { get; private set; }
        public string? SafetyObservations { get; private set; }
        public bool PreAnestheticMedication { get; private set; }
        public bool ProphylacticAntibioticUsed { get; private set; }
        public string BloodPressure { get; private set; } = default!;
        public int RespiratoryRate { get; private set; }
        public decimal Temperature { get; private set; }
        public int OxygenSaturation { get; private set; }
        public decimal WeightKg { get; private set; }
        public AsaClassificationEnum AsaClassification { get; private set; }
        public TimeOnly RoomEntryTime { get; private set; }
        public TimeOnly AnesthesiaStartTime { get; private set; }
        public TimeOnly SurgeryEndTime { get; private set; }
        public TimeOnly AnesthesiaEndTime { get; private set; }
        public string Surgeon { get; private set; } = default!;
        public string Assistant { get; private set; } = default!;
        public string PreOperativeDiagnosis { get; private set; } = default!;
        public SurgicalPositionEnum SurgicalPosition { get; private set; }
        public bool UsesCushions { get; private set; }
        public VenousAccessTypeEnum VenousAccessType { get; private set; }
        public string VenousAccessLocation { get; private set; } = default!;
        public bool DifficultVenousPuncture { get; private set; }
        public bool GeneralAnesthesia { get; private set; }
        public RespirationModeEnum RespirationMode { get; private set; }
        public ControlledVentilationModeEnum? ControlledVentilationMode { get; private set; }
        public bool Co2AbsorberCircuit { get; private set; }
        public AirwayDeviceTypeEnum? AirwayDeviceType { get; private set; }
        public string? AirwayDeviceNumber { get; private set; }
        public bool OralTube { get; private set; }
        public bool NasalTube { get; private set; }
        public IntubationDifficultyEnum? IntubationDifficulty { get; private set; }
        public AirwayTypeEnum? AirwayType { get; private set; }
        public string? OtherAirwayTypeDescription { get; private set; }
        public bool Laryngoscopy { get; private set; }
        public bool RetrogradeTechnique { get; private set; }
        public bool VideoLaryngoscopy { get; private set; }
        public bool Bronchofibroscopy { get; private set; }
        public bool Tracheostomy { get; private set; }
        public string? OtherAirwayTechnique { get; private set; }
        public bool SpinalBlockPerformed { get; private set; }
        public bool SedationPerformed { get; private set; }
        public bool OxygenSupplementation { get; private set; }
        public bool PlexusBlockPerformed { get; private set; }
        public string SurgeryPerformed { get; private set; } = default!;
        public string PostOperativeDiagnosis { get; private set; } = default!;
        public int ConsciousnessScore { get; private set; }
        public int ActivityScore { get; private set; }
        public int CirculationScore { get; private set; }
        public int RespirationScore { get; private set; }
        public int OxygenSaturationScore { get; private set; }
        public int TotalAldreteKroulikScore { get; private set; }
        public ClinicalDischargeConditionEnum ClinicalDischargeCondition { get; private set; }
        public PatientDestinationEnum Destination { get; private set; }
        public bool HasPain { get; private set; }
        public int FirstAnesthesiologistId { get; private set; }
        public User FirstAnesthesiologist { get; private set; } = default!;
        public int SecondAnesthesiologistId { get; private set; }
        public User? SecondAnesthesiologist { get; private set; }
        public DateOnly RecordDate { get; private set; }
    }
}