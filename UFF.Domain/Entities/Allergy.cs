namespace UFF.FichaAnestesica.Domain.Entities
{
    namespace UFF.FichaAnestesica.Domain.Entities
    {
        public class Allergy
        {
            public int Id { get; set; }
            public DateTime RegistrationDate { get; set; }
            public string Description { get; set; } = string.Empty;
            public string? Reason { get; set; }
            public string Criticality { get; set; }
            public string CertaintyDegree { get; set; }
            public string? AllergicManifestation { get; set; }
            public int? MedicationId { get; set; }
            public string? MedicationDescription { get; set; }
            public string? CausativeAgent { get; set; }
        }
    }
}