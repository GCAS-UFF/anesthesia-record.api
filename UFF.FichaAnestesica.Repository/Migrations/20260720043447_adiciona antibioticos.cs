using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace UFF.FichaAnestesica.Infra.Migrations
{
    /// <inheritdoc />
    public partial class adicionaantibioticos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "anesthesia_record_antibiotics",
                schema: "siga_db",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    anesthesia_record_id = table.Column<int>(type: "integer", nullable: false),
                    medication_id = table.Column<int>(type: "integer", nullable: false),
                    medication_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    dose = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    route = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    time = table.Column<TimeOnly>(type: "time", nullable: false),
                    has_booster = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_anesthesia_record_antibiotics", x => x.id);
                    table.ForeignKey(
                        name: "f_k_anesthesia_record_antibiotic_anesthesia_records_anesthesia_~",
                        column: x => x.anesthesia_record_id,
                        principalSchema: "siga_db",
                        principalTable: "anesthesia_records",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "anesthesia_record_antibiotic_boosters",
                schema: "siga_db",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    anesthesia_record_antibiotic_id = table.Column<int>(type: "integer", nullable: false),
                    medication_id = table.Column<int>(type: "integer", nullable: false),
                    medication_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    dose = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    route = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    time = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_anesthesia_record_antibiotic_boosters", x => x.id);
                    table.ForeignKey(
                        name: "f_k_anesthesia_record_antibiotic_booster_anesthesia_record_anti~",
                        column: x => x.anesthesia_record_antibiotic_id,
                        principalSchema: "siga_db",
                        principalTable: "anesthesia_record_antibiotics",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_anesthesia_record_antibiotic_boosters_anesthesia_record_ant~",
                schema: "siga_db",
                table: "anesthesia_record_antibiotic_boosters",
                column: "anesthesia_record_antibiotic_id");

            migrationBuilder.CreateIndex(
                name: "IX_anesthesia_record_antibiotics_anesthesia_record_id",
                schema: "siga_db",
                table: "anesthesia_record_antibiotics",
                column: "anesthesia_record_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "anesthesia_record_antibiotic_boosters",
                schema: "siga_db");

            migrationBuilder.DropTable(
                name: "anesthesia_record_antibiotics",
                schema: "siga_db");
        }
    }
}
