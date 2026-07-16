using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace UFF.FichaAnestesica.Infra.Migrations
{
    /// <inheritdoc />
    public partial class adicionatabeladeprocedimento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "procedure",
                schema: "siga_db",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    external_id = table.Column<string>(type: "text", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    cid = table.Column<string>(type: "text", nullable: true),
                    active = table.Column<bool>(type: "boolean", nullable: false),
                    last_sync_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    last_update = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_procedure", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "anesthesia_record_procedures",
                schema: "siga_db",
                columns: table => new
                {
                    anesthesia_record_id = table.Column<int>(type: "integer", nullable: false),
                    procedure_id = table.Column<int>(type: "integer", nullable: false),
                    is_primary = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    last_update = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_anesthesia_record_procedures", x => new { x.anesthesia_record_id, x.procedure_id });
                    table.ForeignKey(
                        name: "f_k_anesthesia_record_procedure__anesthesia_records_anesthesia_re~",
                        column: x => x.anesthesia_record_id,
                        principalSchema: "siga_db",
                        principalTable: "anesthesia_records",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "f_k_anesthesia_record_procedure__procedure_procedure_id",
                        column: x => x.procedure_id,
                        principalSchema: "siga_db",
                        principalTable: "procedure",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_anesthesia_record_procedures_procedure_id",
                schema: "siga_db",
                table: "anesthesia_record_procedures",
                column: "procedure_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "anesthesia_record_procedures",
                schema: "siga_db");

            migrationBuilder.DropTable(
                name: "procedure",
                schema: "siga_db");
        }
    }
}
