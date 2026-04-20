using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Enums;
using UFF.FichaAnestesica.Domain.Repositories;

namespace UFF.FichaAnestesica.Infra.Repositories
{
    public class SurgeryRepository : ISurgeryRepository
    {
        public async Task<IEnumerable<Surgery>> GetSurgeriesAsync(DateTime date, SurgeryStatus status)
        {
            return null;
        }
    }
}