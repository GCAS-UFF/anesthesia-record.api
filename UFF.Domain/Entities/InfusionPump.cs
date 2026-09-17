using UFF.FichaAnestesica.Domain.Commands.AnesthesiaRecord;
using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Enums;

public class InfusionPump : Base
{
    public TimeSpan Time { get; set; }
    public DateTime Date { get; set; }
    public int DrugId { get; private set; }
    public Drug Drug { get; private set; } = null!;
    public decimal Rate { get; private set; }
    public InfusionRateUnitEnum RateUnit { get; private set; }
    public decimal VolumeMl { get; private set; }
    public DateTime EndAt { get; private set; }
    public int MonitoringRecordId { get; private set; }
    public MonitoringRecord MonitoringRecord { get; private set; }

    public static InfusionPump Create(InfusionPumpCommand command) => new InfusionPump
    {
        Date = command.Date,
        Time = command.Time,
        DrugId = command.DrugId,
        Rate = command.Rate,
        RateUnit = command.RateUnit,
        VolumeMl = command.VolumeMl,
        EndAt = command.EndAt,
        CreatedAt = DateTime.UtcNow
    };

    public void SetMonitoringRecord(MonitoringRecord monitoringRecord)
    {
        MonitoringRecord = monitoringRecord;
    }

    public void Update(InfusionPumpCommand command)
    {
        Time = command.Time;
        Date = command.Date;
        DrugId = command.DrugId;
        Rate = command.Rate;
        RateUnit = command.RateUnit;
        VolumeMl = command.VolumeMl;
        EndAt = command.EndAt;

        LastUpdate = DateTime.UtcNow;
    }
}
