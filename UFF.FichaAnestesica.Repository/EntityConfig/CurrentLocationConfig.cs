using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UFF.FichaAnestesica.Domain.Entities;

namespace UFF.FichaAnestesica.Infra.EntityConfig
{
    public class CurrentLocationConfig : IEntityTypeConfiguration<CurrentLocation>
    {
        public void Configure(EntityTypeBuilder<CurrentLocation> builder)
        {
            builder.ToTable("current_locations");

            builder.HasKey(cl => cl.Id);

            builder.Property(cl => cl.Bed)
                .HasColumnName("bed")
                .HasColumnType("varchar(50)");

            builder.Property(cl => cl.Floor)
                .HasColumnName("floor")
                .HasColumnType("varchar(10)");

            builder.Property(cl => cl.Room)
                .HasColumnName("room")
                .HasColumnType("varchar(50)");

            builder.HasOne(cl => cl.Unit)
                .WithMany()
                .HasForeignKey("unit_id")
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(cl => cl.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("timestamptz");

            builder.Property(cl => cl.LastUpdate)
                .HasColumnName("last_update")
                .HasColumnType("timestamptz");
        }
    }
}