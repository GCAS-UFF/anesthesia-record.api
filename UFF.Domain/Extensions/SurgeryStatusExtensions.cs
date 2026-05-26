using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Domain.Extensions
{
    public static class SurgeryStatusExtensions
    {
        public static SurgeryStatusEnum ToSurgeryStatus(this string status)
        {
            return status?.ToLower() switch
            {
                "agendada" => SurgeryStatusEnum.Scheduled,
                "waiting" or "aguardando" => SurgeryStatusEnum.Waiting,
                "em_andamento" or "in_progress" => SurgeryStatusEnum.InProgress,
                "concluida" or "completed" => SurgeryStatusEnum.Completed,
                "cancelada" or "cancelled" => SurgeryStatusEnum.Cancelled,
                _ => SurgeryStatusEnum.Scheduled
            };
        }

        public static string ToStatusString(this SurgeryStatusEnum status)
        {
            return status switch
            {
                SurgeryStatusEnum.Scheduled => "agendada",
                SurgeryStatusEnum.Waiting => "aguardando",
                SurgeryStatusEnum.InProgress => "em_andamento",
                SurgeryStatusEnum.Completed => "concluida",
                SurgeryStatusEnum.Cancelled => "cancelada",
                _ => "agendada"
            };
        }
    }
}