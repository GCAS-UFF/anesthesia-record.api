using UFF.FichaAnestesica.Domain.Commands.AnesthesiaRecord;

namespace UFF.FichaAnestesica.Domain.Entities
{
    public class PatientPosition : Base
    {
        public DateTime Timestamp { get; private set; }

        public string Position { get; private set; } = string.Empty;

        public int MonitoringRecordId { get; private set; }
        public MonitoringRecord MonitoringRecord { get; private set; }

        public static PatientPosition Create(PatientPositionCommand command)
        {
            return new PatientPosition
            {
                Timestamp = command.Timestamp,
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
            Timestamp = command.Timestamp;
            Position = command.Position;
            LastUpdate = DateTime.UtcNow;
        }
    }
}