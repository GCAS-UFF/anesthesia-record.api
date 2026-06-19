using System.Reflection;
using UFF.FichaAnestesica.Domain.Commands.AnesthesiaRecord;
using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Test.Entities
{
    public class AdministeredAgentTest
    {
        [Fact]
        public void Create_Should_Set_Properties_From_Command()
        {
            var command = new AdministeredAgentCommand
            {
                Timestamp = new DateTime(2025, 6, 1, 14, 30, 0),
                Dose = 10.5m,
                Unit = "mg",
                Route = AdministrationRouteEnum.IV,
                Presentation = "Ampola"
            };
            typeof(AdministeredAgentCommand)
                .GetProperty("DrugId", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)!
                .SetValue(command, 3);

            var agent = AdministeredAgent.Create(command);

            Assert.Equal(command.Timestamp, agent.Timestamp);
            Assert.Equal(3, agent.DrugId);
            Assert.Equal(10.5m, agent.Dose);
            Assert.Equal("mg", agent.Unit);
            Assert.Equal(AdministrationRouteEnum.IV, agent.Route);
            Assert.NotEqual(default, agent.CreatedAt);
        }

        [Fact]
        public void Update_Should_Update_All_Fields_From_Command()
        {
            var createCommand = new AdministeredAgentCommand
            {
                Timestamp = new DateTime(2025, 6, 1, 14, 30, 0),
                Dose = 5,
                Unit = "ml",
                Route = AdministrationRouteEnum.IM
            };
            typeof(AdministeredAgentCommand)
                .GetProperty("DrugId", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)!
                .SetValue(createCommand, 1);
            var agent = AdministeredAgent.Create(createCommand);

            var updateCommand = new AdministeredAgentCommand
            {
                Timestamp = new DateTime(2025, 6, 1, 15, 0, 0),
                Dose = 20,
                Unit = "mcg",
                Route = AdministrationRouteEnum.Epidural
            };
            typeof(AdministeredAgentCommand)
                .GetProperty("DrugId", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)!
                .SetValue(updateCommand, 2);

            agent.Update(updateCommand);

            Assert.Equal(updateCommand.Timestamp, agent.Timestamp);
            Assert.Equal(2, agent.DrugId);
            Assert.Equal(20, agent.Dose);
            Assert.Equal("mcg", agent.Unit);
            Assert.Equal(AdministrationRouteEnum.Epidural, agent.Route);
        }

        [Fact]
        public void Update_Should_Set_LastUpdate()
        {
            var command = new AdministeredAgentCommand
            {
                Timestamp = new DateTime(2025, 6, 1, 14, 30, 0),
                Dose = 8,
                Unit = "mg",
                Route = AdministrationRouteEnum.VO
            };
            typeof(AdministeredAgentCommand)
                .GetProperty("DrugId", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)!
                .SetValue(command, 4);
            var agent = AdministeredAgent.Create(command);

            agent.Update(command);

            Assert.NotNull(agent.LastUpdate);
        }
    }
}