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

        private static readonly Dictionary<string, string> ConductActionLabels = new()
        {
            ["patientClearedForProcedure"] = "Paciente liberado para o procedimento anestésico-cirúrgico",
            ["patientInstructedOnFasting"] = "Paciente orientado quanto ao jejum",
            ["anesthesiaConsentSigned"] = "Termo de Consentimento informado para Anestesia ou Sedação foi aplicado após os esclarecimentos",
            ["transfusionConsentSigned"] = "Termo de consentimento para Transfusão foi aplicado após os esclarecimentos",
            ["preAnestheticMedicationPrescribed"] = "Medicação pré-anestésica prescrita no prontuário"
        };

        public static string ComorbidityGroupTitle(string? groupKey)
            => groupKey != null && ComorbidityGroups.TryGetValue(groupKey, out var group) ? group.Title : Fallback(groupKey);

        public static string ComorbidityFinding(string? groupKey, string findingKey)
            => groupKey != null
               && ComorbidityGroups.TryGetValue(groupKey, out var group)
               && group.Findings.TryGetValue(findingKey, out var label)
                ? label
                : Fallback(findingKey);

        public static string ConductAction(string actionKey)
            => ConductActionLabels.TryGetValue(actionKey, out var label) ? label : Fallback(actionKey);

        private static string Fallback(string? key) => string.IsNullOrWhiteSpace(key) ? "—" : key;
    }
}
