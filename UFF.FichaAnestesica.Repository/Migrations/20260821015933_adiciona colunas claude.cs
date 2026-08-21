using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace UFF.FichaAnestesica.Infra.Migrations
{
    /// <inheritdoc />
    public partial class adicionacolunasclaude : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "f_k_pre_anesthesia_records__users_first_anesthesiologist_id",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropForeignKey(
                name: "f_k_pre_anesthesia_records__users_second_anesthesiologist_id",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropIndex(
                name: "IX_pre_anesthesia_records_first_anesthesiologist_id1",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropIndex(
                name: "IX_pre_anesthesia_records_second_anesthesiologist_id1",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "activity_score",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "airway_device_number",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "anesthesia_end_time",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "anesthesia_equipment_checked",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "anesthesia_start_time",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "anesthetic_consent_signed",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "assistant",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "blood_pressure",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "bronchofibroscopy",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "circulation_score",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "clinical_discharge_condition",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "co2_absorber_circuit",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "consciousness_score",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "destination",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "difficult_venous_puncture",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "first_anesthesiologist_id",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "first_anesthesiologist_id1",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "general_anesthesia",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "has_pain",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "laryngoscopy",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "nasal_tube",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "oral_tube",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "other_airway_technique",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "other_airway_type_description",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "oxygen_saturation",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "oxygen_saturation_score",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "oxygen_supplementation",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "patient_identified_before_induction",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "plexus_block_performed",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "post_operative_diagnosis",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "pre_anesthetic_medication",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "prophylactic_antibiotic_used",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "record_date",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "respiration_mode",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "respiration_score",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "respiratory_rate",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "retrograde_technique",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "room_entry_time",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "second_anesthesiologist_id",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "second_anesthesiologist_id1",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "sedation_performed",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "spinal_block_performed",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "surgeon",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "surgery_end_time",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "surgery_performed",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "surgical_position",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "total_aldrete_kroulik_score",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "tracheostomy",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "venous_access_location",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.RenameColumn(
                name: "video_laryngoscopy",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                newName: "not_cleared");

            migrationBuilder.RenameColumn(
                name: "venous_access_type",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                newName: "anesthesia_record_id");

            migrationBuilder.RenameColumn(
                name: "uses_cushions",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                newName: "is_emergency");

            migrationBuilder.RenameColumn(
                name: "safety_observations",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                newName: "urinalysis");

            migrationBuilder.RenameColumn(
                name: "intubation_difficulty",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                newName: "upper_incisor_length");

            migrationBuilder.RenameColumn(
                name: "controlled_ventilation_mode",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                newName: "thyromental_distance");

            migrationBuilder.RenameColumn(
                name: "airway_type",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                newName: "systolic_blood_pressure");

            migrationBuilder.RenameColumn(
                name: "airway_device_type",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                newName: "sternomental_distance");

            migrationBuilder.AlterColumn<decimal>(
                name: "weight_kg",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "numeric(6,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(6,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "temperature",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "numeric(5,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(5,2)");

            migrationBuilder.AlterColumn<string>(
                name: "pre_operative_diagnosis",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "asa_classification",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<List<string>>(
                name: "airway_mucosa",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "text[]",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "airway_observations",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "alcohol_grams_per_day",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "alcohol_use",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "allergy_other_description",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "allergy_reaction_type",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<List<string>>(
                name: "allergy_substances",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "text[]",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "anesthetic_history",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "aptt",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "numeric(10,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "bmi",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "numeric(5,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "chest_x_ray",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "comorbidities_other_description",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<List<string>>(
                name: "conduct_actions",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "text[]",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "conduct_notes",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "consultation_date",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "creatinine",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "numeric(10,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "dentition",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "diastolic_blood_pressure",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "difficult_intubation_prediction",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<List<string>>(
                name: "drug_types",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "text[]",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "drugs_other_description",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ecg",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "echocardiogram",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "family_history",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "fasting_liquids_hours",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "numeric(4,1)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "fasting_solids_hours",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "numeric(4,1)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "glucose",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "numeric(10,2)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "has_allergy",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "heart_rate",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "height_cm",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "numeric(5,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "hematocrit",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "numeric(10,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "hemoglobin",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "numeric(10,2)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "illicit_drug_use",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "incisor_relation",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "inter_incisor_distance",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "laterality",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "leukocytes",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "numeric(10,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "liver_function_tests",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "mallampati_class",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "mandible_protrusion",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "mandibular_space_compliance",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "neck_extension",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "neck_flexion",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "neck_length",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "neck_width",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "not_cleared_reason",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "other_imaging",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "palate",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "platelets",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "numeric(10,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "potassium",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "numeric(10,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "pregnancy_test",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "procedure_observation",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "pulmonary_function_test",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "signed_at",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "timestamptz",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "signed_by_name",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "signed_by_professional_id",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "smoker",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "smoking_load",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "sodium",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "numeric(10,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "spo2",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "tap_inr",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "numeric(10,2)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "thoracic_cage_abnormality",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "thoracic_cage_abnormality_description",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tp",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "urea",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "numeric(10,2)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "uses_medication",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "boolean",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "pre_anesthesia_comorbidities",
                schema: "siga_db",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    group_key = table.Column<string>(type: "varchar(50)", nullable: false),
                    findings = table.Column<List<string>>(type: "text[]", nullable: false),
                    other_description = table.Column<string>(type: "text", nullable: true),
                    observations = table.Column<string>(type: "text", nullable: true),
                    pre_anesthesia_record_id = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    last_update = table.Column<DateTime>(type: "timestamptz", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pre_anesthesia_comorbidities", x => x.id);
                    table.ForeignKey(
                        name: "fk_pre_anesthesia_comorbidities_pre_anesthesia_record",
                        column: x => x.pre_anesthesia_record_id,
                        principalSchema: "siga_db",
                        principalTable: "pre_anesthesia_records",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pre_anesthesia_medications",
                schema: "siga_db",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    dose = table.Column<string>(type: "text", nullable: true),
                    route = table.Column<string>(type: "text", nullable: true),
                    frequency = table.Column<string>(type: "text", nullable: true),
                    pre_anesthesia_record_id = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    last_update = table.Column<DateTime>(type: "timestamptz", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pre_anesthesia_medications", x => x.id);
                    table.ForeignKey(
                        name: "fk_pre_anesthesia_medications_pre_anesthesia_record",
                        column: x => x.pre_anesthesia_record_id,
                        principalSchema: "siga_db",
                        principalTable: "pre_anesthesia_records",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pre_anesthesia_physical_exam_areas",
                schema: "siga_db",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    area_key = table.Column<string>(type: "varchar(50)", nullable: false),
                    findings = table.Column<List<string>>(type: "text[]", nullable: false),
                    other_description = table.Column<string>(type: "text", nullable: true),
                    observations = table.Column<string>(type: "text", nullable: true),
                    pre_anesthesia_record_id = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    last_update = table.Column<DateTime>(type: "timestamptz", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pre_anesthesia_physical_exam_areas", x => x.id);
                    table.ForeignKey(
                        name: "fk_pre_anesthesia_physical_exam_areas_pre_anesthesia_record",
                        column: x => x.pre_anesthesia_record_id,
                        principalSchema: "siga_db",
                        principalTable: "pre_anesthesia_records",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pre_anesthesia_reports",
                schema: "siga_db",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    specialty = table.Column<int>(type: "integer", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    pre_anesthesia_record_id = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    last_update = table.Column<DateTime>(type: "timestamptz", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pre_anesthesia_reports", x => x.id);
                    table.ForeignKey(
                        name: "fk_pre_anesthesia_reports_pre_anesthesia_record",
                        column: x => x.pre_anesthesia_record_id,
                        principalSchema: "siga_db",
                        principalTable: "pre_anesthesia_records",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pre_anesthesia_surgeries",
                schema: "siga_db",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    is_primary = table.Column<bool>(type: "boolean", nullable: false),
                    pre_anesthesia_record_id = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    last_update = table.Column<DateTime>(type: "timestamptz", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pre_anesthesia_surgeries", x => x.id);
                    table.ForeignKey(
                        name: "fk_pre_anesthesia_surgeries_pre_anesthesia_record",
                        column: x => x.pre_anesthesia_record_id,
                        principalSchema: "siga_db",
                        principalTable: "pre_anesthesia_records",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_pre_anesthesia_records_anesthesia_record_id",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                column: "anesthesia_record_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_pre_anesthesia_records_signed_by_professional_id",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                column: "signed_by_professional_id");

            migrationBuilder.CreateIndex(
                name: "IX_pre_anesthesia_comorbidities_pre_anesthesia_record_id",
                schema: "siga_db",
                table: "pre_anesthesia_comorbidities",
                column: "pre_anesthesia_record_id");

            migrationBuilder.CreateIndex(
                name: "IX_pre_anesthesia_medications_pre_anesthesia_record_id",
                schema: "siga_db",
                table: "pre_anesthesia_medications",
                column: "pre_anesthesia_record_id");

            migrationBuilder.CreateIndex(
                name: "IX_pre_anesthesia_physical_exam_areas_pre_anesthesia_record_id",
                schema: "siga_db",
                table: "pre_anesthesia_physical_exam_areas",
                column: "pre_anesthesia_record_id");

            migrationBuilder.CreateIndex(
                name: "IX_pre_anesthesia_reports_pre_anesthesia_record_id",
                schema: "siga_db",
                table: "pre_anesthesia_reports",
                column: "pre_anesthesia_record_id");

            migrationBuilder.CreateIndex(
                name: "IX_pre_anesthesia_surgeries_pre_anesthesia_record_id",
                schema: "siga_db",
                table: "pre_anesthesia_surgeries",
                column: "pre_anesthesia_record_id");

            migrationBuilder.AddForeignKey(
                name: "fk_pre_anesthesia_records_anesthesia_record",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                column: "anesthesia_record_id",
                principalSchema: "siga_db",
                principalTable: "anesthesia_records",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_pre_anesthesia_records_signed_by_professional",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                column: "signed_by_professional_id",
                principalSchema: "siga_db",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_pre_anesthesia_records_anesthesia_record",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropForeignKey(
                name: "fk_pre_anesthesia_records_signed_by_professional",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropTable(
                name: "pre_anesthesia_comorbidities",
                schema: "siga_db");

            migrationBuilder.DropTable(
                name: "pre_anesthesia_medications",
                schema: "siga_db");

            migrationBuilder.DropTable(
                name: "pre_anesthesia_physical_exam_areas",
                schema: "siga_db");

            migrationBuilder.DropTable(
                name: "pre_anesthesia_reports",
                schema: "siga_db");

            migrationBuilder.DropTable(
                name: "pre_anesthesia_surgeries",
                schema: "siga_db");

            migrationBuilder.DropIndex(
                name: "IX_pre_anesthesia_records_anesthesia_record_id",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropIndex(
                name: "IX_pre_anesthesia_records_signed_by_professional_id",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "airway_mucosa",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "airway_observations",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "alcohol_grams_per_day",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "alcohol_use",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "allergy_other_description",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "allergy_reaction_type",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "allergy_substances",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "anesthetic_history",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "aptt",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "bmi",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "chest_x_ray",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "comorbidities_other_description",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "conduct_actions",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "conduct_notes",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "consultation_date",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "creatinine",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "dentition",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "diastolic_blood_pressure",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "difficult_intubation_prediction",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "drug_types",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "drugs_other_description",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "ecg",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "echocardiogram",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "family_history",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "fasting_liquids_hours",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "fasting_solids_hours",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "glucose",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "has_allergy",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "heart_rate",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "height_cm",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "hematocrit",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "hemoglobin",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "illicit_drug_use",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "incisor_relation",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "inter_incisor_distance",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "laterality",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "leukocytes",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "liver_function_tests",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "mallampati_class",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "mandible_protrusion",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "mandibular_space_compliance",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "neck_extension",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "neck_flexion",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "neck_length",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "neck_width",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "not_cleared_reason",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "other_imaging",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "palate",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "platelets",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "potassium",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "pregnancy_test",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "procedure_observation",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "pulmonary_function_test",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "signed_at",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "signed_by_name",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "signed_by_professional_id",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "smoker",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "smoking_load",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "sodium",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "spo2",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "tap_inr",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "thoracic_cage_abnormality",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "thoracic_cage_abnormality_description",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "tp",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "urea",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.DropColumn(
                name: "uses_medication",
                schema: "siga_db",
                table: "pre_anesthesia_records");

            migrationBuilder.RenameColumn(
                name: "urinalysis",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                newName: "safety_observations");

            migrationBuilder.RenameColumn(
                name: "upper_incisor_length",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                newName: "intubation_difficulty");

            migrationBuilder.RenameColumn(
                name: "thyromental_distance",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                newName: "controlled_ventilation_mode");

            migrationBuilder.RenameColumn(
                name: "systolic_blood_pressure",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                newName: "airway_type");

            migrationBuilder.RenameColumn(
                name: "sternomental_distance",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                newName: "airway_device_type");

            migrationBuilder.RenameColumn(
                name: "not_cleared",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                newName: "video_laryngoscopy");

            migrationBuilder.RenameColumn(
                name: "is_emergency",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                newName: "uses_cushions");

            migrationBuilder.RenameColumn(
                name: "anesthesia_record_id",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                newName: "venous_access_type");

            migrationBuilder.AlterColumn<decimal>(
                name: "weight_kg",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "numeric(6,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric(6,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "temperature",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "numeric(5,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric(5,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "pre_operative_diagnosis",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "asa_classification",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "activity_score",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "airway_device_number",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "varchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<TimeOnly>(
                name: "anesthesia_end_time",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<bool>(
                name: "anesthesia_equipment_checked",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<TimeOnly>(
                name: "anesthesia_start_time",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<bool>(
                name: "anesthetic_consent_signed",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "assistant",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "varchar(150)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "blood_pressure",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "varchar(20)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "bronchofibroscopy",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "circulation_score",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "clinical_discharge_condition",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "co2_absorber_circuit",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "consciousness_score",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "destination",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "difficult_venous_puncture",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "first_anesthesiologist_id",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "first_anesthesiologist_id1",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "general_anesthesia",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "has_pain",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "laryngoscopy",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "nasal_tube",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "oral_tube",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "other_airway_technique",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "varchar(200)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "other_airway_type_description",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "varchar(200)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "oxygen_saturation",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "oxygen_saturation_score",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "oxygen_supplementation",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "patient_identified_before_induction",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "plexus_block_performed",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "post_operative_diagnosis",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "pre_anesthetic_medication",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "prophylactic_antibiotic_used",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateOnly>(
                name: "record_date",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<int>(
                name: "respiration_mode",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "respiration_score",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "respiratory_rate",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "retrograde_technique",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<TimeOnly>(
                name: "room_entry_time",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<int>(
                name: "second_anesthesiologist_id",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "second_anesthesiologist_id1",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "sedation_performed",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "spinal_block_performed",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "surgeon",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "varchar(150)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<TimeOnly>(
                name: "surgery_end_time",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<string>(
                name: "surgery_performed",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "surgical_position",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "total_aldrete_kroulik_score",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "tracheostomy",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "venous_access_location",
                schema: "siga_db",
                table: "pre_anesthesia_records",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "");

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
    }
}
