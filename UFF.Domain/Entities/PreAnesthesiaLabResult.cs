using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Domain.Entities
{
    public class PreAnesthesiaLabResult : Base
    {
        private PreAnesthesiaLabResult() { }

        public int LabExamId { get; private set; }
        public PreAnesthesiaLabExam LabExam { get; private set; } = default!;

        public LabAnalyteEnum Analyte { get; private set; }
        public decimal? Value { get; private set; }
        public string? Unit { get; private set; }
        public string? ReferenceRange { get; private set; }
        public LabResultSourceEnum Source { get; private set; }

        public decimal? ImportedValue { get; private set; }

        public string? AghuCode { get; private set; }

        internal static PreAnesthesiaLabResult FromAghu(LabAnalyteEnum analyte, decimal? value, string? unit, string? referenceRange, string? aghuCode)
        {
            return new PreAnesthesiaLabResult
            {
                Analyte = analyte,
                Value = value,
                ImportedValue = value,
                Unit = unit,
                ReferenceRange = referenceRange,
                AghuCode = aghuCode,
                Source = LabResultSourceEnum.AGHU,
                CreatedAt = DateTime.UtcNow
            };
        }

        internal static PreAnesthesiaLabResult Manual(LabAnalyteEnum analyte, decimal? value, string? unit)
        {
            return new PreAnesthesiaLabResult
            {
                Analyte = analyte,
                Value = value,
                Unit = unit,
                Source = LabResultSourceEnum.MANUAL,
                CreatedAt = DateTime.UtcNow
            };
        }

        internal void SetLabExam(PreAnesthesiaLabExam labExam) => LabExam = labExam;

        internal bool ApplyManualValue(decimal? value)
        {
            if (Value == value)
                return false;

            Value = value;
            Source = LabResultSourceEnum.MANUAL;
            LastUpdate = DateTime.UtcNow;
            return true;
        }
    }
}
