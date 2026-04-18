using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UFF.FichaAnestesica.Domain.Entities;

namespace UFF.FichaAnestesica.Infra.Mapping
{
    public class PatientMapping : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.ExternalIdHuap)
                .HasMaxLength(50)
                .IsRequired(false);

            builder.Property(p => p.MedicalRecord)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(p => p.Name)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(p => p.Gender)
                .HasMaxLength(20);
                
            builder.Property(p => p.BirthDate).IsRequired();
            builder.Property(p => p.RegisteringDate).IsRequired();
            builder.Property(p => p.LastUpdate).IsRequired();
        }
    }
}