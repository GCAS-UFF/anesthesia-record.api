using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Domain.Extensions
{
    /// <summary>
    /// Catálogo dos analitos laboratoriais da avaliação pré-anestésica:
    /// grupo, unidade padrão e a chave semântica ("analito") devolvida pelo
    /// integrador AGHU em GET /exames-laboratoriais/{pacienteId}.
    /// </summary>
    public static class LabAnalyteCatalog
    {
        public sealed record Definition(LabAnalyteEnum Analyte, LabExamGroupEnum Group, string IntegratorKey, string DefaultUnit);

        public static readonly IReadOnlyList<Definition> All = new List<Definition>
        {
            new(LabAnalyteEnum.RED_BLOOD_CELLS,      LabExamGroupEnum.HEMOGRAM,       "HEMACIAS",           "milhões/mm³"),
            new(LabAnalyteEnum.HEMOGLOBIN,           LabExamGroupEnum.HEMOGRAM,       "HEMOGLOBINA",        "g/dL"),
            new(LabAnalyteEnum.HEMATOCRIT,           LabExamGroupEnum.HEMOGRAM,       "HEMATOCRITO",        "%"),
            new(LabAnalyteEnum.MCV,                  LabExamGroupEnum.HEMOGRAM,       "VCM",                "fL"),
            new(LabAnalyteEnum.MCH,                  LabExamGroupEnum.HEMOGRAM,       "HCM",                "pg"),
            new(LabAnalyteEnum.MCHC,                 LabExamGroupEnum.HEMOGRAM,       "CHCM",               "g/dL"),
            new(LabAnalyteEnum.RDW,                  LabExamGroupEnum.HEMOGRAM,       "RDW",                "%"),
            new(LabAnalyteEnum.LEUKOCYTES,           LabExamGroupEnum.HEMOGRAM,       "LEUCOCITOS",         "/mm³"),
            new(LabAnalyteEnum.PLATELETS,            LabExamGroupEnum.HEMOGRAM,       "PLAQUETAS",          "/mm³"),
            new(LabAnalyteEnum.PROTHROMBIN_TIME,     LabExamGroupEnum.COAGULATION,    "TP",                 "s"),
            new(LabAnalyteEnum.INR,                  LabExamGroupEnum.COAGULATION,    "INR",                ""),
            new(LabAnalyteEnum.APTT,                 LabExamGroupEnum.COAGULATION,    "TTPA",               "s"),
            new(LabAnalyteEnum.AST,                  LabExamGroupEnum.LIVER_FUNCTION, "TGO",                "U/L"),
            new(LabAnalyteEnum.ALT,                  LabExamGroupEnum.LIVER_FUNCTION, "TGP",                "U/L"),
            new(LabAnalyteEnum.GGT,                  LabExamGroupEnum.LIVER_FUNCTION, "GGT",                "U/L"),
            new(LabAnalyteEnum.ALKALINE_PHOSPHATASE, LabExamGroupEnum.LIVER_FUNCTION, "FOSFATASE_ALCALINA", "U/L"),
            new(LabAnalyteEnum.UREA,                 LabExamGroupEnum.RENAL_FUNCTION, "UREIA",              "mg/dL"),
            new(LabAnalyteEnum.CREATININE,           LabExamGroupEnum.RENAL_FUNCTION, "CREATININA",         "mg/dL"),
        };

        private static readonly Dictionary<LabAnalyteEnum, Definition> ByAnalyte = All.ToDictionary(x => x.Analyte);

        private static readonly Dictionary<string, Definition> ByIntegratorKey =
            All.ToDictionary(x => x.IntegratorKey, StringComparer.OrdinalIgnoreCase);

        public static Definition Get(LabAnalyteEnum analyte) => ByAnalyte[analyte];

        public static Definition? FindByIntegratorKey(string? key)
            => key != null && ByIntegratorKey.TryGetValue(key.Trim(), out var definition) ? definition : null;

        /// <summary>Aceita apenas o nome do analito (ex.: "HEMOGLOBIN"), nunca o valor numérico.</summary>
        public static bool TryParseAnalyte(string? value, out LabAnalyteEnum analyte)
        {
            analyte = default;
            return !string.IsNullOrWhiteSpace(value)
                && !int.TryParse(value, out _)
                && Enum.TryParse(value.Trim(), true, out analyte)
                && ByAnalyte.ContainsKey(analyte);
        }
    }
}
