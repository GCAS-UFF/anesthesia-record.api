namespace UFF.FichaAnestesica.Domain.Extensions
{
    /// <summary>
    /// Rótulos em português para as chaves de checklist da Avaliação Pré-anestésica
    /// (comorbidades e conduta). Espelha exatamente o catálogo definido em
    /// app/src/app/shared/models/pre-anesthesic-record.model.ts (COMORBIDITY_GROUPS e
    /// CONDUCT_OPTIONS) — essa é a fonte de verdade dos textos; o backend persiste as
    /// mesmas chaves em inglês, então essa tradução fica só na apresentação.
    /// </summary>
    public static class PreAnesthesiaCatalogLabels
    {
        private static readonly Dictionary<string, (string Title, Dictionary<string, string> Findings)> ComorbidityGroups = new()
        {
            ["cardiovascular"] = ("Cardiovascular", new Dictionary<string, string>
            {
                ["noChanges"] = "Sem alterações",
                ["hypertension"] = "Hipertensão Arterial",
                ["heartDisease"] = "Cardiopatia",
                ["arrhythmia"] = "Arritmia",
                ["heartFailure"] = "Insuficiência Cardíaca",
                ["other"] = "Outros"
            }),
            ["respiratory"] = ("Respiratório", new Dictionary<string, string>
            {
                ["noChanges"] = "Sem alterações",
                ["asthma"] = "Asma",
                ["copd"] = "DPOC",
                ["bronchitis"] = "Bronquite",
                ["other"] = "Outros"
            }),
            ["neurological"] = ("Neurológico", new Dictionary<string, string>
            {
                ["noChanges"] = "Sem alterações",
                ["epilepsy"] = "Epilepsia",
                ["parkinsons"] = "Parkinson",
                ["diabeticPeripheralNeuropathy"] = "Neuropatia Periférica Diabética",
                ["other"] = "Outros"
            }),
            ["genitourinary"] = ("Sistema gênito-urinário, incluindo DUM", new Dictionary<string, string>
            {
                ["noChanges"] = "Sem alterações",
                ["renalFailure"] = "Insuficiência renal",
                ["chronicKidneyDisease"] = "Doença renal crônica",
                ["other"] = "Outros"
            }),
            ["endocrine"] = ("Endócrino", new Dictionary<string, string>
            {
                ["noChanges"] = "Sem alterações",
                ["diabetes"] = "Diabetes",
                ["metabolicSyndrome"] = "Síndrome metabólica",
                ["hypothyroidism"] = "Hipotireoidismo",
                ["hyperthyroidism"] = "Hipertireoidismo",
                ["obesity"] = "Obesidade",
                ["other"] = "Outros"
            }),
            ["digestive"] = ("Digestivo", new Dictionary<string, string>
            {
                ["noChanges"] = "Sem alterações",
                ["gastroesophagealReflux"] = "Refluxo gastroesofágico",
                ["gastricUlcer"] = "Úlcera gástrica",
                ["duodenalUlcer"] = "Úlcera duodenal",
                ["other"] = "Outros"
            }),
            ["immunologic"] = ("Imunológico", new Dictionary<string, string>
            {
                ["noChanges"] = "Sem alterações",
                ["lupus"] = "Lúpus",
                ["rheumatoidArthritis"] = "Artrite reumatóide",
                ["hashimotoThyroiditis"] = "Tireoidite de Hashimoto",
                ["gravesDisease"] = "Doença de Graves",
                ["other"] = "Outros"
            })
        };

        private static readonly Dictionary<string, (string Title, Dictionary<string, string> Findings)> PhysicalExamGroups = new()
        {
            ["cardiacAuscultation"] = ("Ausculta cardíaca", new Dictionary<string, string>
            {
                ["noChanges"] = "Sem alterações",
                ["snaps"] = "Estalidos",
                ["clicks"] = "Cliques",
                ["thirdHeartSound"] = "Terceira Bulha",
                ["fourthHeartSound"] = "Quarta Bulha",
                ["hypophonesis"] = "Hipofonese",
                ["other"] = "Outros"
            }),
            ["pulmonaryAuscultation"] = ("Ausculta pulmonar", new Dictionary<string, string>
            {
                ["noChanges"] = "Sem alterações",
                ["fineCrackles"] = "Estertores crepitantes",
                ["coarseCrackles"] = "Estertores grossos",
                ["wheezes"] = "Sibilos",
                ["rhonchi"] = "Roncos",
                ["other"] = "Outros"
            }),
            ["abdomen"] = ("Abdome", new Dictionary<string, string> { ["noChanges"] = "Sem alterações" }),
            ["upperLimbs"] = ("Membros Superiores", new Dictionary<string, string> { ["noChanges"] = "Sem alterações" }),
            ["lowerLimbs"] = ("Membros Inferiores", new Dictionary<string, string> { ["noChanges"] = "Sem alterações" }),
            ["lumbarBack"] = ("Dorso e Região Lombar", new Dictionary<string, string> { ["noChanges"] = "Sem alterações" })
        };

        private static readonly Dictionary<string, string> ConductActionLabels = new()
        {
            ["patientClearedForProcedure"] = "Paciente liberado para o procedimento anestésico-cirúrgico",
            ["patientInstructedOnFasting"] = "Paciente orientado quanto ao jejum",
            ["anesthesiaConsentSigned"] = "Termo de Consentimento informado para Anestesia ou Sedação foi aplicado após os esclarecimentos",
            ["transfusionConsentSigned"] = "Termo de consentimento para Transfusão foi aplicado após os esclarecimentos",
            ["preAnestheticMedicationPrescribed"] = "Medicação pré-anestésica prescrita no prontuário"
        };

        private static readonly Dictionary<string, string> DrugTypeLabels = new()
        {
            ["marijuana"] = "Maconha",
            ["cocaine"] = "Cocaína",
            ["heroin"] = "Heroína",
            ["lsd"] = "LSD",
            ["other"] = "Outros"
        };

        private static readonly Dictionary<string, string> AllergySubstanceLabels = new()
        {
            ["latex"] = "Látex",
            ["penicillin"] = "Penicilina",
            ["dipyrone"] = "Dipirona",
            ["ibuprofen"] = "Ibuprofeno",
            ["other"] = "Outros"
        };

        private static readonly Dictionary<string, string> LateralityLabels = new()
        {
            ["RIGHT"] = "Direita",
            ["LEFT"] = "Esquerda",
            ["BILATERAL"] = "Bilateral",
            ["NOT_APPLICABLE"] = "Não se aplica"
        };

        private static readonly Dictionary<string, string> MucosaLabels = new()
        {
            ["NORMAL_COLOR"] = "Coradas",
            ["PALE"] = "Hipocoradas",
            ["HYPEREMIC"] = "Hipercoradas",
            ["HYDRATED"] = "Hidratadas",
            ["DEHYDRATED"] = "Desidratadas",
            ["OVERHYDRATED"] = "Hiperhidratadas"
        };

        private static readonly Dictionary<string, string> DentitionLabels = new()
        {
            ["PRESENT"] = "Presente",
            ["ABSENT"] = "Ausente",
            ["UPPER_DENTURE"] = "Prótese Superior",
            ["LOWER_DENTURE"] = "Prótese Inferior"
        };

        private static readonly Dictionary<string, string> InterIncisorDistanceLabels = new()
        {
            ["GREATER_THAN_3CM"] = "> 3 cm",
            ["LESS_THAN_3CM"] = "< 3 cm",
            ["NOT_APPLICABLE"] = "NA"
        };

        private static readonly Dictionary<string, string> UpperIncisorLengthLabels = new()
        {
            ["SHORT"] = "Curto",
            ["LONG"] = "Longo",
            ["NOT_APPLICABLE"] = "NA"
        };

        private static readonly Dictionary<string, string> IncisorRelationLabels = new()
        {
            ["ALIGNED"] = "Maxilares alinhados aos mandibulares",
            ["ANTERIOR"] = "Maxilares anteriores",
            ["POSTERIOR"] = "Maxilares posteriores",
            ["NOT_APPLICABLE"] = "NA"
        };

        private static readonly Dictionary<string, string> PalateLabels = new()
        {
            ["NORMAL"] = "Normal",
            ["NARROW"] = "Estreito",
            ["HIGH_ARCHED"] = "Ogival"
        };

        private static readonly Dictionary<string, string> YesNoNaLabels = new()
        {
            ["YES"] = "Sim",
            ["NO"] = "Não",
            ["NOT_APPLICABLE"] = "NA"
        };

        private static readonly Dictionary<string, string> NeckLengthLabels = new()
        {
            ["NORMAL"] = "Normal",
            ["LONG"] = "Longo",
            ["SHORT"] = "Curto"
        };

        private static readonly Dictionary<string, string> NeckWidthLabels = new()
        {
            ["NORMAL"] = "Normal",
            ["THICK"] = "Grosso"
        };

        private static readonly Dictionary<string, string> SternomentalDistanceLabels = new()
        {
            ["GREATER_THAN_12_5CM"] = "> 12,5 cm",
            ["LESS_THAN_12_5CM"] = "< 12,5 cm"
        };

        private static readonly Dictionary<string, string> ThyromentalDistanceLabels = new()
        {
            ["GREATER_OR_EQUAL_5CM"] = "≥ 5 cm",
            ["LESS_THAN_5CM"] = "< 5 cm"
        };

        private static readonly Dictionary<string, string> NormalAbnormalLabels = new()
        {
            ["NORMAL"] = "Normal",
            ["ABNORMAL"] = "Anormal"
        };

        private static readonly Dictionary<string, string> HuapSpecialtyLabels = new()
        {
            ["CARDIOLOGIST"] = "Cardiologista",
            ["GENERAL_PRACTITIONER"] = "Clínico Geral",
            ["PULMONOLOGIST"] = "Pneumologista",
            ["NEPHROLOGIST"] = "Nefrologista",
            ["ENDOCRINOLOGIST"] = "Endocrinologista",
            ["OTHER"] = "Outra"
        };

        public static string ComorbidityGroupTitle(string? groupKey)
            => groupKey != null && ComorbidityGroups.TryGetValue(groupKey, out var group) ? group.Title : Fallback(groupKey);

        public static string ComorbidityFinding(string? groupKey, string findingKey)
            => groupKey != null
               && ComorbidityGroups.TryGetValue(groupKey, out var group)
               && group.Findings.TryGetValue(findingKey, out var label)
                ? label
                : Fallback(findingKey);

        public static string PhysicalExamGroupTitle(string? groupKey)
            => groupKey != null && PhysicalExamGroups.TryGetValue(groupKey, out var group) ? group.Title : Fallback(groupKey);

        public static string PhysicalExamFinding(string? groupKey, string findingKey)
            => groupKey != null
               && PhysicalExamGroups.TryGetValue(groupKey, out var group)
               && group.Findings.TryGetValue(findingKey, out var label)
                ? label
                : Fallback(findingKey);

        public static string ConductAction(string actionKey)
            => ConductActionLabels.TryGetValue(actionKey, out var label) ? label : Fallback(actionKey);

        public static string DrugType(string key) => DrugTypeLabels.TryGetValue(key, out var label) ? label : Fallback(key);

        public static string AllergySubstance(string key) => AllergySubstanceLabels.TryGetValue(key, out var label) ? label : Fallback(key);

        public static string Laterality(string? key) => Lookup(LateralityLabels, key);

        public static string Mucosa(string key) => Lookup(MucosaLabels, key);

        public static string Dentition(string? key) => Lookup(DentitionLabels, key);

        public static string InterIncisorDistance(string? key) => Lookup(InterIncisorDistanceLabels, key);

        public static string UpperIncisorLength(string? key) => Lookup(UpperIncisorLengthLabels, key);

        public static string IncisorRelation(string? key) => Lookup(IncisorRelationLabels, key);

        public static string Palate(string? key) => Lookup(PalateLabels, key);

        public static string YesNoNa(string? key) => Lookup(YesNoNaLabels, key);

        public static string NeckLength(string? key) => Lookup(NeckLengthLabels, key);

        public static string NeckWidth(string? key) => Lookup(NeckWidthLabels, key);

        public static string SternomentalDistance(string? key) => Lookup(SternomentalDistanceLabels, key);

        public static string ThyromentalDistance(string? key) => Lookup(ThyromentalDistanceLabels, key);

        public static string NormalAbnormal(string? key) => Lookup(NormalAbnormalLabels, key);

        public static string HuapSpecialty(string? key) => key != null && HuapSpecialtyLabels.TryGetValue(key, out var label) ? label : Fallback(key);

        private static string Lookup(Dictionary<string, string> map, string? key)
            => !string.IsNullOrWhiteSpace(key) && map.TryGetValue(key, out var label) ? label : "Não informado";

        private static string Fallback(string? key) => string.IsNullOrWhiteSpace(key) ? "—" : key;
    }
}
