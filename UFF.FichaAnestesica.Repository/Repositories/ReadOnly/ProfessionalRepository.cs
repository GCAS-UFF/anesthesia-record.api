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

        public async Task<UserListDto> GetProfessionalsForAnethesiaRecord(string name)
        {
            //var url = $"profissionais?pesquisa={Uri.EscapeDataString(name)}";

            var url = $"profissionais";

            var response = await _httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<UserListDto>();
        }
    }
}