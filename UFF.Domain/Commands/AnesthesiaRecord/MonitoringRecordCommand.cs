namespace UFF.FichaAnestesica.Domain.Commands.AnesthesiaRecord
{
    public class MonitoringRecordCommand
    {
        public int AnesthesiaRecordId { get; set; }
        public int SurgeryId { get; set; }
        public int RecordedByProfessionalId { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? EndedAt { get; set; }
        public List<VitalSignRecordCommand> VitalSigns { get; set; } = new();
        public List<AdministeredAgentCommand> AdministeredAgents { get; set; } = new();
        public List<ClinicalEventCommand> ClinicalEvents { get; set; } = new();
        public List<FluidBalanceCommand> FluidBalances { get; set; } = new();
    }

    public class VitalSignRecordCommand
    {
        public DateTime Timestamp { get; set; }
        public int? SystolicBloodPressure { get; set; }
        public int? DiastolicBloodPressure { get; set; }
        public int? MeanArterialPressure { get; set; }
        public int? HeartRate { get; set; }
        public int? Spo2 { get; set; }
        public int? Etco2 { get; set; }
        public double? Temperature { get; set; }
        public int? Bis { get; set; }
        public double? Pvc { get; set; }
        public double? Pcap { get; set; }
        public List<CustomFieldCommand> CustomFields { get; set; } = new();
    }

    public class CustomFieldCommand
    {
        public string Name { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }

    public class AdministeredAgentCommand
    {
        public DateTime Timestamp { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Dose { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
        public string Route { get; set; } = string.Empty;
        public string Presentation { get; set; } = string.Empty;
    }

    public class ClinicalEventCommand
    {
        public DateTime Timestamp { get; set; }
        public string EventType { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Observations { get; set; }
    }

    public class FluidBalanceCommand
    {
        public DateTime Timestamp { get; set; }
        public string BalanceType { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int VolumeMl { get; set; }
    }
}