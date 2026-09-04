using System.ComponentModel;

namespace UFF.FichaAnestesica.Domain.Enums
{
    public enum SurgicalPositionEnum
    {
        [Description("Supina")] Supine = 1,
        [Description("Prona")] Prone = 2,
        [Description("Sentado")] Sitting = 3,
        [Description("Lateral Esquerdo")] LeftLateral = 4,
        [Description("Lateral Direito")] RightLateral = 5,
        [Description("Trendelenburg")] Trendelenburg = 6,
        [Description("Litotômica")] Lithotomy = 7,
        [Description("Trendelenburg Reverso")] ReverseTrendelenburg = 8,
        [Description("Jackknife")] Jackknife = 9,
        [Description("Fowler")] Fowler = 10
    }
}
