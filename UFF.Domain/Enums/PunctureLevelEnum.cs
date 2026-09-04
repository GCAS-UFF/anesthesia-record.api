using System.ComponentModel;

namespace UFF.FichaAnestesica.Domain.Enums
{
    public enum PunctureLevelEnum
    {
        [Description("L1-L2")] L1_L2 = 1,
        [Description("L2-L3")] L2_L3 = 2,
        [Description("L3-L4")] L3_L4 = 3,
        [Description("L4-L5")] L4_L5 = 4,
        [Description("Hiato Sacro")] L5_S1 = 5,
        [Description("Outro")] Outro = 6
    }
}
