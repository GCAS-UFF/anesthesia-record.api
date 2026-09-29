using UFF.FichaAnestesica.Domain.Commands.AnesthesiaRecord;
using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Helpers;

namespace UFF.FichaAnestesica.Test.Entities
{
    public class AnesthesiaRecordProceduresTest
    {
        private static Procedure CreateProcedure(int id, string externalId)
        {
            var procedure = Procedure.Create(externalId, externalId, $"Procedimento {externalId}", null);
            procedure.Id = id;
            return procedure;
        }

        private static AnesthesiaRecord CreateRecord()
            => AnesthesiaRecord.Create(new AnesthesiaRecordCommand { SurgeryId = 10, PatientId = "P1" }, DateTime.UtcNow);

        [Fact]
        public void Create_And_Update_Should_Not_Mark_Procedures_As_Customized()
        {
            var record = CreateRecord();
            record.Update(new AnesthesiaRecordCommand { SurgeryId = 10, PatientId = "P1" });

            Assert.False(record.ProceduresCustomized);
            Assert.False(record.HasOfficialProcedures);
        }

        [Fact]
        public void DefineProcedures_Should_Replace_Relations_And_Mark_Customized()
        {
            var record = CreateRecord();
            var a = CreateProcedure(1, "100");
            var b = CreateProcedure(2, "200");

            record.DefineProcedures([new ProcedureChoice(a, true)]);
            record.DefineProcedures([new ProcedureChoice(b, false), new ProcedureChoice(a, false)]);

            Assert.True(record.HasOfficialProcedures);
            Assert.Equal(new[] { 1, 2 }, record.Surgeries.Select(x => x.ProcedureId).OrderBy(x => x));
            Assert.Single(record.Surgeries, x => x.IsPrimary);
            Assert.True(record.Surgeries.First(x => x.ProcedureId == 2).IsPrimary);
        }

        [Fact]
        public void DefineProcedures_With_Empty_Selection_Should_Keep_Official_Procedure()
        {
            var record = CreateRecord();
            var a = CreateProcedure(1, "100");
            record.DefineProcedures([new ProcedureChoice(a, true)]);

            record.DefineProcedures([]);

            Assert.Equal(new[] { 1 }, record.Surgeries.Select(x => x.ProcedureId));
        }

        [Fact]
        public void SyncProceduresFromAghu_Should_Be_Ignored_After_Doctor_Choice()
        {
            var record = CreateRecord();
            var a = CreateProcedure(1, "100");
            var c = CreateProcedure(3, "300");

            record.SyncProceduresFromAghu([new ProcedureChoice(a, true)]);
            Assert.False(record.ProceduresCustomized);
            Assert.Equal(new[] { 1 }, record.Surgeries.Select(x => x.ProcedureId));

            record.DefineProcedures([new ProcedureChoice(c, true)]);
            record.SyncProceduresFromAghu([new ProcedureChoice(a, true)]);

            Assert.Equal(new[] { 3 }, record.Surgeries.Select(x => x.ProcedureId));
        }

        [Fact]
        public void ResolveChoices_Should_Throw_For_Unknown_Procedure()
        {
            Assert.Throws<Exception>(() =>
                AnesthesiaRecord.ResolveChoices([("999", true, null)], [CreateProcedure(1, "100")]));
        }

        [Theory]
        [InlineData("100", "100", false)]
        [InlineData("200", "100", true)]
        public void HasChanged_Should_Compare_Selection_With_Baseline(string current, string baseline, bool expected)
        {
            Assert.Equal(expected, ProcedureSelection.HasChanged([(current, true)], [(baseline, true)]));
        }

        [Fact]
        public void HasChanged_Should_Consider_Primary_And_Missing_Baseline()
        {
            Assert.True(ProcedureSelection.HasChanged([("100", false), ("200", true)], [("100", true), ("200", false)]));
            Assert.True(ProcedureSelection.HasChanged([("100", true)], null));
        }
    }
}
