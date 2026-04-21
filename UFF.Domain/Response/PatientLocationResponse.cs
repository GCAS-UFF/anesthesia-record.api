namespace UFF.FichaAnestesica.Domain.Response
{
    public class PatientLocationResponse
    {
        public UnitResponse Unit { get; set; }
        public string Bed { get; set; }
        public string Floor { get; set; }
        public string Room { get; set; }
    }
}
