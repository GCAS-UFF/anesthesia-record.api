using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Domain.Extensions
{
    public static class ParsePresentationsExtensions
    {
        public static MedicationPresentationEnum ParseToEnum(string presentation)
        {
            if (string.IsNullOrWhiteSpace(presentation))
                return MedicationPresentationEnum.Bottle;

            presentation = presentation.Trim().ToLower();

            return presentation switch
            {
                "ampola" => MedicationPresentationEnum.Ampoule,
                "ampoule" => MedicationPresentationEnum.Ampoule,
                "frasco-ampola" => MedicationPresentationEnum.Vial,
                "frasco ampola" => MedicationPresentationEnum.Vial,
                "vial" => MedicationPresentationEnum.Vial,
                "frasco" => MedicationPresentationEnum.Bottle,
                "bottle" => MedicationPresentationEnum.Bottle,
                "comprimido" => MedicationPresentationEnum.Tablet,
                "tablet" => MedicationPresentationEnum.Tablet,
                "capsula" => MedicationPresentationEnum.Capsule,
                "cápsula" => MedicationPresentationEnum.Capsule,
                "gotas" => MedicationPresentationEnum.Drops,
                "seringa" => MedicationPresentationEnum.PreFilledSyringe,
                "seringa pré-preenchida" => MedicationPresentationEnum.PreFilledSyringe,
                "pre-filled syringe" => MedicationPresentationEnum.PreFilledSyringe,
                "spray" => MedicationPresentationEnum.Spray,
                "pomada" => MedicationPresentationEnum.Ointment,
                "creme" => MedicationPresentationEnum.Cream,
                "gel" => MedicationPresentationEnum.Gel,
                "po" => MedicationPresentationEnum.Powder,
                "pó" => MedicationPresentationEnum.Powder,
                "solucao oral" => MedicationPresentationEnum.OralSolution,
                "solução oral" => MedicationPresentationEnum.OralSolution,
                "suspensao oral" => MedicationPresentationEnum.OralSuspension,
                "suspensão oral" => MedicationPresentationEnum.OralSuspension,
                "inalacao" => MedicationPresentationEnum.InhalationSolution,
                "inalação" => MedicationPresentationEnum.InhalationSolution,

                _ => MedicationPresentationEnum.Bottle
            };
        }
    }
}