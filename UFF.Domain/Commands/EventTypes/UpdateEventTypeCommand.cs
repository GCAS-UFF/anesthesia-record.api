namespace UFF.FichaAnestesica.Domain.Commands.EventTypes
{
    public class UpdateEventTypeCommand
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
    }
}
