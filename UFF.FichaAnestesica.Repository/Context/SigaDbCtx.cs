using Microsoft.EntityFrameworkCore;
using UFF.FichaAnestesica.CrossCutting.Extensions;
using UFF.FichaAnestesica.Domain.Entities;

namespace UFF.FichaAnestesica.Infra.Context
{
    public class SigaDbCtx : DbContext, ISigaDbCtx
    {
        public SigaDbCtx(DbContextOptions<SigaDbCtx> options)
                : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<CurrentLocation> CurrentLocations { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Surgery> Surgeries { get; set; }
        public DbSet<Specialty> Specialties { get; set; }
        public DbSet<Procedure> Procedures { get; set; }
        public DbSet<SurgeryLocation> SurgeryLocations { get; set; }
        public DbSet<SurgicalCenter> SurgicalCenters { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("siga_db");

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.SetTableName(entity.GetTableName()?.ToSnakeCase());

                foreach (var property in entity.GetProperties())
                {
                    property.SetColumnName(property.Name.ToSnakeCase());
                }

                foreach (var key in entity.GetForeignKeys())
                {
                    key.SetConstraintName(key.GetConstraintName()?.ToSnakeCase());
                }
            }

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SigaDbCtx).Assembly);
            base.OnModelCreating(modelBuilder);
        }     

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("CreatedAt") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("CreatedAt").CurrentValue = DateTime.UtcNow;
                    entry.Property("LastUpdate").CurrentValue = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Property("CreatedAt").IsModified = false;
                    entry.Property("Id").IsModified = false;
                    entry.Property("LastUpdate").CurrentValue = DateTime.UtcNow;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("CreatedAt") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("CreatedAt").CurrentValue = DateTime.UtcNow;
                    entry.Property("LastUpdate").CurrentValue = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Property("CreatedAt").IsModified = false;
                    entry.Property("Id").IsModified = false;
                    entry.Property("LastUpdate").CurrentValue = DateTime.UtcNow;
                }
            }
            return base.SaveChanges();
        }
    }
}
