namespace UFF.FichaAnestesica.Domain.Entities
{
    public class CurrentLocation : Base
    {

        private CurrentLocation()
        {                
        }

        public Unit Unit { get; set; }
        public string Bed { get; set; }
        public string Floor { get; set; }
        public string Room { get; set; }
    }
}