using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using UFF.FichaAnestesica.CrossCutting.Extensions;
using UFF.FichaAnestesica.CrossCutting.Helpers;
using UFF.FichaAnestesica.Domain.Dto;
using UFF.FichaAnestesica.Domain.Enums;
using UFF.FichaAnestesica.Domain.Repositories.Aghu;
using UFF.FichaAnestesica.Domain.Repositories.ReadOnly;
using UFF.FichaAnestesica.Domain.Response;

namespace UFF.FichaAnestesica.Infra.Repositories.Aghu
{
    public class PatientReadOnlyRepository : IPatientReadOnlyRepository
    {
        private readonly IAghuHttpClientFactory _aghuHttpClientFactory;

        public PatientReadOnlyRepository(IAghuHttpClientFactory aghuHttpClientFactory)
        {
            _aghuHttpClientFactory = aghuHttpClientFactory;
        }

        public async Task<PagedResponse<PatientDetailDto>> GetPatientsFromHospitalAsync(DateTime? date, string term, SurgeryStatusEnum? status, int page = 1, int pageSize = 10)
        {
            var queryParams = new List<string>();

            if (date.HasValue)
                queryParams.Add($"data={date.Value:yyyy-MM-dd}");

            if (!string.IsNullOrWhiteSpace(term))
                queryParams.Add($"termo={term}");

            if (status.HasValue)
                queryParams.Add($"status={EnumExtensions.GetDescription(status.Value)}");

            queryParams.Add($"page={page}");
            queryParams.Add($"pageSize={pageSize}");

            var queryString = string.Join("&", queryParams);

            var client = await _aghuHttpClientFactory.CreateClientAsync();
            var response = await client.GetAsync($"cirurgias?{queryString}");

            response.EnsureSuccessStatusCode();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new CustomDateTimeConverter(), new DateOnlyConverter(), new StringToDoubleConverter() }
            };

            var data = await response.Content.ReadFromJsonAsync<PatientsListDto>(options);

            return new PagedResponse<PatientDetailDto>
            {
                Data = data?.Patients ?? [],
                Page = data?.Page ?? page,
                PageSize = data?.PageSize ?? pageSize,
                TotalItems = data?.TotalItems ?? 0,
                HasNext = data?.HasNext ?? false
            };
        }

        public async Task<PagedResponse<PatientDetailDto>> GetMyPatientsFromHospitalAsync(IEnumerable<int> surgeryIds, string? term, int page = 1, int pageSize = 10)
        {
            if (surgeryIds == null || !surgeryIds.Any())
            {
                return new PagedResponse<PatientDetailDto>
                {
                    Data = [],
                    Page = page,
                    PageSize = pageSize,
                    TotalItems = 0,
                    HasNext = false
                };
            }

            var queryParams = new List<string>
            {
                $"ids={string.Join(",", surgeryIds)}",
                $"page={page}",
                $"pageSize={pageSize}"
            };

            if (!string.IsNullOrWhiteSpace(term))
                queryParams.Add($"termo={Uri.EscapeDataString(term)}");          

            var queryString = string.Join("&", queryParams);

            var client = await _aghuHttpClientFactory.CreateClientAsync();
            var response = await client.GetAsync($"cirurgias/por-ids?{queryString}");

            response.EnsureSuccessStatusCode();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new CustomDateTimeConverter(), new CrossCutting.Helpers.DateOnlyConverter(), new StringToDoubleConverter() }
            };

            var data = await response.Content.ReadFromJsonAsync<PatientsListDto>(options);

            return new PagedResponse<PatientDetailDto>
            {
                Data = data?.Patients ?? [],
                Page = data?.Page ?? page,
                PageSize = data?.PageSize ?? pageSize,
                TotalItems = data?.TotalItems ?? 0,
                HasNext = data?.HasNext ?? false
            };
        }

        public async Task<PatientDetailDto?> GetFromHospitalByPatientIdAndSurgeryIdAsync(string patientId, int surgeryId)
        {
            if (string.IsNullOrWhiteSpace(patientId) || surgeryId == default)
                return null;

            var client = await _aghuHttpClientFactory.CreateClientAsync();
            var response = await client.GetAsync($"cirurgias/{patientId}/{surgeryId}");

            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<PatientDetailDto>();
        }
    }
}