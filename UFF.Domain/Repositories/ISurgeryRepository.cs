using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Domain.Repositories
{
    public interface ISurgeryRepository
    {
        Task AddOrUpdatePatientsAsync(IEnumerable<Patient> patients);
        Task<IEnumerable<Patient>> GetPatientsWithSurgeriesAsync(DateTime? date = null, SurgeryStatus? status = null);
    }
}
