using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UFF.FichaAnestesica.Domain.Entities;

namespace UFF.FichaAnestesica.Infra.EntityConfig
{
    public class AnesthesiaRecordAirwayDeviceConfig : IEntityTypeConfiguration<AnesthesiaRecordAirwayDevice>
    {
        public void Configure(EntityTypeBuilder<AnesthesiaRecordAirwayDevice> builder)
        {
            builder.ToTable("anesthesia_record_airway_devices");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.AnesthesiaRecordId)
                .HasColumnName("anesthesia_record_id")
                .IsRequired();

            builder.Property(x => x.DeviceType)
                .HasColumnName("device_type")
                .HasConversion<int>()
                .IsRequired();

            builder.HasOne(x => x.AnesthesiaRecord)
                .WithMany(x => x.AirwayDevices)
                .HasForeignKey(x => x.AnesthesiaRecordId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}