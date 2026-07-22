using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UFF.FichaAnestesica.Domain.Entities;

namespace UFF.FichaAnestesica.Infra.EntityConfig
{
    public class AnesthesiaRecordOxygenSupplementationConfig : IEntityTypeConfiguration<AnesthesiaRecordOxygenSupplementation>
    {
        public void Configure(EntityTypeBuilder<AnesthesiaRecordOxygenSupplementation> builder)
        {
            builder.ToTable("anesthesia_record_oxygen_supplementations");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.AnesthesiaRecordId)
                .HasColumnName("anesthesia_record_id")
                .IsRequired();

            builder.Property(x => x.Type)
                .HasColumnName("type")
                .HasConversion<int>()
                .IsRequired();

            builder.HasOne(x => x.AnesthesiaRecord)
                .WithMany(x => x.OxygenSupplementationTypes)
                .HasForeignKey(x => x.AnesthesiaRecordId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}