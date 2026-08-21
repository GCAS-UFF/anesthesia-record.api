using UFF.FichaAnestesica.Domain.Commands.PreAnesthesiaRecord;
using UFF.FichaAnestesica.Domain.Enums;
using UFF.FichaAnestesica.Domain.Helpers;

namespace UFF.FichaAnestesica.Domain.Entities
{
    public class PreAnesthesiaRecord : Base
    {
        private PreAnesthesiaRecord() { }

        #region Vínculo        
        public int AnesthesiaRecordId { get; private set; }
        public AnesthesiaRecord AnesthesiaRecord { get; private set; } = default!;
        #endregion

        #region Procedimento
        public List<PreAnesthesiaSurgery> Surgeries { get; private set; } = new();
        public LateralityEnum? Laterality { get; private set; }
        public string? PreOperativeDiagnosis { get; private set; }
        public DateOnly? ConsultationDate { get; private set; }
        public string? ProcedureObservation { get; private set; }
        #endregion

        #region Antropometria
        public decimal? WeightKg { get; private set; }
        public decimal? HeightCm { get; private set; }
        public decimal? Bmi { get; private set; }
        public int? HeartRate { get; private set; }
        public int? SystolicBloodPressure { get; private set; }
        public int? DiastolicBloodPressure { get; private set; }
        public int? Spo2 { get; private set; }
        public decimal? Temperature { get; private set; }
        public decimal? FastingSolidsHours { get; private set; }
        public decimal? FastingLiquidsHours { get; private set; }
        #endregion

        #region Comorbidades
        public List<PreAnesthesiaComorbidity> Comorbidities { get; private set; } = new();
        public string? ComorbiditiesOtherDescription { get; private set; }
        public string? FamilyHistory { get; private set; }
        #endregion

        #region Hábitos
        public bool? IllicitDrugUse { get; private set; }
        public List<string> DrugTypes { get; private set; } = new();
        public string? DrugsOtherDescription { get; private set; }
        public bool? Smoker { get; private set; }
        public string? SmokingLoad { get; private set; }
        public bool? AlcoholUse { get; private set; }
        public string? AlcoholGramsPerDay { get; private set; }
        #endregion

        #region Alergias
        public bool? HasAllergy { get; private set; }
        public List<string> AllergySubstances { get; private set; } = new();
        public string? AllergyOtherDescription { get; private set; }
        public string? AllergyReactionType { get; private set; }
        public string? AnestheticHistory { get; private set; }
        #endregion

        #region Medicações em uso
        public bool? UsesMedication { get; private set; }
        public List<PreAnesthesiaMedication> Medications { get; private set; } = new();
        #endregion

        #region Exame físico / via aérea
        public List<PreAnesthesiaPhysicalExamArea> PhysicalExamAreas { get; private set; } = new();
        public List<string> AirwayMucosa { get; private set; } = new();
        public DentitionEnum? Dentition { get; private set; }
        public InterIncisorDistanceEnum? InterIncisorDistance { get; private set; }
        public UpperIncisorLengthEnum? UpperIncisorLength { get; private set; }
        public int? MallampatiClass { get; private set; }
        public IncisorRelationEnum? IncisorRelation { get; private set; }
        public PalateEnum? Palate { get; private set; }
        public YesNoNaEnum? MandibleProtrusion { get; private set; }
        public NeckLengthEnum? NeckLength { get; private set; }
        public NeckWidthEnum? NeckWidth { get; private set; }
        public SternomentalDistanceEnum? SternomentalDistance { get; private set; }
        public ThyromentalDistanceEnum? ThyromentalDistance { get; private set; }
        public YesNoNaEnum? NeckFlexion { get; private set; }
        public YesNoNaEnum? NeckExtension { get; private set; }
        public NormalAbnormalEnum? MandibularSpaceCompliance { get; private set; }
        public string? AirwayObservations { get; private set; }
        public bool? ThoracicCageAbnormality { get; private set; }
        public string? ThoracicCageAbnormalityDescription { get; private set; }
        public bool? DifficultIntubationPrediction { get; private set; }
        #endregion

