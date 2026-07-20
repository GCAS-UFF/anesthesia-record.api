using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UFF.FichaAnestesica.Infra.Migrations
{
    /// <inheritdoc />
    public partial class adicionacolunasausentes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeOnly>(
                name: "time",
                schema: "siga_db",
                table: "procedures",
                type: "time without time zone",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<string>(
                name: "conduta",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "dor_b_p_s",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "dor_e_n_v",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "dor_p_a_i_n_a_d",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "dor_usou_b_p_s",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "dor_usou_e_n_v",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "dor_usou_p_a_i_n_a_d",
                schema: "siga_db",
                table: "anesthesia_records",
                type: "boolean",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "time",
                schema: "siga_db",
                table: "procedures");

            migrationBuilder.DropColumn(
                name: "conduta",
                schema: "siga_db",
                table: "anesthesia_records");

            migrationBuilder.DropColumn(
                name: "dor_b_p_s",
                schema: "siga_db",
                table: "anesthesia_records");

            migrationBuilder.DropColumn(
                name: "dor_e_n_v",
                schema: "siga_db",
                table: "anesthesia_records");

            migrationBuilder.DropColumn(
                name: "dor_p_a_i_n_a_d",
                schema: "siga_db",
                table: "anesthesia_records");

            migrationBuilder.DropColumn(
                name: "dor_usou_b_p_s",
                schema: "siga_db",
                table: "anesthesia_records");

            migrationBuilder.DropColumn(
                name: "dor_usou_e_n_v",
                schema: "siga_db",
                table: "anesthesia_records");

            migrationBuilder.DropColumn(
                name: "dor_usou_p_a_i_n_a_d",
                schema: "siga_db",
                table: "anesthesia_records");
        }
    }
}
