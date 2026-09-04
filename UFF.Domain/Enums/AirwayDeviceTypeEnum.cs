using System.ComponentModel;

namespace UFF.FichaAnestesica.Domain.Enums
{
    public enum AirwayDeviceTypeEnum
    {
        [Description("Guedel")] Guedel = 1,
        [Description("Máscara Laríngea")] LaryngealMask = 2,
        [Description("Máscara Facial")] FacialMask = 3,
        [Description("Tubo")] Tube = 4
    }
}
