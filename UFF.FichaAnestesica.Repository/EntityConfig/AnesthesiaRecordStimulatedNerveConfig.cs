using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UFF.FichaAnestesica.Domain.Entities;

namespace UFF.FichaAnestesica.Infra.EntityConfig
{
    public class AnesthesiaRecordStimulatedNerveConfig : IEntityTypeConfiguration<AnesthesiaRecordStimulatedNerve>
    {
        public void Configure(EntityTypeBuilder<AnesthesiaRecordStimulatedNerve> builder)
        {
            builder.ToTable("anesthesia_record_stimulated_nerves");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.AnesthesiaRecordId)
                .HasColumnName("anesthesia_record_id")
                .IsRequired();

            builder.Property(x => x.Nerve)
                .HasColumnName("nerve")
                .HasConversion<int>()
                .IsRequired();

            builder.HasOne(x => x.AnesthesiaRecord)
                .WithMany(x => x.StimulatedNerves)
                .HasForeignKey(x => x.AnesthesiaRecordId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}