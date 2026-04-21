using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UFF.FichaAnestesica.Domain.Entities;

namespace UFF.FichaAnestesica.Infra.EntityConfig
{
    public class SurgicalCenterConfig : IEntityTypeConfiguration<SurgicalCenter>
    {
        public void Configure(EntityTypeBuilder<SurgicalCenter> builder)
        {
            builder.ToTable("surgical_centers");

            builder.Property(s => s.Id)
                .HasColumnName("id")
                .IsRequired();

            builder.Property(sc => sc.Code)
                .HasColumnName("code")
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.Property(sc => sc.Description)
                .HasColumnName("description")
                .HasColumnType("varchar(200)");

            builder.HasIndex(sc => sc.Code).IsUnique();

            builder.Property(sc => sc.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("timestamptz");

            builder.Property(sc => sc.LastUpdate)
                .HasColumnName("last_update")
                .HasColumnType("timestamptz");
        }
    }
}