using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Domain.Commands.AnesthesiaRecord
{
    public class AnesthesiaRecordCommand
    {
        public int SurgeryId { get; set; }
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
        public SurgeryStatusEnum Status { get; set; }

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
        public int? SurgeonId { get; set; } = default!;
        public int? AssistantId { get; set; } = default!;
        public int? FirstAnesthesiologistId { get; set; }
        public int? SecondAnesthesiologistId { get; set; }

        public int PatientId { get; set; }

        public DateOnly RecordDate { get; set; }
        public DateTime SurgeryDate { get; set; }
        public string ExternalPatientId { get; set; }
    }
}