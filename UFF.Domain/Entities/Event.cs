using UFF.FichaAnestesica.Domain.Commands.AnesthesiaRecord;
using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Domain.Entities
{
    public class Event : Base
    {
        protected Event() { }

        public ClinicalEventTypeEnum Type { get; private set; }
        public TimeSpan Time { get; set; }
        public DateTime Date { get; set; }
        public string Observations { get; private set; }
        public MonitoringRecord MonitoringRecord { get; private set; }

        public static Event Create(ClinicalEventCommand command)
        {
            return new Event
            {
                Date = command.Date,
                Time = command.Time,
                Observations = command.Observations,
                Type = command.EventType,
                LastUpdate = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
            };
        }

        public void SetMonitoringRecord(MonitoringRecord monitoringRecord)
        {
            MonitoringRecord = monitoringRecord;
        }
    }
}