using System.ComponentModel;

namespace UFF.FichaAnestesica.Domain.Enums
{
    public enum StimulatedNerveEnum
    {
        [Description("Plexo Braquial")] PlexoBraquial = 1,
        [Description("Plexo Lombar")] PlexoLombar = 2,
        [Description("Plexo Sacral")] PlexoSacral = 3,
        [Description("Nervo Femoral")] NervoFemoral = 4,
        [Description("Nervo Ciático")] NervoCientifico = 5,
        [Description("Nervo Axilar")] NervoAxilar = 6,
        [Description("Outros")] Outros = 7
    }
}
