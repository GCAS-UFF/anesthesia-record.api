using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UFF.FichaAnestesica.Domain.Entities;

namespace UFF.FichaAnestesica.Infra.EntityConfig
{
    public class AnesthesiaRecordConfig : IEntityTypeConfiguration<AnesthesiaRecord>
    {
        public void Configure(EntityTypeBuilder<AnesthesiaRecord> builder)
        {
            builder.ToTable("anesthesia_records");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id").IsRequired().ValueGeneratedNever();

            #region Segurança
            builder.Property(x => x.PatientIdentifiedBeforeInduction)
                .HasColumnName("patient_identified_before_induction");

            builder.Property(x => x.AnestheticConsentSigned)
                .HasColumnName("anesthetic_consent_signed");

            builder.Property(x => x.AnesthesiaEquipmentChecked)
                .HasColumnName("anesthesia_equipment_checked");

            builder.Property(x => x.SafetyObservations)
                .HasColumnName("safety_observations")
                .HasColumnType("text");
            #endregion

            #region Pré-medicação
            builder.Property(x => x.PreAnestheticMedication)
                .HasColumnName("pre_anesthetic_medication");

            builder.Property(x => x.PreAnestheticMedicationId)
                .HasColumnName("pre_anesthetic_medication_id");

            builder.Property(x => x.PreAnestheticMedicationName)
                .HasColumnName("pre_anesthetic_medication_name")
                .HasColumnType("varchar(200)");

            builder.Property(x => x.PreAnestheticMedicationDose)
                .HasColumnName("pre_anesthetic_medication_dose")
                .HasColumnType("varchar(50)");

            builder.Property(x => x.PreAnestheticMedicationRoute)
                .HasColumnName("pre_anesthetic_medication_route")
                .HasColumnType("varchar(50)");

            builder.Property(x => x.PreAnestheticMedicationOtherRoute)
                .HasColumnName("pre_anesthetic_medication_other_route")
                .HasColumnType("varchar(100)");

            builder.Property(x => x.PreAnestheticMedicationTime)
                .HasColumnName("pre_anesthetic_medication_time")
                .HasColumnType("time");
            #endregion

            #region Dor
            builder.Property(x => x.DorUsouENV)
                .HasColumnName("dor_usou_env");

            builder.Property(x => x.DorENV)
                .HasColumnName("dor_env");

            builder.Property(x => x.DorUsouPAINAD)
                .HasColumnName("dor_usou_painad");

            builder.Property(x => x.DorPAINAD)
                .HasColumnName("dor_painad");

            builder.Property(x => x.DorUsouBPS)
                .HasColumnName("dor_usou_bps");

            builder.Property(x => x.DorBPS)
                .HasColumnName("dor_bps");

            builder.Property(x => x.Conduta)
                .HasColumnName("conduta")
                .HasColumnType("text");
            #endregion

            #region Antibióticos
            builder.Property(x => x.ProphylacticAntibioticUsed)
                .HasColumnName("prophylactic_antibiotic_used");
            #endregion

            #region Sinais Vitais
            builder.Property(x => x.BloodPressure)
                .HasColumnName("blood_pressure")
                .HasColumnType("varchar(20)");

            builder.Property(x => x.RespiratoryRate)
                .HasColumnName("respiratory_rate");

            builder.Property(x => x.Temperature)
                .HasColumnName("temperature")
                .HasColumnType("numeric(5,2)");

            builder.Property(x => x.OxygenSaturation)
                .HasColumnName("oxygen_saturation");

            builder.Property(x => x.WeightKg)
                .HasColumnName("weight_kg")
                .HasColumnType("numeric(6,2)");

            builder.Property(x => x.AsaClassification)
                .HasColumnName("asa_classification")
                .HasConversion<int>();
            #endregion

            #region Horários
            builder.Property(x => x.RoomEntryTime)
                .HasColumnName("room_entry_time")
                .HasColumnType("time");

            builder.Property(x => x.AnesthesiaStartTime)
                .HasColumnName("anesthesia_start_time")
                .HasColumnType("time");

            builder.Property(x => x.SurgeryEndTime)
                .HasColumnName("surgery_end_time")
                .HasColumnType("time");

            builder.Property(x => x.AnesthesiaEndTime)
                .HasColumnName("anesthesia_end_time")
                .HasColumnType("time");
            #endregion

            #region Equipe
            builder.Property(x => x.SurgeonId)
                .HasColumnName("surgeon_id");

            builder.Property(x => x.AssistantId)
                .HasColumnName("assistant_id");

            builder.Property(x => x.FirstAnesthesiologistId)
                .HasColumnName("first_anesthesiologist_id");

            builder.Property(x => x.SecondAnesthesiologistId)
                .HasColumnName("second_anesthesiologist_id");
            #endregion

            #region Procedimento
            builder.Property(x => x.PreOperativeDiagnosis)
                .HasColumnName("pre_operative_diagnosis")
                .HasColumnType("text");

            builder.Property(x => x.SurgicalPosition)
                .HasColumnName("surgical_position")
                .HasConversion<int>();

            builder.Property(x => x.OtherSurgicalPosition)
                .HasColumnName("other_surgical_position")
                .HasColumnType("varchar(200)");

            builder.Property(x => x.UsesCushions)
                .HasColumnName("uses_cushions");

            builder.Property(x => x.CushionsAccessLocation)
                .HasColumnName("cushions_access_location")
                .HasColumnType("varchar(100)");

            builder.Property(x => x.VenousAccessType)
                .HasColumnName("venous_access_type")
                .HasConversion<int>();

            builder.Property(x => x.OtherVenousAccess)
                .HasColumnName("other_venous_access")
                .HasColumnType("varchar(100)");

            builder.Property(x => x.VenousAccessLocation)
                .HasColumnName("venous_access_location")
                .HasColumnType("varchar(200)");

            builder.Property(x => x.DifficultVenousPuncture)
                .HasColumnName("difficult_venous_puncture");

            builder.Property(x => x.GeneralAnesthesia)
                .HasColumnName("general_anesthesia");

            builder.Property(x => x.RespirationMode)
                .HasColumnName("respiration_mode")
                .HasConversion<int>();

            builder.Property(x => x.ControlledVentilationMode)
                .HasColumnName("controlled_ventilation_mode")
                .HasConversion<int>();

            builder.Property(x => x.Co2AbsorberCircuit)
                .HasColumnName("co2_absorber_circuit");
            #endregion

            #region Via Aérea - Dispositivos
            builder.Property(x => x.AirwayDeviceNumbers)
                .HasColumnName("airway_device_numbers")
                .HasColumnType("jsonb"); // ou "text" se não usar PostgreSQL

            builder.Property(x => x.Cuff)
                .HasColumnName("cuff");

            builder.Property(x => x.Iot)
                .HasColumnName("iot");

            builder.Property(x => x.OralTube)
                .HasColumnName("oral_tube");

            builder.Property(x => x.NasalTube)
                .HasColumnName("nasal_tube");

            builder.Property(x => x.IntubationDifficulty)
                .HasColumnName("intubation_difficulty")
                .HasConversion<int>();
            #endregion

            #region Via Aérea - Tipo
            builder.Property(x => x.AirwayType)
                .HasColumnName("airway_type")
                .HasConversion<int>();

            builder.Property(x => x.OtherAirwayTypeDescription)
                .HasColumnName("other_airway_type_description")
                .HasColumnType("varchar(200)");
            #endregion

            #region Via Aérea - Técnicas
            builder.Property(x => x.Laryngoscopy)
                .HasColumnName("laryngoscopy");

            builder.Property(x => x.RetrogradeTechnique)
                .HasColumnName("retrograde_technique");

            builder.Property(x => x.VideoLaryngoscopy)
                .HasColumnName("video_laryngoscopy");

            builder.Property(x => x.Bronchofibroscopy)
                .HasColumnName("bronchofibroscopy");

            builder.Property(x => x.Tracheostomy)
                .HasColumnName("tracheostomy");

            builder.Property(x => x.HasOtherAirwayTechnique)
                .HasColumnName("has_other_airway_technique");

            builder.Property(x => x.OtherAirwayTechnique)
                .HasColumnName("other_airway_technique")
                .HasColumnType("varchar(200)");
            #endregion

            #region Bloqueios Espinhais
            builder.Property(x => x.SpinalBlockPerformed)
                .HasColumnName("spinal_block_performed");

            builder.Property(x => x.PuncturePosition)
                .HasColumnName("puncture_position")
                .HasConversion<int>();

            builder.Property(x => x.SpinalCatheter)
                .HasColumnName("spinal_catheter");

            builder.Property(x => x.SpinalOpioid)
                .HasColumnName("spinal_opioid");

            builder.Property(x => x.PunctureCount)
                .HasColumnName("puncture_count");
            #endregion

            #region Sedação e Oxigênio
            builder.Property(x => x.SedationPerformed)
                .HasColumnName("sedation_performed");

            builder.Property(x => x.OxygenSupplementation)
                .HasColumnName("oxygen_supplementation");

            builder.Property(x => x.HasOxygenSupplementationOther)
                .HasColumnName("has_oxygen_supplementation_other");

            builder.Property(x => x.OxygenSupplementationOther)
                .HasColumnName("oxygen_supplementation_other")
                .HasColumnType("varchar(200)");
            #endregion

            #region Bloqueio Plexo
            builder.Property(x => x.PlexusBlockPerformed)
                .HasColumnName("plexus_block_performed");

            builder.Property(x => x.NeurostimulatorUsed)
                .HasColumnName("neurostimulator_used");
            #endregion

            #region Pós-Procedimento
            builder.Property(x => x.SurgeryPerformed)
                .HasColumnName("surgery_performed")
                .HasColumnType("text");

            builder.Property(x => x.PostOperativeDiagnosis)
                .HasColumnName("post_operative_diagnosis")
                .HasColumnType("text");
            #endregion

            #region Recuperação
            builder.Property(x => x.ConsciousnessScore)
                .HasColumnName("consciousness_score");

            builder.Property(x => x.ActivityScore)
                .HasColumnName("activity_score");

            builder.Property(x => x.CirculationScore)
                .HasColumnName("circulation_score");

            builder.Property(x => x.RespirationScore)
                .HasColumnName("respiration_score");

            builder.Property(x => x.OxygenSaturationScore)
                .HasColumnName("oxygen_saturation_score");

            builder.Property(x => x.TotalAldreteKroulikScore)
                .HasColumnName("total_aldrete_kroulik_score");

            builder.Property(x => x.AldreteEvaluationTime)
                .HasColumnName("aldrete_evaluation_time")
                .HasColumnType("time");

            builder.Property(x => x.ClinicalDischargeCondition)
                .HasColumnName("clinical_discharge_condition")
                .HasConversion<int>();

            builder.Property(x => x.DischargeConditionOther)
                .HasColumnName("discharge_condition_other")
                .HasColumnType("varchar(200)");

            builder.Property(x => x.Destination)
                .HasColumnName("destination")
                .HasConversion<int>();

            builder.Property(x => x.HasPain)
                .HasColumnName("has_pain");
            #endregion

            #region Assinatura
            builder.Property(x => x.SignatureDate)
                .HasColumnName("signature_date")
                .HasColumnType("date");
            #endregion

            builder.Property(x => x.Status)
                .HasColumnName("status")
                .HasConversion<int>()
                .IsRequired();

            builder.Property(x => x.PatientId)
                .HasColumnName("patient_id")
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(x => x.SurgeryDate)
                .HasColumnName("surgery_date")
                .HasColumnType("date")
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("timestamptz");

            builder.Property(x => x.LastUpdate)
                .HasColumnName("last_update")
                .HasColumnType("timestamptz");

            #region Relacionamentos
            builder.HasMany(x => x.Surgeries)
                .WithOne(x => x.AnesthesiaRecord)
                .HasForeignKey(x => x.AnesthesiaRecordId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Antibiotics)
                .WithOne(x => x.AnesthesiaRecord)
                .HasForeignKey(x => x.AnesthesiaRecordId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.AirwayDevices)
                .WithOne(x => x.AnesthesiaRecord)
                .HasForeignKey(x => x.AnesthesiaRecordId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.PunctureLevels)
                .WithOne(x => x.AnesthesiaRecord)
                .HasForeignKey(x => x.AnesthesiaRecordId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.OxygenSupplementationTypes)
                .WithOne(x => x.AnesthesiaRecord)
                .HasForeignKey(x => x.AnesthesiaRecordId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.StimulatedNerves)
                .WithOne(x => x.AnesthesiaRecord)
                .HasForeignKey(x => x.AnesthesiaRecordId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.FirstAnesthesiologist)
                .WithMany()
                .HasForeignKey(x => x.FirstAnesthesiologistId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.SecondAnesthesiologist)
                .WithMany()
                .HasForeignKey(x => x.SecondAnesthesiologistId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Surgeon)
                .WithMany()
                .HasForeignKey(x => x.SurgeonId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Assistant)
                .WithMany()
                .HasForeignKey(x => x.AssistantId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.MonitoringRecord)
                .WithOne(x => x.AnesthesiaRecord)
                .HasForeignKey<MonitoringRecord>(x => x.AnesthesiaRecordId);
            #endregion
        }
    }
}