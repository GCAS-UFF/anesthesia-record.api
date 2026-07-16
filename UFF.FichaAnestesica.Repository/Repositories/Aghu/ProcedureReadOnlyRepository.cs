using System;
using System.Net.Http.Json;
using UFF.FichaAnestesica.Domain.Dto;
using UFF.FichaAnestesica.Domain.Repositories.ReadOnly;

namespace UFF.FichaAnestesica.Infra.Repositories.Aghu
{
    public class ProcedureReadOnlyRepository : IProcedureReadOnlyRepository
    {
        private readonly HttpClient _httpClient;

        public ProcedureReadOnlyRepository(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("HospitalApi");
        }

        public async Task<ProcedureListDto> GetProceduresFromAGHU()
        {
            var response = await _httpClient.GetAsync("/procedimentos");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<ProcedureListDto>() ?? new ProcedureListDto();
        }
    }
}