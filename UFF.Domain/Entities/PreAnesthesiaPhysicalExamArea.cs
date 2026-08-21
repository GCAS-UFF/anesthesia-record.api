using UFF.FichaAnestesica.Domain.Commands.PreAnesthesiaRecord;

namespace UFF.FichaAnestesica.Domain.Entities
{
    public class PreAnesthesiaPhysicalExamArea : Base
    {
        private PreAnesthesiaPhysicalExamArea() { }

        public string AreaKey { get; private set; } = default!;
        public List<string> Findings { get; private set; } = new();
        public string? OtherDescription { get; private set; }
        public string? Observations { get; private set; }

        public int PreAnesthesiaRecordId { get; private set; }
        public PreAnesthesiaRecord PreAnesthesiaRecord { get; private set; } = default!;

        public static PreAnesthesiaPhysicalExamArea Create(PreAnesthesiaChecklistGroupCommand command)
        {
            return new PreAnesthesiaPhysicalExamArea
            {
                AreaKey = command.GroupKey,
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
