using System.ComponentModel;

namespace UFF.FichaAnestesica.Domain.Enums
{
    public enum InfusionRateUnitEnum
    {
        [Description("mL/h")]
        MillilitersPerHour = 1,
        [Description("mcg/kg/min")]
        MicrogramsPerKgPerMinute = 2,
        [Description("mcg/min")] 
        MicrogramsPerMinute = 3,
        [Description("mg/h")] 
        MilligramsPerHour = 4,
        [Description("UI/h")] 
        InternationalUnitsPerHour = 5
    }
}
