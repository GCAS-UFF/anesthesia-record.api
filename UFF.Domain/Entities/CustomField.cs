namespace UFF.FichaAnestesica.Domain.Entities
{
    public class CustomField : Base
    {
        public string Name { get; private set; } = string.Empty;
        public string Value { get; private set; } = string.Empty;
    }
}