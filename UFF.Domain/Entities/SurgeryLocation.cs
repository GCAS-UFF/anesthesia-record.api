namespace UFF.FichaAnestesica.Domain.Entities
{
    public class SurgeryLocation : Base
    {
        private SurgeryLocation()
        {
        }

        public SurgicalCenter SurgicalCenter { get; set; }
        public string Room { get; set; }
    }
}