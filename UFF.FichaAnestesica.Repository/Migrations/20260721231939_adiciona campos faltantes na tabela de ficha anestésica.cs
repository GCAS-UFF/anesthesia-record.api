using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace UFF.FichaAnestesica.Infra.Migrations
{
    /// <inheritdoc />
    public partial class adicionacamposfaltantesnatabeladefichaanestésica : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "airway_device_number",
                schema: "siga_db",
                table: "anesthesia_records");

            migrationBuilder.RenameColumn(
                name: "dor_usou_p_a_i_n_a_d",
                schema: "siga_db",
                table: "anesthesia_records",
                newName: "dor_usou_painad");

            migrationBuilder.RenameColumn(
                name: "dor_usou_e_n_v",
                schema: "siga_db",
                table: "anesthesia_records",
                newName: "dor_usou_env");

            migrationBuilder.RenameColumn(
                name: "dor_usou_b_p_s",
                schema: "siga_db",
                table: "anesthesia_records",
                newName: "dor_usou_bps");

            migrationBuilder.RenameColumn(
                name: "dor_p_a_i_n_a_d",
                schema: "siga_db",
                table: "anesthesia_records",
                newName: "dor_painad");

            migrationBuilder.RenameColumn(
                name: "dor_e_n_v",
                schema: "siga_db",
                table: "anesthesia_records",
                newName: "dor_env");

            migrationBuilder.RenameColumn(
                name: "dor_b_p_s",
                schema: "siga_db",
                table: "anesthesia_records",
                newName: "dor_bps");

            migrationBuilder.RenameColumn(
                name: "airway_device_type",
                schema: "siga_db",
                table: "anesthesia_records",
                newName: "puncture_position");

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "pre_anesthetic_medication_time",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "time",
                nullable: true,
                oldClrType: typeof(TimeOnly),
                oldType: "time without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "pre_anesthetic_medication_route",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "varchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "pre_anesthetic_medication_other_route",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "varchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "pre_anesthetic_medication_name",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "varchar(200)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "pre_anesthetic_medication_dose",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "varchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "airway_device_numbers",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "jsonb",
                nullable: true);

            migrationBuilder.AddColumn<TimeOnly>(
                name: "aldrete_evaluation_time",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "cuff",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "discharge_condition_other",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "varchar(200)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "has_other_airway_technique",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "has_oxygen_supplementation_other",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "iot",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "neurostimulator_used",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "other_surgical_position",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "varchar(200)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "other_venous_access",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "varchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "oxygen_supplementation_other",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "varchar(200)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "puncture_count",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "signature_date",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "spinal_catheter",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "spinal_opioid",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "boolean",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "anesthesia_record_airway_devices",
                schema: "siga_db",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    anesthesia_record_id = table.Column<int>(type: "integer", nullable: false),
                    device_type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_anesthesia_record_airway_devices", x => x.id);
                    table.ForeignKey(
                        name: "f_k_anesthesia_record_airway_devices_anesthesia_records_anesthe~",
                        column: x => x.anesthesia_record_id,
                        principalSchema: "siga_db",
                        principalTable: "anesthesia_records",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "anesthesia_record_oxygen_supplementations",
                schema: "siga_db",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    anesthesia_record_id = table.Column<int>(type: "integer", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_anesthesia_record_oxygen_supplementations", x => x.id);
                    table.ForeignKey(
                        name: "f_k_anesthesia_record_oxygen_supplementations_anesthesia_record~",
                        column: x => x.anesthesia_record_id,
                        principalSchema: "siga_db",
                        principalTable: "anesthesia_records",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "anesthesia_record_puncture_levels",
                schema: "siga_db",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    anesthesia_record_id = table.Column<int>(type: "integer", nullable: false),
                    level = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_anesthesia_record_puncture_levels", x => x.id);
                    table.ForeignKey(
                        name: "f_k_anesthesia_record_puncture_levels_anesthesia_records_anesth~",
                        column: x => x.anesthesia_record_id,
                        principalSchema: "siga_db",
                        principalTable: "anesthesia_records",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "anesthesia_record_stimulated_nerves",
                schema: "siga_db",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    anesthesia_record_id = table.Column<int>(type: "integer", nullable: false),
                    nerve = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_anesthesia_record_stimulated_nerves", x => x.id);
                    table.ForeignKey(
                        name: "f_k_anesthesia_record_stimulated_nerves_anesthesia_records_anes~",
                        column: x => x.anesthesia_record_id,
                        principalSchema: "siga_db",
                        principalTable: "anesthesia_records",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_anesthesia_record_airway_devices_anesthesia_record_id",
                schema: "siga_db",
                table: "anesthesia_record_airway_devices",
                column: "anesthesia_record_id");

            migrationBuilder.CreateIndex(
                name: "IX_anesthesia_record_oxygen_supplementations_anesthesia_record~",
                schema: "siga_db",
                table: "anesthesia_record_oxygen_supplementations",
                column: "anesthesia_record_id");

            migrationBuilder.CreateIndex(
                name: "IX_anesthesia_record_puncture_levels_anesthesia_record_id",
                schema: "siga_db",
                table: "anesthesia_record_puncture_levels",
                column: "anesthesia_record_id");

            migrationBuilder.CreateIndex(
                name: "IX_anesthesia_record_stimulated_nerves_anesthesia_record_id",
                schema: "siga_db",
                table: "anesthesia_record_stimulated_nerves",
                column: "anesthesia_record_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "anesthesia_record_airway_devices",
                schema: "siga_db");

            migrationBuilder.DropTable(
                name: "anesthesia_record_oxygen_supplementations",
                schema: "siga_db");

            migrationBuilder.DropTable(
                name: "anesthesia_record_puncture_levels",
                schema: "siga_db");

            migrationBuilder.DropTable(
                name: "anesthesia_record_stimulated_nerves",
                schema: "siga_db");

            migrationBuilder.DropColumn(
                name: "airway_device_numbers",
                schema: "siga_db",
                table: "anesthesia_records");

            migrationBuilder.DropColumn(
                name: "aldrete_evaluation_time",
                schema: "siga_db",
                table: "anesthesia_records");

            migrationBuilder.DropColumn(
                name: "cuff",
                schema: "siga_db",
                table: "anesthesia_records");

            migrationBuilder.DropColumn(
                name: "discharge_condition_other",
                schema: "siga_db",
                table: "anesthesia_records");

            migrationBuilder.DropColumn(
                name: "has_other_airway_technique",
                schema: "siga_db",
                table: "anesthesia_records");

            migrationBuilder.DropColumn(
                name: "has_oxygen_supplementation_other",
                schema: "siga_db",
                table: "anesthesia_records");

            migrationBuilder.DropColumn(
                name: "iot",
                schema: "siga_db",
                table: "anesthesia_records");

            migrationBuilder.DropColumn(
                name: "neurostimulator_used",
                schema: "siga_db",
                table: "anesthesia_records");

            migrationBuilder.DropColumn(
                name: "other_surgical_position",
                schema: "siga_db",
                table: "anesthesia_records");

            migrationBuilder.DropColumn(
                name: "other_venous_access",
                schema: "siga_db",
                table: "anesthesia_records");

            migrationBuilder.DropColumn(
                name: "oxygen_supplementation_other",
                schema: "siga_db",
                table: "anesthesia_records");

            migrationBuilder.DropColumn(
                name: "puncture_count",
                schema: "siga_db",
                table: "anesthesia_records");

            migrationBuilder.DropColumn(
                name: "signature_date",
                schema: "siga_db",
                table: "anesthesia_records");

            migrationBuilder.DropColumn(
                name: "spinal_catheter",
                schema: "siga_db",
                table: "anesthesia_records");

            migrationBuilder.DropColumn(
                name: "spinal_opioid",
                schema: "siga_db",
                table: "anesthesia_records");

            migrationBuilder.RenameColumn(
                name: "dor_usou_painad",
                schema: "siga_db",
                table: "anesthesia_records",
                newName: "dor_usou_p_a_i_n_a_d");

            migrationBuilder.RenameColumn(
                name: "dor_usou_env",
                schema: "siga_db",
                table: "anesthesia_records",
                newName: "dor_usou_e_n_v");

            migrationBuilder.RenameColumn(
                name: "dor_usou_bps",
                schema: "siga_db",
                table: "anesthesia_records",
                newName: "dor_usou_b_p_s");

            migrationBuilder.RenameColumn(
                name: "dor_painad",
                schema: "siga_db",
                table: "anesthesia_records",
                newName: "dor_p_a_i_n_a_d");

            migrationBuilder.RenameColumn(
                name: "dor_env",
                schema: "siga_db",
                table: "anesthesia_records",
                newName: "dor_e_n_v");

            migrationBuilder.RenameColumn(
                name: "dor_bps",
                schema: "siga_db",
                table: "anesthesia_records",
                newName: "dor_b_p_s");

            migrationBuilder.RenameColumn(
                name: "puncture_position",
                schema: "siga_db",
                table: "anesthesia_records",
                newName: "airway_device_type");

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "pre_anesthetic_medication_time",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "time without time zone",
                nullable: true,
                oldClrType: typeof(TimeOnly),
                oldType: "time",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "pre_anesthetic_medication_route",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "pre_anesthetic_medication_other_route",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "pre_anesthetic_medication_name",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "pre_anesthetic_medication_dose",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "airway_device_number",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "varchar(20)",
                nullable: true);
        }
    }
}
