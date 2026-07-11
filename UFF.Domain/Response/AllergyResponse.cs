namespace UFF.FichaAnestesica.Domain.Response
{
    public class ListAllergyDto
    {
        public DateTime? RegisterDate { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string Description { get; set; }

        public string Reason { get; set; }

        public string AllergyCriticality { get; set; }

        public string CertaintyLevel { get; set; }

        public string AllergyManifestation { get; set; }

        public string CausativeAgent { get; set; }

        public MedicationResponse? Medication { get; set; }
    }   
}