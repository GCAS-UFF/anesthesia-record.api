using UFF.FichaAnestesica.Domain.Commands.AnesthesiaRecord;
using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Domain.Entities
{
    public class FluidBalance : Base
    {
        public DateTime Timestamp { get; private set; }
        public FluidBalanceTypeEnum Type { get; private set; }
        public FluidCategoryEnum Category { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public decimal VolumeMl { get; private set; }
        public int MonitoringRecordId { get; private set; }
        public MonitoringRecord MonitoringRecord { get; private set; }

        public static FluidBalance Create(FluidBalanceCommand command)
        {
            return new FluidBalance
            {
                Timestamp = command.Timestamp,
                Type = command.Type,
                Category = command.Category,
                Name = command.Description,
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
            Timestamp = command.Timestamp;
            Type = command.Type;
            Category = command.Category;
            Name = command.Description;
            VolumeMl = command.VolumeMl;

            LastUpdate = DateTime.UtcNow;
        }
    }
}