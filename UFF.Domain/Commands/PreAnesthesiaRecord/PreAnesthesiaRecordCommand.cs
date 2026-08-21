using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Domain.Commands.PreAnesthesiaRecord
{
    public class PreAnesthesiaRecordCommand
    {
        public int AnesthesiaRecordId { get; set; }

        #region Procedimento
        public List<PreAnesthesiaSurgeryCommand> Surgeries { get; set; } = new();
        public string? Laterality { get; set; }
        public string? PreOperativeDiagnosis { get; set; }
        public string? ConsultationDate { get; set; }
        public string? ProcedureObservation { get; set; }
        #endregion

        #region Antropometria
        public decimal? WeightKg { get; set; }
        public decimal? HeightCm { get; set; }
        public decimal? Bmi { get; set; }
        public int? HeartRate { get; set; }
        public int? SystolicBloodPressure { get; set; }
        public int? DiastolicBloodPressure { get; set; }
        public int? Spo2 { get; set; }
        public decimal? Temperature { get; set; }
        public decimal? FastingSolidsHours { get; set; }
        public decimal? FastingLiquidsHours { get; set; }
        #endregion

        #region Comorbidades
        public List<PreAnesthesiaChecklistGroupCommand> Comorbidities { get; set; } = new();
        public string? ComorbiditiesOtherDescription { get; set; }
        public string? FamilyHistory { get; set; }
        #endregion

        #region Hábitos
        public bool? IllicitDrugUse { get; set; }
        public List<string> DrugTypes { get; set; } = new();
        public string? DrugsOtherDescription { get; set; }
        public bool? Smoker { get; set; }
        public string? SmokingLoad { get; set; }
        public bool? AlcoholUse { get; set; }
        public string? AlcoholGramsPerDay { get; set; }
        #endregion

        #region Alergias
        public bool? HasAllergy { get; set; }
        public List<string> AllergySubstances { get; set; } = new();
        public string? AllergyOtherDescription { get; set; }
        public string? AllergyReactionType { get; set; }
        public string? AnestheticHistory { get; set; }
        #endregion

        #region Medicações em uso
        public bool? UsesMedication { get; set; }
        public List<PreAnesthesiaMedicationCommand> Medications { get; set; } = new();
        #endregion

        #region Exame físico
        public List<PreAnesthesiaChecklistGroupCommand> PhysicalExamAreas { get; set; } = new();
        public List<string> AirwayMucosa { get; set; } = new();
        public string? Dentition { get; set; }
        public string? InterIncisorDistance { get; set; }
        public string? UpperIncisorLength { get; set; }
        public int? MallampatiClass { get; set; }
        public string? IncisorRelation { get; set; }
        public string? Palate { get; set; }
        public string? MandibleProtrusion { get; set; }
        public string? NeckLength { get; set; }
        public string? NeckWidth { get; set; }
        public string? SternomentalDistance { get; set; }
        public string? ThyromentalDistance { get; set; }
        public string? NeckFlexion { get; set; }
        public string? NeckExtension { get; set; }
        public string? MandibularSpaceCompliance { get; set; }
        public string? AirwayObservations { get; set; }
        public bool? ThoracicCageAbnormality { get; set; }
        public string? ThoracicCageAbnormalityDescription { get; set; }
        public bool? DifficultIntubationPrediction { get; set; }
        #endregion

        #region Exames laboratoriais
        public decimal? Hemoglobin { get; set; }
        public decimal? Hematocrit { get; set; }
        public decimal? Leukocytes { get; set; }
        public decimal? Platelets { get; set; }
        public decimal? TapInr { get; set; }
        public decimal? Aptt { get; set; }
        public decimal? Glucose { get; set; }
        public decimal? Urea { get; set; }
        public decimal? Creatinine { get; set; }
        public decimal? Sodium { get; set; }
        public decimal? Potassium { get; set; }
        public string? Tp { get; set; }
        public string? Urinalysis { get; set; }
        public string? LiverFunctionTests { get; set; }
        public string? PregnancyTest { get; set; }
        #endregion

        #region Exames de imagem
        public string? Ecg { get; set; }
        public string? ChestXRay { get; set; }
        public string? Echocardiogram { get; set; }
        public string? PulmonaryFunctionTest { get; set; }
        public string? OtherImaging { get; set; }
        #endregion

        public List<PreAnesthesiaReportCommand> Reports { get; set; } = new();

        #region Conduta
        public AsaClassificationEnum? AsaClassification { get; set; }
        public bool IsEmergency { get; set; }
        public bool NotCleared { get; set; }
        public string? NotClearedReason { get; set; }
        public List<string> ConductActions { get; set; } = new();
        public string? ConductNotes { get; set; }
        #endregion

        #region Assinatura
        public int? SignedByProfessionalId { get; set; }
        public string? SignedByName { get; set; }
        public DateTime? SignedAt { get; set; }
        #endregion
    }

    public class PreAnesthesiaSurgeryCommand
    {
        public string Name { get; set; } = string.Empty;
        public bool IsPrimary { get; set; }
    }

    /// <summary>
    /// Usado tanto para um item de Comorbidities quanto de PhysicalExamAreas
    /// — no frontend, os dois campos são exatamente o mesmo formato
    /// (Record&lt;string, PreAnesthesicChecklistFinding&gt;), só a lista de
    /// chaves de grupo válidas muda (COMORBIDITY_GROUPS x
    /// PHYSICAL_EXAM_GROUPS).
    /// </summary>
    public class PreAnesthesiaChecklistGroupCommand
    {
        public string GroupKey { get; set; } = string.Empty;
        public List<string> Findings { get; set; } = new();
        public string? OtherDescription { get; set; }
        public string? Observations { get; set; }
    }

    public class PreAnesthesiaMedicationCommand
    {
        public string Name { get; set; } = string.Empty;
        public string? Dose { get; set; }
        public string? Route { get; set; }
        public string? Frequency { get; set; }
    }

    public class PreAnesthesiaReportCommand
    {
        public string? Specialty { get; set; }
        public string? Description { get; set; }
    }
}