        #region Exames laboratoriais
        public decimal? Hemoglobin { get; private set; }
        public decimal? Hematocrit { get; private set; }
        public decimal? Leukocytes { get; private set; }
        public decimal? Platelets { get; private set; }
        public decimal? TapInr { get; private set; }
        public decimal? Aptt { get; private set; }
        public decimal? Glucose { get; private set; }
        public decimal? Urea { get; private set; }
        public decimal? Creatinine { get; private set; }
        public decimal? Sodium { get; private set; }
        public decimal? Potassium { get; private set; }
        public string? Tp { get; private set; }
        public string? Urinalysis { get; private set; }
        public string? LiverFunctionTests { get; private set; }
        public string? PregnancyTest { get; private set; }
        #endregion

        #region Exames de imagem
        public string? Ecg { get; private set; }
        public string? ChestXRay { get; private set; }
        public string? Echocardiogram { get; private set; }
        public string? PulmonaryFunctionTest { get; private set; }
        public string? OtherImaging { get; private set; }
        #endregion

        public List<PreAnesthesiaReport> Reports { get; private set; } = new();

        #region Conduta
        public AsaClassificationEnum? AsaClassification { get; private set; }
        public bool IsEmergency { get; private set; }
        public bool NotCleared { get; private set; }
        public string? NotClearedReason { get; private set; }
        public List<string> ConductActions { get; private set; } = new();
        public string? ConductNotes { get; private set; }
        #endregion

        #region Assinatura
        public int? SignedByProfessionalId { get; private set; }
        public User? SignedByProfessional { get; private set; }
        public string? SignedByName { get; private set; }
        public DateTime? SignedAt { get; private set; }
        #endregion

        public static PreAnesthesiaRecord Create(PreAnesthesiaRecordCommand command)
        {
            var record = new PreAnesthesiaRecord
            {
                CreatedAt = DateTime.UtcNow
            };

            record.SetValues(command);

            return record;
        }

        public void Update(PreAnesthesiaRecordCommand command)
        {
            SetValues(command);
            LastUpdate = DateTime.UtcNow;
        }

