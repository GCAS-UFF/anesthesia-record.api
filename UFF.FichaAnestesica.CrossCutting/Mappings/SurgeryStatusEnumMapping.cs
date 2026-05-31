using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.CrossCutting.Mappings
{
    public static class SurgeryStatusEnumMapping
    {
        public static SurgeryStatusEnum Parse(string status)
        {
            return status?.ToLower().Trim() switch
            {
                "agendado" => SurgeryStatusEnum.Scheduled,
                "em_preparo" => SurgeryStatusEnum.Preparing,
                "em_progresso" => SurgeryStatusEnum.InProgress,
                "concluido" => SurgeryStatusEnum.Completed,
                "cancelada" => SurgeryStatusEnum.Canceled,

                _ => throw new ArgumentException($"Status inválido: {status}")
            };
        }
    }
}