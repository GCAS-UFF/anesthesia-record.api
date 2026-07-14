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

            builder.Property(x => x.PatientIdentifiedBeforeInduction)
                .HasColumnName("patient_identified_before_induction");

            builder.Property(x => x.AnestheticConsentSigned)
                .HasColumnName("anesthetic_consent_signed");

            builder.Property(x => x.AnesthesiaEquipmentChecked)
                .HasColumnName("anesthesia_equipment_checked");

            builder.Property(x => x.SafetyObservations)
                .HasColumnName("safety_observations")
                .HasColumnType("text");

            builder.Property(x => x.PreAnestheticMedication)
                .HasColumnName("pre_anesthetic_medication");

            builder.Property(x => x.ProphylacticAntibioticUsed)
                .HasColumnName("prophylactic_antibiotic_used");

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

            builder.Property(x => x.PreOperativeDiagnosis)
                .HasColumnName("pre_operative_diagnosis")
                .HasColumnType("text");

            builder.Property(x => x.SurgicalPosition)
                .HasColumnName("surgical_position")
                .HasConversion<int>();

            builder.Property(x => x.UsesCushions)
                .HasColumnName("uses_cushions");

            builder.Property(x => x.VenousAccessType)
                .HasColumnName("venous_access_type")
                .HasConversion<int>();

            builder.Property(x => x.VenousAccessLocation)
                .HasColumnName("venous_access_location")
                .HasColumnType("varchar(100)");

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

            builder.Property(x => x.AirwayDeviceType)
                .HasColumnName("airway_device_type")
                .HasConversion<int>();

            builder.Property(x => x.AirwayDeviceNumber)
                .HasColumnName("airway_device_number")
                .HasColumnType("varchar(20)");

            builder.Property(x => x.OralTube)
                .HasColumnName("oral_tube");

            builder.Property(x => x.NasalTube)
                .HasColumnName("nasal_tube");

            builder.Property(x => x.IntubationDifficulty)
                .HasColumnName("intubation_difficulty")
                .HasConversion<int>();

            builder.Property(x => x.AirwayType)
                .HasColumnName("airway_type")
                .HasConversion<int>();

            builder.Property(x => x.OtherAirwayTypeDescription)
                .HasColumnName("other_airway_type_description")
                .HasColumnType("varchar(200)");

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

            builder.Property(x => x.OtherAirwayTechnique)
                .HasColumnName("other_airway_technique")
                .HasColumnType("varchar(200)");

            builder.Property(x => x.SpinalBlockPerformed)
                .HasColumnName("spinal_block_performed");

            builder.Property(x => x.SedationPerformed)
                .HasColumnName("sedation_performed");

            builder.Property(x => x.OxygenSupplementation)
                .HasColumnName("oxygen_supplementation");

            builder.Property(x => x.PlexusBlockPerformed)
                .HasColumnName("plexus_block_performed");

            builder.Property(x => x.SurgeryPerformed)
                .HasColumnName("surgery_performed")
                .HasColumnType("text");

            builder.Property(x => x.PostOperativeDiagnosis)
                .HasColumnName("post_operative_diagnosis")
                .HasColumnType("text");

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

            builder.Property(x => x.ClinicalDischargeCondition)
                .HasColumnName("clinical_discharge_condition")
                .HasConversion<int>();

            builder.Property(x => x.Destination)
                .HasColumnName("destination")
                .HasConversion<int>();

            builder.Property(x => x.HasPain)
                .HasColumnName("has_pain");

            builder.Property(x => x.Status)
                .HasColumnName("status")
                .HasConversion<int>()
                .IsRequired();

            builder.Property(x => x.ExternalPatientId)
                .HasColumnName("patient_id")
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(x => x.RecordDate)
                .HasColumnName("record_date")
                .HasColumnType("date")
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("timestamptz");

            builder.Property(x => x.LastUpdate)
                .HasColumnName("last_update")
                .HasColumnType("timestamptz");

            builder.Property(x => x.FirstAnesthesiologistId)
                .HasColumnName("first_anesthesiologist_id");

            builder.Property(x => x.SecondAnesthesiologistId)
                .HasColumnName("second_anesthesiologist_id");

            builder.Property(x => x.SurgeonId)
                .HasColumnName("surgeon_id");

            builder.Property(x => x.AssistantId)
                .HasColumnName("assistant_id");

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
        }
    }
}
