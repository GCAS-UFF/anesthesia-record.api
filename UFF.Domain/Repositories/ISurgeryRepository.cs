using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Domain.Repositories
{
    public interface ISurgeryRepository
    {
        Task AddOrUpdatePatientsAsync(IList<Patient> patients);
        Task<List<Patient>> GetPatientsWithSurgeriesAsync(DateTime? date = null, SurgeryStatus? status = null, int page = 1, int size = 10);
    }
}