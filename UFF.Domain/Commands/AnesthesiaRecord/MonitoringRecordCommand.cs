using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Domain.Commands.AnesthesiaRecord
{
    public class MonitoringRecordCommand
    {

        public MonitoringRecordCommand(int surgeryId)
        {
           SurgeryId = surgeryId;
        }

        public int AnesthesiaRecordId { get; set; }
        public int SurgeryId { get; set; }
        public int RecordedByProfessionalId { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? EndedAt { get; set; }
        public List<VitalSignRecordCommand> VitalSigns { get; set; } = new();
        public List<AdministeredAgentCommand> AdministeredAgents { get; set; } = new();
        public List<ClinicalEventCommand> ClinicalEvents { get; set; } = new();
        public List<FluidBalanceCommand> FluidBalances { get; set; } = new();
        public SurgeryStatusEnum Status { get; private set; }
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
        public decimal? Temperature { get; set; }
        public int? Bis { get; set; }
        public decimal? Pvc { get; set; }
        public decimal? Pcap { get; set; }
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
        public decimal Dose { get; set; }
        public string Unit { get; set; }
        public AdministrationRouteEnum Route { get; set; }
        public string Presentation { get; set; } = string.Empty;
        public int DrugId { get; internal set; }
    }

    public class ClinicalEventCommand
    {
        public DateTime Timestamp { get; set; }
        public ClinicalEventTypeEnum EventType { get; set; }
        public string Name { get; set; }
        public string? Observations { get; set; }
        public string Description { get; set; }
    }

    public class FluidBalanceCommand
    {
        public DateTime Timestamp { get; set; }
        public FluidCategoryEnum Category { get; set; }
        public string Description { get; set; } = string.Empty;
        public int VolumeMl { get; set; }
        public FluidBalanceTypeEnum Type { get; set; }
    }
}