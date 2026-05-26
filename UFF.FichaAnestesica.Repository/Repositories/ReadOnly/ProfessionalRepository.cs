using System.Net.Http.Json;
using UFF.FichaAnestesica.Domain.Dto;
using UFF.FichaAnestesica.Domain.Repositories.ReadOnly;

namespace UFF.FichaAnestesica.Infra.Repositories.ReadOnly
{
    public class ProfessionalRepository : IProfessionalRepository
    {
        private readonly HttpClient _httpClient;

        public ProfessionalRepository(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("HospitalApi");
        }

        public async Task<List<UserDto>> GetProfessionalsForAnethesiaRecord(string name)
        {
            var url = $"profissionais/{Uri.EscapeDataString(name)}";

            var response = await _httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<List<UserDto>>();
        }
    }
}