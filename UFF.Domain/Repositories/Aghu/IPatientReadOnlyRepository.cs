using UFF.FichaAnestesica.Domain.Dto;
using UFF.FichaAnestesica.Domain.Enums;
using UFF.FichaAnestesica.Domain.Response;

namespace UFF.FichaAnestesica.Domain.Repositories.ReadOnly
{
    public interface IPatientReadOnlyRepository
    {
        Task<PagedResponse<PatientDetailDto>> GetPatientsFromHospitalAsync(DateTime? date, string term, SurgeryStatusEnum? status, int page = 1, int pageSize = 10);      
        Task<PatientDetailDto> GetFromHospitalByPatientIdAndSurgeryIdAsync(string patientId, int surgeryId);
        Task<PagedResponse<PatientDetailDto>> GetMyPatientsFromHospitalAsync(IEnumerable<int> surgeryIds, string? term, int page = 1, int pageSize = 10);
    }
}
