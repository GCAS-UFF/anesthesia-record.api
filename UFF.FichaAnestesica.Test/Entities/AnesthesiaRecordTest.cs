using UFF.FichaAnestesica.Domain.Commands.AnesthesiaRecord;
using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Test.Entities
{
    public class AnesthesiaRecordTest
    {
        [Fact]
        public void Create_Should_Set_Fields_Status_Scheduled_And_Dates()
        {
            var command = new AnesthesiaRecordCommand
            {
                PatientIdentifiedBeforeInduction = true,
                BloodPressure = "120/80",
                WeightKg = 70,
                AsaClassification = AsaClassificationEnum.ASA_I,
                RoomEntryTime = new TimeOnly(8, 0),
                AnesthesiaStartTime = new TimeOnly(8, 15),
                PreOperativeDiagnosis = "Apendicite",
                SurgicalPosition = SurgicalPositionEnum.Supine,
                RecordDate = new DateOnly(2025, 6, 1),
                ExternalPatientId = "123"
            };

            var record = AnesthesiaRecord.Create(command);

            Assert.True(record.PatientIdentifiedBeforeInduction);
            Assert.Equal("120/80", record.BloodPressure);
            Assert.Equal(70, record.WeightKg);
            Assert.Equal(AsaClassificationEnum.ASA_I, record.AsaClassification);
            Assert.Equal(new TimeOnly(8, 0), record.RoomEntryTime);
            Assert.Equal(new TimeOnly(8, 15), record.AnesthesiaStartTime);
            Assert.Equal("Apendicite", record.PreOperativeDiagnosis);
            Assert.Equal(SurgicalPositionEnum.Supine, record.SurgicalPosition);
            Assert.Equal(new DateOnly(2025, 6, 1), record.RecordDate);
            Assert.Equal("123", record.ExternalPatientId);
            Assert.Equal(SurgeryStatusEnum.Scheduled, record.Status);
            Assert.NotEqual(default, record.CreatedAt);
            Assert.NotEqual(default, record.LastUpdate);
        }

        [Fact]
        public void Update_Should_Update_Fields_Set_Status_Completed_And_Update_LastUpdate()
        {
            var initialCommand = new AnesthesiaRecordCommand
            {
                BloodPressure = "110/70",
                WeightKg = 60,
                PreOperativeDiagnosis = "Hérnia"
            };
            var record = AnesthesiaRecord.Create(initialCommand);

            var updateCommand = new AnesthesiaRecordCommand
            {
                BloodPressure = "130/85",
                WeightKg = 61,
                PreOperativeDiagnosis = "Hérnia inguinal",
                SurgeryPerformed = "Hernioplastia",
                PostOperativeDiagnosis = "Hérnia inguinal direta",
                ConsciousnessScore = 2,
                ActivityScore = 2,
                CirculationScore = 2,
                RespirationScore = 2,
                OxygenSaturationScore = 2,
                TotalAldreteKroulikScore = 10
            };

            record.Update(updateCommand);

            Assert.Equal("130/85", record.BloodPressure);
            Assert.Equal(61, record.WeightKg);
            Assert.Equal("Hérnia inguinal", record.PreOperativeDiagnosis);
            Assert.Equal("Hernioplastia", record.SurgeryPerformed);
            Assert.Equal("Hérnia inguinal direta", record.PostOperativeDiagnosis);
            Assert.Equal(2, record.ConsciousnessScore);
            Assert.Equal(10, record.TotalAldreteKroulikScore);
            Assert.Equal(SurgeryStatusEnum.Completed, record.Status);
            Assert.NotEqual(default, record.LastUpdate);
        }

        [Fact]
        public void SetStatus_Should_Change_Status_And_Update_LastUpdate()
        {
            var record = AnesthesiaRecord.Create(new AnesthesiaRecordCommand());
            var oldLastUpdate = record.LastUpdate;

            record.SetStatus(SurgeryStatusEnum.Canceled);

            Assert.Equal(SurgeryStatusEnum.Canceled, record.Status);
            Assert.NotEqual(oldLastUpdate, record.LastUpdate);
        }

        [Fact]
        public void AssignFirstAnesthesiologistId_Should_Set_Id_When_Greater_Than_Zero()
        {
            var record = AnesthesiaRecord.Create(new AnesthesiaRecordCommand());

            record.AssignFirstAnesthesiologistId(10);

            Assert.Equal(10, record.FirstAnesthesiologistId);
            Assert.NotEqual(default, record.LastUpdate);
        }

        [Fact]
        public void AssignFirstAnesthesiologistId_Should_Set_Null_When_Id_Is_Zero()
        {
            var record = AnesthesiaRecord.Create(new AnesthesiaRecordCommand());
            record.AssignFirstAnesthesiologistId(10);

            record.AssignFirstAnesthesiologistId(0);

            Assert.Null(record.FirstAnesthesiologistId);
        }

        [Fact]
        public void AssignFirstAnesthesiologistId_Should_Set_Null_When_Id_Is_Negative()
        {
            var record = AnesthesiaRecord.Create(new AnesthesiaRecordCommand());
            record.AssignFirstAnesthesiologistId(10);

            record.AssignFirstAnesthesiologistId(-5);

            Assert.Null(record.FirstAnesthesiologistId);
        }
    }
}