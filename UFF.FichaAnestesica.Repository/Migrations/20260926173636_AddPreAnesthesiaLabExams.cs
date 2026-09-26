using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace UFF.FichaAnestesica.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddPreAnesthesiaLabExams : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "pre_anesthesia_lab_exams",
                schema: "siga_db",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    anesthesia_record_id = table.Column<int>(type: "integer", nullable: false),
                    import_status = table.Column<int>(type: "integer", nullable: false),
                    source_exam_id = table.Column<long>(type: "bigint", nullable: true),
                    requested_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    collected_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    released_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    imported_at = table.Column<DateTime>(type: "timestamptz", nullable: true),
                    import_attempts = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    last_import_attempt_at = table.Column<DateTime>(type: "timestamptz", nullable: true),
                    last_import_message = table.Column<string>(type: "text", nullable: true),
                    last_manual_edit_at = table.Column<DateTime>(type: "timestamptz", nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    last_update = table.Column<DateTime>(type: "timestamptz", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pre_anesthesia_lab_exams", x => x.id);
                    table.ForeignKey(
                        name: "fk_pre_anesthesia_lab_exams_anesthesia_record",
                        column: x => x.anesthesia_record_id,
                        principalSchema: "siga_db",
                        principalTable: "anesthesia_records",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pre_anesthesia_lab_results",
                schema: "siga_db",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    lab_exam_id = table.Column<int>(type: "integer", nullable: false),
                    analyte = table.Column<int>(type: "integer", nullable: false),
                    value = table.Column<decimal>(type: "numeric(14,4)", nullable: true),
                    unit = table.Column<string>(type: "text", nullable: true),
                    reference_range = table.Column<string>(type: "text", nullable: true),
                    source = table.Column<int>(type: "integer", nullable: false),
                    imported_value = table.Column<decimal>(type: "numeric(14,4)", nullable: true),
                    aghu_code = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    last_update = table.Column<DateTime>(type: "timestamptz", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pre_anesthesia_lab_results", x => x.id);
                    table.ForeignKey(
                        name: "fk_pre_anesthesia_lab_results_lab_exam",
                        column: x => x.lab_exam_id,
                        principalSchema: "siga_db",
                        principalTable: "pre_anesthesia_lab_exams",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_pre_anesthesia_lab_exams_anesthesia_record_id",
                schema: "siga_db",
                table: "pre_anesthesia_lab_exams",
                column: "anesthesia_record_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_pre_anesthesia_lab_results_lab_exam_id_analyte",
                schema: "siga_db",
                table: "pre_anesthesia_lab_results",
                columns: new[] { "lab_exam_id", "analyte" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "pre_anesthesia_lab_results",
                schema: "siga_db");

            migrationBuilder.DropTable(
                name: "pre_anesthesia_lab_exams",
                schema: "siga_db");
        }
    }
}
