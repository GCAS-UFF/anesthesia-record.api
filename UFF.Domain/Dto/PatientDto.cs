using System.Text.Json.Serialization;

namespace UFF.FichaAnestesica.Domain.Dto
{
    public class HospitalApiResponseDto
    {
        [JsonPropertyName("pacientes")]
        public List<PatientDto> Patients { get; set; } = [];
    }

    public class PatientDto
    {
        [JsonPropertyName("id")]
        public string PatientId { get; set; }

        [JsonPropertyName("numero_prontuario")]
        public string MedicalRecordNumber { get; set; }

        [JsonPropertyName("nome_completo")]
        public string FullName { get; set; }

        [JsonPropertyName("data_nascimento")]
        public DateTime BirthDate { get; set; }

        [JsonPropertyName("sexo")]
        public string Gender { get; set; }

        [JsonPropertyName("peso_kg")]
        public double WeightKg { get; set; }

        [JsonPropertyName("altura_cm")]
        public int HeightCm { get; set; }

        [JsonPropertyName("localizacao_atual")]
        public CurrentLocationDto CurrentLocation { get; set; }

        [JsonPropertyName("alergias")]
        public List<AllergyDto> Allergies { get; set; } = [];

        [JsonPropertyName("equipe")]
        public List<TeamDto> Team { get; set; } = [];

        [JsonPropertyName("cirurgias")]
        public List<SurgeryDto> Surgeries { get; set; } = [];

        [JsonPropertyName("anestesista_responsavel")]
        public UserDto? ResponsibleAnesthesiologist { get; set; }
    }

    public class TeamDto
    {
        [JsonPropertyName("funcao")]
        public string Function { get; set; }

        [JsonPropertyName("profissional")]
        public ProfessionalDto Professional { get; set; }

    }

    public class ProfessionalDto
    {
        [JsonPropertyName("id")]
        public string Function { get; set; }

        [JsonPropertyName("nome")]
        public string Name { get; set; }

        [JsonPropertyName("especialidade")]
        public string Especiality { get; set; }

    }

    public class CurrentLocationDto
    {
        [JsonPropertyName("unidade")]
        public UnitDto Unit { get; set; }

        [JsonPropertyName("leito")]
        public string Bed { get; set; }

        [JsonPropertyName("andar")]
        public string Floor { get; set; }

        [JsonPropertyName("quarto")]
        public string Room { get; set; }
    }

    public class UnitDto
    {
        [JsonPropertyName("codigo")]
        public string Code { get; set; }

        [JsonPropertyName("descricao")]
        public string Description { get; set; }
    }

    public class SurgeryDto
    {
        [JsonPropertyName("id")]
        public string SurgeryId { get; set; }

        [JsonPropertyName("data_cirurgia")]
        public DateTime SurgeryDate { get; set; }

        [JsonPropertyName("status_cirurgia")]
        public string Status { get; set; }

        [JsonPropertyName("especialidade")]
        public SpecialtyDto Specialty { get; set; }

        [JsonPropertyName("local")]
        public SurgeryLocationDto Location { get; set; }

        [JsonPropertyName("procedimentos")]
        public List<ProcedureDto> Procedures { get; set; } = [];
    }

    public class SpecialtyDto
    {
        [JsonPropertyName("codigo")]
        public string Code { get; set; }

        [JsonPropertyName("descricao")]
        public string Description { get; set; }
    }

    public class SurgeryLocationDto
    {
        [JsonPropertyName("centro_cirurgico")]
        public SurgicalCenterDto SurgicalCenter { get; set; }

        [JsonPropertyName("sala")]
        public string Room { get; set; }
    }

    public class SurgicalCenterDto
    {
        [JsonPropertyName("codigo")]
        public string Code { get; set; }

        [JsonPropertyName("descricao")]
        public string Description { get; set; }
    }

    public class ProcedureDto
    {
        [JsonPropertyName("id")]
        public string ExternalId { get; set; }

        [JsonPropertyName("descricao")]
        public string Description { get; set; }

        [JsonPropertyName("cid")]
        public string Cid { get; set; }

        [JsonPropertyName("principal")]
        public bool IsPrimary { get; set; }
    }

    public class AllergyDto
    {
        [JsonPropertyName("data_registro")]
        public DateTime? RegisterDate { get; set; }

        [JsonPropertyName("criado_em")]
        public DateTime? CreatedAt { get; set; }

        [JsonPropertyName("descricao")]
        public string Description { get; set; }

        [JsonPropertyName("motivo")]
        public string Reason { get; set; }

        [JsonPropertyName("criticidade_alergica")]
        public string AllergyCriticality { get; set; }

        [JsonPropertyName("grau_certeza")]
        public string CertaintyLevel { get; set; }

        [JsonPropertyName("manifestacao_alergica")]
        public string AllergyManifestation { get; set; }

        [JsonPropertyName("medicamento")]
        public MedicationDto Medication { get; set; }

        [JsonPropertyName("agente_causador")]
        public string CausativeAgent { get; set; }
    }

    public class MedicationDto
    {
        [JsonPropertyName("descricao")]
        public string? Description { get; set; }
    }
}