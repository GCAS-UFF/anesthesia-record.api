using UFF.FichaAnestesica.Domain.Dto;
using UFF.FichaAnestesica.Domain.Enums;
using UFF.FichaAnestesica.Domain.Extensions;

namespace UFF.FichaAnestesica.Domain.Entities
{
    public class PreAnesthesiaLabExam : Base
    {
        private PreAnesthesiaLabExam() { }

        public int AnesthesiaRecordId { get; private set; }
        public AnesthesiaRecord AnesthesiaRecord { get; private set; } = default!;

        public LabExamImportStatusEnum ImportStatus { get; private set; }

        #region Rastreabilidade da coleta importada (AGHU)
        public long? SourceExamId { get; private set; }
        public DateTime? RequestedAt { get; private set; }
        public DateTime? CollectedAt { get; private set; }
        public DateTime? ReleasedAt { get; private set; }
        public DateTime? ImportedAt { get; private set; }
        #endregion

        #region Tentativas de importação
        public int ImportAttempts { get; private set; }
        public DateTime? LastImportAttemptAt { get; private set; }
        public string? LastImportMessage { get; private set; }
        #endregion

        public DateTime? LastManualEditAt { get; private set; }

        public uint Version { get; private set; }

        public List<PreAnesthesiaLabResult> Results { get; private set; } = new();

        public bool HasSigaData => ImportStatus == LabExamImportStatusEnum.IMPORTED || Results.Count > 0;

        public bool CanAutoImport => !HasSigaData;

        public static PreAnesthesiaLabExam Create(int anesthesiaRecordId)
        {
            if (anesthesiaRecordId <= 0)
                throw new ArgumentException("Ficha anestésica inválida.", nameof(anesthesiaRecordId));

            return new PreAnesthesiaLabExam
            {
                AnesthesiaRecordId = anesthesiaRecordId,
                ImportStatus = LabExamImportStatusEnum.NOT_IMPORTED,
                CreatedAt = DateTime.UtcNow
            };
        }

        public void RegisterImport(AghuLabExamDto exam, IEnumerable<AghuLabResultDto> results, DateTime nowUtc)
        {
            if (!CanAutoImport)
                throw new InvalidOperationException("Os exames laboratoriais desta ficha já estão registrados no SIGA e não podem ser sobrescritos pela importação.");

            foreach (var item in results)
            {
                var definition = LabAnalyteCatalog.FindByIntegratorKey(item.Analyte);
                if (definition == null || Results.Any(r => r.Analyte == definition.Analyte))
                    continue;

                var result = PreAnesthesiaLabResult.FromAghu(
                    definition.Analyte,
                    item.Value,
                    string.IsNullOrWhiteSpace(item.Unit) ? definition.DefaultUnit : item.Unit.Trim(),
                    string.IsNullOrWhiteSpace(item.ReferenceRange) ? null : item.ReferenceRange.Trim(),
                    item.Code);

                result.SetLabExam(this);
                Results.Add(result);
            }

            if (Results.Count == 0)
                throw new InvalidOperationException("A coleta importada não possui resultados reconhecidos.");

            ImportStatus = LabExamImportStatusEnum.IMPORTED;
            SourceExamId = exam.Id;
            RequestedAt = exam.RequestedAt;
            CollectedAt = exam.CollectedAt;
            ReleasedAt = exam.ReleasedAt;
            ImportedAt = nowUtc;
            ImportAttempts++;
            LastImportAttemptAt = nowUtc;
            LastImportMessage = null;
            LastUpdate = nowUtc;
        }

        public void RegisterImportAttempt(LabExamImportStatusEnum status, string message, DateTime nowUtc)
        {
            if (status is LabExamImportStatusEnum.IMPORTED or LabExamImportStatusEnum.NOT_IMPORTED)
                throw new ArgumentException("Use RegisterImport para importações concluídas.", nameof(status));

            if (!CanAutoImport)
                return;

            ImportStatus = status;
            ImportAttempts++;
            LastImportAttemptAt = nowUtc;
            LastImportMessage = message;
            LastUpdate = nowUtc;
        }

       
        public bool ApplyManualValues(IReadOnlyDictionary<LabAnalyteEnum, decimal?> values, DateTime nowUtc)
        {
            var changed = false;

            foreach (var (analyte, value) in values)
            {
                var existing = Results.FirstOrDefault(r => r.Analyte == analyte);

                if (existing != null)
                {
                    changed |= existing.ApplyManualValue(value);
                    continue;
                }

                if (value == null)
                    continue;

                var result = PreAnesthesiaLabResult.Manual(analyte, value, LabAnalyteCatalog.Get(analyte).DefaultUnit);
                result.SetLabExam(this);
                Results.Add(result);
                changed = true;
            }

            if (changed)
            {
                LastManualEditAt = nowUtc;
                LastUpdate = nowUtc;
            }

            return changed;
        }
    }
}
