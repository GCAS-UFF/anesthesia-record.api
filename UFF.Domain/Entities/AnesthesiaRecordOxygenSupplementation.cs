using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Domain.Entities
{
    public class AnesthesiaRecordOxygenSupplementation
    {
        private AnesthesiaRecordOxygenSupplementation() { }

        public int Id { get; private set; }
        public int AnesthesiaRecordId { get; private set; }
        public AnesthesiaRecord AnesthesiaRecord { get; private set; } = null!;
        public OxygenSupplementationTypeEnum Type { get; private set; }

        public static AnesthesiaRecordOxygenSupplementation Create(OxygenSupplementationTypeEnum type)
        {
            return new AnesthesiaRecordOxygenSupplementation
            {
                Type = type
            };
        }
    }
}