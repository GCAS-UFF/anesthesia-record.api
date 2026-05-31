using System.Net.Http.Json;
using UFF.FichaAnestesica.Domain.Dto;
using UFF.FichaAnestesica.Domain.Enums;
using UFF.FichaAnestesica.Domain.Repositories.ReadOnly;
using UFF.FichaAnestesica.Domain.Response;

namespace UFF.FichaAnestesica.Infra.Repositories.Aghu
{
    public class PatientReadOnlyRepository : IPatientReadOnlyRepository
    {
        private readonly HttpClient _httpClient;

        public PatientReadOnlyRepository(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("HospitalApi");
        }

        public async Task<PagedResponse<PatientListDto>> GetPatientsFromHospitalAsync(DateTime? date, SurgeryStatusEnum? status, int page = 1, int pageSize = 10)
        {
            var queryParams = new List<string>();

            if (date.HasValue)
                queryParams.Add($"date={date.Value:yyyy-MM-dd}");

            if (status.HasValue)
                queryParams.Add($"status={status}");

            queryParams.Add($"page={page}");
            queryParams.Add($"pageSize={pageSize}");

            var queryString = string.Join("&", queryParams);

            var response = await _httpClient.GetAsync($"/cirurgias?{queryString}");

            response.EnsureSuccessStatusCode();

            var data = await response.Content.ReadFromJsonAsync<PatientsApiListDto>();

            return new PagedResponse<PatientListDto>
            {
                Data = data?.Patients ?? [],
                Page = page,
                PageSize = pageSize,
                TotalItems = data?.Patients.Count ?? 0
            };
        }

        public async Task<PatientDto> GetPatientFromHospitalByIdAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return null;

            var response = await _httpClient.GetAsync($"/cirurgia/{id}");

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<PatientDto>();
        }

        public async Task<PatientDto> GetFromHospitalByPatientIdAndSurgeryIdAsync(string patientId, int surgeryId)
        {
            if (string.IsNullOrWhiteSpace(patientId) || surgeryId == default)
                return null;

            var response = await _httpClient.GetAsync($"/cirurgia/{patientId}/{surgeryId}");

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<PatientDto>();
        }
      
    }
}