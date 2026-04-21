using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UFF.FichaAnestesica.Domain.Entities;

namespace UFF.FichaAnestesica.Infra.EntityConfig
{
    public class SpecialtyConfig : IEntityTypeConfiguration<Specialty>
    {
        public void Configure(EntityTypeBuilder<Specialty> builder)
        {
            builder.ToTable("specialties");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Code)
                .HasColumnName("code")
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.Property(s => s.Description)
                .HasColumnName("description")
                .HasColumnType("varchar(200)");

            builder.HasIndex(s => s.Code).IsUnique();

            builder.Property(s => s.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("timestamptz");

            builder.Property(s => s.LastUpdate)
                .HasColumnName("last_update")
                .HasColumnType("timestamptz");
        }
    }
}