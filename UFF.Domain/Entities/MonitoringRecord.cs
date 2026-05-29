namespace UFF.FichaAnestesica.Domain.Entities
{
    public class MonitoringRecord
    {
        public int AnesthesiaRecordId { get; set; }
        public int SurgeryId { get; set; }
        public int RecordedByProfessionalId { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? EndedAt { get; set; }
        public List<VitalSignRecord> VitalSigns { get; set; } = new();
        public List<AdministeredAgent> AdministeredAgents { get; set; } = new();
        public List<ClinicalEvent> ClinicalEvents { get; set; } = new();
        public List<FluidBalance> FluidBalances { get; set; } = new();
    }
}