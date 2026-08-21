using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UFF.FichaAnestesica.Domain.Entities;

namespace UFF.FichaAnestesica.Infra.EntityConfig
{
    public class PreAnesthesiaMedicationConfig : IEntityTypeConfiguration<PreAnesthesiaMedication>
    {
        public void Configure(EntityTypeBuilder<PreAnesthesiaMedication> builder)
        {
            builder.ToTable("pre_anesthesia_medications");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .UseIdentityColumn()
                .IsRequired();

            builder.Property(x => x.Name)
                .HasColumnName("name")
                .HasColumnType("text")
                .IsRequired();

            builder.Property(x => x.Dose)
                .HasColumnName("dose")
                .HasColumnType("text");

            builder.Property(x => x.Route)
                .HasColumnName("route")
                .HasColumnType("text");

            builder.Property(x => x.Frequency)
                .HasColumnName("frequency")
                .HasColumnType("text");

            builder.Property(x => x.PreAnesthesiaRecordId)
                .HasColumnName("pre_anesthesia_record_id")
                .IsRequired();

            builder.HasOne(x => x.PreAnesthesiaRecord)
                .WithMany(x => x.Medications)
                .HasForeignKey(x => x.PreAnesthesiaRecordId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_pre_anesthesia_medications_pre_anesthesia_record");

            builder.Property(x => x.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("timestamptz")
                .IsRequired();

            builder.Property(x => x.LastUpdate)
                .HasColumnName("last_update")
                .HasColumnType("timestamptz");

            builder.HasIndex(x => x.PreAnesthesiaRecordId);
        }
    }
}
