namespace UFF.FichaAnestesica.Domain.Enums
{
    /// <summary>
    /// Especialidades usadas nos pareceres (interconsultas) solicitados na
    /// avaliação pré-anestésica. Nomes de membro espelham exatamente os
    /// valores de HUAP_SPECIALTY_OPTIONS em pre-anesthesic-record.model.ts
    /// no frontend, que é a fonte de verdade para este conjunto fechado.
    /// </summary>
    public enum HuapSpecialtyEnum
    {
        CARDIOLOGIST = 1,
        GENERAL_PRACTITIONER = 2,
        PULMONOLOGIST = 3,
        NEPHROLOGIST = 4,
        ENDOCRINOLOGIST = 5,
        OTHER = 6
    }
}
