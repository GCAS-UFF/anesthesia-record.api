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

            builder.HasOne(x => x.AnesthesiaRecord)
                .WithOne(x => x.MonitoringRecord)
                .HasForeignKey<MonitoringRecord>(x => x.AnesthesiaRecordId)
                .OnDelete(DeleteBehavior.Cascade);


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


            builder.Property(x => x.SurgeryStartedAt)
                .HasColumnName("surgery_started_at")
                .HasColumnType("timestamptz");


            builder.Property(x => x.SurgeryEndedAt)
                .HasColumnName("surgery_ended_at")
                .HasColumnType("timestamptz");


            builder.Property(x => x.IsMonitoringDraft)
                .HasColumnName("is_monitoring_draft")
                .IsRequired();


            builder.Property(x => x.MonitoringUpdatedAt)
                .HasColumnName("monitoring_updated_at")
                .HasColumnType("timestamptz");


            builder.Property(x => x.Status)
                .HasColumnName("status")
                .HasConversion<int>()
                .IsRequired();


            builder.Property(x => x.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("timestamptz")
                .IsRequired();


            builder.Property(x => x.LastUpdate)
                .HasColumnName("last_update")
                .HasColumnType("timestamptz");


            /*
             * Relacionamentos filhos
             */

            builder.HasMany(x => x.VitalSigns)
                .WithOne(x => x.MonitoringRecord)
                .HasForeignKey(x => x.MonitoringRecordId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.HasMany(x => x.AdministeredAgents)
                .WithOne(x => x.MonitoringRecord)
                .HasForeignKey(x => x.MonitoringRecordId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.HasMany(x => x.ClinicalEvents)
                .WithOne(x => x.MonitoringRecord)
                .HasForeignKey(x => x.MonitoringRecordId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.HasMany(x => x.FluidBalances)
                .WithOne(x => x.MonitoringRecord)
                .HasForeignKey(x => x.MonitoringRecordId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.HasMany(x => x.Positions)
                .WithOne(x => x.MonitoringRecord)
                .HasForeignKey(x => x.MonitoringRecordId)
                .OnDelete(DeleteBehavior.Cascade);



            builder.HasIndex(x => x.AnesthesiaRecordId);
            builder.HasIndex(x => x.RecordedByProfessionalId);
            builder.HasIndex(x => x.StartedAt);
        }
    }
}