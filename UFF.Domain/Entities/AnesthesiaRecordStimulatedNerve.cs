using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Domain.Entities
{
    public class AnesthesiaRecordStimulatedNerve
    {
        private AnesthesiaRecordStimulatedNerve() { }

        public int Id { get; private set; }
        public int AnesthesiaRecordId { get; private set; }
        public AnesthesiaRecord AnesthesiaRecord { get; private set; } = null!;
        public StimulatedNerveEnum Nerve { get; private set; }

        public static AnesthesiaRecordStimulatedNerve Create(StimulatedNerveEnum nerve)
        {
            return new AnesthesiaRecordStimulatedNerve
            {
                Nerve = nerve
            };
        }
    }
}