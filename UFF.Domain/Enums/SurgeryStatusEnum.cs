using System.ComponentModel;

namespace UFF.FichaAnestesica.Domain.Enums
{
    public enum SurgeryStatusEnum
    {
        [Description("AGENDADA")]
        Scheduled,
        [Description("EM_PREPARO")]
        Preparing,
        [Description("EM_PROGRESSO")]
        InProgress,
        [Description("CONCLUIDA")]
        Completed,
        [Description("CANCELADA")]
        Canceled
    }
}
