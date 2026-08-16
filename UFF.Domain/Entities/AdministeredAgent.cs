using UFF.FichaAnestesica.Domain.Commands.AnesthesiaRecord;
using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Enums;

public class AdministeredAgent : Base
{
    public TimeSpan Time { get; set; }
    public DateTime Date { get; set; }
    public int DrugId { get; private set; }
    public Drug Drug { get; private set; } = null!;
    public decimal Dose { get; private set; }
    public MedicationUnitEnum Unit { get; private set; }
    public AdministrationRouteEnum Route { get; private set; }
    public int MonitoringRecordId { get; private set; }
    public MonitoringRecord MonitoringRecord { get; private set; }

    public static AdministeredAgent Create(AdministeredAgentCommand command) => new AdministeredAgent
    {
        Date = command.Date,
        Time = command.Time,
        DrugId = command.DrugId,
        Dose = command.Dose,
        Unit = command.Unit,
        Route = command.Route,
        CreatedAt = DateTime.UtcNow
    };

    public void SetMonitoringRecord(MonitoringRecord monitoringRecord)
    {
        MonitoringRecord = monitoringRecord;
    }

    public void Update(AdministeredAgentCommand command)
    {
        Time = command.Time;
        Date = command.Date;
        DrugId = command.DrugId;
        Dose = command.Dose;
        Unit = command.Unit;
        Route = command.Route;

        LastUpdate = DateTime.UtcNow;
    }
}