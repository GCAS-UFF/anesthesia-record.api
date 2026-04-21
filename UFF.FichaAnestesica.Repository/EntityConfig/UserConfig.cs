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

            builder.Property(s => s.Id)
               .HasColumnName("id")
               .IsRequired();

            builder.Property(u => u.UserName)
                .HasColumnName("username")
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(u => u.PassWord)
                .HasColumnName("password")
                .HasColumnType("varchar(255)")
                .IsRequired();

            builder.HasIndex(u => u.UserName).IsUnique();

            builder.Property(u => u.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("timestamptz");

            builder.Property(u => u.LastUpdate)
                .HasColumnName("last_update")
                .HasColumnType("timestamptz");
        }
    }
}