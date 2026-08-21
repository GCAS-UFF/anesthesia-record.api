using UFF.FichaAnestesica.Domain.Commands.PreAnesthesiaRecord;
using UFF.FichaAnestesica.Domain.Enums;
using UFF.FichaAnestesica.Domain.Helpers;

namespace UFF.FichaAnestesica.Domain.Entities
{
    public class PreAnesthesiaReport : Base
    {
        private PreAnesthesiaReport() { }

        public HuapSpecialtyEnum? Specialty { get; private set; }
        public string? Description { get; private set; }

        public int PreAnesthesiaRecordId { get; private set; }
        public PreAnesthesiaRecord PreAnesthesiaRecord { get; private set; } = default!;

        public static PreAnesthesiaReport Create(PreAnesthesiaReportCommand command)
        {
            return new PreAnesthesiaReport
            {
                Specialty = ParseHelper.ParseEnum<HuapSpecialtyEnum>(command.Specialty),
                Description = command.Description,
                CreatedAt = DateTime.UtcNow
            };
        }

        public void SetPreAnesthesiaRecord(PreAnesthesiaRecord preAnesthesiaRecord)
        {
            PreAnesthesiaRecord = preAnesthesiaRecord;
        }
    }
}
