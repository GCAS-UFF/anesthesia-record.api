using System.ComponentModel;

namespace UFF.FichaAnestesica.Domain.Enums
{
    public enum SurgeryStatusEnum
    {
        [Description("AGENDADA")]
        Scheduled = 1,
        [Description("EM_PREPARO")]
        Preparing = 2,
        [Description("EM_PROGRESSO")]
        InProgress = 3,
        [Description("CONCLUIDA")]
        Completed = 4,
        [Description("CANCELADA")]
        Canceled = 5
    }
}
