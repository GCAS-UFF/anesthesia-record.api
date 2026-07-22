using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UFF.FichaAnestesica.Domain.Entities;

namespace UFF.FichaAnestesica.Infra.EntityConfig
{
    public class AnesthesiaRecordPunctureLevelConfig : IEntityTypeConfiguration<AnesthesiaRecordPunctureLevel>
    {
        public void Configure(EntityTypeBuilder<AnesthesiaRecordPunctureLevel> builder)
        {
            builder.ToTable("anesthesia_record_puncture_levels");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.AnesthesiaRecordId)
                .HasColumnName("anesthesia_record_id")
                .IsRequired();

            builder.Property(x => x.Level)
                .HasColumnName("level")
                .HasConversion<int>()
                .IsRequired();

            builder.HasOne(x => x.AnesthesiaRecord)
                .WithMany(x => x.PunctureLevels)
                .HasForeignKey(x => x.AnesthesiaRecordId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}