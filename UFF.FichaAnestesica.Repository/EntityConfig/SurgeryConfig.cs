using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UFF.FichaAnestesica.Domain.Entities;

namespace UFF.FichaAnestesica.Infra.EntityConfig
{
    //public class SurgeryConfig : IEntityTypeConfiguration<Surgery>
    //{
    //    public void Configure(EntityTypeBuilder<Surgery> builder)
    //    {
    //        builder.HasKey(s => s.Id);

    //        builder.Property(s => s.ExternalIdHuap)
    //            .HasMaxLength(50)
    //            .IsRequired(false);

    //        // Chave Estrangeira com Paciente (Patient)
    //        builder.HasOne(s => s.Patient)
    //            .WithMany() // Paciente não tem lista reversa explicitamente
    //            .HasForeignKey(s => s.PatientId)
    //            .OnDelete(DeleteBehavior.Restrict);

    //        builder.Property(s => s.ProposedProcedure)
    //            .HasMaxLength(250).IsRequired();

    //        builder.Property(s => s.Room).HasMaxLength(50);
    //        builder.Property(s => s.Bed).HasMaxLength(50);

    //        builder.Property(s => s.Status)
    //            .HasMaxLength(30)
    //            .HasDefaultValue("waiting");

    //        builder.Property(s => s.Surgeon).HasMaxLength(150);
    //        builder.Property(s => s.Specialty).HasMaxLength(100);

    //        builder.Property(s => s.SurgeryDate).IsRequired();
    //        builder.Property(s => s.RegisteringDate).IsRequired();
    //        builder.Property(s => s.LastUpdate).IsRequired();
    //    }
    //}
}