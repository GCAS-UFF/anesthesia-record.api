namespace UFF.FichaAnestesica.Domain.Response
{
    public class BoosterResponse
    {
        public int MedicationId { get; set; }
        public string? MedicationName { get; set; }
        public string? Name { get; set; }
        public string? Dose { get; set; }
        public string? Route { get; set; }
        public TimeOnly? Time { get; set; }
    }
}