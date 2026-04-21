using UFF.FichaAnestesica.Domain.Dto;
using UFF.FichaAnestesica.Domain.Enums;
using UFF.FichaAnestesica.Domain.Response;

namespace UFF.FichaAnestesica.Domain.Repositories.ReadOnly
{
    public interface IHospitalReadRepository
    {
        Task<PagedResponse<PatientViewDto>> GetSurgeriesFromHospitalAsync(DateTime? date, SurgeryStatus? status, int page = 1, int pageSize = 10);
    }
}
