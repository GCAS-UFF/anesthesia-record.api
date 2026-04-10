using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using UFF.FichaAnestesica.Domain.Dto;

namespace UFF.FichaAnestesica.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CirurgiasController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CirurgiasController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("hoje")]
        public async Task<IActionResult> GetCirurgiasHoje()
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var response = await client.GetAsync("http://localhost:8000/api/cirurgias/hoje");

                if (!response.IsSuccessStatusCode)
                {
                    return StatusCode((int)response.StatusCode, "Erro ao buscar dados do HUAP.");
                }

                var content = await response.Content.ReadAsStringAsync();
                
                // Parseando o JSON gigantesco do PHP
                using var jsonDoc = JsonDocument.Parse(content);
                var root = jsonDoc.RootElement;
                
                var listaMapeada = new List<CirurgiaListaDto>();

                if (root.TryGetProperty("pacientes", out var pacientesElement))
                {
                    foreach (var paciente in pacientesElement.EnumerateArray())
                    {
                        var nome = paciente.GetProperty("nome_completo").GetString() ?? string.Empty;
                        var prontuario = paciente.GetProperty("numero_prontuario").GetString() ?? string.Empty;
                        var nascimento = paciente.GetProperty("data_nascimento").GetString() ?? string.Empty;
                        
                        if (paciente.TryGetProperty("cirurgias", out var cirurgiasElement))
                        {
                            foreach (var cirurgia in cirurgiasElement.EnumerateArray())
                            {
                                var statusPhp = cirurgia.GetProperty("status_cirurgia").GetString() ?? "espera";
                                var statusMapeado = statusPhp.ToLower() == "agendada" ? "espera" : statusPhp;
                                
                                var sala = cirurgia.GetProperty("local").GetProperty("sala").GetString() ?? "";
                                
                                string procedimento = "";
                                if (cirurgia.TryGetProperty("procedimentos", out var procedimentos))
                                {
                                    foreach (var proc in procedimentos.EnumerateArray())
                                    {
                                        if (proc.TryGetProperty("principal", out var isPrincipal) && isPrincipal.GetBoolean())
                                        {
                                            procedimento = proc.GetProperty("descricao").GetString() ?? "";
                                            break;
                                        }
                                    }
                                }

                                listaMapeada.Add(new CirurgiaListaDto
                                {
                                    Id = Guid.NewGuid(), // Gerando temp até salvarmos no PostgreSQL de fato
                                    Nome = nome,
                                    Prontuario = prontuario,
                                    Nascimento = nascimento,
                                    Sala = sala,
                                    Procedimento = procedimento,
                                    Status = statusMapeado
                                });
                            }
                        }
                    }
                }

                return Ok(listaMapeada);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Falha ao acessar ou mapear o HUAP.", details = ex.Message });
            }
        }
    }
}
