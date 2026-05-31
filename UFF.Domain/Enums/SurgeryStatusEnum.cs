using System.ComponentModel;

namespace UFF.FichaAnestesica.Domain.Enums
{
    public enum SurgeryStatusEnum
    {
        [Description("Agendado")]
        Scheduled,
        [Description("Em_preparo")]
        Preparing,
        [Description("Em_progresso")]
        InProgress,
        [Description("Concluido")]
        Completed,
        [Description("Cancelada")]
        Canceled
    }
}
