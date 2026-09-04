using System.ComponentModel;

namespace UFF.FichaAnestesica.Domain.Enums
{
    public enum RespirationModeEnum
    {
        [Description("Espontânea")] Spontaneous = 1,
        [Description("Manual")] Manual = 2,
        [Description("Controlada")] Controlled = 3
    }
}
