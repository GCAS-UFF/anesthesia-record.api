using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UFF.FichaAnestesica.Domain.Entities;

namespace UFF.FichaAnestesica.Infra.EntityConfig
{
    public class VitalSignRecordsConfig : IEntityTypeConfiguration<VitalSignRecord>
    {
        public void Configure(EntityTypeBuilder<VitalSignRecord> builder)
        {
            builder.ToTable("vital_sign_records");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .UseIdentityColumn()
                .IsRequired();

            builder.Property(x => x.Date)
                .HasColumnName("timestamp")
                .HasColumnType("timestamptz")
                .IsRequired();

            builder.Property(x => x.Time)
                .HasColumnName("time")
                .HasColumnType("time without time zone")
                .IsRequired();

            builder.Property(x => x.SystolicBloodPressure)
                .HasColumnName("systolic_blood_pressure");

            builder.Property(x => x.DiastolicBloodPressure)
                .HasColumnName("diastolic_blood_pressure");

            builder.Property(x => x.MeanArterialPressure)
                .HasColumnName("mean_arterial_pressure");

            builder.Property(x => x.HeartRate)
                .HasColumnName("heart_rate");

            builder.Property(x => x.Spo2)
                .HasColumnName("spo2");

            builder.Property(x => x.Etco2)
                .HasColumnName("etco2");

            builder.Property(x => x.Temperature)
                .HasColumnName("temperature")
                .HasColumnType("numeric(5,2)");

            builder.Property(x => x.Bis)
                .HasColumnName("bis");

            builder.Property(x => x.Pvc)
                .HasColumnName("pvc")
                .HasColumnType("numeric(5,2)");

            builder.Property(x => x.Pcap)
                .HasColumnName("pcap")
                .HasColumnType("numeric(5,2)");

            builder.Property(x => x.MonitoringRecordId)
             .HasColumnName("monitoring_record_id")
             .IsRequired();

            builder.HasOne(x => x.MonitoringRecord)
                .WithMany(x => x.VitalSigns)
                .HasForeignKey(x => x.MonitoringRecordId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.Property(x => x.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("timestamptz")
                .IsRequired();

            builder.Property(x => x.LastUpdate)
                .HasColumnName("last_update")
                .HasColumnType("timestamptz");

            builder.HasMany(x => x.CustomFields)
                .WithOne()
                .HasForeignKey("vital_sign_record_id")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => x.Date);
        }
    }
}