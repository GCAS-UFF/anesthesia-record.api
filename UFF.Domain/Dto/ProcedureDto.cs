using System.Text.Json.Serialization;

namespace UFF.FichaAnestesica.Domain.Dto
{
    public class ProcedureListDto
    {
        [JsonPropertyName("procedimentos")]
        public List<ProcedureDto> Procedures { get; set; }
    }

    public class ProcedureDto
    {
        [JsonPropertyName("id")]
        public int ExternalId { get; set; }

        [JsonPropertyName("codigo")]
        public string Codigo { get; set; }

        [JsonPropertyName("descricao")]
        public string Description { get; set; }

        [JsonPropertyName("cid")]
        public string Cid { get; set; }

        [JsonPropertyName("principal")]
        public bool IsPrimary { get; set; }
    }
}