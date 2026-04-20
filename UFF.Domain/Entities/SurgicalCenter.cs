namespace UFF.FichaAnestesica.Domain.Entities
{
    public class SurgicalCenter : Base
    {
        private SurgicalCenter()
        {                
        }

        public string Code { get; set; }
        public string Description { get; set; }
    }
}
