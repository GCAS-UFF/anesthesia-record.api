using Microsoft.EntityFrameworkCore;
using UFF.FichaAnestesica.Domain.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace UFF.FichaAnestesica.Infra.Context
{
    public interface ISigaDbCtx
    {
        public DbSet<AnesthesiaRecord> AnesthesiaRecords { get; }
        public DbSet<PreAnesthesiaRecord> PreAnesthesiaRecords { get; }
        public DbSet<User> Users { get; }
        public DbSet<AnesthesiaRecordSurgery> AnesthesiaRecordProcedures { get; }
        public DbSet<AdministeredAgent> AdministeredAgents { get; }
        public DbSet<ClinicalEvent> ClinicalEvents { get; }
        public DbSet<CustomField> CustomFields { get; }
        public DbSet<Drug> Drugs { get; }
        public DbSet<Procedure> Procedures { get; }
        public DbSet<FluidBalance> FluidBalances { get; }
        public DbSet<MonitoringRecord> MonitoringRecords { get; }
        public DbSet<VitalSignRecord> VitalSignRecords { get; }
        public DbSet<AnesthesiaRecordAirwayDevice> AnesthesiaRecordAirwayDevices { get; }
        public DbSet<AnesthesiaRecordPunctureLevel> AnesthesiaRecordPunctureLevels { get; }
        public DbSet<AnesthesiaRecordOxygenSupplementation> AnesthesiaRecordOxygenSupplementations { get; }
        public DbSet<AnesthesiaRecordStimulatedNerve> AnesthesiaRecordStimulatedNerves { get; }

        EntityEntry Entry(object entity);
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
