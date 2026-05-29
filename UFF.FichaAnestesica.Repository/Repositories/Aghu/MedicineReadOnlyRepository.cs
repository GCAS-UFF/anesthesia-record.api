using System.Net.Http.Json;
using UFF.FichaAnestesica.Domain.Dto;
using UFF.FichaAnestesica.Domain.Repositories.ReadOnly;

namespace UFF.FichaAnestesica.Infra.Repositories.Aghu
{
    public class MedicineReadOnlyRepository : IMedicineReadOnlyRepository
    {
        private readonly HttpClient _httpClient;

        public MedicineReadOnlyRepository(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("HospitalApi");
        }

        public async Task<List<DrugDto>> GetDrugssFromAGHU()
        {
            var url = $"medicamentos";

            var response = await _httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<List<DrugDto>>();
        }
    }
}