using System.ComponentModel;

namespace UFF.FichaAnestesica.Domain.Enums
{
    public enum AdministrationRouteEnum
    {
        [Description("IV")] IV = 1,
        [Description("IM")] IM = 2,
        [Description("VO")] VO = 3,
        [Description("SC")] SC = 4,
        [Description("IN")] IN = 5,
        [Description("Peridural")] Epidural = 6,
        [Description("Raquianestesia")] Raquianesthesia = 7
    }
}
