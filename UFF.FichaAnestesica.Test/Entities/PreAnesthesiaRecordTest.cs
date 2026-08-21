using UFF.FichaAnestesica.Domain.Commands.PreAnesthesiaRecord;
using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Test.Entities
{
    public class PreAnesthesiaRecordTest
    {
        private static PreAnesthesiaRecordCommand BaseCommand(int anesthesiaRecordId = 10)
        {
            return new PreAnesthesiaRecordCommand
            {
                AnesthesiaRecordId = anesthesiaRecordId,
                Surgeries = new List<PreAnesthesiaSurgeryCommand>
                {
                    new PreAnesthesiaSurgeryCommand { Name = "Colecistectomia videolaparoscópica", IsPrimary = true }
                },
                Laterality = "RIGHT",
                PreOperativeDiagnosis = "Colelitíase",
                Comorbidities = new List<PreAnesthesiaChecklistGroupCommand>
                {
                    new PreAnesthesiaChecklistGroupCommand
                    {
                        GroupKey = "cardiovascular",
                        Findings = new List<string> { "hypertension" },
                        Observations = "Controlada com losartana"
                    }
                },
                Medications = new List<PreAnesthesiaMedicationCommand>
                {
                    new PreAnesthesiaMedicationCommand { Name = "Losartana", Dose = "50mg", Frequency = "1x/dia" }
                },
                PhysicalExamAreas = new List<PreAnesthesiaChecklistGroupCommand>
                {
                    new PreAnesthesiaChecklistGroupCommand { GroupKey = "cardiacAuscultation", Findings = new List<string> { "noChanges" } }
                },
                Dentition = "PRESENT",
                MallampatiClass = 2,
                Reports = new List<PreAnesthesiaReportCommand>
                {
                    new PreAnesthesiaReportCommand { Specialty = "CARDIOLOGIST", Description = "Liberado para cirurgia" }
                },
                AsaClassification = AsaClassificationEnum.ASA_II,
                IsEmergency = false,
                NotCleared = false,
                ConductActions = new List<string> { "clearedForSurgery" },
                SignedByProfessionalId = 3,
                SignedByName = "Dr. Fulano",
                SignedAt = new DateTime(2026, 8, 20, 10, 0, 0)
            };
        }

        [Fact]
        public void Create_Should_Set_Scalar_Properties()
        {
            var record = PreAnesthesiaRecord.Create(BaseCommand());

            Assert.Equal(10, record.AnesthesiaRecordId);
            Assert.Equal(LateralityEnum.RIGHT, record.Laterality);
            Assert.Equal("Colelitíase", record.PreOperativeDiagnosis);
            Assert.Equal(DentitionEnum.PRESENT, record.Dentition);
            Assert.Equal(2, record.MallampatiClass);
            Assert.Equal(AsaClassificationEnum.ASA_II, record.AsaClassification);
            Assert.False(record.IsEmergency);
            Assert.False(record.NotCleared);
            Assert.Equal(3, record.SignedByProfessionalId);
            Assert.Equal("Dr. Fulano", record.SignedByName);
            Assert.NotEqual(default, record.CreatedAt);
        }

        [Fact]
        public void Create_Should_Build_Child_Collections_And_Link_Back_To_Parent()
        {
            var record = PreAnesthesiaRecord.Create(BaseCommand());

            Assert.Single(record.Surgeries);
            Assert.Equal("Colecistectomia videolaparoscópica", record.Surgeries[0].Name);
            Assert.True(record.Surgeries[0].IsPrimary);
            Assert.Same(record, record.Surgeries[0].PreAnesthesiaRecord);

            Assert.Single(record.Comorbidities);
            Assert.Equal("cardiovascular", record.Comorbidities[0].GroupKey);
            Assert.Contains("hypertension", record.Comorbidities[0].Findings);
            Assert.Same(record, record.Comorbidities[0].PreAnesthesiaRecord);

            Assert.Single(record.Medications);
            Assert.Equal("Losartana", record.Medications[0].Name);
            Assert.Same(record, record.Medications[0].PreAnesthesiaRecord);

            Assert.Single(record.PhysicalExamAreas);
            Assert.Equal("cardiacAuscultation", record.PhysicalExamAreas[0].AreaKey);
            Assert.Same(record, record.PhysicalExamAreas[0].PreAnesthesiaRecord);

            Assert.Single(record.Reports);
            Assert.Equal(HuapSpecialtyEnum.CARDIOLOGIST, record.Reports[0].Specialty);
            Assert.Same(record, record.Reports[0].PreAnesthesiaRecord);
        }

        [Fact]
        public void Create_Should_Parse_Invalid_Enum_String_As_Null_Instead_Of_Throwing()
        {
            var command = BaseCommand();
            command.Laterality = "VALOR_INEXISTENTE";
            command.Dentition = "";

            var record = PreAnesthesiaRecord.Create(command);

            Assert.Null(record.Laterality);
            Assert.Null(record.Dentition);
        }

        [Fact]
        public void Create_Should_Initialize_Empty_Lists_When_Commands_Have_Null_Lists()
        {
            var command = new PreAnesthesiaRecordCommand
            {
                AnesthesiaRecordId = 10,
                Surgeries = null!,
                Comorbidities = null!,
                Medications = null!,
                PhysicalExamAreas = null!,
                Reports = null!,
                DrugTypes = null!,
                AllergySubstances = null!,
                AirwayMucosa = null!,
                ConductActions = null!
            };

            var record = PreAnesthesiaRecord.Create(command);

            Assert.Empty(record.Surgeries);
            Assert.Empty(record.Comorbidities);
            Assert.Empty(record.Medications);
            Assert.Empty(record.PhysicalExamAreas);
            Assert.Empty(record.Reports);
            Assert.Empty(record.DrugTypes);
            Assert.Empty(record.AllergySubstances);
            Assert.Empty(record.AirwayMucosa);
            Assert.Empty(record.ConductActions);
        }

        [Fact]
        public void Update_Should_Replace_Scalars_And_Rebuild_Child_Collections()
        {
            var record = PreAnesthesiaRecord.Create(BaseCommand());
            Assert.Single(record.Surgeries);

            var updateCommand = BaseCommand();
            updateCommand.PreOperativeDiagnosis = "Apendicite aguda";
            updateCommand.Surgeries = new List<PreAnesthesiaSurgeryCommand>
            {
                new PreAnesthesiaSurgeryCommand { Name = "Apendicectomia", IsPrimary = true },
                new PreAnesthesiaSurgeryCommand { Name = "Herniorrafia", IsPrimary = false }
            };
            updateCommand.AsaClassification = AsaClassificationEnum.ASA_III;

            record.Update(updateCommand);

            Assert.Equal("Apendicite aguda", record.PreOperativeDiagnosis);
            Assert.Equal(AsaClassificationEnum.ASA_III, record.AsaClassification);
            Assert.Equal(2, record.Surgeries.Count);
            Assert.Equal("Apendicectomia", record.Surgeries[0].Name);
            Assert.NotNull(record.LastUpdate);
        }
    }
}
