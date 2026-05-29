using System.Text.Json.Serialization;

namespace UFF.FichaAnestesica.Domain.Dto
{
    public class MonitoringRecordDto
    {
        [JsonPropertyName("anesthesiaRecordId")]
        public int AnesthesiaRecordId { get; set; }

        [JsonPropertyName("surgeryId")]
        public int SurgeryId { get; set; }

        [JsonPropertyName("recordedByProfessionalId")]
        public int RecordedByProfessionalId { get; set; }

        [JsonPropertyName("startedAt")]
        public DateTime StartedAt { get; set; }

        [JsonPropertyName("endedAt")]
        public DateTime? EndedAt { get; set; }

        [JsonPropertyName("vitalSigns")]
        public List<VitalSignRecordDto> VitalSigns { get; set; } = new();

        [JsonPropertyName("administeredAgents")]
        public List<AdministeredAgentDto> AdministeredAgents { get; set; } = new();

        [JsonPropertyName("clinicalEvents")]
        public List<ClinicalEventDto> ClinicalEvents { get; set; } = new();

        [JsonPropertyName("fluidBalances")]
        public List<FluidBalanceDto> FluidBalances { get; set; } = new();
    }

    public class VitalSignRecordDto
    {
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonPropertyName("systolicBloodPressure")]
        public int? SystolicBloodPressure { get; set; }

        [JsonPropertyName("diastolicBloodPressure")]
        public int? DiastolicBloodPressure { get; set; }

        [JsonPropertyName("meanArterialPressure")]
        public int? MeanArterialPressure { get; set; }

        [JsonPropertyName("heartRate")]
        public int? HeartRate { get; set; }

        [JsonPropertyName("spo2")]
        public int? Spo2 { get; set; }

        [JsonPropertyName("etco2")]
        public int? Etco2 { get; set; }

        [JsonPropertyName("temperature")]
        public double? Temperature { get; set; }

        [JsonPropertyName("bis")]
        public int? Bis { get; set; }

        [JsonPropertyName("pvc")]
        public double? Pvc { get; set; }

        [JsonPropertyName("pcap")]
        public double? Pcap { get; set; }

        [JsonPropertyName("customFields")]
        public List<CustomFieldDto> CustomFields { get; set; } = new();
    }

    public class CustomFieldDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("value")]
        public string Value { get; set; } = string.Empty;
    }

    public class AdministeredAgentDto
    {
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("dose")]
        public string Dose { get; set; } = string.Empty;

        [JsonPropertyName("unit")]
        public string Unit { get; set; } = string.Empty;

        [JsonPropertyName("route")]
        public string Route { get; set; } = string.Empty;

        [JsonPropertyName("presentation")]
        public string Presentation { get; set; } = string.Empty;
    }

    public class ClinicalEventDto
    {
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonPropertyName("eventType")]
        public string EventType { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("observations")]
        public string? Observations { get; set; }
    }

    public class FluidBalanceDto
    {
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonPropertyName("balanceType")]
        public string BalanceType { get; set; } = string.Empty;

        [JsonPropertyName("category")]
        public string Category { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("volumeMl")]
        public int VolumeMl { get; set; }
    }
}