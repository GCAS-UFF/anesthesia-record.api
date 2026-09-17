using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UFF.FichaAnestesica.Infra.EntityConfig
{
    public class InfusionPumpConfig : IEntityTypeConfiguration<InfusionPump>
    {
        public void Configure(EntityTypeBuilder<InfusionPump> builder)
        {
            builder.ToTable("infusion_pumps");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .UseIdentityColumn()
                .IsRequired();

            builder.Property(x => x.Time)
                .HasColumnName("time")
                .HasColumnType("time without time zone")
                .IsRequired();

            builder.Property(x => x.Date)
                .HasColumnName("timestamp")
                .HasColumnType("timestamptz")
                .IsRequired();

            // Coluna original "rate_ml_per_hour" — renomeada para "rate" (genérica) na
            // migration AddBolusAndInfusionRateUnit, junto com a nova "rate_unit".
            builder.Property(x => x.Rate)
                .HasColumnName("rate")
                .HasColumnType("numeric(10,2)")
                .IsRequired();

            builder.Property(x => x.RateUnit)
                .HasColumnName("rate_unit")
                .HasConversion<int>()
                .IsRequired();

            builder.Property(x => x.VolumeMl)
                .HasColumnName("volume_ml")
                .HasColumnType("numeric(10,2)")
                .IsRequired();

            builder.Property(x => x.EndAt)
                .HasColumnName("end_at")
                .HasColumnType("timestamptz")
                .IsRequired();

            builder.Property(x => x.DrugId)
                .HasColumnName("drug_id")
                .IsRequired();

            builder.HasOne(x => x.Drug)
                .WithMany()
                .HasForeignKey(x => x.DrugId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("timestamptz")
                .IsRequired();

            builder.Property(x => x.LastUpdate)
                .HasColumnName("last_update")
                .HasColumnType("timestamptz");

            builder.Property(x => x.MonitoringRecordId)
                .HasColumnName("monitoring_record_id")
                .IsRequired();

            builder.HasOne(x => x.MonitoringRecord)
                .WithMany(x => x.InfusionPumps)
                .HasForeignKey(x => x.MonitoringRecordId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => x.Date).HasDatabaseName("IX_infusion_pumps_timestamp");
            builder.HasIndex(x => x.DrugId);
            builder.HasIndex(x => x.MonitoringRecordId);
        }
    }
}
