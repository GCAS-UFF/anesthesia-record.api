using UFF.FichaAnestesica.Domain.Repositories;
using UFF.FichaAnestesica.Domain.Repositories.Aghu;

namespace UFF.FichaAnestesica.Infra.Repositories.Aghu
{
    public class AghuHttpClientFactory : IAghuHttpClientFactory
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IInstitutionSettingsRepository _institutionSettingsRepository;

        public AghuHttpClientFactory(
            IHttpClientFactory httpClientFactory,
            IInstitutionSettingsRepository institutionSettingsRepository)
        {
            _httpClientFactory = httpClientFactory;
            _institutionSettingsRepository = institutionSettingsRepository;
        }

        public async Task<HttpClient> CreateClientAsync()
        {
            var settings = await _institutionSettingsRepository.GetSingletonAsync();

            if (string.IsNullOrWhiteSpace(settings?.AghuApiUrl))
                throw new InvalidOperationException("Integração com o AGHU ainda não foi configurada");

            var client = _httpClientFactory.CreateClient("HospitalApi");
            client.BaseAddress = new Uri(settings.AghuApiUrl.TrimEnd('/') + "/");

            return client;
        }
    }
}
