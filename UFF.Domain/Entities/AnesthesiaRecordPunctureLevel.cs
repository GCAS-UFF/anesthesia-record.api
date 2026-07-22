using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Domain.Entities
{
    public class AnesthesiaRecordPunctureLevel
    {
        private AnesthesiaRecordPunctureLevel() { }

        public int Id { get; private set; }
        public int AnesthesiaRecordId { get; private set; }
        public AnesthesiaRecord AnesthesiaRecord { get; private set; } = null!;
        public PunctureLevelEnum Level { get; private set; }

        public static AnesthesiaRecordPunctureLevel Create(PunctureLevelEnum level)
        {
            return new AnesthesiaRecordPunctureLevel
            {
                Level = level
            };
        }
    }
}