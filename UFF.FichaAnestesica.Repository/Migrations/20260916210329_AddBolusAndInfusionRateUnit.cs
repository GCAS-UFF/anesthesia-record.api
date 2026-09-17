using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UFF.FichaAnestesica.Infra.Migrations
{
    /// <inheritdoc />
    /// <remarks>
    /// As tabelas oxygen_flows/compressed_air_flows/infusion_pumps já existem no banco
    /// (criadas pela migration 20260912151623_AddOxygenAirInfusionPumpTimelines, aplicada
    /// antes das classes C# correspondentes terem sido escritas — por isso o snapshot do
    /// EF não as conhecia até agora). O `dotnet ef migrations add` gerou CreateTable para
    /// as 3 de novo (por não saber que já existiam); esta migration foi editada à mão para
    /// conter só as mudanças reais: renomear infusion_pumps.rate_ml_per_hour -> rate,
    /// adicionar infusion_pumps.rate_unit e administered_agents.is_bolus.
    /// </remarks>
    public partial class AddBolusAndInfusionRateUnit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_bolus",
                schema: "siga_db",
                table: "administered_agents",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.RenameColumn(
                name: "rate_ml_per_hour",
                schema: "siga_db",
                table: "infusion_pumps",
                newName: "rate");

            migrationBuilder.AddColumn<int>(
                name: "rate_unit",
                schema: "siga_db",
                table: "infusion_pumps",
                type: "integer",
                nullable: false,
                defaultValue: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "rate_unit",
                schema: "siga_db",
                table: "infusion_pumps");

            migrationBuilder.RenameColumn(
                name: "rate",
                schema: "siga_db",
                table: "infusion_pumps",
                newName: "rate_ml_per_hour");

            migrationBuilder.DropColumn(
                name: "is_bolus",
                schema: "siga_db",
                table: "administered_agents");
        }
    }
}
