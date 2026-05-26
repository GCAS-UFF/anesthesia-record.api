using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Domain.Commands.AnesthesiaRecord
{
    public class AnesthesiaRecordCommand
    {
        // =========================================
        // SEGURANÇA DO PACIENTE
        // =========================================
        public bool PatientIdentifiedBeforeInduction { get; set; }
        public bool AnestheticConsentSigned { get; set; }
        public bool AnesthesiaEquipmentChecked { get; set; }
        public string? SafetyObservations { get; set; }

        // =========================================
        // PRÉ-INDUÇÃO
        // =========================================
        public bool PreAnestheticMedication { get; set; }

        // =========================================
        // ANTIBIÓTICO
        // =========================================
        public bool ProphylacticAntibioticUsed { get; set; }

        // =========================================
        // DADOS VITAIS
        // =========================================
        public string BloodPressure { get; set; } = default!;
        public int RespiratoryRate { get; set; }
        public decimal Temperature { get; set; }
        public int OxygenSaturation { get; set; }
        public decimal WeightKg { get; set; }

        public AsaClassificationEnum AsaClassification { get; set; }

        // =========================================
        // HORÁRIOS
        // =========================================
        public TimeOnly RoomEntryTime { get; set; }
        public TimeOnly AnesthesiaStartTime { get; set; }
        public TimeOnly SurgeryEndTime { get; set; }
        public TimeOnly AnesthesiaEndTime { get; set; }

        // =========================================
        // EQUIPE CIRÚRGICA
        // =========================================
        public string Surgeon { get; set; } = default!;
        public string Assistant { get; set; } = default!;
        public string PreOperativeDiagnosis { get; set; } = default!;

        // =========================================
        // POSIÇÃO OPERATÓRIA
        // =========================================
        public SurgicalPositionEnum SurgicalPosition { get; set; }
        public bool UsesCushions { get; set; }

        // =========================================
        // ACESSO VENOSO
        // =========================================
        public VenousAccessTypeEnum VenousAccessType { get; set; }
        public string VenousAccessLocation { get; set; } = default!;
        public bool DifficultVenousPuncture { get; set; }

        // =========================================
        // ANESTESIA
        // =========================================
        public bool GeneralAnesthesia { get; set; }

        public RespirationModeEnum RespirationMode { get; set; }
        public ControlledVentilationModeEnum? ControlledVentilationMode { get; set; }

        public bool Co2AbsorberCircuit { get; set; }

        // =========================================
        // VIA AÉREA
        // =========================================
        public AirwayDeviceTypeEnum? AirwayDeviceType { get; set; }
        public string? AirwayDeviceNumber { get; set; }

        public bool OralTube { get; set; }
        public bool NasalTube { get; set; }

        public IntubationDifficultyEnum? IntubationDifficulty { get; set; }

        public AirwayTypeEnum? AirwayType { get; set; }
        public string? OtherAirwayTypeDescription { get; set; }

        // =========================================
        // TÉCNICAS
        // =========================================
        public bool Laryngoscopy { get; set; }
        public bool RetrogradeTechnique { get; set; }
        public bool VideoLaryngoscopy { get; set; }
        public bool Bronchofibroscopy { get; set; }
        public bool Tracheostomy { get; set; }
        public string? OtherAirwayTechnique { get; set; }

        // =========================================
        // BLOQUEIOS / SEDAÇÃO
        // =========================================
        public bool SpinalBlockPerformed { get; set; }

        public bool SedationPerformed { get; set; }
        public bool OxygenSupplementation { get; set; }

        public bool PlexusBlockPerformed { get; set; }

        // =========================================
        // PÓS-OPERATÓRIO
        // =========================================
        public string SurgeryPerformed { get; set; } = default!;
        public string PostOperativeDiagnosis { get; set; } = default!;

        // =========================================
        // ALDRETE & KROULIK
        // =========================================
        public int ConsciousnessScore { get; set; }
        public int ActivityScore { get; set; }
        public int CirculationScore { get; set; }
        public int RespirationScore { get; set; }
        public int OxygenSaturationScore { get; set; }

        public int TotalAldreteKroulikScore { get; set; }

        // =========================================
        // ALTA
        // =========================================
        public ClinicalDischargeConditionEnum ClinicalDischargeCondition { get; set; }

        public PatientDestinationEnum Destination { get; set; }

        public bool HasPain { get; set; }

        // =========================================
        // ANESTESISTAS
        // =========================================
        public int? FirstAnesthesiologistId { get; set; }
        public int? SecondAnesthesiologistId { get; set; }

        // =========================================
        // PACIENTE
        // =========================================
        public int PatientId { get; set; }

        // =========================================
        // DATA
        // =========================================
        public DateOnly RecordDate { get; set; }
        public string ExternalPatientId { get; set; }
    }
}