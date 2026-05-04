using UFF.FichaAnestesica.Domain.Enums;
using UFF.FichaAnestesica.Domain.Response;

namespace UFF.FichaAnestesica.Domain.Services
{
    public interface ISurgeryService
    {
        Task<PagedResponse<PatientSurgeryResponse>> GetPatientsWithSurgeriesAsync(DateTime? date, SurgeryStatus? status, int page = 1, int size = 10);
    }
}