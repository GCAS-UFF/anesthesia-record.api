using UFF.FichaAnestesica.Domain.Commands.AnesthesiaRecord;
using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Domain.Entities
{
    public class ClinicalEvent : Base
    {
        public DateTime Date { get; private set; }
        public TimeSpan Time { get; private set; }
        public ClinicalEventTypeEnum EventType { get; private set; }
        public string? Observations { get; private set; }
        public int MonitoringRecordId { get; private set; }
        public MonitoringRecord MonitoringRecord { get; private set; }         

        public static ClinicalEvent Create(ClinicalEventCommand command)
        {
            return new ClinicalEvent
            {
                //Timestamp = command.Timestamp,
                //EventType = command.EventType,
                //Description = command.Description,
                Observations = command.Observations,
                CreatedAt = DateTime.UtcNow
            };
        }

        public void SetMonitoringRecord(MonitoringRecord monitoringRecord)
        {
            MonitoringRecord = monitoringRecord;
        }

        public void Update(ClinicalEventCommand command)
        {
            //Timestamp = command.Timestamp;
            //EventType = command.EventType;
            //Description = command.Description;
            Observations = command.Observations;

            LastUpdate = DateTime.UtcNow;
        }
    }
}