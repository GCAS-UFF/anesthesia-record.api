using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UFF.FichaAnestesica.Domain.Entities;

namespace UFF.FichaAnestesica.Infra.Data.Configurations
{
    public class AnesthesiaRecordAntibioticConfiguration : IEntityTypeConfiguration<AnesthesiaRecordAntibiotic>
    {
        public void Configure(EntityTypeBuilder<AnesthesiaRecordAntibiotic> builder)
        {
            builder.ToTable("anesthesia_record_antibiotics");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.MedicationName)
                .HasMaxLength(200);

            builder.Property(x => x.Name)
                .HasMaxLength(200);

            builder.Property(x => x.Dose)
                .HasMaxLength(100);

            builder.Property(x => x.Route)
                .HasMaxLength(50);

            builder.Property(x => x.Time)
                .HasColumnType("time");
        }
    }
}