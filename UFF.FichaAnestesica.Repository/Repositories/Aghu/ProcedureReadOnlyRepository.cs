using System;
using System.Net.Http.Json;
using UFF.FichaAnestesica.Domain.Dto;
using UFF.FichaAnestesica.Domain.Repositories.Aghu;
using UFF.FichaAnestesica.Domain.Repositories.ReadOnly;

namespace UFF.FichaAnestesica.Infra.Repositories.Aghu
{
    public class ProcedureReadOnlyRepository : IProcedureReadOnlyRepository
    {
        private readonly IAghuHttpClientFactory _aghuHttpClientFactory;

        public ProcedureReadOnlyRepository(IAghuHttpClientFactory aghuHttpClientFactory)
        {
            _aghuHttpClientFactory = aghuHttpClientFactory;
        }

        public async Task<ProcedureListDto> GetProceduresFromAGHU()
        {
            var client = await _aghuHttpClientFactory.CreateClientAsync();
            var response = await client.GetAsync("procedimentos");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<ProcedureListDto>() ?? new ProcedureListDto();
        }
    }
}