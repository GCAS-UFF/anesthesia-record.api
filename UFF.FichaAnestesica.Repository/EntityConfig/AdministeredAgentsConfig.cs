using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UFF.FichaAnestesica.Infra.EntityConfig
{
    public class AdministeredAgentConfig : IEntityTypeConfiguration<AdministeredAgent>
    {
        public void Configure(EntityTypeBuilder<AdministeredAgent> builder)
        {
            builder.ToTable("administered_agents");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .UseIdentityColumn()
                .IsRequired();

            builder.Property(x => x.Timestamp)
                .HasColumnName("timestamp")
                .HasColumnType("timestamptz")
                .IsRequired();

            builder.Property(x => x.Dose)
                .HasColumnName("dose")
                .HasColumnType("numeric(10,2)")
                .IsRequired();

            builder.Property(x => x.Unit)
                .HasColumnName("unit")
                .HasConversion<int>()
                .IsRequired();

            builder.Property(x => x.Route)
                .HasColumnName("route")
                .HasConversion<int>()
                .IsRequired();          

            builder.Property(x => x.DrugId)
                .HasColumnName("drug_id")
                .IsRequired();


            builder.Property(x => x.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("timestamptz")
                .IsRequired();

            builder.Property(x => x.LastUpdate)
                .HasColumnName("last_update")
                .HasColumnType("timestamptz");

            builder.HasOne(x => x.Drug)
                .WithMany()
                .HasForeignKey(x => x.DrugId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.Property(x => x.MonitoringRecordId)
                .HasColumnName("monitoring_record_id")
                .IsRequired();

            builder.HasOne(x => x.MonitoringRecord)
                .WithMany(x => x.AdministeredAgents)
                .HasForeignKey(x => x.MonitoringRecordId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => x.Timestamp);

            builder.HasIndex(x => x.DrugId);

            builder.HasIndex(x => x.MonitoringRecordId);
        }
    }
}