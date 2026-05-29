using UFF.FichaAnestesica.Domain.Dto;
using UFF.FichaAnestesica.Domain.Enums;
using UFF.FichaAnestesica.Domain.Response;

namespace UFF.FichaAnestesica.Domain.Repositories.ReadOnly
{
    public interface IPatientReadOnlyRepository
    {
        Task<PagedResponse<PatientDto>> GetPatientsFromHospitalAsync(DateTime? date, SurgeryStatusEnum? status, int page = 1, int pageSize = 10);
        Task<PatientDto> GetPatientFromHospitalByIdAsync(string id);
    }
}
