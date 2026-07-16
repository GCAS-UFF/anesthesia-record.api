using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UFF.FichaAnestesica.Infra.EntityConfig
{
    public class AnesthesiaRecordProcedureConfig : IEntityTypeConfiguration<AnesthesiaRecordProcedure>
    {
        public void Configure(EntityTypeBuilder<AnesthesiaRecordProcedure> builder)
        {
            builder.ToTable("anesthesia_record_procedures");

            builder.HasKey(x => new
            {
                x.AnesthesiaRecordId,
                x.ProcedureId
            });

            builder.Property(x => x.AnesthesiaRecordId)
                .HasColumnName("anesthesia_record_id");

            builder.Property(x => x.ProcedureId)
                .HasColumnName("procedure_id");

            builder.Property(x => x.IsPrimary)
                .HasColumnName("is_primary")
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("timestamptz");

            builder.HasOne(x => x.AnesthesiaRecord)
                .WithMany(x => x.Procedures)
                .HasForeignKey(x => x.AnesthesiaRecordId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Procedure)
                .WithMany(x => x.AnesthesiaRecords)
                .HasForeignKey(x => x.ProcedureId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}