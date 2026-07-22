using System;
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

        public async Task<DrugListDto> GetDrugsFromAGHU()
        {
            var response = await _httpClient.GetAsync("medicamentos");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<DrugListDto>() ?? new DrugListDto();
        }
    }
}