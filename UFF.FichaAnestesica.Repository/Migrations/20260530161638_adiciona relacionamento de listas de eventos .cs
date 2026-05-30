using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UFF.FichaAnestesica.Infra.Migrations
{
    /// <inheritdoc />
    public partial class adicionarelacionamentodelistasdeeventos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_monitoring_records_anesthesia_record_id",
                schema: "siga_db",
                table: "monitoring_records");

            migrationBuilder.DropColumn(
                name: "category",
                schema: "siga_db",
                table: "drugs");

            migrationBuilder.DropColumn(
                name: "default_presentation",
                schema: "siga_db",
                table: "drugs");

            migrationBuilder.AddColumn<int>(
                name: "surgery_id",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "anesthesia_record_id",
                schema: "siga_db",
                table: "administered_agents",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_monitoring_records_anesthesia_record_id",
                schema: "siga_db",
                table: "monitoring_records",
                column: "anesthesia_record_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_administered_agents_anesthesia_record_id",
                schema: "siga_db",
                table: "administered_agents",
                column: "anesthesia_record_id");

            migrationBuilder.AddForeignKey(
                name: "f_k_administered_agents__anesthesia_records_anesthesia_record_id",
                schema: "siga_db",
                table: "administered_agents",
                column: "anesthesia_record_id",
                principalSchema: "siga_db",
                principalTable: "anesthesia_records",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "f_k_monitoring_records_anesthesia_records_anesthesia_record_id",
                schema: "siga_db",
                table: "monitoring_records",
                column: "anesthesia_record_id",
                principalSchema: "siga_db",
                principalTable: "anesthesia_records",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "f_k_administered_agents__anesthesia_records_anesthesia_record_id",
                schema: "siga_db",
                table: "administered_agents");

            migrationBuilder.DropForeignKey(
                name: "f_k_monitoring_records_anesthesia_records_anesthesia_record_id",
                schema: "siga_db",
                table: "monitoring_records");

            migrationBuilder.DropIndex(
                name: "IX_monitoring_records_anesthesia_record_id",
                schema: "siga_db",
                table: "monitoring_records");

            migrationBuilder.DropIndex(
                name: "IX_administered_agents_anesthesia_record_id",
                schema: "siga_db",
                table: "administered_agents");

            migrationBuilder.DropColumn(
                name: "surgery_id",
                schema: "siga_db",
                table: "anesthesia_records");

            migrationBuilder.DropColumn(
                name: "anesthesia_record_id",
                schema: "siga_db",
                table: "administered_agents");

            migrationBuilder.AddColumn<int>(
                name: "category",
                schema: "siga_db",
                table: "drugs",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "default_presentation",
                schema: "siga_db",
                table: "drugs",
                type: "varchar(150)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_monitoring_records_anesthesia_record_id",
                schema: "siga_db",
                table: "monitoring_records",
                column: "anesthesia_record_id");
        }
    }
}
