using System.ComponentModel;

namespace UFF.FichaAnestesica.Domain.Enums
{
    public enum MedicationUnitEnum
    {
        [Description("mg")]
        Milligram = 1,
        [Description("g")]
        Gram = 2,
        [Description("mcg")]
        Microgram = 3,
        [Description("mL")]
        Milliliter = 4,
        [Description("L")]
        Liter = 5,
        [Description("UI")]
        InternationalUnit = 6,
        [Description("%")]
        Percentage = 7      
    }
}
