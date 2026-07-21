using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UFF.FichaAnestesica.Infra.Migrations
{
    /// <inheritdoc />
    public partial class ajustacolunadeobrigatoriedade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "f_k_anesthesia_record_procedure__anesthesia_records_anesthesia_re~",
                schema: "siga_db",
                table: "anesthesia_record_procedures");

            migrationBuilder.DropForeignKey(
                name: "f_k_anesthesia_record_procedure__procedures_procedure_id",
                schema: "siga_db",
                table: "anesthesia_record_procedures");

            migrationBuilder.AlterColumn<string>(
                name: "cushions_access_location",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "varchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)");

            migrationBuilder.AddForeignKey(
                name: "f_k_anesthesia_record_procedures__anesthesia_records_anesthesia_r~",
                schema: "siga_db",
                table: "anesthesia_record_procedures",
                column: "anesthesia_record_id",
                principalSchema: "siga_db",
                principalTable: "anesthesia_records",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "f_k_anesthesia_record_procedures__procedures_procedure_id",
                schema: "siga_db",
                table: "anesthesia_record_procedures",
                column: "procedure_id",
                principalSchema: "siga_db",
                principalTable: "procedures",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "f_k_anesthesia_record_procedures__anesthesia_records_anesthesia_r~",
                schema: "siga_db",
                table: "anesthesia_record_procedures");

            migrationBuilder.DropForeignKey(
                name: "f_k_anesthesia_record_procedures__procedures_procedure_id",
                schema: "siga_db",
                table: "anesthesia_record_procedures");

            migrationBuilder.AlterColumn<string>(
                name: "cushions_access_location",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "f_k_anesthesia_record_procedure__anesthesia_records_anesthesia_re~",
                schema: "siga_db",
                table: "anesthesia_record_procedures",
                column: "anesthesia_record_id",
                principalSchema: "siga_db",
                principalTable: "anesthesia_records",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "f_k_anesthesia_record_procedure__procedures_procedure_id",
                schema: "siga_db",
                table: "anesthesia_record_procedures",
                column: "procedure_id",
                principalSchema: "siga_db",
                principalTable: "procedures",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
