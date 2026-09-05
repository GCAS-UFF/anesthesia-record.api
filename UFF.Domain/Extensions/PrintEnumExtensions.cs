using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Domain.Extensions
{
    /// <summary>
    /// Conversões de enum para texto exclusivas da apresentação de relatórios/PDF.
    /// Não usar para regra de negócio: <see cref="EnumExtensions.GetDescription"/> continua
    /// sendo a fonte usada por integrações (ex.: status AGHU) e não deve ser alterada aqui.
    /// </summary>
    public static class PrintEnumExtensions
    {
        public static string SafeDescription<T>(this T? value, string fallback = "Não informado") where T : struct, Enum
            => value.HasValue ? SafeDescription(value.Value, fallback) : fallback;

        public static string SafeDescription<T>(this T value, string fallback = "Não informado") where T : struct, Enum
            => Enum.IsDefined(typeof(T), value) ? ((Enum)value).GetDescription() : fallback;

        public static string DisplayLabel(this SurgeryStatusEnum status) => status switch
        {
            SurgeryStatusEnum.Scheduled => "Agendada",
            SurgeryStatusEnum.Preparing => "Em preparo",
            SurgeryStatusEnum.InProgress => "Em andamento",
            SurgeryStatusEnum.Completed => "Concluída",
            SurgeryStatusEnum.Canceled => "Cancelada",
            _ => "Não informado"
        };
    }
}
