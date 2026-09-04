using System.ComponentModel;

namespace UFF.FichaAnestesica.Domain.Enums
{
    public enum PatientDestinationEnum
    {
        [Description("RPA")] RPA = 1,
        [Description("Enfermaria")] Room = 2,
        [Description("UTI")] ICU = 3,
        [Description("Hospital-dia")] DayClinic = 4,
        [Description("Alta")] Discharge = 5
    }
}
