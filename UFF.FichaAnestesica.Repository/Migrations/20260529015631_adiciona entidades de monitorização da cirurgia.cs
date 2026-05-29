using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace UFF.FichaAnestesica.Infra.Migrations
{
    /// <inheritdoc />
    public partial class adicionaentidadesdemonitorizaçãodacirurgia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "f_k_anesthesia_record__users_first_anesthesiologist_id",
                schema: "siga_db",
                table: "anesthesia_records");

            migrationBuilder.DropForeignKey(
                name: "f_k_anesthesia_record__users_second_anesthesiologist_id",
                schema: "siga_db",
                table: "anesthesia_records");

            migrationBuilder.DropForeignKey(
                name: "f_k_pre_anesthesia_record__users_first_anesthesiologist_id",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropForeignKey(
                name: "f_k_pre_anesthesia_record__users_second_anesthesiologist_id",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "last_login_at",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "last_sync_at",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "last_login_at",
                schema: "siga_db",
                table: "anesthesia_records");

            migrationBuilder.DropColumn(
                name: "last_sync_at",
                schema: "siga_db",
                table: "anesthesia_records");

            migrationBuilder.CreateTable(
                name: "drugs",
                schema: "siga_db",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "varchar(150)", nullable: false),
                    default_presentation = table.Column<string>(type: "varchar(150)", nullable: false),
                    default_unit = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    last_update = table.Column<DateTime>(type: "timestamptz", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_drugs", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "monitoring_records",
                schema: "siga_db",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    anesthesia_record_id = table.Column<int>(type: "integer", nullable: false),
                    surgery_id = table.Column<int>(type: "integer", nullable: false),
                    recorded_by_professional_id = table.Column<int>(type: "integer", nullable: false),
                    started_at = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    ended_at = table.Column<DateTime>(type: "timestamptz", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    last_update = table.Column<DateTime>(type: "timestamptz", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_monitoring_records", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "administered_agents",
                schema: "siga_db",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    timestamp = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    drug_id = table.Column<int>(type: "integer", nullable: false),
                    dose = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    unit = table.Column<int>(type: "integer", nullable: false),
                    route = table.Column<int>(type: "integer", nullable: false),
                    presentation = table.Column<string>(type: "varchar(150)", nullable: true),
                    monitoring_record_id = table.Column<int>(type: "integer", nullable: false),
                    monitoring_record_id1 = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    last_update = table.Column<DateTime>(type: "timestamptz", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_administered_agents", x => x.id);
                    table.ForeignKey(
                        name: "FK_administered_agents_monitoring_records_monitoring_record_id",
                        column: x => x.monitoring_record_id,
                        principalSchema: "siga_db",
                        principalTable: "monitoring_records",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "f_k_administered_agents__drugs_drug_id",
                        column: x => x.drug_id,
                        principalSchema: "siga_db",
                        principalTable: "drugs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "f_k_administered_agents__monitoring_records_monitoring_record_id",
                        column: x => x.monitoring_record_id1,
                        principalSchema: "siga_db",
                        principalTable: "monitoring_records",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "clinical_events",
                schema: "siga_db",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    timestamp = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    event_type = table.Column<int>(type: "integer", nullable: false),
                    description = table.Column<string>(type: "varchar(500)", nullable: false),
                    observations = table.Column<string>(type: "text", nullable: true),
                    monitoring_record_id = table.Column<int>(type: "integer", nullable: false),
                    monitoring_record_id1 = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    last_update = table.Column<DateTime>(type: "timestamptz", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clinical_events", x => x.id);
                    table.ForeignKey(
                        name: "FK_clinical_events_monitoring_records_monitoring_record_id",
                        column: x => x.monitoring_record_id,
                        principalSchema: "siga_db",
                        principalTable: "monitoring_records",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "f_k_clinical_events__monitoring_records_monitoring_record_id",
                        column: x => x.monitoring_record_id1,
                        principalSchema: "siga_db",
                        principalTable: "monitoring_records",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "fluid_balances",
                schema: "siga_db",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    timestamp = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    category = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "varchar(150)", nullable: false),
                    volume_ml = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    monitoring_record_id = table.Column<int>(type: "integer", nullable: false),
                    monitoring_record_id1 = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    last_update = table.Column<DateTime>(type: "timestamptz", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fluid_balances", x => x.id);
                    table.ForeignKey(
                        name: "FK_fluid_balances_monitoring_records_monitoring_record_id",
                        column: x => x.monitoring_record_id,
                        principalSchema: "siga_db",
                        principalTable: "monitoring_records",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "f_k_fluid_balances__monitoring_records_monitoring_record_id",
                        column: x => x.monitoring_record_id1,
                        principalSchema: "siga_db",
                        principalTable: "monitoring_records",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "vital_sign_records",
                schema: "siga_db",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    timestamp = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    systolic_blood_pressure = table.Column<int>(type: "integer", nullable: true),
                    diastolic_blood_pressure = table.Column<int>(type: "integer", nullable: true),
                    mean_arterial_pressure = table.Column<int>(type: "integer", nullable: true),
                    heart_rate = table.Column<int>(type: "integer", nullable: true),
                    spo2 = table.Column<int>(type: "integer", nullable: true),
                    etco2 = table.Column<int>(type: "integer", nullable: true),
                    temperature = table.Column<decimal>(type: "numeric(5,2)", nullable: true),
                    bis = table.Column<int>(type: "integer", nullable: true),
                    pvc = table.Column<decimal>(type: "numeric(5,2)", nullable: true),
                    pcap = table.Column<decimal>(type: "numeric(5,2)", nullable: true),
                    monitoring_record_id = table.Column<int>(type: "integer", nullable: false),
                    monitoring_record_id1 = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    last_update = table.Column<DateTime>(type: "timestamptz", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vital_sign_records", x => x.id);
                    table.ForeignKey(
                        name: "f_k_vital_sign_records_monitoring_records_monitoring_record_id",
                        column: x => x.monitoring_record_id,
                        principalSchema: "siga_db",
                        principalTable: "monitoring_records",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "custom_fields",
                schema: "siga_db",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "varchar(100)", nullable: false),
                    value = table.Column<string>(type: "varchar(500)", nullable: false),
                    vital_sign_record_id = table.Column<int>(type: "integer", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    last_update = table.Column<DateTime>(type: "timestamptz", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_custom_fields", x => x.id);
                    table.ForeignKey(
                        name: "f_k_custom_fields__vital_sign_records_vital_sign_record_id",
                        column: x => x.vital_sign_record_id,
                        principalSchema: "siga_db",
                        principalTable: "vital_sign_records",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_administered_agents_drug_id",
                schema: "siga_db",
                table: "administered_agents",
                column: "drug_id");

            migrationBuilder.CreateIndex(
                name: "IX_administered_agents_monitoring_record_id",
                schema: "siga_db",
                table: "administered_agents",
                column: "monitoring_record_id");

            migrationBuilder.CreateIndex(
                name: "IX_administered_agents_monitoring_record_id1",
                schema: "siga_db",
                table: "administered_agents",
                column: "monitoring_record_id1");

            migrationBuilder.CreateIndex(
                name: "IX_administered_agents_timestamp",
                schema: "siga_db",
                table: "administered_agents",
                column: "timestamp");

            migrationBuilder.CreateIndex(
                name: "IX_clinical_events_event_type",
                schema: "siga_db",
                table: "clinical_events",
                column: "event_type");

            migrationBuilder.CreateIndex(
                name: "IX_clinical_events_monitoring_record_id",
                schema: "siga_db",
                table: "clinical_events",
                column: "monitoring_record_id");

            migrationBuilder.CreateIndex(
                name: "IX_clinical_events_monitoring_record_id1",
                schema: "siga_db",
                table: "clinical_events",
                column: "monitoring_record_id1");

            migrationBuilder.CreateIndex(
                name: "IX_clinical_events_timestamp",
                schema: "siga_db",
                table: "clinical_events",
                column: "timestamp");

            migrationBuilder.CreateIndex(
                name: "IX_custom_fields_vital_sign_record_id",
                schema: "siga_db",
                table: "custom_fields",
                column: "vital_sign_record_id");

            migrationBuilder.CreateIndex(
                name: "IX_drugs_name",
                schema: "siga_db",
                table: "drugs",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_fluid_balances_category",
                schema: "siga_db",
                table: "fluid_balances",
                column: "category");

            migrationBuilder.CreateIndex(
                name: "IX_fluid_balances_monitoring_record_id",
                schema: "siga_db",
                table: "fluid_balances",
                column: "monitoring_record_id");

            migrationBuilder.CreateIndex(
                name: "IX_fluid_balances_monitoring_record_id1",
                schema: "siga_db",
                table: "fluid_balances",
                column: "monitoring_record_id1");

            migrationBuilder.CreateIndex(
                name: "IX_fluid_balances_timestamp",
                schema: "siga_db",
                table: "fluid_balances",
                column: "timestamp");

            migrationBuilder.CreateIndex(
                name: "IX_fluid_balances_type",
                schema: "siga_db",
                table: "fluid_balances",
                column: "type");

            migrationBuilder.CreateIndex(
                name: "IX_monitoring_records_anesthesia_record_id",
                schema: "siga_db",
                table: "monitoring_records",
                column: "anesthesia_record_id");

            migrationBuilder.CreateIndex(
                name: "IX_monitoring_records_recorded_by_professional_id",
                schema: "siga_db",
                table: "monitoring_records",
                column: "recorded_by_professional_id");

            migrationBuilder.CreateIndex(
                name: "IX_monitoring_records_started_at",
                schema: "siga_db",
                table: "monitoring_records",
                column: "started_at");

            migrationBuilder.CreateIndex(
                name: "IX_monitoring_records_surgery_id",
                schema: "siga_db",
                table: "monitoring_records",
                column: "surgery_id");

            migrationBuilder.CreateIndex(
                name: "IX_vital_sign_records_monitoring_record_id",
                schema: "siga_db",
                table: "vital_sign_records",
                column: "monitoring_record_id");

            migrationBuilder.CreateIndex(
                name: "IX_vital_sign_records_timestamp",
                schema: "siga_db",
                table: "vital_sign_records",
                column: "timestamp");

            migrationBuilder.AddForeignKey(
                name: "f_k_anesthesia_records__users_first_anesthesiologist_id",
                schema: "siga_db",
                table: "anesthesia_records",
                column: "first_anesthesiologist_id",
                principalSchema: "siga_db",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "f_k_anesthesia_records__users_second_anesthesiologist_id",
                schema: "siga_db",
                table: "anesthesia_records",
                column: "second_anesthesiologist_id",
                principalSchema: "siga_db",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "f_k_pre_anesthesia_records__users_first_anesthesiologist_id",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                column: "first_anesthesiologist_id1",
                principalSchema: "siga_db",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "f_k_pre_anesthesia_records__users_second_anesthesiologist_id",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                column: "second_anesthesiologist_id1",
                principalSchema: "siga_db",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "f_k_anesthesia_records__users_first_anesthesiologist_id",
                schema: "siga_db",
                table: "anesthesia_records");

            migrationBuilder.DropForeignKey(
                name: "f_k_anesthesia_records__users_second_anesthesiologist_id",
                schema: "siga_db",
                table: "anesthesia_records");

            migrationBuilder.DropForeignKey(
                name: "f_k_pre_anesthesia_records__users_first_anesthesiologist_id",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropForeignKey(
                name: "f_k_pre_anesthesia_records__users_second_anesthesiologist_id",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropTable(
                name: "administered_agents",
                schema: "siga_db");

            migrationBuilder.DropTable(
                name: "clinical_events",
                schema: "siga_db");

            migrationBuilder.DropTable(
                name: "custom_fields",
                schema: "siga_db");

            migrationBuilder.DropTable(
                name: "fluid_balances",
                schema: "siga_db");

            migrationBuilder.DropTable(
                name: "drugs",
                schema: "siga_db");

            migrationBuilder.DropTable(
                name: "vital_sign_records",
                schema: "siga_db");

            migrationBuilder.DropTable(
                name: "monitoring_records",
                schema: "siga_db");

            migrationBuilder.AddColumn<DateTime>(
                name: "last_login_at",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "timestamptz",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "last_sync_at",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "timestamptz",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_login_at",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "timestamptz",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_sync_at",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "timestamptz",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "f_k_anesthesia_record__users_first_anesthesiologist_id",
                schema: "siga_db",
                table: "anesthesia_records",
                column: "first_anesthesiologist_id",
                principalSchema: "siga_db",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "f_k_anesthesia_record__users_second_anesthesiologist_id",
                schema: "siga_db",
                table: "anesthesia_records",
                column: "second_anesthesiologist_id",
                principalSchema: "siga_db",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "f_k_pre_anesthesia_record__users_first_anesthesiologist_id",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                column: "first_anesthesiologist_id1",
                principalSchema: "siga_db",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "f_k_pre_anesthesia_record__users_second_anesthesiologist_id",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                column: "second_anesthesiologist_id1",
                principalSchema: "siga_db",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
