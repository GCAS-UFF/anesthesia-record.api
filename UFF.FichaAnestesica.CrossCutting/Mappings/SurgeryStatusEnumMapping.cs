using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.CrossCutting.Mappings
{
    public static class SurgeryStatusEnumMapping
    {
        public static SurgeryStatusEnum Parse(string status)
        {
            return status?.ToLower() switch
            {
                "agendada" => SurgeryStatusEnum.Scheduled,
                "em_progresso" => SurgeryStatusEnum.InProgress,
                "cancelada" => SurgeryStatusEnum.Canceled,
                "em_preparacao" => SurgeryStatusEnum.Preparing,
                "em_andamento" => SurgeryStatusEnum.InProgress,
                "concluida" => SurgeryStatusEnum.Completed,
                _ => SurgeryStatusEnum.Scheduled
            };
        }
    }
}