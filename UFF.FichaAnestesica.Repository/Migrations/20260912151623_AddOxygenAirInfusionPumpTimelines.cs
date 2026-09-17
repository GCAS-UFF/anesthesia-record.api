using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace UFF.FichaAnestesica.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddOxygenAirInfusionPumpTimelines : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "compressed_air_flows",
                schema: "siga_db",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    time = table.Column<TimeSpan>(type: "time without time zone", nullable: false),
                    timestamp = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    flow_rate_l_per_min = table.Column<decimal>(type: "numeric(6,2)", nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    monitoring_record_id = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    last_update = table.Column<DateTime>(type: "timestamptz", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_compressed_air_flows", x => x.id);
                    table.ForeignKey(
                        name: "f_k_compressed_air_flows__monitoring_records_monitoring_record_id",
                        column: x => x.monitoring_record_id,
                        principalSchema: "siga_db",
                        principalTable: "monitoring_records",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "infusion_pumps",
                schema: "siga_db",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    time = table.Column<TimeSpan>(type: "time without time zone", nullable: false),
                    timestamp = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    drug_id = table.Column<int>(type: "integer", nullable: false),
                    rate_ml_per_hour = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    volume_ml = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    end_at = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    monitoring_record_id = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    last_update = table.Column<DateTime>(type: "timestamptz", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_infusion_pumps", x => x.id);
                    table.ForeignKey(
                        name: "f_k_infusion_pumps__monitoring_records_monitoring_record_id",
                        column: x => x.monitoring_record_id,
                        principalSchema: "siga_db",
                        principalTable: "monitoring_records",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "f_k_infusion_pumps_drugs_drug_id",
                        column: x => x.drug_id,
                        principalSchema: "siga_db",
                        principalTable: "drugs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "oxygen_flows",
                schema: "siga_db",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    time = table.Column<TimeSpan>(type: "time without time zone", nullable: false),
                    timestamp = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    flow_rate_l_per_min = table.Column<decimal>(type: "numeric(6,2)", nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    monitoring_record_id = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    last_update = table.Column<DateTime>(type: "timestamptz", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_oxygen_flows", x => x.id);
                    table.ForeignKey(
                        name: "f_k_oxygen_flows_monitoring_records_monitoring_record_id",
                        column: x => x.monitoring_record_id,
                        principalSchema: "siga_db",
                        principalTable: "monitoring_records",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_compressed_air_flows_monitoring_record_id",
                schema: "siga_db",
                table: "compressed_air_flows",
                column: "monitoring_record_id");

            migrationBuilder.CreateIndex(
                name: "IX_compressed_air_flows_timestamp",
                schema: "siga_db",
                table: "compressed_air_flows",
                column: "timestamp");

            migrationBuilder.CreateIndex(
                name: "IX_infusion_pumps_drug_id",
                schema: "siga_db",
                table: "infusion_pumps",
                column: "drug_id");

            migrationBuilder.CreateIndex(
                name: "IX_infusion_pumps_monitoring_record_id",
                schema: "siga_db",
                table: "infusion_pumps",
                column: "monitoring_record_id");

            migrationBuilder.CreateIndex(
                name: "IX_infusion_pumps_timestamp",
                schema: "siga_db",
                table: "infusion_pumps",
                column: "timestamp");

            migrationBuilder.CreateIndex(
                name: "IX_oxygen_flows_monitoring_record_id",
                schema: "siga_db",
                table: "oxygen_flows",
                column: "monitoring_record_id");

            migrationBuilder.CreateIndex(
                name: "IX_oxygen_flows_timestamp",
                schema: "siga_db",
                table: "oxygen_flows",
                column: "timestamp");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "compressed_air_flows",
                schema: "siga_db");

            migrationBuilder.DropTable(
                name: "infusion_pumps",
                schema: "siga_db");

            migrationBuilder.DropTable(
                name: "oxygen_flows",
                schema: "siga_db");
        }
    }
}
