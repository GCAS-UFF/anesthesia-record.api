using UFF.FichaAnestesica.Domain.Commands.PreAnesthesiaRecord;

namespace UFF.FichaAnestesica.Domain.Entities
{
    public class PreAnesthesiaComorbidity : Base
    {
        private PreAnesthesiaComorbidity() { }

        public string GroupKey { get; private set; } = default!;
        public List<string> Findings { get; private set; } = new();
        public string? OtherDescription { get; private set; }
        public string? Observations { get; private set; }

        public int PreAnesthesiaRecordId { get; private set; }
        public PreAnesthesiaRecord PreAnesthesiaRecord { get; private set; } = default!;

        public static PreAnesthesiaComorbidity Create(PreAnesthesiaChecklistGroupCommand command)
        {
            return new PreAnesthesiaComorbidity
            {
                GroupKey = command.GroupKey,
                Findings = command.Findings ?? new(),
                OtherDescription = command.OtherDescription,
                Observations = command.Observations,
                CreatedAt = DateTime.UtcNow
            };
        }

        public void SetPreAnesthesiaRecord(PreAnesthesiaRecord preAnesthesiaRecord)
        {
            PreAnesthesiaRecord = preAnesthesiaRecord;
        }
    }
}
