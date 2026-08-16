using System.ComponentModel;

namespace UFF.FichaAnestesica.Domain.Enums
{
    public enum ClinicalEventTypeEnum
    {
        [Description("Intubação")]
        Intubation = 1,
        [Description("Extubação")]
        Extubation = 2,
        [Description("Incisão")]
        Incision = 3,
        [Description("Bloqueio")]
        Block = 4,
        [Description("Garrote ON")]
        TourniquetOn = 5,
        [Description("Garrote OFF")]
        TourniquetOff = 6,
        [Description("Posição")]
        Position = 7,
        [Description("Complicação")]
        Complication = 8,
        [Description("Outro")]
        Other = 9
    }
}