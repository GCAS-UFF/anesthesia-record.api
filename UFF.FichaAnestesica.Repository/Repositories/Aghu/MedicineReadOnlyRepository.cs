using System;
using System.Net.Http.Json;
using UFF.FichaAnestesica.Domain.Dto;
using UFF.FichaAnestesica.Domain.Repositories.Aghu;
using UFF.FichaAnestesica.Domain.Repositories.ReadOnly;

namespace UFF.FichaAnestesica.Infra.Repositories.Aghu
{
    public class MedicineReadOnlyRepository : IMedicineReadOnlyRepository
    {
        private readonly IAghuHttpClientFactory _aghuHttpClientFactory;

        public MedicineReadOnlyRepository(IAghuHttpClientFactory aghuHttpClientFactory)
        {
            _aghuHttpClientFactory = aghuHttpClientFactory;
        }

        public async Task<DrugListDto> GetDrugsFromAGHU()
        {
            var client = await _aghuHttpClientFactory.CreateClientAsync();
            var response = await client.GetAsync("medicamentos");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<DrugListDto>() ?? new DrugListDto();
        }
    }
}