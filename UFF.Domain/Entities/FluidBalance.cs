using UFF.FichaAnestesica.Domain.Commands.AnesthesiaRecord;
using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Domain.Entities
{
    public class FluidBalance : Base
    {
        public TimeSpan Time { get; private set; }
        public DateTime Date { get; private set; }
        public FluidBalanceTypeEnum Type { get; private set; }
        public FluidCategoryEnum Category { get; private set; }
        public string Details { get; private set; } = string.Empty;
        public decimal VolumeMl { get; private set; }
        public int MonitoringRecordId { get; private set; }
        public MonitoringRecord MonitoringRecord { get; private set; }

        public static FluidBalance Create(FluidBalanceCommand command)
        {
            return new FluidBalance
            {
                Time = command.Time,
                Date = command.Date,
                Type = command.Type,
                Category = command.Category,
                Details = command.Details ?? string.Empty,
                VolumeMl = command.VolumeMl,
                CreatedAt = DateTime.UtcNow
            };
        }

        public void SetMonitoringRecord(MonitoringRecord monitoringRecord)
        {
            MonitoringRecord = monitoringRecord;
        }

        public void Update(FluidBalanceCommand command)
        {
            Time = command.Time;
            Date = command.Date;
            Type = command.Type;
            Category = command.Category;
            Details = command.Details ?? string.Empty;
            VolumeMl = command.VolumeMl;
            LastUpdate = DateTime.UtcNow;
        }
    }
}