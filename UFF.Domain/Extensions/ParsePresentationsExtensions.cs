using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Domain.Extensions
{
    public class ParsePresentationsExtensions
    {
        public static PresentationEnum ParseToEnum(string presentation)
        {
            if (string.IsNullOrWhiteSpace(presentation))
                return PresentationEnum.Dose;

            presentation = presentation.Trim().ToLower();

            return presentation switch
            {
                "ampola" => PresentationEnum.Ampola,
                "dose" => PresentationEnum.Dose,
                _ => PresentationEnum.Dose
            };
        }

    }
}