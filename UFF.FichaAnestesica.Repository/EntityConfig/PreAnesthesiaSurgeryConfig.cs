using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UFF.FichaAnestesica.Domain.Entities;

namespace UFF.FichaAnestesica.Infra.EntityConfig
{
    public class PreAnesthesiaSurgeryConfig : IEntityTypeConfiguration<PreAnesthesiaSurgery>
    {
        public void Configure(EntityTypeBuilder<PreAnesthesiaSurgery> builder)
        {
            builder.ToTable("pre_anesthesia_surgeries");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .UseIdentityColumn()
                .IsRequired();

            builder.Property(x => x.Name)
                .HasColumnName("name")
                .HasColumnType("text")
                .IsRequired();

            builder.Property(x => x.IsPrimary)
                .HasColumnName("is_primary")
                .IsRequired();

            builder.Property(x => x.PreAnesthesiaRecordId)
                .HasColumnName("pre_anesthesia_record_id")
                .IsRequired();

            builder.HasOne(x => x.PreAnesthesiaRecord)
                .WithMany(x => x.Surgeries)
                .HasForeignKey(x => x.PreAnesthesiaRecordId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_pre_anesthesia_surgeries_pre_anesthesia_record");

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
