using System.ComponentModel;

namespace UFF.FichaAnestesica.Domain.Enums
{
    public enum AirwayTypeEnum
    {
        [Description("Simples")] Simple = 1,
        [Description("Endobrônquico")] Endobronchial = 2,
        [Description("Aramado")] Reinforced = 3,
        [Description("Outras")] Other = 4
    }
}
