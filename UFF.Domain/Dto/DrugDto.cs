using System.Text.Json.Serialization;

namespace UFF.FichaAnestesica.Domain.Dto
{
    public class DrugListDto
    {
        [JsonPropertyName("medicamentos")]
        public List<DrugDto> Drugs { get; set; }
    }

    public class DrugDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("codigo")]
        public string Codigo { get; set; }

        [JsonPropertyName("descricao")]
        public string Description { get; set; }

        [JsonPropertyName("unidade")]
        public string Unity { get; set; }

        [JsonPropertyName("ativo")]
        public bool Active { get; set; }

        [JsonPropertyName("tipo")]
        public string Type { get; set; }
    }
}