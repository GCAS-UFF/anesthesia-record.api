using System.ComponentModel;

namespace UFF.FichaAnestesica.Domain.Enums
{
    public enum ClinicalEventTypeEnum
    {
        [Description("Posição")]
        Position = 1,
        [Description("Via aérea")]
        Airway = 2,
        [Description("Cirúrgico")]
        Surgical = 3,
        [Description("Clínico")]
        Clinical = 4,
        [Description("Medicação")]
        Medication = 5,
        [Description("Anestesia")]
        Anesthesia = 6,
        [Description("Outro")]
        Other = 7
    }
}