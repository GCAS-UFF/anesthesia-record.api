using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UFF.FichaAnestesica.Domain.Entities;

namespace UFF.FichaAnestesica.Infra.EntityConfig
{
    public class PatientPositionConfig : IEntityTypeConfiguration<PatientPosition>
    {
        public void Configure(EntityTypeBuilder<PatientPosition> builder)
        {
            builder.ToTable("patient_positions");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .UseIdentityColumn();

            builder.Property(x => x.Timestamp)
                .HasColumnName("timestamp")
                .HasColumnType("timestamptz")
                .IsRequired();

            builder.Property(x => x.Position)
                .HasColumnName("position")
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.MonitoringRecordId)
                .HasColumnName("monitoring_record_id")
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("timestamptz")
                .IsRequired();

            builder.Property(x => x.LastUpdate)
                .HasColumnName("last_update")
                .HasColumnType("timestamptz");

            builder.HasOne(x => x.MonitoringRecord)
                .WithMany(x => x.Positions)
                .HasForeignKey(x => x.MonitoringRecordId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => x.MonitoringRecordId);
            builder.HasIndex(x => x.Timestamp);
        }
    }
}