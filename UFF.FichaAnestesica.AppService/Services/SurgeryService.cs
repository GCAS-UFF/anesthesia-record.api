using UFF.FichaAnestesica.Domain.Enums;
using UFF.FichaAnestesica.Domain.Repositories;
using UFF.FichaAnestesica.Domain.Repositories.ReadOnly;
using UFF.FichaAnestesica.Domain.Response;
using UFF.FichaAnestesica.Domain.Services;
using UFF.FichaAnestesica.Service.Helpers;
using UFF.FichaAnestesica.Service.Mappers;

public class SurgeryService : ISurgeryService
{
    private readonly IHospitalReadOnlyRepository _hospitalReadRepository;
    private readonly ISurgeryRepository _surgeryRepository;

    public SurgeryService(
        IHospitalReadOnlyRepository hospitalReadRepository,
        ISurgeryRepository surgeryRepository)
    {
        _hospitalReadRepository = hospitalReadRepository;
        _surgeryRepository = surgeryRepository;
    }

    public async Task<PagedResponse<PatientSurgeryResponse>> GetPatientsWithSurgeriesAsync(DateTime? date, SurgeryStatus? status, int page = 1, int size = 10)
    {
        if (date.HasValue)
            date = DateTime.SpecifyKind(date.Value, DateTimeKind.Utc);

        var hospitalData = await _hospitalReadRepository.GetSurgeriesFromHospitalAsync(date, status, page, size);


        if (hospitalData.Data == null || !hospitalData.Data.Any())
        {
            return new PagedResponse<PatientSurgeryResponse>
            {
                Data = [],
                Page = page,
                PageSize = size,
                TotalItems = hospitalData.TotalItems
            };
        }

        var patients = PatientMapper.Map(hospitalData.Data);


        await _surgeryRepository.AddOrUpdatePatientsAsync(patients);

        var savedPatients = await _surgeryRepository.GetPatientsWithSurgeriesAsync(date, status, page, size);
        var ordered = PatientOrderingHelper.Apply(savedPatients);

        return new PagedResponse<PatientSurgeryResponse>
        {
            Data = PatientResponseMapper.Map(ordered),
            Page = page,
            PageSize = size,
            TotalItems = hospitalData.TotalItems
        };
    }

    public async Task<PatientSurgeryResponse> GetPatientByIdAsync(int id)
    {
        var patient = await _surgeryRepository.GetPatientByIdAsync(id);

        return PatientResponseMapper.Map(patient);
    }
}