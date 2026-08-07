using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UFF.FichaAnestesica.Infra.Migrations
{
    /// <inheritdoc />
    public partial class ajustarelacionamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_administered_agents_monitoring_records_monitoring_record_id",
                schema: "siga_db",
                table: "administered_agents");

            migrationBuilder.DropForeignKey(
                name: "f_k_administered_agents__monitoring_records_monitoring_record_id",
                schema: "siga_db",
                table: "administered_agents");

            migrationBuilder.DropIndex(
                name: "IX_administered_agents_monitoring_record_id1",
                schema: "siga_db",
                table: "administered_agents");

            migrationBuilder.DropColumn(
                name: "monitoring_record_id1",
                schema: "siga_db",
                table: "administered_agents");

            migrationBuilder.AlterColumn<string>(
                name: "unit",
                schema: "siga_db",
                table: "administered_agents",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "f_k_administered_agents__monitoring_records_monitoring_record_id",
                schema: "siga_db",
                table: "administered_agents",
                column: "monitoring_record_id",
                principalSchema: "siga_db",
                principalTable: "monitoring_records",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "f_k_administered_agents__monitoring_records_monitoring_record_id",
                schema: "siga_db",
                table: "administered_agents");

            migrationBuilder.AlterColumn<int>(
                name: "unit",
                schema: "siga_db",
                table: "administered_agents",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "monitoring_record_id1",
                schema: "siga_db",
                table: "administered_agents",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_administered_agents_monitoring_record_id1",
                schema: "siga_db",
                table: "administered_agents",
                column: "monitoring_record_id1");

            migrationBuilder.AddForeignKey(
                name: "FK_administered_agents_monitoring_records_monitoring_record_id",
                schema: "siga_db",
                table: "administered_agents",
                column: "monitoring_record_id",
                principalSchema: "siga_db",
                principalTable: "monitoring_records",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "f_k_administered_agents__monitoring_records_monitoring_record_id",
                schema: "siga_db",
                table: "administered_agents",
                column: "monitoring_record_id1",
                principalSchema: "siga_db",
                principalTable: "monitoring_records",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
