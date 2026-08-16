using System.Reflection;
using UFF.FichaAnestesica.Domain.Commands.AnesthesiaRecord;
using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Test.Entities
{
    public class MonitoringRecordTest
    {
        [Fact]
        public void Create_Should_Set_Properties_And_Status_InProgress()
        {
            var administeredAgentCommand = new AdministeredAgentCommand
            {
                Date = new DateTime(2025, 6, 1, 8, 10, 0),
                Dose = 10,
                Unit = MedicationUnitEnum.Milligram,
                Route = AdministrationRouteEnum.IV
            };

            var command = new MonitoringRecordCommand(1)
            {
                AnesthesiaRecordId = 10,
                RecordedByProfessionalId = 5,
                StartedAt = new DateTime(2025, 6, 1, 8, 0, 0),
                EndedAt = null,
                VitalSigns = new List<VitalSignRecordCommand>
                {
                    new VitalSignRecordCommand
                    {
                        Date = new DateTime(2025, 6, 1, 8, 5, 0),
                        HeartRate = 80,
                        Spo2 = 98
                    }
                },
                AdministeredAgents = new List<AdministeredAgentCommand> { administeredAgentCommand },
                ClinicalEvents = new List<ClinicalEventCommand>
                {
                    //new ClinicalEventCommand
                    //{
                    //    Timestamp = new DateTime(2025, 6, 1, 8, 15, 0),
                    //    EventType = ClinicalEventTypeEnum.Clinical,
                    //    Name = "Hipotensão",
                    //    Observations = "Pressão baixa",
                    //    Description = "Paciente apresentou hipotensão"
                    //}
                },
                FluidBalances = new List<FluidBalanceCommand>
                {
                    new FluidBalanceCommand
                    {
                        Date = new DateTime(2025, 6, 1, 8, 20, 0),
                        Type = FluidBalanceTypeEnum.Gain,
                        Category = FluidCategoryEnum.Crystalloid,
                        Details = "Soro",
                        VolumeMl = 500
                    }
                }
            };

            var monitoring = MonitoringRecord.Create(command);

            Assert.Equal(10, monitoring.AnesthesiaRecordId);
            Assert.Equal(5, monitoring.RecordedByProfessionalId);
            Assert.NotEqual(default, monitoring.StartedAt);
            Assert.Null(monitoring.EndedAt);
            Assert.Equal(SurgeryStatusEnum.InProgress, monitoring.Status);
            Assert.NotEqual(default, monitoring.CreatedAt);

            Assert.Single(monitoring.VitalSigns);
            Assert.Equal(80, monitoring.VitalSigns[0].HeartRate);
            Assert.Single(monitoring.AdministeredAgents);
            Assert.Single(monitoring.ClinicalEvents);
            Assert.Equal("Paciente apresentou hipotensão", monitoring.ClinicalEvents[0].Observations);
            Assert.Single(monitoring.FluidBalances);
            Assert.Equal(500, monitoring.FluidBalances[0].VolumeMl);
        }

        [Fact]
        public void Create_Should_Initialize_Empty_Lists_When_Commands_Have_Null_Lists()
        {
            var command = new MonitoringRecordCommand(1)
            {
                AnesthesiaRecordId = 10,
                RecordedByProfessionalId = 5,
                StartedAt = DateTime.UtcNow,
                VitalSigns = null,
                AdministeredAgents = null,
                ClinicalEvents = null,
                FluidBalances = null
            };

            var monitoring = MonitoringRecord.Create(command);

            Assert.NotNull(monitoring.VitalSigns);
            Assert.Empty(monitoring.VitalSigns);
            Assert.NotNull(monitoring.AdministeredAgents);
            Assert.Empty(monitoring.AdministeredAgents);
            Assert.NotNull(monitoring.ClinicalEvents);
            Assert.Empty(monitoring.ClinicalEvents);
            Assert.NotNull(monitoring.FluidBalances);
            Assert.Empty(monitoring.FluidBalances);
        }

        [Fact]
        public void Update_Should_Replace_All_Fields_And_Lists()
        {
            var initialCommand = new MonitoringRecordCommand(1)
            {
                AnesthesiaRecordId = 10,
                RecordedByProfessionalId = 5,
                StartedAt = new DateTime(2025, 6, 1, 8, 0, 0),
                VitalSigns = new List<VitalSignRecordCommand> { new VitalSignRecordCommand { HeartRate = 70 } }
            };
            var monitoring = MonitoringRecord.Create(initialCommand);

            var updateCommand = new MonitoringRecordCommand(2)
            {
                AnesthesiaRecordId = 20,
                RecordedByProfessionalId = 8,
                StartedAt = new DateTime(2025, 6, 1, 9, 0, 0),
                EndedAt = new DateTime(2025, 6, 1, 10, 0, 0),
                VitalSigns = new List<VitalSignRecordCommand> { new VitalSignRecordCommand { HeartRate = 90, Spo2 = 97 } },
                AdministeredAgents = new List<AdministeredAgentCommand>(),
                ClinicalEvents = new List<ClinicalEventCommand>(),
                FluidBalances = new List<FluidBalanceCommand>()
            };

            monitoring.Update(updateCommand);

            Assert.Equal(20, monitoring.AnesthesiaRecordId);
            Assert.Equal(8, monitoring.RecordedByProfessionalId);
            Assert.Equal(new DateTime(2025, 6, 1, 9, 0, 0), monitoring.StartedAt);
            Assert.Equal(new DateTime(2025, 6, 1, 10, 0, 0), monitoring.EndedAt);
            Assert.NotNull(monitoring.LastUpdate);

            Assert.Single(monitoring.VitalSigns);
            Assert.Equal(90, monitoring.VitalSigns[0].HeartRate);
            Assert.Empty(monitoring.AdministeredAgents);
            Assert.Empty(monitoring.ClinicalEvents);
            Assert.Empty(monitoring.FluidBalances);
        }

        [Fact]
        public void Update_Should_Clear_Lists_When_New_Commands_Have_Empty_Lists()
        {
            var initialCommand = new MonitoringRecordCommand(1)
            {
                AnesthesiaRecordId = 10,
                RecordedByProfessionalId = 5,
                StartedAt = DateTime.UtcNow,
                VitalSigns = new List<VitalSignRecordCommand> { new VitalSignRecordCommand() },
                AdministeredAgents = new List<AdministeredAgentCommand> { new AdministeredAgentCommand() }
            };
            var monitoring = MonitoringRecord.Create(initialCommand);
            Assert.Single(monitoring.VitalSigns);

            var updateCommand = new MonitoringRecordCommand(1)
            {
                AnesthesiaRecordId = 10,
                RecordedByProfessionalId = 5,
                StartedAt = DateTime.UtcNow,
                VitalSigns = new List<VitalSignRecordCommand>(),
                AdministeredAgents = new List<AdministeredAgentCommand>()
            };

            monitoring.Update(updateCommand);

            Assert.Empty(monitoring.VitalSigns);
            Assert.Empty(monitoring.AdministeredAgents);
        }

        [Fact]
        public void SetStatus_Should_Change_Status()
        {
            var command = new MonitoringRecordCommand(1)
            {
                AnesthesiaRecordId = 10,
                RecordedByProfessionalId = 5,
                StartedAt = DateTime.UtcNow
            };
            var monitoring = MonitoringRecord.Create(command);
            Assert.Equal(SurgeryStatusEnum.InProgress, monitoring.Status);

            monitoring.SetStatus(SurgeryStatusEnum.Completed);

            Assert.Equal(SurgeryStatusEnum.Completed, monitoring.Status);
        }
    }
}