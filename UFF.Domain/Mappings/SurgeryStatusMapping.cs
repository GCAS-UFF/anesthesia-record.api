using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Domain.Mappings
{
    public static class SurgeryStatusMapping
    {
        private static readonly Dictionary<string, SurgeryStatus> StatusMap =
            new Dictionary<string, SurgeryStatus>(StringComparer.OrdinalIgnoreCase)
            {
                { "agendada", SurgeryStatus.Scheduled },
                { "scheduled", SurgeryStatus.Scheduled },
                { "waiting", SurgeryStatus.Waiting },
                { "aguardando", SurgeryStatus.Waiting },
                { "em_andamento", SurgeryStatus.InProgress },
                { "in_progress", SurgeryStatus.InProgress },
                { "concluida", SurgeryStatus.Completed },
                { "completed", SurgeryStatus.Completed },
                { "cancelada", SurgeryStatus.Cancelled },
                { "cancelled", SurgeryStatus.Cancelled }
            };

        public static SurgeryStatus Parse(string status)
        {
            if (string.IsNullOrEmpty(status))
                return SurgeryStatus.Scheduled;

            return StatusMap.GetValueOrDefault(status.ToLower(), SurgeryStatus.Scheduled);
        }

        public static string ToString(SurgeryStatus status)
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