using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UFF.FichaAnestesica.Domain.Entities;

namespace UFF.FichaAnestesica.Infra.EntityConfig
{
    public class UnitConfig : IEntityTypeConfiguration<Unit>
    {
        public void Configure(EntityTypeBuilder<Unit> builder)
        {
            builder.ToTable("units");

            builder.Property(s => s.Id)
                   .HasColumnName("id")
                   .IsRequired();

            builder.Property(u => u.Code)
                .HasColumnName("code")
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.Property(u => u.Description)
                .HasColumnName("description")
                .HasColumnType("varchar(200)");

            builder.HasIndex(u => u.Code).IsUnique();

            builder.Property(u => u.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("timestamptz");

            builder.Property(u => u.LastUpdate)
                .HasColumnName("last_update")
                .HasColumnType("timestamptz");
        }
    }
}