using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UFF.FichaAnestesica.Domain.Entities;

namespace UFF.FichaAnestesica.Infra.EntityConfig
{
    public class SurgeryConfig : IEntityTypeConfiguration<Surgery>
    {
        public void Configure(EntityTypeBuilder<Surgery> builder)
        {
            builder.ToTable("surgeries");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                .HasColumnName("id")
                .IsRequired();

            builder.Property(p => p.SurgeryId)
                .HasColumnName("surgery_id")
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.Property(s => s.SurgeryDate)
                .HasColumnName("surgery_date")
                .HasColumnType("date")
                .IsRequired();

            builder.HasOne(s => s.Patient)
                .WithMany(p => p.Surgeries)
                .HasForeignKey(s => s.Id)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(s => s.Status)
                .HasColumnName("status")
                .HasColumnType("varchar(20)")
                .IsRequired();

            builder.HasOne(s => s.Specialty)
                .WithMany()
                .HasForeignKey("specialty_id")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.Location)
                .WithMany()
                .HasForeignKey("surgery_location_id")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(s => s.Procedures)
                .WithMany()
                .UsingEntity(j => j.ToTable("surgery_procedures"));

            builder.Property(s => s.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("timestamptz");

            builder.Property(s => s.LastUpdate)
                .HasColumnName("last_update")
                .HasColumnType("timestamptz");
        }
    }
}