using System.Text.Json.Serialization;

namespace UFF.FichaAnestesica.Domain.Dto
{
    public class HealthDto
    {
        [JsonPropertyName("online")]
        public bool Online { get; set; }
    }
}
