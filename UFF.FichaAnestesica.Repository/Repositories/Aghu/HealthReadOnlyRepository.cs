using System.Net.Http.Json;
using UFF.FichaAnestesica.Domain.Dto;
using UFF.FichaAnestesica.Domain.Repositories.Aghu;
using UFF.FichaAnestesica.Domain.Repositories.ReadOnly;
using UFF.FichaAnestesica.Infra.Context;

namespace UFF.FichaAnestesica.Infra.Repositories.Aghu
{
    public class HealthReadOnlyRepository : IHealthReadOnlyRepository
    {
        private readonly IAghuHttpClientFactory _aghuHttpClientFactory;
        private readonly SigaDbCtx _context;

        public HealthReadOnlyRepository(
            SigaDbCtx context,
            IAghuHttpClientFactory aghuHttpClientFactory)
        {
            _context = context;
            _aghuHttpClientFactory = aghuHttpClientFactory;
        }

        public async Task<(bool bd, bool aghu)> CheckHealth()
        {
            var bdOk = await CheckDatabase();
            var aghuOk = await CheckAghu();

            return (bdOk, aghuOk);
        }

        private async Task<bool> CheckDatabase()
        {
            try
            {
                return await _context.Database.CanConnectAsync();
            }
            catch
            {
                return false;
            }
        }

        private async Task<bool> CheckAghu()
        {
            try
            {
                var client = await _aghuHttpClientFactory.CreateClientAsync();
                var response = await client.GetAsync("saude");

                if (!response.IsSuccessStatusCode)
                    return false;

                var health = await response.Content.ReadFromJsonAsync<HealthDto>();

                return health?.Online ?? false;
            }
            catch
            {
                return false;
            }
        }
    }
}