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

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .IsRequired();

            builder.Property(x => x.PatientIdentifiedBeforeInduction)
                .HasColumnName("patient_identified_before_induction")
                .IsRequired();

            builder.Property(x => x.AnestheticConsentSigned)
                .HasColumnName("anesthetic_consent_signed")
                .IsRequired();

            builder.Property(x => x.AnesthesiaEquipmentChecked)
                .HasColumnName("anesthesia_equipment_checked")
                .IsRequired();

            builder.Property(x => x.SafetyObservations)
                .HasColumnName("safety_observations")
                .HasColumnType("text");

            builder.Property(x => x.PreAnestheticMedication)
                .HasColumnName("pre_anesthetic_medication")
                .IsRequired();

            builder.Property(x => x.ProphylacticAntibioticUsed)
                .HasColumnName("prophylactic_antibiotic_used")
                .IsRequired();

            builder.Property(x => x.BloodPressure)
                .HasColumnName("blood_pressure")
                .HasColumnType("varchar(20)")
                .IsRequired();

            builder.Property(x => x.RespiratoryRate)
                .HasColumnName("respiratory_rate")
                .IsRequired();

            builder.Property(x => x.Temperature)
                .HasColumnName("temperature")
                .HasColumnType("numeric(5,2)")
                .IsRequired();

            builder.Property(x => x.OxygenSaturation)
                .HasColumnName("oxygen_saturation")
                .IsRequired();

            builder.Property(x => x.WeightKg)
                .HasColumnName("weight_kg")
                .HasColumnType("numeric(6,2)")
                .IsRequired();

            builder.Property(x => x.AsaClassification)
                .HasColumnName("asa_classification")
                .HasConversion<int>()
                .IsRequired();

            builder.Property(x => x.RoomEntryTime)
                .HasColumnName("room_entry_time")
                .HasColumnType("time")
                .IsRequired();

            builder.Property(x => x.AnesthesiaStartTime)
                .HasColumnName("anesthesia_start_time")
                .HasColumnType("time")
                .IsRequired();

            builder.Property(x => x.SurgeryEndTime)
                .HasColumnName("surgery_end_time")
                .HasColumnType("time")
                .IsRequired();

            builder.Property(x => x.AnesthesiaEndTime)
                .HasColumnName("anesthesia_end_time")
                .HasColumnType("time")
                .IsRequired();

            builder.Property(x => x.Surgeon)
                .HasColumnName("surgeon")
                .HasColumnType("varchar(150)")
                .IsRequired();

            builder.Property(x => x.Assistant)
                .HasColumnName("assistant")
                .HasColumnType("varchar(150)");

            builder.Property(x => x.PreOperativeDiagnosis)
                .HasColumnName("pre_operative_diagnosis")
                .HasColumnType("text")
                .IsRequired();

            builder.Property(x => x.SurgicalPosition)
                .HasColumnName("surgical_position")
                .HasConversion<int>()
                .IsRequired();

            builder.Property(x => x.UsesCushions)
                .HasColumnName("uses_cushions")
                .IsRequired();

            builder.Property(x => x.VenousAccessType)
                .HasColumnName("venous_access_type")
                .HasConversion<int>()
                .IsRequired();

            builder.Property(x => x.VenousAccessLocation)
                .HasColumnName("venous_access_location")
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(x => x.DifficultVenousPuncture)
                .HasColumnName("difficult_venous_puncture")
                .IsRequired();

            builder.Property(x => x.GeneralAnesthesia)
                .HasColumnName("general_anesthesia")
                .IsRequired();

            builder.Property(x => x.RespirationMode)
                .HasColumnName("respiration_mode")
                .HasConversion<int>()
                .IsRequired();

            builder.Property(x => x.ControlledVentilationMode)
                .HasColumnName("controlled_ventilation_mode")
                .HasConversion<int>();

            builder.Property(x => x.Co2AbsorberCircuit)
                .HasColumnName("co2_absorber_circuit")
                .IsRequired();

            builder.Property(x => x.AirwayDeviceType)
                .HasColumnName("airway_device_type")
                .HasConversion<int>();

            builder.Property(x => x.AirwayDeviceNumber)
                .HasColumnName("airway_device_number")
                .HasColumnType("varchar(20)");

            builder.Property(x => x.OralTube)
                .HasColumnName("oral_tube")
                .IsRequired();

            builder.Property(x => x.NasalTube)
                .HasColumnName("nasal_tube")
                .IsRequired();

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
                .HasColumnName("laryngoscopy")
                .IsRequired();

            builder.Property(x => x.RetrogradeTechnique)
                .HasColumnName("retrograde_technique")
                .IsRequired();

            builder.Property(x => x.VideoLaryngoscopy)
                .HasColumnName("video_laryngoscopy")
                .IsRequired();

            builder.Property(x => x.Bronchofibroscopy)
                .HasColumnName("bronchofibroscopy")
                .IsRequired();

            builder.Property(x => x.Tracheostomy)
                .HasColumnName("tracheostomy")
                .IsRequired();

            builder.Property(x => x.OtherAirwayTechnique)
                .HasColumnName("other_airway_technique")
                .HasColumnType("varchar(200)");

            builder.Property(x => x.SpinalBlockPerformed)
                .HasColumnName("spinal_block_performed")
                .IsRequired();

            builder.Property(x => x.SedationPerformed)
                .HasColumnName("sedation_performed")
                .IsRequired();

            builder.Property(x => x.OxygenSupplementation)
                .HasColumnName("oxygen_supplementation")
                .IsRequired();

            builder.Property(x => x.PlexusBlockPerformed)
                .HasColumnName("plexus_block_performed")
                .IsRequired();

            builder.Property(x => x.SurgeryPerformed)
                .HasColumnName("surgery_performed")
                .HasColumnType("text")
                .IsRequired();

            builder.Property(x => x.PostOperativeDiagnosis)
                .HasColumnName("post_operative_diagnosis")
                .HasColumnType("text")
                .IsRequired();

            builder.Property(x => x.ConsciousnessScore)
                .HasColumnName("consciousness_score")
                .IsRequired();

            builder.Property(x => x.ActivityScore)
                .HasColumnName("activity_score")
                .IsRequired();

            builder.Property(x => x.CirculationScore)
                .HasColumnName("circulation_score")
                .IsRequired();

            builder.Property(x => x.RespirationScore)
                .HasColumnName("respiration_score")
                .IsRequired();

            builder.Property(x => x.OxygenSaturationScore)
                .HasColumnName("oxygen_saturation_score")
                .IsRequired();

            builder.Property(x => x.TotalAldreteKroulikScore)
                .HasColumnName("total_aldrete_kroulik_score")
                .IsRequired();

            builder.Property(x => x.ClinicalDischargeCondition)
                .HasColumnName("clinical_discharge_condition")
                .HasConversion<int>()
                .IsRequired();

            builder.Property(x => x.Destination)
                .HasColumnName("destination")
                .HasConversion<int>()
                .IsRequired();

            builder.Property(x => x.HasPain)
                .HasColumnName("has_pain")
                .IsRequired();

            builder.Property(x => x.ExternalPatientId)
                .HasColumnName("patient_id")
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(x => x.FirstAnesthesiologistId)
                .HasColumnName("first_anesthesiologist_id");

            builder.HasOne(x => x.FirstAnesthesiologist)
                .WithMany()
                .HasForeignKey(x => x.FirstAnesthesiologistId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.SecondAnesthesiologistId)
                .HasColumnName("second_anesthesiologist_id");

            builder.HasOne(x => x.SecondAnesthesiologist)
                .WithMany()
                .HasForeignKey(x => x.SecondAnesthesiologistId)
                .OnDelete(DeleteBehavior.Restrict);

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
        }
    }
}