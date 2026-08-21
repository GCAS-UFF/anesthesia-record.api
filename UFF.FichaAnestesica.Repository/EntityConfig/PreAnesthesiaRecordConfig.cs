using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UFF.FichaAnestesica.Domain.Entities;

namespace UFF.FichaAnestesica.Infra.EntityConfig
{
    public class PreAnesthesiaRecordConfig : IEntityTypeConfiguration<PreAnesthesiaRecord>
    {
        public void Configure(EntityTypeBuilder<PreAnesthesiaRecord> builder)
        {
            builder.ToTable("pre_anesthesia_records");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .UseIdentityColumn()
                .IsRequired();

            builder.Property(x => x.AnesthesiaRecordId)
                .HasColumnName("anesthesia_record_id")
                .IsRequired();

            builder.HasOne(x => x.AnesthesiaRecord)
                .WithMany()
                .HasForeignKey(x => x.AnesthesiaRecordId)
                .OnDelete(DeleteBehavior.Cascade)               
                .HasConstraintName("fk_pre_anesthesia_records_anesthesia_record");

           
            builder.HasIndex(x => x.AnesthesiaRecordId).IsUnique();

            #region Procedimento
            builder.Property(x => x.Laterality).HasColumnName("laterality").HasConversion<int>();
            builder.Property(x => x.PreOperativeDiagnosis).HasColumnName("pre_operative_diagnosis").HasColumnType("text");
            builder.Property(x => x.ConsultationDate).HasColumnName("consultation_date").HasColumnType("date");
            builder.Property(x => x.ProcedureObservation).HasColumnName("procedure_observation").HasColumnType("text");
            #endregion

            #region Antropometria
            builder.Property(x => x.WeightKg).HasColumnName("weight_kg").HasColumnType("numeric(6,2)");
            builder.Property(x => x.HeightCm).HasColumnName("height_cm").HasColumnType("numeric(5,2)");
            builder.Property(x => x.Bmi).HasColumnName("bmi").HasColumnType("numeric(5,2)");
            builder.Property(x => x.HeartRate).HasColumnName("heart_rate");
            builder.Property(x => x.SystolicBloodPressure).HasColumnName("systolic_blood_pressure");
            builder.Property(x => x.DiastolicBloodPressure).HasColumnName("diastolic_blood_pressure");
            builder.Property(x => x.Spo2).HasColumnName("spo2");
            builder.Property(x => x.Temperature).HasColumnName("temperature").HasColumnType("numeric(5,2)");
            builder.Property(x => x.FastingSolidsHours).HasColumnName("fasting_solids_hours").HasColumnType("numeric(4,1)");
            builder.Property(x => x.FastingLiquidsHours).HasColumnName("fasting_liquids_hours").HasColumnType("numeric(4,1)");
            #endregion

            #region Comorbidades
            builder.Property(x => x.ComorbiditiesOtherDescription).HasColumnName("comorbidities_other_description").HasColumnType("text");
            builder.Property(x => x.FamilyHistory).HasColumnName("family_history").HasColumnType("text");

            builder.HasMany(x => x.Comorbidities)
                .WithOne(x => x.PreAnesthesiaRecord)
                .HasForeignKey(x => x.PreAnesthesiaRecordId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region Hábitos
            builder.Property(x => x.IllicitDrugUse).HasColumnName("illicit_drug_use");
            builder.Property(x => x.DrugTypes).HasColumnName("drug_types").HasColumnType("text[]");
            builder.Property(x => x.DrugsOtherDescription).HasColumnName("drugs_other_description").HasColumnType("text");
            builder.Property(x => x.Smoker).HasColumnName("smoker");
            builder.Property(x => x.SmokingLoad).HasColumnName("smoking_load").HasColumnType("text");
            builder.Property(x => x.AlcoholUse).HasColumnName("alcohol_use");
            builder.Property(x => x.AlcoholGramsPerDay).HasColumnName("alcohol_grams_per_day").HasColumnType("text");
            #endregion

            #region Alergias
            builder.Property(x => x.HasAllergy).HasColumnName("has_allergy");
            builder.Property(x => x.AllergySubstances).HasColumnName("allergy_substances").HasColumnType("text[]");
            builder.Property(x => x.AllergyOtherDescription).HasColumnName("allergy_other_description").HasColumnType("text");
            builder.Property(x => x.AllergyReactionType).HasColumnName("allergy_reaction_type").HasColumnType("text");
            builder.Property(x => x.AnestheticHistory).HasColumnName("anesthetic_history").HasColumnType("text");
            #endregion

            #region Medicações em uso
            builder.Property(x => x.UsesMedication).HasColumnName("uses_medication");

            builder.HasMany(x => x.Medications)
                .WithOne(x => x.PreAnesthesiaRecord)
                .HasForeignKey(x => x.PreAnesthesiaRecordId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region Exame físico / via aérea
            builder.HasMany(x => x.PhysicalExamAreas)
                .WithOne(x => x.PreAnesthesiaRecord)
                .HasForeignKey(x => x.PreAnesthesiaRecordId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.AirwayMucosa).HasColumnName("airway_mucosa").HasColumnType("text[]");
            builder.Property(x => x.Dentition).HasColumnName("dentition").HasConversion<int>();
            builder.Property(x => x.InterIncisorDistance).HasColumnName("inter_incisor_distance").HasConversion<int>();
            builder.Property(x => x.UpperIncisorLength).HasColumnName("upper_incisor_length").HasConversion<int>();
            builder.Property(x => x.MallampatiClass).HasColumnName("mallampati_class");
            builder.Property(x => x.IncisorRelation).HasColumnName("incisor_relation").HasConversion<int>();
            builder.Property(x => x.Palate).HasColumnName("palate").HasConversion<int>();
            builder.Property(x => x.MandibleProtrusion).HasColumnName("mandible_protrusion").HasConversion<int>();
            builder.Property(x => x.NeckLength).HasColumnName("neck_length").HasConversion<int>();
            builder.Property(x => x.NeckWidth).HasColumnName("neck_width").HasConversion<int>();
            builder.Property(x => x.SternomentalDistance).HasColumnName("sternomental_distance").HasConversion<int>();
            builder.Property(x => x.ThyromentalDistance).HasColumnName("thyromental_distance").HasConversion<int>();
            builder.Property(x => x.NeckFlexion).HasColumnName("neck_flexion").HasConversion<int>();
            builder.Property(x => x.NeckExtension).HasColumnName("neck_extension").HasConversion<int>();
            builder.Property(x => x.MandibularSpaceCompliance).HasColumnName("mandibular_space_compliance").HasConversion<int>();
            builder.Property(x => x.AirwayObservations).HasColumnName("airway_observations").HasColumnType("text");
            builder.Property(x => x.ThoracicCageAbnormality).HasColumnName("thoracic_cage_abnormality");
            builder.Property(x => x.ThoracicCageAbnormalityDescription).HasColumnName("thoracic_cage_abnormality_description").HasColumnType("text");
            builder.Property(x => x.DifficultIntubationPrediction).HasColumnName("difficult_intubation_prediction");
            #endregion

            #region Exames laboratoriais
            builder.Property(x => x.Hemoglobin).HasColumnName("hemoglobin").HasColumnType("numeric(10,2)");
            builder.Property(x => x.Hematocrit).HasColumnName("hematocrit").HasColumnType("numeric(10,2)");
            builder.Property(x => x.Leukocytes).HasColumnName("leukocytes").HasColumnType("numeric(10,2)");
            builder.Property(x => x.Platelets).HasColumnName("platelets").HasColumnType("numeric(10,2)");
            builder.Property(x => x.TapInr).HasColumnName("tap_inr").HasColumnType("numeric(10,2)");
            builder.Property(x => x.Aptt).HasColumnName("aptt").HasColumnType("numeric(10,2)");
            builder.Property(x => x.Glucose).HasColumnName("glucose").HasColumnType("numeric(10,2)");
            builder.Property(x => x.Urea).HasColumnName("urea").HasColumnType("numeric(10,2)");
            builder.Property(x => x.Creatinine).HasColumnName("creatinine").HasColumnType("numeric(10,2)");
            builder.Property(x => x.Sodium).HasColumnName("sodium").HasColumnType("numeric(10,2)");
            builder.Property(x => x.Potassium).HasColumnName("potassium").HasColumnType("numeric(10,2)");
            builder.Property(x => x.Tp).HasColumnName("tp").HasColumnType("text");
            builder.Property(x => x.Urinalysis).HasColumnName("urinalysis").HasColumnType("text");
            builder.Property(x => x.LiverFunctionTests).HasColumnName("liver_function_tests").HasColumnType("text");
            builder.Property(x => x.PregnancyTest).HasColumnName("pregnancy_test").HasColumnType("text");
            #endregion

            #region Exames de imagem
            builder.Property(x => x.Ecg).HasColumnName("ecg").HasColumnType("text");
            builder.Property(x => x.ChestXRay).HasColumnName("chest_x_ray").HasColumnType("text");
            builder.Property(x => x.Echocardiogram).HasColumnName("echocardiogram").HasColumnType("text");
            builder.Property(x => x.PulmonaryFunctionTest).HasColumnName("pulmonary_function_test").HasColumnType("text");
            builder.Property(x => x.OtherImaging).HasColumnName("other_imaging").HasColumnType("text");
            #endregion

            builder.HasMany(x => x.Reports)
                .WithOne(x => x.PreAnesthesiaRecord)
                .HasForeignKey(x => x.PreAnesthesiaRecordId)
                .OnDelete(DeleteBehavior.Cascade);

            #region Conduta
            builder.Property(x => x.AsaClassification).HasColumnName("asa_classification").HasConversion<int>();
            builder.Property(x => x.IsEmergency).HasColumnName("is_emergency").IsRequired();
            builder.Property(x => x.NotCleared).HasColumnName("not_cleared").IsRequired();
            builder.Property(x => x.NotClearedReason).HasColumnName("not_cleared_reason").HasColumnType("text");
            builder.Property(x => x.ConductActions).HasColumnName("conduct_actions").HasColumnType("text[]");
            builder.Property(x => x.ConductNotes).HasColumnName("conduct_notes").HasColumnType("text");
            #endregion

            #region Assinatura
            builder.Property(x => x.SignedByProfessionalId).HasColumnName("signed_by_professional_id");

            builder.HasOne(x => x.SignedByProfessional)
                .WithMany()
                .HasForeignKey(x => x.SignedByProfessionalId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_pre_anesthesia_records_signed_by_professional");

            builder.Property(x => x.SignedByName).HasColumnName("signed_by_name").HasColumnType("text");
            builder.Property(x => x.SignedAt).HasColumnName("signed_at").HasColumnType("timestamptz");
            #endregion

            builder.Property(x => x.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("timestamptz")
                .IsRequired();

            builder.Property(x => x.LastUpdate)
                .HasColumnName("last_update")
                .HasColumnType("timestamptz");
        }
    }
}
