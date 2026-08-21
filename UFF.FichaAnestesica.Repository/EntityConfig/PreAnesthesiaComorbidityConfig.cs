using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UFF.FichaAnestesica.Domain.Entities;

namespace UFF.FichaAnestesica.Infra.EntityConfig
{
    public class PreAnesthesiaComorbidityConfig : IEntityTypeConfiguration<PreAnesthesiaComorbidity>
    {
        public void Configure(EntityTypeBuilder<PreAnesthesiaComorbidity> builder)
        {
            builder.ToTable("pre_anesthesia_comorbidities");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .UseIdentityColumn()
                .IsRequired();

            builder.Property(x => x.GroupKey)
                .HasColumnName("group_key")
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.Property(x => x.Findings)
                .HasColumnName("findings")
                .HasColumnType("text[]");

            builder.Property(x => x.OtherDescription)
                .HasColumnName("other_description")
                .HasColumnType("text");

            builder.Property(x => x.Observations)
                .HasColumnName("observations")
                .HasColumnType("text");

            builder.Property(x => x.PreAnesthesiaRecordId)
                .HasColumnName("pre_anesthesia_record_id")
                .IsRequired();

            builder.HasOne(x => x.PreAnesthesiaRecord)
                .WithMany(x => x.Comorbidities)
                .HasForeignKey(x => x.PreAnesthesiaRecordId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_pre_anesthesia_comorbidities_pre_anesthesia_record");

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
