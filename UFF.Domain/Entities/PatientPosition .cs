using UFF.FichaAnestesica.Domain.Commands.AnesthesiaRecord;
using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Domain.Entities
{
    public class PatientPosition : Base
    {
        public TimeSpan Time { get; set; }
        public DateTime Date { get; set; }
        public SurgicalPositionEnum Position { get; private set; }
        public int MonitoringRecordId { get; private set; }
        public MonitoringRecord MonitoringRecord { get; private set; }

        public static PatientPosition Create(PatientPositionCommand command)
        {
            return new PatientPosition
            {
                Time = command.Time,
                Date = command.Date,
                Position = command.Position,
                CreatedAt = DateTime.UtcNow
            };
        }

        public void SetMonitoringRecord(MonitoringRecord monitoringRecord)
        {
            MonitoringRecord = monitoringRecord;
        }
        public void Update(PatientPositionCommand command)
        {
            Time = command.Time;
            Date = command.Date;
            Position = command.Position;
            LastUpdate = DateTime.UtcNow;
        }
    }
}