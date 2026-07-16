using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UFF.FichaAnestesica.Domain.Entities;

namespace UFF.FichaAnestesica.Infra.EntityConfig
{
    public class ProcedureConfig : IEntityTypeConfiguration<Procedure>
    {
        public void Configure(EntityTypeBuilder<Procedure> builder)
        {
            builder.ToTable("procedures");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id");

            builder.Property(x => x.ExternalId)
                .HasColumnName("external_id")
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.Property(x => x.Code)
                .HasColumnName("code")
                .HasColumnType("varchar(30)")
                .IsRequired();

            builder.Property(x => x.Description)
                .HasColumnName("description")
                .HasColumnType("varchar(255)")
                .IsRequired();

            builder.Property(x => x.Cid)
                .HasColumnName("cid")
                .HasColumnType("varchar(20)");

            builder.Property(x => x.Active)
                .HasColumnName("active")
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("timestamptz");

            builder.Property(x => x.LastSyncAt)
                .HasColumnName("last_sync_at")
                .HasColumnType("timestamptz");

            builder.HasIndex(x => x.ExternalId)
                .IsUnique();

            builder.HasIndex(x => x.Code);

            builder.HasMany(x => x.AnesthesiaRecords)
                .WithOne(x => x.Procedure)
                .HasForeignKey(x => x.ProcedureId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}