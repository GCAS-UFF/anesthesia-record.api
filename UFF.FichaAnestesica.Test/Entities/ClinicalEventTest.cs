using UFF.FichaAnestesica.Domain.Commands.AnesthesiaRecord;
using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Test.Entities
{
    public class ClinicalEventTest
    {
        [Fact]
        public void Create_Should_Set_Properties_From_Command()
        {
            var command = new ClinicalEventCommand
            {
                Timestamp = new DateTime(2025, 6, 1, 14, 30, 0),
                EventType = ClinicalEventTypeEnum.CardiovascularEvent,
                Name = "Hipotensão",
                Description = "Paciente apresentou queda de pressão",
                Observations = "PA 80x50"
            };

            var clinicalEvent = ClinicalEvent.Create(command);

            Assert.Equal(command.Timestamp, clinicalEvent.Timestamp);
            Assert.Equal(ClinicalEventTypeEnum.CardiovascularEvent, clinicalEvent.EventType);
            Assert.Equal(command.Description, clinicalEvent.Description);
            Assert.Equal(command.Observations, clinicalEvent.Observations);
            Assert.NotEqual(default, clinicalEvent.CreatedAt);
        }

        [Fact]
        public void Update_Should_Update_All_Fields_From_Command()
        {
            var createCommand = new ClinicalEventCommand
            {
                Timestamp = new DateTime(2025, 6, 1, 14, 30, 0),
                EventType = ClinicalEventTypeEnum.Procedure,
                Description = "Evento inicial",
                Observations = null
            };
            var clinicalEvent = ClinicalEvent.Create(createCommand);

            var updateCommand = new ClinicalEventCommand
            {
                Timestamp = new DateTime(2025, 6, 1, 15, 0, 0),
                EventType = ClinicalEventTypeEnum.RespiratoryEvent,
                Description = "Evento atualizado",
                Observations = "Observação nova"
            };

            clinicalEvent.Update(updateCommand);

            Assert.Equal(updateCommand.Timestamp, clinicalEvent.Timestamp);
            Assert.Equal(ClinicalEventTypeEnum.RespiratoryEvent, clinicalEvent.EventType);
            Assert.Equal(updateCommand.Description, clinicalEvent.Description);
            Assert.Equal(updateCommand.Observations, clinicalEvent.Observations);
        }

        [Fact]
        public void Update_Should_Set_LastUpdate()
        {
            var command = new ClinicalEventCommand
            {
                Timestamp = new DateTime(2025, 6, 1, 14, 30, 0),
                EventType = ClinicalEventTypeEnum.Other,
                Description = "Evento qualquer"
            };
            var clinicalEvent = ClinicalEvent.Create(command);

            clinicalEvent.Update(command);

            Assert.NotNull(clinicalEvent.LastUpdate);
        }
    }
}