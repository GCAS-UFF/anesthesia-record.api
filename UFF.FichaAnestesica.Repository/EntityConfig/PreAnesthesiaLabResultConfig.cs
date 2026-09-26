using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UFF.FichaAnestesica.Domain.Entities;

namespace UFF.FichaAnestesica.Infra.EntityConfig
{
    public class PreAnesthesiaLabResultConfig : IEntityTypeConfiguration<PreAnesthesiaLabResult>
    {
        public void Configure(EntityTypeBuilder<PreAnesthesiaLabResult> builder)
        {
            builder.ToTable("pre_anesthesia_lab_results");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .UseIdentityColumn()
                .IsRequired();

            builder.Property(x => x.LabExamId).HasColumnName("lab_exam_id").IsRequired();
                        
            builder.HasIndex(x => new { x.LabExamId, x.Analyte }).IsUnique();

            builder.Property(x => x.Analyte).HasColumnName("analyte").HasConversion<int>().IsRequired();
            builder.Property(x => x.Value).HasColumnName("value").HasColumnType("numeric(14,4)");
            builder.Property(x => x.Unit).HasColumnName("unit").HasColumnType("text");
            builder.Property(x => x.ReferenceRange).HasColumnName("reference_range").HasColumnType("text");
            builder.Property(x => x.Source).HasColumnName("source").HasConversion<int>().IsRequired();
            builder.Property(x => x.ImportedValue).HasColumnName("imported_value").HasColumnType("numeric(14,4)");
            builder.Property(x => x.AghuCode).HasColumnName("aghu_code").HasColumnType("text");

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
