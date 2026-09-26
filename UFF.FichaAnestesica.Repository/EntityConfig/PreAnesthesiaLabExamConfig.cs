using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UFF.FichaAnestesica.Domain.Entities;

namespace UFF.FichaAnestesica.Infra.EntityConfig
{
    public class PreAnesthesiaLabExamConfig : IEntityTypeConfiguration<PreAnesthesiaLabExam>
    {
        public void Configure(EntityTypeBuilder<PreAnesthesiaLabExam> builder)
        {
            builder.ToTable("pre_anesthesia_lab_exams");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .UseIdentityColumn()
                .IsRequired();

            builder.Property(x => x.AnesthesiaRecordId)
                .HasColumnName("anesthesia_record_id")
                .IsRequired();

            builder.HasOne(x => x.AnesthesiaRecord)
                .WithMany()
                .HasForeignKey(x => x.AnesthesiaRecordId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_pre_anesthesia_lab_exams_anesthesia_record");
                        
            builder.HasIndex(x => x.AnesthesiaRecordId).IsUnique();

            builder.Property(x => x.ImportStatus).HasColumnName("import_status").HasConversion<int>().IsRequired();

            builder.Property(x => x.SourceExamId).HasColumnName("source_exam_id");            
            builder.Property(x => x.RequestedAt).HasColumnName("requested_at").HasColumnType("timestamp without time zone");
            builder.Property(x => x.CollectedAt).HasColumnName("collected_at").HasColumnType("timestamp without time zone");
            builder.Property(x => x.ReleasedAt).HasColumnName("released_at").HasColumnType("timestamp without time zone");
            builder.Property(x => x.ImportedAt).HasColumnName("imported_at").HasColumnType("timestamptz");

            builder.Property(x => x.ImportAttempts).HasColumnName("import_attempts").IsRequired().HasDefaultValue(0);
            builder.Property(x => x.LastImportAttemptAt).HasColumnName("last_import_attempt_at").HasColumnType("timestamptz");
            builder.Property(x => x.LastImportMessage).HasColumnName("last_import_message").HasColumnType("text");
            builder.Property(x => x.LastManualEditAt).HasColumnName("last_manual_edit_at").HasColumnType("timestamptz");

            // Coluna de sistema do PostgreSQL (não é criada pela migration). O nome é explícito
            // porque o SigaDbCtx converte todos os nomes para snake_case antes das configurações.
            builder.Property(x => x.Version)
                .HasColumnName("xmin")
                .HasColumnType("xid")
                .IsRowVersion();

            builder.HasMany(x => x.Results)
                .WithOne(x => x.LabExam)
                .HasForeignKey(x => x.LabExamId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_pre_anesthesia_lab_results_lab_exam");

            builder.Property(x => x.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("timestamptz")
                .IsRequired();

            builder.Property(x => x.LastUpdate)
                .HasColumnName("last_update")
                .HasColumnType("timestamptz");
        }
    }
}
