// AntibioticResponse.cs
namespace UFF.FichaAnestesica.Domain.Response
{
    public class AntibioticResponse
    {
        public int MedicationId { get; set; }
        public string? MedicationName { get; set; }
        public string? Name { get; set; }
        public string? Dose { get; set; }
        public string? Route { get; set; }
        public TimeOnly? Time { get; set; }
        public bool HasBooster { get; set; }
        public List<BoosterResponse>? Boosters { get; set; }
    }
}