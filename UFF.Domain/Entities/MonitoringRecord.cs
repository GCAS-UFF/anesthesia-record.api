namespace UFF.FichaAnestesica.Domain.Entities
{
    public class MonitoringRecord : Base
    {
        public int AnesthesiaRecordId { get; private set; }
        public int SurgeryId { get; private set; }
        public int RecordedByProfessionalId { get; private set; }
        public DateTime StartedAt { get; private set; }
        public DateTime? EndedAt { get; private set; }
        public List<VitalSignRecord> VitalSigns { get; private set; } = new();
        public List<AdministeredAgent> AdministeredAgents { get; private set; } = new();
        public List<ClinicalEvent> ClinicalEvents { get; private set; } = new();
        public List<FluidBalance> FluidBalances { get; private set; } = new();
    }
}