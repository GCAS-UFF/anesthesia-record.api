using UFF.FichaAnestesica.Domain.Commands;
using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Domain.Services
{
    public interface ISurgeryService
    {
        Task<CommandResult> GetPatientsWithSurgeriesAsync(int doctorId, DateTime? date, string term, SurgeryStatusEnum? status, int page = 1, int size = 10);
        Task<CommandResult> GetPatientAnesthesiaRecordByIdAsync(string patientId, int surgeryId);
        Task<CommandResult> AssumePatientAsync(string patientId, int surgeryId, int? responsibleAnesthesiologistId);
        Task<CommandResult> GetMyPatientsAsync(int doctorId, DateTime? date, string? term, SurgeryStatusEnum? status, int page = 1, int size = 10);       
    }
}