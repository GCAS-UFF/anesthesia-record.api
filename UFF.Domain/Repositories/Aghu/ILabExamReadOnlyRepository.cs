using UFF.FichaAnestesica.Domain.Dto;

namespace UFF.FichaAnestesica.Domain.Repositories.Aghu
{
    public interface ILabExamReadOnlyRepository
    {
        Task<AghuLabExamLookup> GetLatestReleasedByPatientIdAsync(string patientId, CancellationToken cancellationToken = default);
    }
}
