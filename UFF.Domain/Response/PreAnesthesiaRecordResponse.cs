using UFF.FichaAnestesica.Domain.Entities;

namespace UFF.FichaAnestesica.Domain.Response
{
    public class PreAnesthesiaRecordResponse
    {
        public int Id { get; set; }
        public int AnesthesiaRecordId { get; set; }
        public string PatientId { get; set; } = string.Empty;
        public int? FirstAnesthesiologistId { get; set; }

        #region Procedimento
        public List<PreAnesthesiaSurgeryResponse> Surgeries { get; set; } = new();
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
        public List<PreAnesthesiaChecklistGroupResponse> Comorbidities { get; set; } = new();
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
        public List<PreAnesthesiaMedicationResponse> Medications { get; set; } = new();
        #endregion

        #region Exame físico / via aérea
        public List<PreAnesthesiaChecklistGroupResponse> PhysicalExamAreas { get; set; } = new();
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

        public List<PreAnesthesiaReportResponse> Reports { get; set; } = new();

        #region Conduta
        public int? AsaClassification { get; set; }
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

        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdate { get; set; }

        public static PreAnesthesiaRecordResponse ToResponse(PreAnesthesiaRecord entity)
        {
            return new PreAnesthesiaRecordResponse
            {
                Id = entity.Id,
                AnesthesiaRecordId = entity.AnesthesiaRecordId,
                PatientId = entity.AnesthesiaRecord?.PatientId ?? string.Empty,
                FirstAnesthesiologistId = entity.AnesthesiaRecord?.FirstAnesthesiologistId,

                Surgeries = entity.Surgeries.Select(PreAnesthesiaSurgeryResponse.ToResponse).ToList(),
                Laterality = entity.Laterality?.ToString(),
                PreOperativeDiagnosis = entity.PreOperativeDiagnosis,
                ConsultationDate = entity.ConsultationDate?.ToString("yyyy-MM-dd"),
                ProcedureObservation = entity.ProcedureObservation,

                WeightKg = entity.WeightKg,
                HeightCm = entity.HeightCm,
                Bmi = entity.Bmi,
                HeartRate = entity.HeartRate,
                SystolicBloodPressure = entity.SystolicBloodPressure,
                DiastolicBloodPressure = entity.DiastolicBloodPressure,
                Spo2 = entity.Spo2,
                Temperature = entity.Temperature,
                FastingSolidsHours = entity.FastingSolidsHours,
                FastingLiquidsHours = entity.FastingLiquidsHours,

                Comorbidities = entity.Comorbidities.Select(PreAnesthesiaChecklistGroupResponse.ToResponse).ToList(),
                ComorbiditiesOtherDescription = entity.ComorbiditiesOtherDescription,
                FamilyHistory = entity.FamilyHistory,

                IllicitDrugUse = entity.IllicitDrugUse,
                DrugTypes = entity.DrugTypes,
                DrugsOtherDescription = entity.DrugsOtherDescription,
                Smoker = entity.Smoker,
                SmokingLoad = entity.SmokingLoad,
                AlcoholUse = entity.AlcoholUse,
                AlcoholGramsPerDay = entity.AlcoholGramsPerDay,

                HasAllergy = entity.HasAllergy,
                AllergySubstances = entity.AllergySubstances,
                AllergyOtherDescription = entity.AllergyOtherDescription,
                AllergyReactionType = entity.AllergyReactionType,
                AnestheticHistory = entity.AnestheticHistory,

                UsesMedication = entity.UsesMedication,
                Medications = entity.Medications.Select(PreAnesthesiaMedicationResponse.ToResponse).ToList(),

                PhysicalExamAreas = entity.PhysicalExamAreas.Select(PreAnesthesiaChecklistGroupResponse.ToResponse).ToList(),
                AirwayMucosa = entity.AirwayMucosa,
                Dentition = entity.Dentition?.ToString(),
                InterIncisorDistance = entity.InterIncisorDistance?.ToString(),
                UpperIncisorLength = entity.UpperIncisorLength?.ToString(),
                MallampatiClass = entity.MallampatiClass,
                IncisorRelation = entity.IncisorRelation?.ToString(),
                Palate = entity.Palate?.ToString(),
                MandibleProtrusion = entity.MandibleProtrusion?.ToString(),
                NeckLength = entity.NeckLength?.ToString(),
                NeckWidth = entity.NeckWidth?.ToString(),
                SternomentalDistance = entity.SternomentalDistance?.ToString(),
                ThyromentalDistance = entity.ThyromentalDistance?.ToString(),
                NeckFlexion = entity.NeckFlexion?.ToString(),
                NeckExtension = entity.NeckExtension?.ToString(),
                MandibularSpaceCompliance = entity.MandibularSpaceCompliance?.ToString(),
                AirwayObservations = entity.AirwayObservations,
                ThoracicCageAbnormality = entity.ThoracicCageAbnormality,
                ThoracicCageAbnormalityDescription = entity.ThoracicCageAbnormalityDescription,
                DifficultIntubationPrediction = entity.DifficultIntubationPrediction,

                Hemoglobin = entity.Hemoglobin,
                Hematocrit = entity.Hematocrit,
                Leukocytes = entity.Leukocytes,
                Platelets = entity.Platelets,
                TapInr = entity.TapInr,
                Aptt = entity.Aptt,
                Glucose = entity.Glucose,
                Urea = entity.Urea,
                Creatinine = entity.Creatinine,
                Sodium = entity.Sodium,
                Potassium = entity.Potassium,
                Tp = entity.Tp,
                Urinalysis = entity.Urinalysis,
                LiverFunctionTests = entity.LiverFunctionTests,
                PregnancyTest = entity.PregnancyTest,

                Ecg = entity.Ecg,
                ChestXRay = entity.ChestXRay,
                Echocardiogram = entity.Echocardiogram,
                PulmonaryFunctionTest = entity.PulmonaryFunctionTest,
                OtherImaging = entity.OtherImaging,

                Reports = entity.Reports.Select(PreAnesthesiaReportResponse.ToResponse).ToList(),

                AsaClassification = (int?)entity.AsaClassification,
                IsEmergency = entity.IsEmergency,
                NotCleared = entity.NotCleared,
                NotClearedReason = entity.NotClearedReason,
                ConductActions = entity.ConductActions,
                ConductNotes = entity.ConductNotes,

                SignedByProfessionalId = entity.SignedByProfessionalId,
                SignedByName = entity.SignedByName,
                SignedAt = entity.SignedAt,

                CreatedAt = entity.CreatedAt,
                LastUpdate = entity.LastUpdate
            };
        }
    }
}
