using UFF.FichaAnestesica.Domain.Commands.AnesthesiaRecord;
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

        public static ClinicalEvent Create(ClinicalEventCommand command)
        {
            return new ClinicalEvent
            {
                Timestamp = command.Timestamp,
                EventType = command.EventType,
                Description = command.Description,
                Observations = command.Observations,
                CreatedAt = DateTime.UtcNow
            };
        }

        public void Update(ClinicalEventCommand command)
        {
            Timestamp = command.Timestamp;
            EventType = command.EventType;
            Description = command.Description;
            Observations = command.Observations;

            LastUpdate = DateTime.UtcNow;
        }
    }
}