using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Infra.EntityConfig
{
    public class PatientConfig : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("patients");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("id")
                .IsRequired();

            builder.Property(p => p.PatientId)
                .HasColumnName("patient_id")
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.Property(p => p.MedicalRecordNumber)
                .HasColumnName("medical_record_number")
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.Property(p => p.FullName)
                .HasColumnName("full_name")
                .HasColumnType("varchar(200)")
                .IsRequired();

            builder.Property(p => p.BirthDate)
                .HasColumnName("birth_date")
                .HasColumnType("date")
                .IsRequired();

            builder.HasMany(p => p.Surgeries)
                .WithOne(s => s.Patient)
                .HasForeignKey(s => s.Id);

            builder.Property(p => p.Gender)
                   .HasColumnName("gender")
                   .HasColumnType("varchar(1)")
                   .HasConversion(
                       v => v == GenderEnum.Male ? "M" : "F",
                       v => v == "M" ? GenderEnum.Male : GenderEnum.Female
                   ).IsRequired();

            builder.Property(p => p.WeightKg)
                .HasColumnName("weight_kg")
                .HasColumnType("numeric(10,2)")
                .IsRequired();

            builder.Property(p => p.HeightCm)
                .HasColumnName("height_cm")
                .HasColumnType("integer")
                .IsRequired();

            builder.HasOne(p => p.CurrentLocation)
                .WithOne()
                .HasForeignKey<CurrentLocation>("patient_id")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Surgeries)
                .WithOne()
                .HasForeignKey("patient_id");

            builder.Property(p => p.CreatedAt)
                .HasColumnName("created_at")
               .HasColumnType("timestamptz");

            builder.Property(p => p.LastUpdate)
                .HasColumnName("last_update")
              .HasColumnType("timestamptz");
        }
    }
}