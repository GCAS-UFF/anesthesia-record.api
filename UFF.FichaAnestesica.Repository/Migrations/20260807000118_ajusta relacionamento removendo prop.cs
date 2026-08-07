using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UFF.FichaAnestesica.Infra.Migrations
{
    /// <inheritdoc />
    public partial class ajustarelacionamentoremovendoprop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "f_k_administered_agents__anesthesia_records_anesthesia_record_id",
                schema: "siga_db",
                table: "administered_agents");

            migrationBuilder.DropIndex(
                name: "IX_administered_agents_anesthesia_record_id",
                schema: "siga_db",
                table: "administered_agents");

            migrationBuilder.DropColumn(
                name: "anesthesia_record_id",
                schema: "siga_db",
                table: "administered_agents");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "anesthesia_record_id",
                schema: "siga_db",
                table: "administered_agents",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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
        }
    }
}
