using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using UFF.FichaAnestesica.Domain.Dto;

namespace UFF.FichaAnestesica.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SurgeriesController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public SurgeriesController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("today")]
        public async Task<IActionResult> GetSurgeriesToday()
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                // We keep the request string pointing to the PHP endpoint in Portuguese since that is external
                var response = await client.GetAsync("http://localhost:8000/api/cirurgias/hoje");

                if (!response.IsSuccessStatusCode)
                {
                    return StatusCode((int)response.StatusCode, "Error fetching data from HUAP.");
                }

                var content = await response.Content.ReadAsStringAsync();
                
                using var jsonDoc = JsonDocument.Parse(content);
                var root = jsonDoc.RootElement;
                
                var mappedList = new List<SurgeryListDto>();

                if (root.TryGetProperty("pacientes", out var patientsElement))
                {
                    foreach (var patient in patientsElement.EnumerateArray())
                    {
                        var name = patient.GetProperty("nome_completo").GetString() ?? string.Empty;
                        var medicalRecord = patient.GetProperty("numero_prontuario").GetString() ?? string.Empty;
                        var birthDate = patient.GetProperty("data_nascimento").GetString() ?? string.Empty;
                        
                        if (patient.TryGetProperty("cirurgias", out var surgeriesElement))
                        {
                            foreach (var surgery in surgeriesElement.EnumerateArray())
                            {
                                var statusPhp = surgery.GetProperty("status_cirurgia").GetString() ?? "espera";
                                var mappedStatus = statusPhp.ToLower() == "agendada" ? "waiting" : statusPhp;
                                
                                var room = surgery.GetProperty("local").GetProperty("sala").GetString() ?? "";
                                
                                string procedure = "";
                                if (surgery.TryGetProperty("procedimentos", out var procedures))
                                {
                                    foreach (var proc in procedures.EnumerateArray())
                                    {
                                        if (proc.TryGetProperty("principal", out var isPrincipal) && isPrincipal.GetBoolean())
                                        {
                                            procedure = proc.GetProperty("descricao").GetString() ?? "";
                                            break;
                                        }
                                    }
                                }

                                mappedList.Add(new SurgeryListDto
                                {
                                    Id = Guid.NewGuid(),
                                    Name = name,
                                    MedicalRecord = medicalRecord,
                                    BirthDate = birthDate,
                                    Room = room,
                                    Procedure = procedure,
                                    Status = mappedStatus
                                });
                            }
                        }
                    }
                }

                return Ok(mappedList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Failed to access or map data from HUAP.", details = ex.Message });
            }
        }
    }
}