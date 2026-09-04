using System.ComponentModel;

namespace UFF.FichaAnestesica.Domain.Enums
{
    public enum ControlledVentilationModeEnum
    {
        [Description("Volume")] Volume = 1,
        [Description("Pressão")] Pressure = 2
    }
}
