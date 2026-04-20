namespace UFF.FichaAnestesica.Domain.Entities
{
    public class Specialty : Base
    {
        private Specialty()
        {
        }

        public string Code { get; set; }
        public string Description { get; set; }
    }
}
