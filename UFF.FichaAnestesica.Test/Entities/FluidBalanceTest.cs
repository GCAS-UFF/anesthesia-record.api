using UFF.FichaAnestesica.Domain.Commands.AnesthesiaRecord;
using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Test.Entities
{
    public class FluidBalanceTest
    {
        [Fact]
        public void Create_Should_Set_Properties_From_Command()
        {
            var command = new FluidBalanceCommand
            {
                Timestamp = new DateTime(2025, 6, 1, 14, 30, 0),
                Type = FluidBalanceTypeEnum.Gain,
                Category = FluidCategoryEnum.Crystalloid,
                Description = "Soro fisiológico 500ml",
                VolumeMl = 500
            };

            var fluidBalance = FluidBalance.Create(command);

            Assert.Equal(command.Timestamp, fluidBalance.Timestamp);
            Assert.Equal(FluidBalanceTypeEnum.Gain, fluidBalance.Type);
            Assert.Equal(FluidCategoryEnum.Crystalloid, fluidBalance.Category);
            Assert.Equal(command.Description, fluidBalance.Name);
            Assert.Equal(500, fluidBalance.VolumeMl);
            Assert.NotEqual(default, fluidBalance.CreatedAt);
        }

        [Fact]
        public void Update_Should_Update_All_Fields_From_Command()
        {
            var createCommand = new FluidBalanceCommand
            {
                Timestamp = new DateTime(2025, 6, 1, 14, 30, 0),
                Type = FluidBalanceTypeEnum.Gain,
                Category = FluidCategoryEnum.Crystalloid,
                Description = "Soro fisiológico 500ml",
                VolumeMl = 500
            };
            var fluidBalance = FluidBalance.Create(createCommand);

            var updateCommand = new FluidBalanceCommand
            {
                Timestamp = new DateTime(2025, 6, 1, 15, 0, 0),
                Type = FluidBalanceTypeEnum.Loss,
                Category = FluidCategoryEnum.Diuresis,
                Description = "Diurese 200ml",
                VolumeMl = 200
            };

            fluidBalance.Update(updateCommand);

            Assert.Equal(updateCommand.Timestamp, fluidBalance.Timestamp);
            Assert.Equal(FluidBalanceTypeEnum.Loss, fluidBalance.Type);
            Assert.Equal(FluidCategoryEnum.Diuresis, fluidBalance.Category);
            Assert.Equal(updateCommand.Description, fluidBalance.Name);
            Assert.Equal(200, fluidBalance.VolumeMl);
        }

        [Fact]
        public void Update_Should_Set_LastUpdate()
        {
            var command = new FluidBalanceCommand
            {
                Timestamp = new DateTime(2025, 6, 1, 14, 30, 0),
                Type = FluidBalanceTypeEnum.Gain,
                Category = FluidCategoryEnum.Crystalloid,
                Description = "Soro",
                VolumeMl = 300
            };
            var fluidBalance = FluidBalance.Create(command);

            fluidBalance.Update(command);

            Assert.NotNull(fluidBalance.LastUpdate);
        }
    }
}