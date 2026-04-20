using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Domain.Repositories
{
    public interface ISurgeryRepository
    {
        Task<IEnumerable<Surgery>> GetSurgeriesAsync(DateTime date, SurgeryStatus status);
    }
}
