using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace UFF.FichaAnestesica.Infra.Migrations
{
    /// <inheritdoc />
    public partial class adicionapropriedadesausentesnaficha : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "pre_anesthetic_medication_dose",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "pre_anesthetic_medication_id",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "pre_anesthetic_medication_name",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "pre_anesthetic_medication_other_route",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "pre_anesthetic_medication_route",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<TimeOnly>(
                name: "pre_anesthetic_medication_time",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "time without time zone",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "anesthesia_record_antibiotic",
                schema: "siga_db",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    anesthesia_record_id = table.Column<int>(type: "integer", nullable: false),
                    medication_id = table.Column<int>(type: "integer", nullable: false),
                    medication_name = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    dose = table.Column<string>(type: "text", nullable: false),
                    route = table.Column<string>(type: "text", nullable: false),
                    time = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    has_booster = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_anesthesia_record_antibiotic", x => x.id);
                    table.ForeignKey(
                        name: "f_k_anesthesia_record_antibiotic_anesthesia_records_anesthesia_~",
                        column: x => x.anesthesia_record_id,
                        principalSchema: "siga_db",
                        principalTable: "anesthesia_records",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "anesthesia_record_antibiotic_booster",
                schema: "siga_db",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    anesthesia_record_antibiotic_id = table.Column<int>(type: "integer", nullable: false),
                    medication_id = table.Column<int>(type: "integer", nullable: false),
                    medication_name = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    dose = table.Column<string>(type: "text", nullable: false),
                    route = table.Column<string>(type: "text", nullable: false),
                    time = table.Column<TimeOnly>(type: "time without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_anesthesia_record_antibiotic_booster", x => x.id);
                    table.ForeignKey(
                        name: "f_k_anesthesia_record_antibiotic_booster_anesthesia_record_anti~",
                        column: x => x.anesthesia_record_antibiotic_id,
                        principalSchema: "siga_db",
                        principalTable: "anesthesia_record_antibiotic",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_anesthesia_record_antibiotic_anesthesia_record_id",
                schema: "siga_db",
                table: "anesthesia_record_antibiotic",
                column: "anesthesia_record_id");

            migrationBuilder.CreateIndex(
                name: "IX_anesthesia_record_antibiotic_booster_anesthesia_record_anti~",
                schema: "siga_db",
                table: "anesthesia_record_antibiotic_booster",
                column: "anesthesia_record_antibiotic_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "anesthesia_record_antibiotic_booster",
                schema: "siga_db");

            migrationBuilder.DropTable(
                name: "anesthesia_record_antibiotic",
                schema: "siga_db");

            migrationBuilder.DropColumn(
                name: "pre_anesthetic_medication_dose",
                schema: "siga_db",
                table: "anesthesia_records");

            migrationBuilder.DropColumn(
                name: "pre_anesthetic_medication_id",
                schema: "siga_db",
                table: "anesthesia_records");

            migrationBuilder.DropColumn(
                name: "pre_anesthetic_medication_name",
                schema: "siga_db",
                table: "anesthesia_records");

            migrationBuilder.DropColumn(
                name: "pre_anesthetic_medication_other_route",
                schema: "siga_db",
                table: "anesthesia_records");

            migrationBuilder.DropColumn(
                name: "pre_anesthetic_medication_route",
                schema: "siga_db",
                table: "anesthesia_records");

            migrationBuilder.DropColumn(
                name: "pre_anesthetic_medication_time",
                schema: "siga_db",
                table: "anesthesia_records");
        }
    }
}
