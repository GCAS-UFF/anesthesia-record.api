using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace UFF.FichaAnestesica.Infra.Migrations
{
    /// <inheritdoc />
    public partial class adicionacontextoinicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "siga_db");

            migrationBuilder.CreateTable(
                name: "users",
                schema: "siga_db",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "varchar(150)", nullable: false),
                    registration = table.Column<string>(type: "varchar(50)", nullable: false),
                    sector = table.Column<string>(type: "varchar(100)", nullable: false),
                    email = table.Column<string>(type: "varchar(150)", nullable: false),
                    role = table.Column<string>(type: "varchar(100)", nullable: false),
                    can_login = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    last_update = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    last_login_at = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    last_sync_at = table.Column<DateTime>(type: "timestamptz", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "anesthesia_records",
                schema: "siga_db",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    patient_identified_before_induction = table.Column<bool>(type: "boolean", nullable: false),
                    anesthetic_consent_signed = table.Column<bool>(type: "boolean", nullable: false),
                    anesthesia_equipment_checked = table.Column<bool>(type: "boolean", nullable: false),
                    safety_observations = table.Column<string>(type: "text", nullable: true),
                    pre_anesthetic_medication = table.Column<bool>(type: "boolean", nullable: false),
                    prophylactic_antibiotic_used = table.Column<bool>(type: "boolean", nullable: false),
                    blood_pressure = table.Column<string>(type: "varchar(20)", nullable: false),
                    respiratory_rate = table.Column<int>(type: "integer", nullable: false),
                    temperature = table.Column<decimal>(type: "numeric(5,2)", nullable: false),
                    oxygen_saturation = table.Column<int>(type: "integer", nullable: false),
                    weight_kg = table.Column<decimal>(type: "numeric(6,2)", nullable: false),
                    asa_classification = table.Column<int>(type: "integer", nullable: false),
                    room_entry_time = table.Column<TimeOnly>(type: "time", nullable: false),
                    anesthesia_start_time = table.Column<TimeOnly>(type: "time", nullable: false),
                    surgery_end_time = table.Column<TimeOnly>(type: "time", nullable: false),
                    anesthesia_end_time = table.Column<TimeOnly>(type: "time", nullable: false),
                    surgeon = table.Column<string>(type: "varchar(150)", nullable: false),
                    assistant = table.Column<string>(type: "varchar(150)", nullable: false),
                    pre_operative_diagnosis = table.Column<string>(type: "text", nullable: false),
                    surgical_position = table.Column<int>(type: "integer", nullable: false),
                    uses_cushions = table.Column<bool>(type: "boolean", nullable: false),
                    venous_access_type = table.Column<int>(type: "integer", nullable: false),
                    venous_access_location = table.Column<string>(type: "varchar(100)", nullable: false),
                    difficult_venous_puncture = table.Column<bool>(type: "boolean", nullable: false),
                    general_anesthesia = table.Column<bool>(type: "boolean", nullable: false),
                    respiration_mode = table.Column<int>(type: "integer", nullable: false),
                    controlled_ventilation_mode = table.Column<int>(type: "integer", nullable: true),
                    co2_absorber_circuit = table.Column<bool>(type: "boolean", nullable: false),
                    airway_device_type = table.Column<int>(type: "integer", nullable: true),
                    airway_device_number = table.Column<string>(type: "varchar(20)", nullable: true),
                    oral_tube = table.Column<bool>(type: "boolean", nullable: false),
                    nasal_tube = table.Column<bool>(type: "boolean", nullable: false),
                    intubation_difficulty = table.Column<int>(type: "integer", nullable: true),
                    airway_type = table.Column<int>(type: "integer", nullable: true),
                    other_airway_type_description = table.Column<string>(type: "varchar(200)", nullable: true),
                    laryngoscopy = table.Column<bool>(type: "boolean", nullable: false),
                    retrograde_technique = table.Column<bool>(type: "boolean", nullable: false),
                    video_laryngoscopy = table.Column<bool>(type: "boolean", nullable: false),
                    bronchofibroscopy = table.Column<bool>(type: "boolean", nullable: false),
                    tracheostomy = table.Column<bool>(type: "boolean", nullable: false),
                    other_airway_technique = table.Column<string>(type: "varchar(200)", nullable: true),
                    spinal_block_performed = table.Column<bool>(type: "boolean", nullable: false),
                    sedation_performed = table.Column<bool>(type: "boolean", nullable: false),
                    oxygen_supplementation = table.Column<bool>(type: "boolean", nullable: false),
                    plexus_block_performed = table.Column<bool>(type: "boolean", nullable: false),
                    surgery_performed = table.Column<string>(type: "text", nullable: false),
                    post_operative_diagnosis = table.Column<string>(type: "text", nullable: false),
                    consciousness_score = table.Column<int>(type: "integer", nullable: false),
                    activity_score = table.Column<int>(type: "integer", nullable: false),
                    circulation_score = table.Column<int>(type: "integer", nullable: false),
                    respiration_score = table.Column<int>(type: "integer", nullable: false),
                    oxygen_saturation_score = table.Column<int>(type: "integer", nullable: false),
                    total_aldrete_kroulik_score = table.Column<int>(type: "integer", nullable: false),
                    clinical_discharge_condition = table.Column<int>(type: "integer", nullable: false),
                    destination = table.Column<int>(type: "integer", nullable: false),
                    has_pain = table.Column<bool>(type: "boolean", nullable: false),
                    first_anesthesiologist_id = table.Column<int>(type: "integer", nullable: false),
                    second_anesthesiologist_id = table.Column<int>(type: "integer", nullable: true),
                    patient_id = table.Column<string>(type: "varchar(100)", nullable: false),
                    record_date = table.Column<DateOnly>(type: "date", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    last_update = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    last_login_at = table.Column<DateTime>(type: "timestamptz", nullable: true),
                    last_sync_at = table.Column<DateTime>(type: "timestamptz", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_anesthesia_records", x => x.id);
                    table.ForeignKey(
                        name: "f_k_anesthesia_record__users_first_anesthesiologist_id",
                        column: x => x.first_anesthesiologist_id,
                        principalSchema: "siga_db",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "f_k_anesthesia_record__users_second_anesthesiologist_id",
                        column: x => x.second_anesthesiologist_id,
                        principalSchema: "siga_db",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "pre_anesthesia_records",
                schema: "siga_db",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    patient_identified_before_induction = table.Column<bool>(type: "boolean", nullable: false),
                    anesthetic_consent_signed = table.Column<bool>(type: "boolean", nullable: false),
                    anesthesia_equipment_checked = table.Column<bool>(type: "boolean", nullable: false),
                    safety_observations = table.Column<string>(type: "text", nullable: true),
                    pre_anesthetic_medication = table.Column<bool>(type: "boolean", nullable: false),
                    prophylactic_antibiotic_used = table.Column<bool>(type: "boolean", nullable: false),
                    blood_pressure = table.Column<string>(type: "varchar(20)", nullable: false),
                    respiratory_rate = table.Column<int>(type: "integer", nullable: false),
                    temperature = table.Column<decimal>(type: "numeric(5,2)", nullable: false),
                    oxygen_saturation = table.Column<int>(type: "integer", nullable: false),
                    weight_kg = table.Column<decimal>(type: "numeric(6,2)", nullable: false),
                    asa_classification = table.Column<int>(type: "integer", nullable: false),
                    room_entry_time = table.Column<TimeOnly>(type: "time", nullable: false),
                    anesthesia_start_time = table.Column<TimeOnly>(type: "time", nullable: false),
                    surgery_end_time = table.Column<TimeOnly>(type: "time", nullable: false),
                    anesthesia_end_time = table.Column<TimeOnly>(type: "time", nullable: false),
                    surgeon = table.Column<string>(type: "varchar(150)", nullable: false),
                    assistant = table.Column<string>(type: "varchar(150)", nullable: false),
                    pre_operative_diagnosis = table.Column<string>(type: "text", nullable: false),
                    surgical_position = table.Column<int>(type: "integer", nullable: false),
                    uses_cushions = table.Column<bool>(type: "boolean", nullable: false),
                    venous_access_type = table.Column<int>(type: "integer", nullable: false),
                    venous_access_location = table.Column<string>(type: "varchar(100)", nullable: false),
                    difficult_venous_puncture = table.Column<bool>(type: "boolean", nullable: false),
                    general_anesthesia = table.Column<bool>(type: "boolean", nullable: false),
                    respiration_mode = table.Column<int>(type: "integer", nullable: false),
                    controlled_ventilation_mode = table.Column<int>(type: "integer", nullable: true),
                    co2_absorber_circuit = table.Column<bool>(type: "boolean", nullable: false),
                    airway_device_type = table.Column<int>(type: "integer", nullable: true),
                    airway_device_number = table.Column<string>(type: "varchar(20)", nullable: true),
                    oral_tube = table.Column<bool>(type: "boolean", nullable: false),
                    nasal_tube = table.Column<bool>(type: "boolean", nullable: false),
                    intubation_difficulty = table.Column<int>(type: "integer", nullable: true),
                    airway_type = table.Column<int>(type: "integer", nullable: true),
                    other_airway_type_description = table.Column<string>(type: "varchar(200)", nullable: true),
                    laryngoscopy = table.Column<bool>(type: "boolean", nullable: false),
                    retrograde_technique = table.Column<bool>(type: "boolean", nullable: false),
                    video_laryngoscopy = table.Column<bool>(type: "boolean", nullable: false),
                    bronchofibroscopy = table.Column<bool>(type: "boolean", nullable: false),
                    tracheostomy = table.Column<bool>(type: "boolean", nullable: false),
                    other_airway_technique = table.Column<string>(type: "varchar(200)", nullable: true),
                    spinal_block_performed = table.Column<bool>(type: "boolean", nullable: false),
                    sedation_performed = table.Column<bool>(type: "boolean", nullable: false),
                    oxygen_supplementation = table.Column<bool>(type: "boolean", nullable: false),
                    plexus_block_performed = table.Column<bool>(type: "boolean", nullable: false),
                    surgery_performed = table.Column<string>(type: "text", nullable: false),
                    post_operative_diagnosis = table.Column<string>(type: "text", nullable: false),
                    consciousness_score = table.Column<int>(type: "integer", nullable: false),
                    activity_score = table.Column<int>(type: "integer", nullable: false),
                    circulation_score = table.Column<int>(type: "integer", nullable: false),
                    respiration_score = table.Column<int>(type: "integer", nullable: false),
                    oxygen_saturation_score = table.Column<int>(type: "integer", nullable: false),
                    total_aldrete_kroulik_score = table.Column<int>(type: "integer", nullable: false),
                    clinical_discharge_condition = table.Column<int>(type: "integer", nullable: false),
                    destination = table.Column<int>(type: "integer", nullable: false),
                    has_pain = table.Column<bool>(type: "boolean", nullable: false),
                    first_anesthesiologist_id = table.Column<int>(type: "integer", nullable: false),
                    first_anesthesiologist_id1 = table.Column<int>(type: "integer", nullable: false),
                    second_anesthesiologist_id = table.Column<int>(type: "integer", nullable: false),
                    second_anesthesiologist_id1 = table.Column<int>(type: "integer", nullable: false),
                    record_date = table.Column<DateOnly>(type: "date", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    last_update = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    last_login_at = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    last_sync_at = table.Column<DateTime>(type: "timestamptz", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pre_anesthesia_records", x => x.id);
                    table.ForeignKey(
                        name: "f_k_pre_anesthesia_record__users_first_anesthesiologist_id",
                        column: x => x.first_anesthesiologist_id1,
                        principalSchema: "siga_db",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "f_k_pre_anesthesia_record__users_second_anesthesiologist_id",
                        column: x => x.second_anesthesiologist_id1,
                        principalSchema: "siga_db",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_anesthesia_records_first_anesthesiologist_id",
                schema: "siga_db",
                table: "anesthesia_records",
                column: "first_anesthesiologist_id");

            migrationBuilder.CreateIndex(
                name: "IX_anesthesia_records_second_anesthesiologist_id",
                schema: "siga_db",
                table: "anesthesia_records",
                column: "second_anesthesiologist_id");

            migrationBuilder.CreateIndex(
                name: "IX_pre_anesthesia_records_first_anesthesiologist_id1",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                column: "first_anesthesiologist_id1");

            migrationBuilder.CreateIndex(
                name: "IX_pre_anesthesia_records_second_anesthesiologist_id1",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                column: "second_anesthesiologist_id1");

            migrationBuilder.CreateIndex(
                name: "IX_users_can_login",
                schema: "siga_db",
                table: "users",
                column: "can_login");

            migrationBuilder.CreateIndex(
                name: "IX_users_email",
                schema: "siga_db",
                table: "users",
                column: "email");

            migrationBuilder.CreateIndex(
                name: "IX_users_registration",
                schema: "siga_db",
                table: "users",
                column: "registration",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_status",
                schema: "siga_db",
                table: "users",
                column: "status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "anesthesia_records",
                schema: "siga_db");

            migrationBuilder.DropTable(
                name: "pre_anesthesia_records",
                schema: "siga_db");

            migrationBuilder.DropTable(
                name: "users",
                schema: "siga_db");
        }
    }
}
