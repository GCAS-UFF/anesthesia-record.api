using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Domain.Commands.AnesthesiaRecord
{
    public class AnesthesiaRecordCommand
    {
        public int Id { get; set; }
        public int SurgeryId { get; set; }
        public string PatientId { get; set; } = default!;
        public DateTime SurgeryDate { get; set; }
        public SurgeryStatusEnum Status { get; set; }

        #region Dor
        public bool? DorUsouENV { get; set; }
        public int? DorENV { get; set; }
        public bool? DorUsouPAINAD { get; set; }
        public int? DorPAINAD { get; set; }
        public bool? DorUsouBPS { get; set; }
        public int? DorBPS { get; set; }
        public string? Conduta { get; set; }
        #endregion

        public List<SurgeryCommand> Surgeries { get; set; } = [];

        #region Segurança
        public bool PatientIdentifiedBeforeInduction { get; set; }
        public bool AnestheticConsentSigned { get; set; }
        public bool AnesthesiaEquipmentChecked { get; set; }
        public string? SafetyObservations { get; set; }
        #endregion

        #region Pré-medicação
        public bool PreAnestheticMedication { get; set; }
        public int? PreAnestheticMedicationId { get; set; }
        public string? PreAnestheticMedicationName { get; set; }
        public string? PreAnestheticMedicationDose { get; set; }
        public string? PreAnestheticMedicationRoute { get; set; }
        public string? PreAnestheticMedicationOtherRoute { get; set; }
        public TimeOnly? PreAnestheticMedicationTime { get; set; }
        #endregion

        #region Antibióticos
        public bool ProphylacticAntibioticUsed { get; set; }
        public List<AntibioticCommand> AntibioticsList { get; set; } = [];
        #endregion

        #region Sinais vitais
        public string BloodPressure { get; set; } = default!;
        public int RespiratoryRate { get; set; }
        public decimal Temperature { get; set; }
        public int OxygenSaturation { get; set; }
        public decimal WeightKg { get; set; }
        public AsaClassificationEnum AsaClassification { get; set; }
        #endregion

        #region Horários
        public TimeOnly RoomEntryTime { get; set; }
        public TimeOnly AnesthesiaStartTime { get; set; }
        public TimeOnly SurgeryEndTime { get; set; }
        public TimeOnly AnesthesiaEndTime { get; set; }
        #endregion

        #region Equipe
        public int? SurgeonId { get; set; }
        public int? AssistantId { get; set; }
        public int? FirstAnesthesiologistId { get; set; }
        public string? FirstAnesthesiologistName { get; set; }
        public int? SecondAnesthesiologistId { get; set; }
        public string? SecondAnesthesiologistName { get; set; }
        #endregion

        #region Procedimento
        public string PreOperativeDiagnosis { get; set; } = default!;
        public SurgicalPositionEnum SurgicalPosition { get; set; }
        public string? OtherSurgicalPosition { get; set; }
        public bool UsesCushions { get; set; }
        public string? CushionsAccessLocation { get; set; }
        public VenousAccessTypeEnum VenousAccessType { get; set; }
        public string? OtherVenousAccess { get; set; }
        public string VenousAccessLocation { get; set; } = default!;
        public bool DifficultVenousPuncture { get; set; }

        public bool GeneralAnesthesia { get; set; }
        public RespirationModeEnum RespirationMode { get; set; }
        public ControlledVentilationModeEnum? ControlledVentilationMode { get; set; }
        public bool Co2AbsorberCircuit { get; set; }

        #region Via Aérea - Dispositivos
        public List<AirwayDeviceTypeEnum> AirwayDeviceType { get; set; } = [];
        public string? AirwayDeviceNumbers { get; set; }
        public bool Cuff { get; set; }
        public bool Iot { get; set; }
        public bool OralTube { get; set; }
        public bool NasalTube { get; set; }
        public IntubationDifficultyEnum? IntubationDifficulty { get; set; }
        #endregion

        #region Via Aérea - Tipo
        public AirwayTypeEnum? AirwayType { get; set; }
        public string? OtherAirwayTypeDescription { get; set; }
        #endregion

        #region Via Aérea - Técnicas
        public bool Laryngoscopy { get; set; }
        public bool RetrogradeTechnique { get; set; }
        public bool VideoLaryngoscopy { get; set; }
        public bool Bronchofibroscopy { get; set; }
        public bool Tracheostomy { get; set; }
        public bool HasOtherAirwayTechnique { get; set; }
        public string? OtherAirwayTechnique { get; set; }
        #endregion

        #region Bloqueios Espinhais
        public bool SpinalBlockPerformed { get; set; }
        public List<PunctureLevelEnum> PunctureLevels { get; set; } = [];
        public PuncturePositionEnum PuncturePosition { get; set; }
        public bool SpinalCatheter { get; set; }
        public bool SpinalOpioid { get; set; }
        public int? PunctureCount { get; set; }
        #endregion

        #region Sedação e Oxigênio
        public bool SedationPerformed { get; set; }
        public bool OxygenSupplementation { get; set; }
        public List<OxygenSupplementationTypeEnum> OxygenSupplementationTypes { get; set; } = [];
        public bool HasOxygenSupplementationOther { get; set; }
        public string? OxygenSupplementationOther { get; set; }
        #endregion

        #region Bloqueio Plexo
        public bool PlexusBlockPerformed { get; set; }
        public bool NeurostimulatorUsed { get; set; }
        public List<StimulatedNerveEnum> StimulatedNerves { get; set; } = [];
        #endregion

        public string SurgeryPerformed { get; set; } = default!;
        public string PostOperativeDiagnosis { get; set; } = default!;
        #endregion

        #region Recuperação
        public int ConsciousnessScore { get; set; }
        public int ActivityScore { get; set; }
        public int CirculationScore { get; set; }
        public int RespirationScore { get; set; }
        public int OxygenSaturationScore { get; set; }
        public int TotalAldreteKroulikScore { get; set; }
        public TimeOnly? AldreteEvaluationTime { get; set; }
        public ClinicalDischargeConditionEnum ClinicalDischargeCondition { get; set; }
        public string? DischargeConditionOther { get; set; }
        public PatientDestinationEnum Destination { get; set; }
        public bool HasPain { get; set; }
        #endregion

        #region Assinatura
        public DateTime? SignatureDate { get; set; }
        #endregion
    }
}