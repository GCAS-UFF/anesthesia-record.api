using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UFF.FichaAnestesica.Infra.Migrations
{
    /// <inheritdoc />
    public partial class tornacamposdafichaanestésicanullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "weight_kg",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "numeric(6,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(6,2)");

            migrationBuilder.AlterColumn<bool>(
                name: "video_laryngoscopy",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<int>(
                name: "venous_access_type",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "venous_access_location",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "varchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)");

            migrationBuilder.AlterColumn<bool>(
                name: "uses_cushions",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<bool>(
                name: "tracheostomy",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<int>(
                name: "total_aldrete_kroulik_score",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<decimal>(
                name: "temperature",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "numeric(5,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(5,2)");

            migrationBuilder.AlterColumn<int>(
                name: "surgical_position",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "surgery_performed",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "surgery_end_time",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "time",
                nullable: true,
                oldClrType: typeof(TimeOnly),
                oldType: "time");

            migrationBuilder.AlterColumn<bool>(
                name: "spinal_block_performed",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<bool>(
                name: "sedation_performed",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "room_entry_time",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "time",
                nullable: true,
                oldClrType: typeof(TimeOnly),
                oldType: "time");

            migrationBuilder.AlterColumn<bool>(
                name: "retrograde_technique",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<int>(
                name: "respiratory_rate",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "respiration_score",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "respiration_mode",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<bool>(
                name: "prophylactic_antibiotic_used",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<string>(
                name: "pre_operative_diagnosis",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<bool>(
                name: "pre_anesthetic_medication",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<string>(
                name: "post_operative_diagnosis",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<bool>(
                name: "plexus_block_performed",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<bool>(
                name: "patient_identified_before_induction",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<bool>(
                name: "oxygen_supplementation",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<int>(
                name: "oxygen_saturation_score",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "oxygen_saturation",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<bool>(
                name: "oral_tube",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<bool>(
                name: "nasal_tube",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<bool>(
                name: "laryngoscopy",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<bool>(
                name: "has_pain",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<bool>(
                name: "general_anesthesia",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<bool>(
                name: "difficult_venous_puncture",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<int>(
                name: "destination",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "consciousness_score",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<bool>(
                name: "co2_absorber_circuit",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<int>(
                name: "clinical_discharge_condition",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "circulation_score",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<bool>(
                name: "bronchofibroscopy",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<string>(
                name: "blood_pressure",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "varchar(20)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)");

            migrationBuilder.AlterColumn<int>(
                name: "asa_classification",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<bool>(
                name: "anesthetic_consent_signed",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "anesthesia_start_time",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "time",
                nullable: true,
                oldClrType: typeof(TimeOnly),
                oldType: "time");

            migrationBuilder.AlterColumn<bool>(
                name: "anesthesia_equipment_checked",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "anesthesia_end_time",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "time",
                nullable: true,
                oldClrType: typeof(TimeOnly),
                oldType: "time");

            migrationBuilder.AlterColumn<int>(
                name: "activity_score",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "weight_kg",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "numeric(6,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric(6,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "video_laryngoscopy",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "venous_access_type",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "venous_access_location",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "uses_cushions",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "tracheostomy",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "total_aldrete_kroulik_score",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "temperature",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "numeric(5,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric(5,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "surgical_position",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "surgery_performed",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "surgery_end_time",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0),
                oldClrType: typeof(TimeOnly),
                oldType: "time",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "spinal_block_performed",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "sedation_performed",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "room_entry_time",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0),
                oldClrType: typeof(TimeOnly),
                oldType: "time",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "retrograde_technique",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "respiratory_rate",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "respiration_score",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "respiration_mode",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "prophylactic_antibiotic_used",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "pre_operative_diagnosis",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "pre_anesthetic_medication",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "post_operative_diagnosis",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "plexus_block_performed",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "patient_identified_before_induction",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "oxygen_supplementation",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "oxygen_saturation_score",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "oxygen_saturation",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "oral_tube",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "nasal_tube",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "laryngoscopy",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "has_pain",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "general_anesthesia",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "difficult_venous_puncture",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "destination",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "consciousness_score",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "co2_absorber_circuit",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "clinical_discharge_condition",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "circulation_score",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "bronchofibroscopy",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "blood_pressure",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "varchar(20)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "asa_classification",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "anesthetic_consent_signed",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "anesthesia_start_time",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0),
                oldClrType: typeof(TimeOnly),
                oldType: "time",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "anesthesia_equipment_checked",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "anesthesia_end_time",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0),
                oldClrType: typeof(TimeOnly),
                oldType: "time",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "activity_score",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }
    }
}
