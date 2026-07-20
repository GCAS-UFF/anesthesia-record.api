using UFF.FichaAnestesica.Domain.Entities;

public class AnesthesiaRecordSurgery 
{
    public int AnesthesiaRecordId { get; private set; }
    public AnesthesiaRecord AnesthesiaRecord { get; private set; }

    public int ProcedureId { get; private set; }
    public Procedure Procedure { get; private set; }

    public TimeOnly? Time { get; private set; }

    public bool IsPrimary { get; private set; }
    public DateTime CreatedAt { get; protected set; }
    public DateTime LastUpdate { get; protected set; }

    public static AnesthesiaRecordSurgery Create(int anesthesiaRecordId, int procedureId, bool isPrimary, TimeOnly? time)
    {
        return new()
        {
            AnesthesiaRecordId = anesthesiaRecordId,
            ProcedureId = procedureId,
            IsPrimary = isPrimary,
            Time = time,
            CreatedAt = DateTime.UtcNow,
            LastUpdate = DateTime.UtcNow
        };
    }

    public void SetPrimary(bool primary)
    {
        IsPrimary = primary;
    }

    public void SetTime(TimeOnly? time)
    {
        Time = time;
    }
}