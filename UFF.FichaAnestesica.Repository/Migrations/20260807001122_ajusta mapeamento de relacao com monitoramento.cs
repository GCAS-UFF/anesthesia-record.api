using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UFF.FichaAnestesica.Infra.Migrations
{
    /// <inheritdoc />
    public partial class ajustamapeamentoderelacaocommonitoramento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_clinical_events_monitoring_records_monitoring_record_id",
                schema: "siga_db",
                table: "clinical_events");

            migrationBuilder.DropForeignKey(
                name: "f_k_clinical_events__monitoring_records_monitoring_record_id",
                schema: "siga_db",
                table: "clinical_events");

            migrationBuilder.DropForeignKey(
                name: "FK_fluid_balances_monitoring_records_monitoring_record_id",
                schema: "siga_db",
                table: "fluid_balances");

            migrationBuilder.DropForeignKey(
                name: "f_k_fluid_balances__monitoring_records_monitoring_record_id",
                schema: "siga_db",
                table: "fluid_balances");

            migrationBuilder.DropIndex(
                name: "IX_fluid_balances_monitoring_record_id1",
                schema: "siga_db",
                table: "fluid_balances");

            migrationBuilder.DropIndex(
                name: "IX_clinical_events_monitoring_record_id1",
                schema: "siga_db",
                table: "clinical_events");

            migrationBuilder.DropColumn(
                name: "monitoring_record_id1",
                schema: "siga_db",
                table: "vital_sign_records");

            migrationBuilder.DropColumn(
                name: "monitoring_record_id1",
                schema: "siga_db",
                table: "fluid_balances");

            migrationBuilder.DropColumn(
                name: "monitoring_record_id1",
                schema: "siga_db",
                table: "clinical_events");

            migrationBuilder.AddForeignKey(
                name: "f_k_clinical_events__monitoring_records_monitoring_record_id",
                schema: "siga_db",
                table: "clinical_events",
                column: "monitoring_record_id",
                principalSchema: "siga_db",
                principalTable: "monitoring_records",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "f_k_fluid_balances__monitoring_records_monitoring_record_id",
                schema: "siga_db",
                table: "fluid_balances",
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
                name: "f_k_clinical_events__monitoring_records_monitoring_record_id",
                schema: "siga_db",
                table: "clinical_events");

            migrationBuilder.DropForeignKey(
                name: "f_k_fluid_balances__monitoring_records_monitoring_record_id",
                schema: "siga_db",
                table: "fluid_balances");

            migrationBuilder.AddColumn<int>(
                name: "monitoring_record_id1",
                schema: "siga_db",
                table: "vital_sign_records",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "monitoring_record_id1",
                schema: "siga_db",
                table: "fluid_balances",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "monitoring_record_id1",
                schema: "siga_db",
                table: "clinical_events",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_fluid_balances_monitoring_record_id1",
                schema: "siga_db",
                table: "fluid_balances",
                column: "monitoring_record_id1");

            migrationBuilder.CreateIndex(
                name: "IX_clinical_events_monitoring_record_id1",
                schema: "siga_db",
                table: "clinical_events",
                column: "monitoring_record_id1");

            migrationBuilder.AddForeignKey(
                name: "FK_clinical_events_monitoring_records_monitoring_record_id",
                schema: "siga_db",
                table: "clinical_events",
                column: "monitoring_record_id",
                principalSchema: "siga_db",
                principalTable: "monitoring_records",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "f_k_clinical_events__monitoring_records_monitoring_record_id",
                schema: "siga_db",
                table: "clinical_events",
                column: "monitoring_record_id1",
                principalSchema: "siga_db",
                principalTable: "monitoring_records",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_fluid_balances_monitoring_records_monitoring_record_id",
                schema: "siga_db",
                table: "fluid_balances",
                column: "monitoring_record_id",
                principalSchema: "siga_db",
                principalTable: "monitoring_records",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "f_k_fluid_balances__monitoring_records_monitoring_record_id",
                schema: "siga_db",
                table: "fluid_balances",
                column: "monitoring_record_id1",
                principalSchema: "siga_db",
                principalTable: "monitoring_records",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
