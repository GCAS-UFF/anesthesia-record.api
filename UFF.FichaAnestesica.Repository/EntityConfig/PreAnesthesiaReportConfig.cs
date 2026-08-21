using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UFF.FichaAnestesica.Domain.Entities;

namespace UFF.FichaAnestesica.Infra.EntityConfig
{
    public class PreAnesthesiaReportConfig : IEntityTypeConfiguration<PreAnesthesiaReport>
    {
        public void Configure(EntityTypeBuilder<PreAnesthesiaReport> builder)
        {
            builder.ToTable("pre_anesthesia_reports");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .UseIdentityColumn()
                .IsRequired();

            builder.Property(x => x.Specialty)
                .HasColumnName("specialty")
                .HasConversion<int>();

            builder.Property(x => x.Description)
                .HasColumnName("description")
                .HasColumnType("text");

            builder.Property(x => x.PreAnesthesiaRecordId)
                .HasColumnName("pre_anesthesia_record_id")
                .IsRequired();

            builder.HasOne(x => x.PreAnesthesiaRecord)
                .WithMany(x => x.Reports)
                .HasForeignKey(x => x.PreAnesthesiaRecordId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_pre_anesthesia_reports_pre_anesthesia_record");

            builder.Property(x => x.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("timestamptz")
                .IsRequired();

            builder.Property(x => x.LastUpdate)
                .HasColumnName("last_update")
                .HasColumnType("timestamptz");

            builder.HasIndex(x => x.PreAnesthesiaRecordId);
        }
    }
}
