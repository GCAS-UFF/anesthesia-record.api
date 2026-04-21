using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UFF.FichaAnestesica.Domain.Entities;

namespace UFF.FichaAnestesica.Infra.EntityConfig
{
    public class SurgeryLocationConfig : IEntityTypeConfiguration<SurgeryLocation>
    {
        public void Configure(EntityTypeBuilder<SurgeryLocation> builder)
        {
            builder.ToTable("surgery_locations");

            builder.Property(s => s.Id)
                .HasColumnName("id")
                .IsRequired();

            builder.Property(sl => sl.Room)
                .HasColumnName("room")
                .HasColumnType("varchar(50)");

            builder.HasOne(sl => sl.SurgicalCenter)
                .WithMany()
                .HasForeignKey("surgical_center_id")
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(sl => sl.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("timestamptz");

            builder.Property(sl => sl.LastUpdate)
                .HasColumnName("last_update")
                .HasColumnType("timestamptz");
        }
    }
}