using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Domain.Extensions
{   
    public static class SurgeryStatusDerivation
    {
        public static SurgeryStatusEnum DeriveEffectiveStatus(SurgeryStatusEnum rawStatus, bool hasFirstAnesthesiologist)
        {
            if (rawStatus == SurgeryStatusEnum.Completed || rawStatus == SurgeryStatusEnum.Canceled)
                return rawStatus;

            return hasFirstAnesthesiologist ? SurgeryStatusEnum.InProgress : rawStatus;
        }
    }
}
