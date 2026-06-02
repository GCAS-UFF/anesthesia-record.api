using UFF.FichaAnestesica.Domain.Commands.AnesthesiaRecord;
using UFF.FichaAnestesica.Domain.Entities;

namespace UFF.FichaAnestesica.Test.Entities
{
    public class CustomFieldTest
    {
        [Fact]
        public void Create_Should_Set_Properties_From_Command()
        {
            var command = new CustomFieldCommand
            {
                Name = "Pressão Venosa Central",
                Value = "12"
            };

            var customField = CustomField.Create(command);

            Assert.Equal(command.Name, customField.Name);
            Assert.Equal(command.Value, customField.Value);
            Assert.NotEqual(default, customField.CreatedAt);
        }

        [Fact]
        public void Update_Should_Update_All_Fields_From_Command()
        {
            var createCommand = new CustomFieldCommand
            {
                Name = "PVC",
                Value = "8"
            };
            var customField = CustomField.Create(createCommand);

            var updateCommand = new CustomFieldCommand
            {
                Name = "Pressão Arterial Invasiva",
                Value = "120/80"
            };

            customField.Update(updateCommand);

            Assert.Equal(updateCommand.Name, customField.Name);
            Assert.Equal(updateCommand.Value, customField.Value);
        }

        [Fact]
        public void Update_Should_Set_LastUpdate()
        {
            var command = new CustomFieldCommand
            {
                Name = "Temperatura",
                Value = "36.5"
            };
            var customField = CustomField.Create(command);

            customField.Update(command);

            Assert.NotNull(customField.LastUpdate);
        }
    }
}