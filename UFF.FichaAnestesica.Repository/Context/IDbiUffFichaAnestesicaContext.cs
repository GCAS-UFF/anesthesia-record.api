using Microsoft.EntityFrameworkCore;
using UFF.FichaAnestesica.Domain.Entities;

namespace UFF.FichaAnestesica.Infra.Context
{
    public interface IDbiUffFichaAnestesicaContext
    {
        DbSet<User> User { get; }     

        int SaveChanges();
    }
}