        private void SetValues(PreAnesthesiaRecordCommand command)
        {
            AnesthesiaRecordId = command.AnesthesiaRecordId;

            #region Procedimento
            Surgeries.Clear();
            foreach (var surgeryCommand in command.Surgeries ?? new())
            {
                var surgery = PreAnesthesiaSurgery.Create(surgeryCommand);
                surgery.SetPreAnesthesiaRecord(this);
                Surgeries.Add(surgery);
            }

            Laterality = ParseHelper.ParseEnum<LateralityEnum>(command.Laterality);
            PreOperativeDiagnosis = command.PreOperativeDiagnosis;
            ConsultationDate = DateOnly.TryParse(command.ConsultationDate, out var consultationDate) ? consultationDate : null;
            ProcedureObservation = command.ProcedureObservation;
            #endregion

            #region Antropometria
            WeightKg = command.WeightKg;
            HeightCm = command.HeightCm;
            Bmi = command.Bmi;
            HeartRate = command.HeartRate;
            SystolicBloodPressure = command.SystolicBloodPressure;
            DiastolicBloodPressure = command.DiastolicBloodPressure;
            Spo2 = command.Spo2;
            Temperature = command.Temperature;
            FastingSolidsHours = command.FastingSolidsHours;
            FastingLiquidsHours = command.FastingLiquidsHours;
            #endregion

            #region Comorbidades
            Comorbidities.Clear();
            foreach (var groupCommand in command.Comorbidities ?? new())
            {
                var comorbidity = PreAnesthesiaComorbidity.Create(groupCommand);
                comorbidity.SetPreAnesthesiaRecord(this);
                Comorbidities.Add(comorbidity);
            }

            ComorbiditiesOtherDescription = command.ComorbiditiesOtherDescription;
            FamilyHistory = command.FamilyHistory;
            #endregion

            #region Hábitos
            IllicitDrugUse = command.IllicitDrugUse;
            DrugTypes = command.DrugTypes ?? new();
            DrugsOtherDescription = command.DrugsOtherDescription;
            Smoker = command.Smoker;
            SmokingLoad = command.SmokingLoad;
            AlcoholUse = command.AlcoholUse;
            AlcoholGramsPerDay = command.AlcoholGramsPerDay;
            #endregion

            #region Alergias
            HasAllergy = command.HasAllergy;
            AllergySubstances = command.AllergySubstances ?? new();
            AllergyOtherDescription = command.AllergyOtherDescription;
            AllergyReactionType = command.AllergyReactionType;
            AnestheticHistory = command.AnestheticHistory;
            #endregion

            #region Medicações em uso
            UsesMedication = command.UsesMedication;
            Medications.Clear();
            foreach (var medicationCommand in command.Medications ?? new())
            {
                var medication = PreAnesthesiaMedication.Create(medicationCommand);
                medication.SetPreAnesthesiaRecord(this);
                Medications.Add(medication);
            }
            #endregion

            #region Exame físico / via aérea
            PhysicalExamAreas.Clear();
            foreach (var areaCommand in command.PhysicalExamAreas ?? new())
            {
                var area = PreAnesthesiaPhysicalExamArea.Create(areaCommand);
                area.SetPreAnesthesiaRecord(this);
                PhysicalExamAreas.Add(area);
            }

            AirwayMucosa = command.AirwayMucosa ?? new();
            Dentition = ParseHelper.ParseEnum<DentitionEnum>(command.Dentition);
            InterIncisorDistance = ParseHelper.ParseEnum<InterIncisorDistanceEnum>(command.InterIncisorDistance);
            UpperIncisorLength = ParseHelper.ParseEnum<UpperIncisorLengthEnum>(command.UpperIncisorLength);
            MallampatiClass = command.MallampatiClass;
            IncisorRelation = ParseHelper.ParseEnum<IncisorRelationEnum>(command.IncisorRelation);
            Palate = ParseHelper.ParseEnum<PalateEnum>(command.Palate);
            MandibleProtrusion = ParseHelper.ParseEnum<YesNoNaEnum>(command.MandibleProtrusion);
            NeckLength = ParseHelper.ParseEnum<NeckLengthEnum>(command.NeckLength);
            NeckWidth = ParseHelper.ParseEnum<NeckWidthEnum>(command.NeckWidth);
            SternomentalDistance = ParseHelper.ParseEnum<SternomentalDistanceEnum>(command.SternomentalDistance);
            ThyromentalDistance = ParseHelper.ParseEnum<ThyromentalDistanceEnum>(command.ThyromentalDistance);
            NeckFlexion = ParseHelper.ParseEnum<YesNoNaEnum>(command.NeckFlexion);
            NeckExtension = ParseHelper.ParseEnum<YesNoNaEnum>(command.NeckExtension);
            MandibularSpaceCompliance = ParseHelper.ParseEnum<NormalAbnormalEnum>(command.MandibularSpaceCompliance);
            AirwayObservations = command.AirwayObservations;
            ThoracicCageAbnormality = command.ThoracicCageAbnormality;
            ThoracicCageAbnormalityDescription = command.ThoracicCageAbnormalityDescription;
            DifficultIntubationPrediction = command.DifficultIntubationPrediction;
            #endregion

            #region Exames laboratoriais
            Hemoglobin = command.Hemoglobin;
            Hematocrit = command.Hematocrit;
            Leukocytes = command.Leukocytes;
            Platelets = command.Platelets;
            TapInr = command.TapInr;
            Aptt = command.Aptt;
            Glucose = command.Glucose;
            Urea = command.Urea;
            Creatinine = command.Creatinine;
            Sodium = command.Sodium;
            Potassium = command.Potassium;
            Tp = command.Tp;
            Urinalysis = command.Urinalysis;
            LiverFunctionTests = command.LiverFunctionTests;
            PregnancyTest = command.PregnancyTest;
            #endregion

            #region Exames de imagem
            Ecg = command.Ecg;
            ChestXRay = command.ChestXRay;
            Echocardiogram = command.Echocardiogram;
            PulmonaryFunctionTest = command.PulmonaryFunctionTest;
            OtherImaging = command.OtherImaging;
            #endregion

            Reports.Clear();
            foreach (var reportCommand in command.Reports ?? new())
            {
                var report = PreAnesthesiaReport.Create(reportCommand);
                report.SetPreAnesthesiaRecord(this);
                Reports.Add(report);
            }

            #region Conduta
            AsaClassification = command.AsaClassification;
            IsEmergency = command.IsEmergency;
            NotCleared = command.NotCleared;
            NotClearedReason = command.NotClearedReason;
            ConductActions = command.ConductActions ?? new();
            ConductNotes = command.ConductNotes;
            #endregion

            #region Assinatura
            SignedByProfessionalId = command.SignedByProfessionalId;
            SignedByName = command.SignedByName;
            SignedAt = command.SignedAt;
            #endregion
        }
    }
}
