using System.ComponentModel;

namespace UFF.FichaAnestesica.Domain.Enums
{
    public enum FluidCategoryEnum
    {
        [Description("Cristaloide")] Crystalloid = 1,
        [Description("Colóide")] Colloid = 2,
        [Description("Hemoderivado")] BloodProduct = 3,
        [Description("Diurese")] Diuresis = 4,
        [Description("Sangramento")] Bleeding = 5,
        [Description("Dreno")] Drain = 6,
        [Description("Perda Gástrica")] GastricLoss = 7,
        [Description("Outro")] Other = 8
    }
}
