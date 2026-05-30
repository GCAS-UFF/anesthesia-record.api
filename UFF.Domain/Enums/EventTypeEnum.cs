using System.ComponentModel;

namespace UFF.FichaAnestesica.Domain.Enums
{
    public enum ClinicalEventTypeEnum
    {
        [Description("Procedimento")]
        Procedure,
        [Description("Intercorrência")]
        Intercurrence,
        [Description("Eventos cardiovasculares")]
        CardiovascularEvent,
        [Description("Evento respiratório")]
        RespiratoryEvent,
        [Description("Eventos neurológicos e de consciência")]
        NeurologicalAndConsciousnessEvent,
        [Description("Eventos associados à anestesia regional")]
        RegionalAnesthesiaEvent,
        [Description("Reações alérgicas")]
        AllergicReaction,
        [Description("Outros")]
        Other
    }
}