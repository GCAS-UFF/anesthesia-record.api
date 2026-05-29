using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Domain.Entities
{
    public class ClinicalEvent : Base
    {
        public DateTime Timestamp { get; private set; }
        public ClinicalEventTypeEnum EventType { get; private set; }
        public string Description { get; private set; }
        public string? Observations { get; private set; }
        public int MonitoringRecordId { get; private set; }

        public MonitoringRecord MonitoringRecord { get; private set; }
    }
}