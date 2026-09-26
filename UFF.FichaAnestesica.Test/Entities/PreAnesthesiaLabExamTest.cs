using UFF.FichaAnestesica.Domain.Dto;
using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Test.Entities
{
    public class PreAnesthesiaLabExamTest
    {
        private static readonly AghuLabExamDto Exam = new() { Id = 1, CollectedAt = new DateTime(2026, 9, 20, 8, 0, 0) };

        private static List<AghuLabResultDto> Results(params (string analyte, decimal value)[] values)
            => values.Select(v => new AghuLabResultDto { Analyte = v.analyte, Value = v.value }).ToList();

        [Fact]
        public void Create_Should_Start_Without_Siga_Data()
        {
            var labExam = PreAnesthesiaLabExam.Create(10);

            Assert.Equal(LabExamImportStatusEnum.NOT_IMPORTED, labExam.ImportStatus);
            Assert.True(labExam.CanAutoImport);
            Assert.False(labExam.HasSigaData);
        }

        [Fact]
        public void Create_Should_Reject_Invalid_Anesthesia_Record()
        {
            Assert.Throws<ArgumentException>(() => PreAnesthesiaLabExam.Create(0));
        }

        [Fact]
        public void RegisterImport_Should_Map_Known_Analytes_And_Ignore_Unknown_Or_Duplicated()
        {
            var labExam = PreAnesthesiaLabExam.Create(10);

            labExam.RegisterImport(Exam, Results(("HEMOGLOBINA", 13.5m), ("GLICOSE", 98m), ("hemoglobina", 9m), ("UREIA", 30m)), DateTime.UtcNow);

            Assert.Equal(2, labExam.Results.Count);
            Assert.Equal(13.5m, labExam.Results.Single(r => r.Analyte == LabAnalyteEnum.HEMOGLOBIN).Value);
            Assert.Equal("g/dL", labExam.Results.Single(r => r.Analyte == LabAnalyteEnum.HEMOGLOBIN).Unit);
            Assert.All(labExam.Results, r => Assert.Equal(LabResultSourceEnum.AGHU, r.Source));
            Assert.Equal(LabExamImportStatusEnum.IMPORTED, labExam.ImportStatus);
            Assert.Equal(1, labExam.SourceExamId);
            Assert.False(labExam.CanAutoImport);
        }

        [Fact]
        public void RegisterImport_Should_Refuse_To_Overwrite_Siga_Data()
        {
            var labExam = PreAnesthesiaLabExam.Create(10);
            labExam.ApplyManualValues(new Dictionary<LabAnalyteEnum, decimal?> { [LabAnalyteEnum.HEMOGLOBIN] = 12.8m }, DateTime.UtcNow);

            Assert.Throws<InvalidOperationException>(() =>
                labExam.RegisterImport(Exam, Results(("HEMOGLOBINA", 13.5m)), DateTime.UtcNow));

            Assert.Equal(12.8m, labExam.Results.Single().Value);
        }

        [Fact]
        public void RegisterImport_Should_Refuse_Exam_Without_Recognized_Results()
        {
            var labExam = PreAnesthesiaLabExam.Create(10);

            Assert.Throws<InvalidOperationException>(() => labExam.RegisterImport(Exam, Results(("GLICOSE", 98m)), DateTime.UtcNow));
            Assert.Equal(LabExamImportStatusEnum.NOT_IMPORTED, labExam.ImportStatus);
        }

        [Fact]
        public void RegisterImportAttempt_Should_Not_Change_Imported_Exam()
        {
            var labExam = PreAnesthesiaLabExam.Create(10);
            labExam.RegisterImport(Exam, Results(("HEMOGLOBINA", 13.5m)), DateTime.UtcNow);

            labExam.RegisterImportAttempt(LabExamImportStatusEnum.FAILED, "falha", DateTime.UtcNow);

            Assert.Equal(LabExamImportStatusEnum.IMPORTED, labExam.ImportStatus);
            Assert.Null(labExam.LastImportMessage);
        }

        [Fact]
        public void ApplyManualValues_Should_Mark_Only_Changed_Values_As_Manual()
        {
            var labExam = PreAnesthesiaLabExam.Create(10);
            labExam.RegisterImport(Exam, Results(("HEMOGLOBINA", 13.5m), ("UREIA", 30m)), DateTime.UtcNow);

            var changed = labExam.ApplyManualValues(new Dictionary<LabAnalyteEnum, decimal?>
            {
                [LabAnalyteEnum.HEMOGLOBIN] = 12.8m,
                [LabAnalyteEnum.UREA] = 30.00m,
            }, DateTime.UtcNow);

            var hemoglobin = labExam.Results.Single(r => r.Analyte == LabAnalyteEnum.HEMOGLOBIN);
            Assert.True(changed);
            Assert.Equal(12.8m, hemoglobin.Value);
            Assert.Equal(13.5m, hemoglobin.ImportedValue);
            Assert.Equal(LabResultSourceEnum.MANUAL, hemoglobin.Source);
            Assert.Equal(LabResultSourceEnum.AGHU, labExam.Results.Single(r => r.Analyte == LabAnalyteEnum.UREA).Source);
            Assert.NotNull(labExam.LastManualEditAt);
        }

        [Fact]
        public void ApplyManualValues_Should_Ignore_Empty_Values_For_Missing_Analytes()
        {
            var labExam = PreAnesthesiaLabExam.Create(10);

            var changed = labExam.ApplyManualValues(new Dictionary<LabAnalyteEnum, decimal?> { [LabAnalyteEnum.GGT] = null }, DateTime.UtcNow);

            Assert.False(changed);
            Assert.Empty(labExam.Results);
            Assert.True(labExam.CanAutoImport);
        }
    }
}
