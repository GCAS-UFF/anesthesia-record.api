using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Enums;
using UFF.FichaAnestesica.Domain.Extensions;

namespace UFF.FichaAnestesica.Domain.Response
{
    public class PreAnesthesiaLabExamResponse
    {
        public int AnesthesiaRecordId { get; set; }

        public string ImportStatus { get; set; } = LabExamImportStatusEnum.NOT_IMPORTED.ToString();

        public bool ImportedNow { get; set; }

        public string? SkippedReason { get; set; }
        public bool HasSigaData { get; set; }

        public string? Message { get; set; }

        public long? SourceExamId { get; set; }
        public DateTime? RequestedAt { get; set; }
        public DateTime? CollectedAt { get; set; }
        public DateTime? ReleasedAt { get; set; }
        public DateTime? ImportedAt { get; set; }
        public DateTime? LastImportAttemptAt { get; set; }
        public DateTime? LastManualEditAt { get; set; }

        public List<PreAnesthesiaLabResultResponse> Results { get; set; } = new();

        /// <summary>Analitos sem valor registrado na ficha.</summary>
        public List<string> MissingAnalytes { get; set; } = new();

        public static PreAnesthesiaLabExamResponse Empty(int anesthesiaRecordId, string? message = null, string? skippedReason = null)
        {
            return new PreAnesthesiaLabExamResponse
            {
                AnesthesiaRecordId = anesthesiaRecordId,
                Message = message,
                SkippedReason = skippedReason,
                MissingAnalytes = LabAnalyteCatalog.All.Select(x => x.Analyte.ToString()).ToList()
            };
        }

        public static PreAnesthesiaLabExamResponse ToResponse(PreAnesthesiaLabExam entity, bool importedNow = false)
        {
            var results = entity.Results
                .OrderBy(x => x.Analyte)
                .Select(PreAnesthesiaLabResultResponse.ToResponse)
                .ToList();

            var filled = entity.Results.Where(x => x.Value.HasValue).Select(x => x.Analyte).ToHashSet();

            return new PreAnesthesiaLabExamResponse
            {
                AnesthesiaRecordId = entity.AnesthesiaRecordId,
                ImportStatus = entity.ImportStatus.ToString(),
                ImportedNow = importedNow,
                HasSigaData = entity.HasSigaData,
                Message = entity.LastImportMessage,
                SourceExamId = entity.SourceExamId,
                RequestedAt = entity.RequestedAt,
                CollectedAt = entity.CollectedAt,
                ReleasedAt = entity.ReleasedAt,
                ImportedAt = entity.ImportedAt,
                LastImportAttemptAt = entity.LastImportAttemptAt,
                LastManualEditAt = entity.LastManualEditAt,
                Results = results,
                MissingAnalytes = LabAnalyteCatalog.All
                    .Where(x => !filled.Contains(x.Analyte))
                    .Select(x => x.Analyte.ToString())
                    .ToList()
            };
        }
    }

    public class PreAnesthesiaLabResultResponse
    {
        public string Analyte { get; set; } = string.Empty;
        public string Group { get; set; } = string.Empty;
        public decimal? Value { get; set; }
        public string? Unit { get; set; }
        public string? ReferenceRange { get; set; }

        /// <summary>AGHU | MANUAL</summary>
        public string Source { get; set; } = string.Empty;
        public decimal? ImportedValue { get; set; }

        public static PreAnesthesiaLabResultResponse ToResponse(PreAnesthesiaLabResult entity)
        {
            return new PreAnesthesiaLabResultResponse
            {
                Analyte = entity.Analyte.ToString(),
                Group = LabAnalyteCatalog.Get(entity.Analyte).Group.ToString(),
                Value = entity.Value,
                Unit = entity.Unit,
                ReferenceRange = entity.ReferenceRange,
                Source = entity.Source.ToString(),
                ImportedValue = entity.ImportedValue
            };
        }
    }
}
