using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Enums;

public class AdministeredAgent : Base
{
    public DateTime Timestamp { get; private set; }

    public int DrugId { get; private set; }

    public Drug Drug { get; private set; } = null!;

    public decimal Dose { get; private set; }

    public UnitEnum Unit { get; private set; }

    public AdministrationRouteEnum Route { get; private set; }
    public string? Presentation { get; private set; }
    public int MonitoringRecordId { get; private set; }
    public MonitoringRecord MonitoringRecord { get; private set; }
}