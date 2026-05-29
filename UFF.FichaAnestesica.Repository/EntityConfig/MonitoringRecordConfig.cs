using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UFF.FichaAnestesica.Domain.Entities;

namespace UFF.FichaAnestesica.Infra.EntityConfig
{
    public class MonitoringRecordConfig : IEntityTypeConfiguration<MonitoringRecord>
    {
        public void Configure(EntityTypeBuilder<MonitoringRecord> builder)
        {
            builder.ToTable("monitoring_records");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .UseIdentityColumn()
                .IsRequired();

            builder.Property(x => x.AnesthesiaRecordId)
                .HasColumnName("anesthesia_record_id")
                .IsRequired();

            builder.Property(x => x.SurgeryId)
                .HasColumnName("surgery_id")
                .IsRequired();

            builder.Property(x => x.RecordedByProfessionalId)
                .HasColumnName("recorded_by_professional_id")
                .IsRequired();

            builder.Property(x => x.StartedAt)
                .HasColumnName("started_at")
                .HasColumnType("timestamptz")
                .IsRequired();

            builder.Property(x => x.EndedAt)
                .HasColumnName("ended_at")
                .HasColumnType("timestamptz");

            builder.Property(x => x.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("timestamptz")
                .IsRequired();

            builder.Property(x => x.LastUpdate)
                .HasColumnName("last_update")
                .HasColumnType("timestamptz");

            builder.HasMany(x => x.VitalSigns)
                .WithOne()
                .HasForeignKey("monitoring_record_id")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.AdministeredAgents)
                .WithOne()
                .HasForeignKey("monitoring_record_id")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.ClinicalEvents)
                .WithOne()
                .HasForeignKey("monitoring_record_id")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.FluidBalances)
                .WithOne()
                .HasForeignKey("monitoring_record_id")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => x.AnesthesiaRecordId);
            builder.HasIndex(x => x.SurgeryId);
            builder.HasIndex(x => x.RecordedByProfessionalId);
            builder.HasIndex(x => x.StartedAt);
        }
    }
}