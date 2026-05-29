using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Domain.Entities
{
    public class Drug : Base
    {

        public string Name { get; private set; }

        public string DefaultPresentation { get; private set; }

        public UnitEnum DefaultUnit { get; private set; }
    }
}