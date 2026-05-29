using System.ComponentModel;

namespace UFF.FichaAnestesica.Domain.Enums
{
    public enum MedicationPresentationEnum
    {
        [Description("Ampola")]
        Ampoule,
        [Description("Frasco-ampola")]
        Vial,
        [Description("Frasco")]
        Bottle,
        [Description("Comprimido")]
        Tablet,
        [Description("Cápsula")]
        Capsule,
        [Description("Drágea")]
        Dragee,
        [Description("Gotas")]
        Drops,
        [Description("Seringa pré-preenchida")]
        PreFilledSyringe,
        [Description("Spray")]
        Spray,
        [Description("Pomada")]
        Ointment,
        [Description("Creme")]
        Cream,
        [Description("Gel")]
        Gel,
        [Description("Pó")]
        Powder,
        [Description("Solução oral")]
        OralSolution,        
        [Description("Suspensão oral")]
        OralSuspension,
        [Description("Solução inalatória")]
        InhalationSolution
    }
}