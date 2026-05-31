using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UFF.FichaAnestesica.Infra.Migrations
{
    /// <inheritdoc />
    public partial class ajustamapeamentomonitoramento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_monitoring_records_surgery_id",
                schema: "siga_db",
                table: "monitoring_records");

            migrationBuilder.DropColumn(
                name: "surgery_id",
                schema: "siga_db",
                table: "monitoring_records");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "surgery_id",
                schema: "siga_db",
                table: "monitoring_records",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_monitoring_records_surgery_id",
                schema: "siga_db",
                table: "monitoring_records",
                column: "surgery_id");
        }
    }
}
