using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UFF.FichaAnestesica.Domain.Entities;

namespace UFF.FichaAnestesica.Infra.EntityConfig
{
    public class ClinicalEventConfig : IEntityTypeConfiguration<ClinicalEvent>
    {
        public void Configure(EntityTypeBuilder<ClinicalEvent> builder)
        {
            builder.ToTable("clinical_events");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .UseIdentityColumn()
                .IsRequired();

            builder.Property(x => x.Timestamp)
                .HasColumnName("timestamp")
                .HasColumnType("timestamptz")
                .IsRequired();

            builder.Property(x => x.EventType)
                .HasColumnName("event_type")
                .HasConversion<int>()
                .IsRequired();

            builder.Property(x => x.Description)
                .HasColumnName("description")
                .HasColumnType("varchar(500)")
                .IsRequired();

            builder.Property(x => x.Observations)
                .HasColumnName("observations")
                .HasColumnType("text");

            builder.Property(x => x.MonitoringRecordId)
             .HasColumnName("monitoring_record_id")
             .IsRequired();

            builder.HasOne(x => x.MonitoringRecord)
                .WithMany(x => x.ClinicalEvents)
                .HasForeignKey(x => x.MonitoringRecordId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("timestamptz")
                .IsRequired();

            builder.Property(x => x.LastUpdate)
                .HasColumnName("last_update")
                .HasColumnType("timestamptz");

            builder.HasIndex(x => x.Timestamp);

            builder.HasIndex(x => x.EventType);
        }
    }
}