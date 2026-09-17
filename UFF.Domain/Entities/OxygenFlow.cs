using UFF.FichaAnestesica.Domain.Commands.AnesthesiaRecord;
using UFF.FichaAnestesica.Domain.Entities;

public class OxygenFlow : Base
{
    public TimeSpan Time { get; set; }
    public DateTime Date { get; set; }
    public decimal? FlowRateLPerMin { get; private set; }
    public bool IsActive { get; private set; }
    public int MonitoringRecordId { get; private set; }
    public MonitoringRecord MonitoringRecord { get; private set; }

    public static OxygenFlow Create(OxygenFlowCommand command) => new OxygenFlow
    {
        Date = command.Date,
        Time = command.Time,
        FlowRateLPerMin = command.FlowRateLPerMin,
        IsActive = command.IsActive,
        CreatedAt = DateTime.UtcNow
    };

    public void SetMonitoringRecord(MonitoringRecord monitoringRecord)
    {
        MonitoringRecord = monitoringRecord;
    }

    public void Update(OxygenFlowCommand command)
    {
        Time = command.Time;
        Date = command.Date;
        FlowRateLPerMin = command.FlowRateLPerMin;
        IsActive = command.IsActive;

        LastUpdate = DateTime.UtcNow;
    }
}
