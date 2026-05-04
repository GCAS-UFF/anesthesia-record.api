using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Domain.Extensions
{
    public static class SurgeryStatusExtensions
    {
        public static SurgeryStatus ToSurgeryStatus(this string status)
        {
            return status?.ToLower() switch
            {
                "agendada" => SurgeryStatus.Scheduled,
                "waiting" or "aguardando" => SurgeryStatus.Waiting,
                "em_andamento" or "in_progress" => SurgeryStatus.InProgress,
                "concluida" or "completed" => SurgeryStatus.Completed,
                "cancelada" or "cancelled" => SurgeryStatus.Cancelled,
                _ => SurgeryStatus.Scheduled
            };
        }

        public static string ToStatusString(this SurgeryStatus status)
        {
            return status switch
            {
                SurgeryStatus.Scheduled => "agendada",
                SurgeryStatus.Waiting => "aguardando",
                SurgeryStatus.InProgress => "em_andamento",
                SurgeryStatus.Completed => "concluida",
                SurgeryStatus.Cancelled => "cancelada",
                _ => "agendada"
            };
        }
    }
}