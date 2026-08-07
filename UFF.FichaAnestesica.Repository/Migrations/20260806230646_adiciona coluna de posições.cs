using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace UFF.FichaAnestesica.Infra.Migrations
{
    /// <inheritdoc />
    public partial class adicionacolunadeposições : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_monitoring_draft",
                schema: "siga_db",
                table: "monitoring_records",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "monitoring_updated_at",
                schema: "siga_db",
                table: "monitoring_records",
                type: "timestamptz",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "surgery_ended_at",
                schema: "siga_db",
                table: "monitoring_records",
                type: "timestamptz",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "surgery_started_at",
                schema: "siga_db",
                table: "monitoring_records",
                type: "timestamptz",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "patient_positions",
                schema: "siga_db",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    timestamp = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    position = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    monitoring_record_id = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    last_update = table.Column<DateTime>(type: "timestamptz", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patient_positions", x => x.id);
                    table.ForeignKey(
                        name: "f_k_patient_position_monitoring_records_monitoring_record_id",
                        column: x => x.monitoring_record_id,
                        principalSchema: "siga_db",
                        principalTable: "monitoring_records",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_patient_positions_monitoring_record_id",
                schema: "siga_db",
                table: "patient_positions",
                column: "monitoring_record_id");

            migrationBuilder.CreateIndex(
                name: "IX_patient_positions_timestamp",
                schema: "siga_db",
                table: "patient_positions",
                column: "timestamp");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "patient_positions",
                schema: "siga_db");

            migrationBuilder.DropColumn(
                name: "is_monitoring_draft",
                schema: "siga_db",
                table: "monitoring_records");

            migrationBuilder.DropColumn(
                name: "monitoring_updated_at",
                schema: "siga_db",
                table: "monitoring_records");

            migrationBuilder.DropColumn(
                name: "surgery_ended_at",
                schema: "siga_db",
                table: "monitoring_records");

            migrationBuilder.DropColumn(
                name: "surgery_started_at",
                schema: "siga_db",
                table: "monitoring_records");
        }
    }
}
