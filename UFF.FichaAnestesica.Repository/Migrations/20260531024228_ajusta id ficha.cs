using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UFF.FichaAnestesica.Infra.Migrations
{
    /// <inheritdoc />
    public partial class ajustaidficha : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "f_k_anesthesia_records__monitoring_records_monitoring_record_id",
                schema: "siga_db",
                table: "monitoring_records");

            migrationBuilder.RenameColumn(
                name: "surgery_id",
                schema: "siga_db",
                table: "anesthesia_records",
                newName: "id");

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
                name: "f_k_monitoring_records_anesthesia_records_anesthesia_record_id",
                schema: "siga_db",
                table: "monitoring_records");

            migrationBuilder.RenameColumn(
                name: "id",
                schema: "siga_db",
                table: "anesthesia_records",
                newName: "surgery_id");

            migrationBuilder.AddForeignKey(
                name: "f_k_anesthesia_records__monitoring_records_monitoring_record_id",
                schema: "siga_db",
                table: "monitoring_records",
                column: "anesthesia_record_id",
                principalSchema: "siga_db",
                principalTable: "anesthesia_records",
                principalColumn: "surgery_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
