using System.Net;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using UFF.FichaAnestesica.CrossCutting.Helpers;
using UFF.FichaAnestesica.Domain.Dto;
using UFF.FichaAnestesica.Domain.Repositories.Aghu;

namespace UFF.FichaAnestesica.Infra.Repositories.Aghu
{
    public class LabExamReadOnlyRepository : ILabExamReadOnlyRepository
    {        
        private static readonly TimeSpan RequestTimeout = TimeSpan.FromSeconds(15);

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new CustomNullableDateTimeConverter() }
        };

        private readonly IAghuHttpClientFactory _aghuHttpClientFactory;
        private readonly ILogger<LabExamReadOnlyRepository> _logger;

        public LabExamReadOnlyRepository(IAghuHttpClientFactory aghuHttpClientFactory, ILogger<LabExamReadOnlyRepository> logger)
        {
            _aghuHttpClientFactory = aghuHttpClientFactory;
            _logger = logger;
        }

        public async Task<AghuLabExamLookup> GetLatestReleasedByPatientIdAsync(string patientId, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(patientId))
                throw new ArgumentException("Paciente não informado.", nameof(patientId));

            using var timeout = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            timeout.CancelAfter(RequestTimeout);

            var client = await _aghuHttpClientFactory.CreateClientAsync();
            var requestPath = $"exames-laboratoriais/{Uri.EscapeDataString(patientId.Trim())}";

            using var response = await client.GetAsync(requestPath, timeout.Token);

            if (response.StatusCode == HttpStatusCode.NotFound)
                return AghuLabExamLookup.PatientNotFound();

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"O integrador AGHU respondeu {(int)response.StatusCode} ao consultar exames laboratoriais.", null, response.StatusCode);

            var raw = await response.Content.ReadAsStringAsync(timeout.Token);

            AghuLabExamResponseDto? data;
            try
            {
                data = JsonSerializer.Deserialize<AghuLabExamResponseDto>(raw, JsonOptions);
            }
            catch (JsonException ex)
            {                
                _logger.LogError(ex, "Resposta inválida do AGHU para exames laboratoriais (status {StatusCode}, {Length} bytes).",
                    (int)response.StatusCode, raw.Length);

                throw new InvalidOperationException("O sistema do hospital (AGHU) retornou uma resposta inesperada ao consultar exames laboratoriais.", ex);
            }

            if (data == null)
                throw new InvalidOperationException("O sistema do hospital (AGHU) retornou uma resposta vazia ao consultar exames laboratoriais.");
                        
            if (!string.Equals(data.PatientId?.Trim(), patientId.Trim(), StringComparison.Ordinal))
                throw new InvalidOperationException("O AGHU retornou exames de um paciente diferente do solicitado.");

            return new AghuLabExamLookup
            {
                PatientFound = true,
                Exam = data.Exam,
                Results = data.Exam == null ? new() : data.Results ?? new()
            };
        }
    }
}
