using System.Text.Json.Serialization;

namespace UFF.FichaAnestesica.Domain.Dto
{
    public class AghuLabExamResponseDto
    {
        [JsonPropertyName("paciente_id")]
        public string? PatientId { get; set; }

        [JsonPropertyName("exame")]
        public AghuLabExamDto? Exam { get; set; }

        [JsonPropertyName("resultados")]
        public List<AghuLabResultDto> Results { get; set; } = new();

        [JsonPropertyName("ausentes")]
        public List<string> MissingCodes { get; set; } = new();
    }

    public class AghuLabExamDto
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("data_solicitacao")]
        public DateTime? RequestedAt { get; set; }

        [JsonPropertyName("data_coleta")]
        public DateTime? CollectedAt { get; set; }

        [JsonPropertyName("data_liberacao")]
        public DateTime? ReleasedAt { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }
    }

    public class AghuLabResultDto
    {
        [JsonPropertyName("grupo")]
        public string? Group { get; set; }

        [JsonPropertyName("analito")]
        public string? Analyte { get; set; }

        [JsonPropertyName("codigo_exame")]
        public string? Code { get; set; }

        [JsonPropertyName("nome_exame")]
        public string? Name { get; set; }

        [JsonPropertyName("valor")]
        public decimal? Value { get; set; }

        [JsonPropertyName("unidade")]
        public string? Unit { get; set; }

        [JsonPropertyName("valor_referencia")]
        public string? ReferenceRange { get; set; }
    }

    public class AghuLabExamLookup
    {
        public bool PatientFound { get; set; }
        public AghuLabExamDto? Exam { get; set; }
        public List<AghuLabResultDto> Results { get; set; } = new();

        public static AghuLabExamLookup PatientNotFound() => new() { PatientFound = false };
    }
}
