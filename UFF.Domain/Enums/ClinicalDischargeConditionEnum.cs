using System.ComponentModel;

namespace UFF.FichaAnestesica.Domain.Enums
{
    public enum ClinicalDischargeConditionEnum
    {
        [Description("Acordado")] Awake = 1,
        [Description("Sonolento")] Drowsy = 2,
        [Description("Intubado")] Intubated = 3
    }
}
