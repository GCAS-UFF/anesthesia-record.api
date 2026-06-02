using UFF.FichaAnestesica.Domain.Commands.AnesthesiaRecord;
using UFF.FichaAnestesica.Domain.Entities;

namespace UFF.FichaAnestesica.Test.Entities
{
    public class VitalSignRecordTest
    {
        [Fact]
        public void Create_Should_Set_All_Properties_From_Command()
        {
            var command = new VitalSignRecordCommand
            {
                Timestamp = new DateTime(2025, 6, 1, 14, 30, 0),
                SystolicBloodPressure = 120,
                DiastolicBloodPressure = 80,
                MeanArterialPressure = 93,
                HeartRate = 72,
                Spo2 = 98,
                Etco2 = 40,
                Temperature = 36.5m,
                Bis = 45,
                Pvc = 8.0m,
                Pcap = 12.0m,
                CustomFields = new List<CustomFieldCommand>
                {
                    new CustomFieldCommand { Name = "CampoExtra", Value = "Valor" }
                }
            };

            var vitalSign = VitalSignRecord.Create(command);

            Assert.Equal(command.Timestamp, vitalSign.Timestamp);
            Assert.Equal(120, vitalSign.SystolicBloodPressure);
            Assert.Equal(80, vitalSign.DiastolicBloodPressure);
            Assert.Equal(93, vitalSign.MeanArterialPressure);
            Assert.Equal(72, vitalSign.HeartRate);
            Assert.Equal(98, vitalSign.Spo2);
            Assert.Equal(40, vitalSign.Etco2);
            Assert.Equal(36.5m, vitalSign.Temperature);
            Assert.Equal(45, vitalSign.Bis);
            Assert.Equal(8.0m, vitalSign.Pvc);
            Assert.Equal(12.0m, vitalSign.Pcap);
            Assert.NotEqual(default, vitalSign.CreatedAt);
            Assert.Single(vitalSign.CustomFields);
            Assert.Equal("CampoExtra", vitalSign.CustomFields[0].Name);
            Assert.Equal("Valor", vitalSign.CustomFields[0].Value);
        }

        [Fact]
        public void Create_Should_Handle_Null_Values()
        {
            var command = new VitalSignRecordCommand
            {
                Timestamp = new DateTime(2025, 6, 1, 14, 30, 0),
                SystolicBloodPressure = null,
                DiastolicBloodPressure = null,
                HeartRate = null,
                Spo2 = null
            };

            var vitalSign = VitalSignRecord.Create(command);

            Assert.Null(vitalSign.SystolicBloodPressure);
            Assert.Null(vitalSign.DiastolicBloodPressure);
            Assert.Null(vitalSign.HeartRate);
            Assert.Null(vitalSign.Spo2);
        }

        [Fact]
        public void Create_Should_Initialize_Empty_CustomFields_When_Null()
        {
            var command = new VitalSignRecordCommand
            {
                Timestamp = new DateTime(2025, 6, 1, 14, 30, 0),
                CustomFields = null
            };

            var vitalSign = VitalSignRecord.Create(command);

            Assert.NotNull(vitalSign.CustomFields);
            Assert.Empty(vitalSign.CustomFields);
        }

        [Fact]
        public void Update_Should_Replace_All_Fields_And_CustomFields()
        {
            var initialCommand = new VitalSignRecordCommand
            {
                Timestamp = new DateTime(2025, 6, 1, 14, 30, 0),
                SystolicBloodPressure = 100,
                DiastolicBloodPressure = 60,
                HeartRate = 80,
                Spo2 = 95,
                CustomFields = new List<CustomFieldCommand>
                {
                    new CustomFieldCommand { Name = "Antigo", Value = "1" }
                }
            };
            var vitalSign = VitalSignRecord.Create(initialCommand);

            var updateCommand = new VitalSignRecordCommand
            {
                Timestamp = new DateTime(2025, 6, 1, 15, 0, 0),
                SystolicBloodPressure = 130,
                DiastolicBloodPressure = 85,
                MeanArterialPressure = 100,
                HeartRate = 90,
                Spo2 = 99,
                Etco2 = 35,
                Temperature = 37.0m,
                Bis = 50,
                Pvc = 10.0m,
                Pcap = 14.0m,
                CustomFields = new List<CustomFieldCommand>
                {
                    new CustomFieldCommand { Name = "Novo", Value = "2" }
                }
            };

            vitalSign.Update(updateCommand);

            Assert.Equal(updateCommand.Timestamp, vitalSign.Timestamp);
            Assert.Equal(130, vitalSign.SystolicBloodPressure);
            Assert.Equal(85, vitalSign.DiastolicBloodPressure);
            Assert.Equal(100, vitalSign.MeanArterialPressure);
            Assert.Equal(90, vitalSign.HeartRate);
            Assert.Equal(99, vitalSign.Spo2);
            Assert.Equal(35, vitalSign.Etco2);
            Assert.Equal(37.0m, vitalSign.Temperature);
            Assert.Equal(50, vitalSign.Bis);
            Assert.Equal(10.0m, vitalSign.Pvc);
            Assert.Equal(14.0m, vitalSign.Pcap);
            Assert.NotNull(vitalSign.LastUpdate);
            Assert.Single(vitalSign.CustomFields);
            Assert.Equal("Novo", vitalSign.CustomFields[0].Name);
        }

        [Fact]
        public void Update_Should_Clear_CustomFields_When_New_List_Is_Empty()
        {
            var initialCommand = new VitalSignRecordCommand
            {
                Timestamp = new DateTime(2025, 6, 1, 14, 30, 0),
                CustomFields = new List<CustomFieldCommand>
                {
                    new CustomFieldCommand { Name = "Campo", Value = "Valor" }
                }
            };
            var vitalSign = VitalSignRecord.Create(initialCommand);

            var updateCommand = new VitalSignRecordCommand
            {
                Timestamp = new DateTime(2025, 6, 1, 15, 0, 0),
                CustomFields = new List<CustomFieldCommand>()
            };

            vitalSign.Update(updateCommand);

            Assert.NotNull(vitalSign.CustomFields);
            Assert.Empty(vitalSign.CustomFields);
        }

        [Fact]
        public void Update_Should_Handle_Null_CustomFields()
        {
            var initialCommand = new VitalSignRecordCommand
            {
                Timestamp = new DateTime(2025, 6, 1, 14, 30, 0),
                CustomFields = new List<CustomFieldCommand>
                {
                    new CustomFieldCommand { Name = "Campo", Value = "Valor" }
                }
            };
            var vitalSign = VitalSignRecord.Create(initialCommand);

            var updateCommand = new VitalSignRecordCommand
            {
                Timestamp = new DateTime(2025, 6, 1, 15, 0, 0),
                CustomFields = null
            };

            vitalSign.Update(updateCommand);

            Assert.NotNull(vitalSign.CustomFields);
            Assert.Empty(vitalSign.CustomFields);
        }
    }
}