using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UFF.FichaAnestesica.Infra.EntityConfig
{
    public class OxygenFlowConfig : IEntityTypeConfiguration<OxygenFlow>
    {
        public void Configure(EntityTypeBuilder<OxygenFlow> builder)
        {
            builder.ToTable("oxygen_flows");

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

            builder.Property(x => x.FlowRateLPerMin)
                .HasColumnName("flow_rate_l_per_min")
                .HasColumnType("numeric(6,2)");

            builder.Property(x => x.IsActive)
                .HasColumnName("is_active")
                .IsRequired();

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
                .WithMany(x => x.OxygenFlows)
                .HasForeignKey(x => x.MonitoringRecordId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => x.Date).HasDatabaseName("IX_oxygen_flows_timestamp");
            builder.HasIndex(x => x.MonitoringRecordId);
        }
    }
}
