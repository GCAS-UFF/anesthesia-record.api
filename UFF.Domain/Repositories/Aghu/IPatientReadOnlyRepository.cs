using UFF.FichaAnestesica.Domain.Dto;
using UFF.FichaAnestesica.Domain.Enums;
using UFF.FichaAnestesica.Domain.Response;

namespace UFF.FichaAnestesica.Domain.Repositories.ReadOnly
{
    public interface IPatientReadOnlyRepository
    {
        Task<PagedResponse<PatientListDto>> GetPatientsFromHospitalAsync(DateTime? date, string term, SurgeryStatusEnum? status, int page = 1, int pageSize = 10);      
        Task<PatientListDto> GetFromHospitalByPatientIdAndSurgeryIdAsync(string patientId, int surgeryId);
    }
}
