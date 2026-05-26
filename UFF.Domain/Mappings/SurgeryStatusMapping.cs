using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Domain.Mappings
{
    public static class SurgeryStatusMapping
    {
        private static readonly Dictionary<string, SurgeryStatusEnum> StatusMap =
            new Dictionary<string, SurgeryStatusEnum>(StringComparer.OrdinalIgnoreCase)
            {
                { "agendada", SurgeryStatusEnum.Scheduled },
                { "scheduled", SurgeryStatusEnum.Scheduled },
                { "waiting", SurgeryStatusEnum.Waiting },
                { "aguardando", SurgeryStatusEnum.Waiting },
                { "em_andamento", SurgeryStatusEnum.InProgress },
                { "in_progress", SurgeryStatusEnum.InProgress },
                { "concluida", SurgeryStatusEnum.Completed },
                { "completed", SurgeryStatusEnum.Completed },
                { "cancelada", SurgeryStatusEnum.Cancelled },
                { "cancelled", SurgeryStatusEnum.Cancelled }
            };

        public static SurgeryStatusEnum Parse(string status)
        {
            if (string.IsNullOrEmpty(status))
                return SurgeryStatusEnum.Scheduled;

            return StatusMap.GetValueOrDefault(status.ToLower(), SurgeryStatusEnum.Scheduled);
        }

        public static string ToString(SurgeryStatusEnum status)
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