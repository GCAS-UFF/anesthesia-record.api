using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UFF.FichaAnestesica.Domain.Entities;

namespace UFF.FichaAnestesica.Infra.EntityConfig
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .HasColumnName("id")
                .UseIdentityColumn()
                .IsRequired();

            builder.Property(u => u.Name)
                .HasColumnName("name")
                .HasColumnType("varchar(150)")
                .IsRequired();

            builder.Property(u => u.Registration)
                .HasColumnName("registration")
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.Property(u => u.Sector)
                .HasColumnName("sector")
                .HasColumnType("varchar(100)");

            builder.Property(u => u.Email)
                .HasColumnName("email")
                .HasColumnType("varchar(150)");

            builder.Property(u => u.Role)
                .HasColumnName("role")
                .HasColumnType("varchar(100)");

            builder.Property(u => u.CanLogIn)
                .HasColumnName("can_login")
                .HasDefaultValue(false)
                .IsRequired();

            builder.Property(u => u.Status)
                .HasColumnName("status")
                .HasConversion<int>()
                .IsRequired();

            builder.Property(u => u.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("timestamptz")
                .IsRequired();

            builder.Property(u => u.LastUpdate)
                .HasColumnName("last_update")
                .HasColumnType("timestamptz");

            builder.Property(u => u.LastLoginAt)
                .HasColumnName("last_login_at")
                .HasColumnType("timestamptz")
                .IsRequired();

            builder.Property(u => u.LastSyncAt)
                .HasColumnName("last_sync_at")
                .HasColumnType("timestamptz");

            builder.HasIndex(u => u.Registration)
                .IsUnique();

            builder.HasIndex(u => u.Email);
            builder.HasIndex(u => u.Status);
            builder.HasIndex(u => u.CanLogIn);
        }
    }
}