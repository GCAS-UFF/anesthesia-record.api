using UFF.FichaAnestesica.Domain.Repositories.ReadOnly;
using UFF.FichaAnestesica.Infra.Context;

namespace UFF.FichaAnestesica.Infra.Repositories.Aghu
{
    public class HealthReadOnlyRepository : IHealthReadOnlyRepository
    {
        private readonly HttpClient _httpClient;
        private readonly SigaDbCtx _context;

        public HealthReadOnlyRepository(
            SigaDbCtx context,
            IHttpClientFactory factory)
        {
            _context = context;
            _httpClient = factory.CreateClient("HospitalApi");
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
                var response = await _httpClient.GetAsync("saude");
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}