using Microsoft.EntityFrameworkCore;
using UFF.FichaAnestesica.Domain.Dto;

namespace UFF.FichaAnestesica.Infra.Context
{
    public class SigaDbReadOnlyCtx : DbContext, ISigaDbReadOnlyCtx
    {
        public SigaDbReadOnlyCtx(DbContextOptions<SigaDbReadOnlyCtx> options)
                : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public DbSet<PatientViewDto> PatientViews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PatientViewDto>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("view_pacientes_cirurgias", "siga_db");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.MedicalRecordNumber).HasColumnName("numero_prontuario");
                entity.Property(e => e.FullName).HasColumnName("nome_completo");
                entity.Property(e => e.BirthDate).HasColumnName("data_nascimento");
                entity.Property(e => e.Gender).HasColumnName("sexo");
                entity.Property(e => e.WeightKg).HasColumnName("peso_kg");
                entity.Property(e => e.HeightCm).HasColumnName("altura_cm");

                entity.Property(e => e.UnitCode).HasColumnName("unidade_codigo");
                entity.Property(e => e.UnitDescription).HasColumnName("unidade_descricao");
                entity.Property(e => e.Bed).HasColumnName("leito");
                entity.Property(e => e.Floor).HasColumnName("andar");
                entity.Property(e => e.Room).HasColumnName("quarto");
           
                entity.Property(e => e.SurgeryId).HasColumnName("cirurgia_id");
                entity.Property(e => e.SurgeryDate).HasColumnName("data_cirurgia");
                entity.Property(e => e.SurgeryStatus).HasColumnName("status_cirurgia");
          
                entity.Property(e => e.SpecialtyCode).HasColumnName("especialidade_codigo");
                entity.Property(e => e.SpecialtyDescription).HasColumnName("especialidade_descricao");
         
                entity.Property(e => e.SurgicalCenterCode).HasColumnName("centro_cirurgico_codigo");
                entity.Property(e => e.SurgicalCenterDescription).HasColumnName("centro_cirurgico_descricao");
                entity.Property(e => e.SurgeryRoom).HasColumnName("sala");
      
                entity.Property(e => e.ProcedureId).HasColumnName("procedimento_id");
                entity.Property(e => e.ProcedureDescription).HasColumnName("procedimento_descricao");
                entity.Property(e => e.ProcedureCid).HasColumnName("procedimento_cid");
                entity.Property(e => e.IsPrimaryProcedure).HasColumnName("procedimento_principal");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
