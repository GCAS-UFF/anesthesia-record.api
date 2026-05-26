using Microsoft.EntityFrameworkCore;
using UFF.FichaAnestesica.Domain.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace UFF.FichaAnestesica.Infra.Context
{
    public interface ISigaDbCtx
    {
        public DbSet<AnesthesiaRecord> AnesthesiaRecord { get; }
        public DbSet<PreAnesthesiaRecord> PreAnesthesiaRecord { get;}      
        public DbSet<User> Users { get; }
        EntityEntry Entry(object entity);
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
