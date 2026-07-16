using System.Text.Json.Serialization;
using UFF.FichaAnestesica.Domain.Helpers;

namespace UFF.FichaAnestesica.Domain.Dto
{
    public class PatientsListDto
    {
        [JsonPropertyName("cirurgias")]
        public List<PatientDetailDto> Patients { get; set; } = [];

        [JsonPropertyName("totalItems")]
        public int TotalItems { get; set; }

        [JsonPropertyName("page")]
        public int Page { get; set; }

        [JsonPropertyName("pageSize")]
        public int PageSize { get; set; }

        [JsonPropertyName("hasNext")]
        public bool HasNext { get; set; }
    }

    public class PatientDetailDto
    {
        [JsonPropertyName("data_cirurgia")]
        public DateTime SurgeryDate { get; set; }

        [JsonPropertyName("cirurgia_id")]
        public int SurgeryId { get; set; }

        [JsonPropertyName("paciente_id")]
        public string PatientId { get; set; }

        [JsonPropertyName("numero_prontuario")]
        public string MedicalRecordNumber { get; set; }

        [JsonPropertyName("nome_completo")]
        public string FullName { get; set; }

        [JsonPropertyName("data_nascimento")]
        public DateTime BirthDate { get; set; }
     

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("sexo")]
        public string Gender { get; set; }

        [JsonPropertyName("peso_kg")]
        public double WeightKg { get; set; }

        [JsonPropertyName("altura_cm")]
        public int HeightCm { get; set; }

        [JsonPropertyName("localizacao_atual")]
        public CurrentLocationDto CurrentLocation { get; set; }

        [JsonPropertyName("alergias")]
        public List<ListAllergyDto> Allergies { get; set; } = new();

        [JsonPropertyName("cirurgias")]
        public List<SurgeryDetailsDto> Surgeries { get; set; } = new();

        [JsonPropertyName("previsao_atendimento")]
        public DateTime? ExpectedAt { get; set; }

        [JsonPropertyName("sala")]
        public string Room { get; set; }

        [JsonPropertyName("especialidade_id")]
        public string SpecialtyId { get; set; }

        [JsonPropertyName("especialidade")]
        public string Specialty { get; set; }

        [JsonPropertyName("procedimentos")]
        public List<ListProcedureDto> Procedures { get; set; } = new();
           

        [JsonIgnore]
        public bool HaveFirstAnesthesist { get; set; }
    }

    public class ListAllergyDto
    {
        [JsonPropertyName("data_registro")]
        [JsonConverter(typeof(CustomDateTimeConverter))]   
        public DateTime RegisterDate { get; set; }

        [JsonPropertyName("descricao")]
        public string Description { get; set; }

        [JsonPropertyName("motivo")]
        public string Reason { get; set; }

        [JsonPropertyName("criticidade_alergica")]
        public string AllergyCriticality { get; set; }

        [JsonPropertyName("grau_certeza")]
        public string CertaintyLevel { get; set; }

        [JsonPropertyName("manifestacao_alergica")]
        public string Manifestation { get; set; }

        [JsonPropertyName("medicamento")]
        public ListMedicationDto Medication { get; set; }

        [JsonPropertyName("agente_causador")]
        public string CausativeAgent { get; set; }

    }

    public class ListMedicationDto
    {
        [JsonPropertyName("descricao")]
        public string Description { get; set; }
    }

    public class ListProcedureDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("descricao")]
        public string Description { get; set; }

        [JsonPropertyName("cid")]
        public string Cid { get; set; }

        [JsonPropertyName("codigo")]
        public string Codigo { get; set; }

        [JsonPropertyName("principal")]
        public bool IsPrimary { get; set; }
    }

    public class PatientDetailsDto
    {
        [JsonPropertyName("cirurgia_id")]
        public int SurgeryId { get; set; }

        [JsonPropertyName("paciente_id")]
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

        [JsonPropertyName("alergias")]
        public List<AllergyDto> Allergies { get; set; } = new();

        [JsonPropertyName("cirurgias")]
        public List<SurgeryDetailsDto> Surgeries { get; set; } = new();
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

    public class AllergyDto
    {
        [JsonPropertyName("data_registro")]
        public DateTime RegisterDate { get; set; }

        [JsonPropertyName("descricao")]
        public string Description { get; set; }

        [JsonPropertyName("motivo")]
        public string Reason { get; set; }

        [JsonPropertyName("criticidade_alergica")]
        public string AllergyCriticality { get; set; }

        [JsonPropertyName("grau_certeza")]
        public string CertaintyLevel { get; set; }

        [JsonPropertyName("manifestacao_alergica")]
        public string Manifestation { get; set; }

        [JsonPropertyName("medicamento")]
        public MedicationDto Medication { get; set; }

        [JsonPropertyName("agente_causador")]
        public string CausativeAgent { get; set; }
    }

    public class MedicationDto
    {
        [JsonPropertyName("descricao")]
        public string Description { get; set; }
    }

    public class SurgeryDetailsDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("data_cirurgia")]
        public DateTime SurgeryDate { get; set; }

        [JsonPropertyName("status_cirurgia")]
        public string SurgeryStatus { get; set; }

        [JsonPropertyName("local")]
        public SurgeryLocationDto Location { get; set; }

        [JsonPropertyName("procedimentos")]
        public List<ProcedureDto> Procedures { get; set; } = new();

        [JsonPropertyName("especialidade")]
        public SpecialtyDto Specialty { get; set; }
    }

    public class SpecialtyDto
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

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
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("descricao")]
        public string Description { get; set; }
    }   
}