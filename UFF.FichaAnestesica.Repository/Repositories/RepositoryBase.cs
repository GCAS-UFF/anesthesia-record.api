using Microsoft.EntityFrameworkCore;
using UFF.FichaAnestesica.Domain.Repositories;
using UFF.FichaAnestesica.Infra.Context;

namespace UFF.FichaAnestesica.Infra.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly SigaDbCtx Db;

        public RepositoryBase(SigaDbCtx context)
        {
            Db = context;
        }

        public async Task AddAsync(T obj)
        {
            await Db.Set<T>().AddAsync(obj);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await Db.Set<T>().ToListAsync();
        }

        public async Task DisposeAsync()
        {
            await Db.DisposeAsync();
        }

        public void Remove(T obj)
        {
            Db.Set<T>().Remove(obj);
        }

        public void Update(T obj)
        {
            Db.Set<T>().Update(obj);
        }

        public async Task SaveChangesAsync()
        {
            await Db.SaveChangesAsync();
        }
    }
}