using System.ComponentModel;

namespace UFF.FichaAnestesica.Domain.Enums
{
    public enum AsaClassificationEnum
    {
        [Description("ASA I")] ASA_I = 1,
        [Description("ASA II")] ASA_II = 2,
        [Description("ASA III")] ASA_III = 3,
        [Description("ASA IV")] ASA_IV = 4,
        [Description("ASA V")] ASA_V = 5,
        [Description("ASA VI")] ASA_VI = 6
    }
}
