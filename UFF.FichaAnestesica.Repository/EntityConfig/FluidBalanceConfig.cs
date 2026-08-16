using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UFF.FichaAnestesica.Domain.Entities;

namespace UFF.FichaAnestesica.Infra.EntityConfig
{
    public class FluidBalanceConfig : IEntityTypeConfiguration<FluidBalance>
    {
        public void Configure(EntityTypeBuilder<FluidBalance> builder)
        {
            builder.ToTable("fluid_balances");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .UseIdentityColumn()
                .IsRequired();

            builder.Property(x => x.Date)
                .HasColumnName("timestamp")
                .HasColumnType("timestamptz")
                .IsRequired();

            builder.Property(x => x.Time)
                .HasColumnName("time")
                .HasColumnType("time without time zone")
                .IsRequired();

            builder.Property(x => x.Type)
                .HasColumnName("type")
                .HasConversion<int>()
                .IsRequired();

            builder.Property(x => x.Category)
                .HasColumnName("category")
                .HasConversion<int>()
                .IsRequired();

            builder.Property(x => x.VolumeMl)
                .HasColumnName("volume_ml")
                .HasColumnType("numeric(10,2)")
                .IsRequired();

            builder.Property(x => x.MonitoringRecordId)
             .HasColumnName("monitoring_record_id")
             .IsRequired();

            builder.HasOne(x => x.MonitoringRecord)
                .WithMany(x => x.FluidBalances)
                .HasForeignKey(x => x.MonitoringRecordId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("timestamptz")
                .IsRequired();

            builder.Property(x => x.LastUpdate)
                .HasColumnName("last_update")
                .HasColumnType("timestamptz");

            builder.HasIndex(x => x.Date);

            builder.HasIndex(x => x.Type);

            builder.HasIndex(x => x.Category);
        }
    }
}