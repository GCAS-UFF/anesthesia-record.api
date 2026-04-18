using Microsoft.EntityFrameworkCore;
using UFF.FichaAnestesica.CrossCutting.Extensions;
using UFF.FichaAnestesica.Domain.Entities;

namespace UFF.FichaAnestesica.Infra.Context
{
    public class DbiUffFichaAnestesicaContext : DbContext, IDbiUffFichaAnestesicaContext
    {
        public DbiUffFichaAnestesicaContext(DbContextOptions<DbiUffFichaAnestesicaContext> options)
                : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Patient> Patient { get; set; }
        public DbSet<SurgicalCase> SurgicalCase { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.SetTableName(entity.GetTableName().ToSnakeCase());

                foreach (var property in entity.GetProperties())
                {
                    property.SetColumnName(property.Name.ToSnakeCase());
                }

                foreach (var key in entity.GetForeignKeys())
                {
                    key.SetConstraintName(key.GetConstraintName().ToSnakeCase());
                }
            }

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DbiUffFichaAnestesicaContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }     

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("RegisteringDate") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("RegisteringDate").CurrentValue = DateTime.UtcNow;
                    entry.Property("LastUpdate").CurrentValue = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Property("RegisteringDate").IsModified = false;
                    entry.Property("Id").IsModified = false;
                    entry.Property("LastUpdate").CurrentValue = DateTime.UtcNow;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("RegisteringDate") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("RegisteringDate").CurrentValue = DateTime.UtcNow;
                    entry.Property("LastUpdate").CurrentValue = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Property("RegisteringDate").IsModified = false;
                    entry.Property("Id").IsModified = false;
                    entry.Property("LastUpdate").CurrentValue = DateTime.UtcNow;
                }
            }
            return base.SaveChanges();
        }
    }
}
