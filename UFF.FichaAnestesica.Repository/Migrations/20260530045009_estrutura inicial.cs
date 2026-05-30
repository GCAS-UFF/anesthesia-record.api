using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace UFF.FichaAnestesica.Infra.Migrations
{
    /// <inheritdoc />
    public partial class estruturainicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "siga_db");

            migrationBuilder.CreateTable(
                name: "drugs",
                schema: "siga_db",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    external_id = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "varchar(150)", nullable: false),
                    default_presentation = table.Column<string>(type: "varchar(150)", nullable: false),
                    default_unit = table.Column<string>(type: "varchar(150)", nullable: false),
                    category = table.Column<int>(type: "integer", nullable: false),
                    active = table.Column<bool>(type: "boolean", nullable: false),
                    last_sync_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
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
                name: "users",
                schema: "siga_db",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    external_id = table.Column<string>(type: "varchar(50)", nullable: false),
                    name = table.Column<string>(type: "varchar(150)", nullable: false),
                    registration = table.Column<string>(type: "varchar(50)", nullable: false),
                    sector = table.Column<string>(type: "varchar(100)", nullable: true),
                    login = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "varchar(150)", nullable: false),
                    can_login = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    medical_specialty = table.Column<int>(type: "integer", nullable: false),
                    last_login_at = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    last_sync_at = table.Column<DateTime>(type: "timestamptz", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    last_update = table.Column<DateTime>(type: "timestamptz", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
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
                    first_anesthesiologist_id = table.Column<int>(type: "integer", nullable: true),
                    second_anesthesiologist_id = table.Column<int>(type: "integer", nullable: true),
                    surgeon_id = table.Column<int>(type: "integer", nullable: true),
                    assistant_id = table.Column<int>(type: "integer", nullable: true),
                    patient_id = table.Column<string>(type: "varchar(100)", nullable: false),
                    record_date = table.Column<DateOnly>(type: "date", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    last_update = table.Column<DateTime>(type: "timestamptz", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_anesthesia_records", x => x.id);
                    table.ForeignKey(
                        name: "f_k_anesthesia_records__users_assistant_id",
                        column: x => x.assistant_id,
                        principalSchema: "siga_db",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "f_k_anesthesia_records__users_first_anesthesiologist_id",
                        column: x => x.first_anesthesiologist_id,
                        principalSchema: "siga_db",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "f_k_anesthesia_records__users_second_anesthesiologist_id",
                        column: x => x.second_anesthesiologist_id,
                        principalSchema: "siga_db",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "f_k_anesthesia_records__users_surgeon_id",
                        column: x => x.surgeon_id,
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
                    last_update = table.Column<DateTime>(type: "timestamptz", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pre_anesthesia_records", x => x.id);
                    table.ForeignKey(
                        name: "f_k_pre_anesthesia_records__users_first_anesthesiologist_id",
                        column: x => x.first_anesthesiologist_id1,
                        principalSchema: "siga_db",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "f_k_pre_anesthesia_records__users_second_anesthesiologist_id",
                        column: x => x.second_anesthesiologist_id1,
                        principalSchema: "siga_db",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "IX_anesthesia_records_assistant_id",
                schema: "siga_db",
                table: "anesthesia_records",
                column: "assistant_id");

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
                name: "IX_anesthesia_records_surgeon_id",
                schema: "siga_db",
                table: "anesthesia_records",
                column: "surgeon_id");

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
                name: "IX_drugs_description",
                schema: "siga_db",
                table: "drugs",
                column: "description",
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "administered_agents",
                schema: "siga_db");

            migrationBuilder.DropTable(
                name: "anesthesia_records",
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
                name: "pre_anesthesia_records",
                schema: "siga_db");

            migrationBuilder.DropTable(
                name: "drugs",
                schema: "siga_db");

            migrationBuilder.DropTable(
                name: "vital_sign_records",
                schema: "siga_db");

            migrationBuilder.DropTable(
                name: "users",
                schema: "siga_db");

            migrationBuilder.DropTable(
                name: "monitoring_records",
                schema: "siga_db");
        }
    }
}
