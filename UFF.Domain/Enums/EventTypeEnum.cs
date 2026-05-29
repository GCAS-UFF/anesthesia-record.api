using System.ComponentModel;

namespace UFF.FichaAnestesica.Domain.Enums
{
    public enum ClinicalEventTypeEnum
    {
        [Description("Procedimento")]
        Procedure,
        [Description("Complicação")]
        Complication,
        [Description("Medicação")]
        Medication,
        [Description("Intercorrência")]
        Intercurrence,
        [Description("Alerta")]
        Alert,
        [Description("Alteração hemodinâmica")]
        HemodynamicChange,
        [Description("Evento de via aérea")]
        AirwayEvent,
        [Description("Entubação")]
        Intubation
    }
}