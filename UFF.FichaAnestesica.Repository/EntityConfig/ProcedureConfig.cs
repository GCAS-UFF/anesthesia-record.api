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

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(p => p.ExternalId)
                .HasColumnName("procedure_id")
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.HasIndex(p => p.ExternalId)
                .IsUnique();

            builder.Property(p => p.Description)
                .HasColumnName("description")
                .HasColumnType("varchar(500)");

            builder.Property(p => p.Cid)
                .HasColumnName("cid")
                .HasColumnType("varchar(20)");

            builder.Property(p => p.IsPrimary)
                .HasColumnName("is_primary");

            builder.Property(p => p.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("timestamptz");

            builder.Property(p => p.LastUpdate)
                .HasColumnName("last_update")
                .HasColumnType("timestamptz");
        }
    }
}