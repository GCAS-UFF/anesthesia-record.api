using Microsoft.EntityFrameworkCore;
using UFF.FichaAnestesica.Domain.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace UFF.FichaAnestesica.Infra.Context
{
    public interface ISigaDbCtx 
    {
        public DbSet<Patient> Patients { get; }
        public DbSet<CurrentLocation> CurrentLocations { get; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Surgery> Surgeries { get; }
        public DbSet<Specialty> Specialties { get; }
        public DbSet<Procedure> Procedures { get; }
        public DbSet<SurgeryLocation> SurgeryLocations { get; }
        public DbSet<SurgicalCenter> SurgicalCenters { get; }
        public DbSet<User> Users { get; }
        EntityEntry Entry(object entity);

        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
