using System.ComponentModel;

namespace UFF.FichaAnestesica.Domain.Enums
{
    public enum FluidBalanceTypeEnum
    {
        [Description("Entrada")] Gain = 1,
        [Description("Saída")] Loss = 2
    }
}
