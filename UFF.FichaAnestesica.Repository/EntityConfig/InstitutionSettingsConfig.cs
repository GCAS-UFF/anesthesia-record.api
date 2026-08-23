using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UFF.FichaAnestesica.Domain.Entities;

namespace UFF.FichaAnestesica.Infra.EntityConfig
{
    public class InstitutionSettingsConfig : IEntityTypeConfiguration<InstitutionSettings>
    {
        public void Configure(EntityTypeBuilder<InstitutionSettings> builder)
        {
            builder.ToTable("institution_settings");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .UseIdentityColumn()
                .IsRequired();

            builder.Property(x => x.MonitoringIntervalMinutes)
                .HasColumnName("monitoring_interval_minutes")
                .HasDefaultValue(InstitutionSettings.DefaultMonitoringIntervalMinutes)
                .IsRequired();

            builder.Property(x => x.SigaApiUrl)
                .HasColumnName("siga_api_url")
                .HasColumnType("varchar(300)");

            builder.Property(x => x.AghuApiUrl)
                .HasColumnName("aghu_api_url")
                .HasColumnType("varchar(300)");

            builder.Property(x => x.HospitalName)
                .HasColumnName("hospital_name")
                .HasColumnType("varchar(200)")
                .HasDefaultValue(InstitutionSettings.DefaultHospitalName)
                .IsRequired();

            builder.Property(x => x.HospitalSector)
                .HasColumnName("hospital_sector")
                .HasColumnType("varchar(200)");

            builder.Property(x => x.HospitalCnpj)
                .HasColumnName("hospital_cnpj")
                .HasColumnType("varchar(20)");

            builder.Property(x => x.HospitalCep)
                .HasColumnName("hospital_cep")
                .HasColumnType("varchar(10)");

            builder.Property(x => x.HospitalStreet)
                .HasColumnName("hospital_street")
                .HasColumnType("varchar(200)");

            builder.Property(x => x.HospitalNumber)
                .HasColumnName("hospital_number")
                .HasColumnType("varchar(20)");

            builder.Property(x => x.HospitalNeighborhood)
                .HasColumnName("hospital_neighborhood")
                .HasColumnType("varchar(120)");

            builder.Property(x => x.HospitalCity)
                .HasColumnName("hospital_city")
                .HasColumnType("varchar(120)")
                .HasDefaultValue(InstitutionSettings.DefaultHospitalCity)
                .IsRequired();

            builder.Property(x => x.HospitalState)
                .HasColumnName("hospital_state")
                .HasColumnType("varchar(2)")
                .HasDefaultValue(InstitutionSettings.DefaultHospitalState)
                .IsRequired();

            builder.Property(x => x.UpdatedByUserId)
                .HasColumnName("updated_by_user_id");

            builder.Property(x => x.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("timestamptz")
                .IsRequired();

            builder.Property(x => x.LastUpdate)
                .HasColumnName("last_update")
                .HasColumnType("timestamptz");

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(x => x.UpdatedByUserId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
