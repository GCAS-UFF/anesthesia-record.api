using UFF.FichaAnestesica.Domain.Entities;

public class AnesthesiaRecordProcedure 
{
    public int AnesthesiaRecordId { get; private set; }
    public AnesthesiaRecord AnesthesiaRecord { get; private set; }

    public int ProcedureId { get; private set; }
    public Procedure Procedure { get; private set; }

    public bool IsPrimary { get; private set; }
    public DateTime CreatedAt { get; protected set; }
    public DateTime LastUpdate { get; protected set; }


    public static AnesthesiaRecordProcedure Create(int anesthesiaRecordId, int procedureId, bool isPrimary)
    {
        return new()
        {
            AnesthesiaRecordId = anesthesiaRecordId,
            ProcedureId = procedureId,
            IsPrimary = isPrimary,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void SetPrimary(bool primary)
    {
        IsPrimary = primary;
    }
}