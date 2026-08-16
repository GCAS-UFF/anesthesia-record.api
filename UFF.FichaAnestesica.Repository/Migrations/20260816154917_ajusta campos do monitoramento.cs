using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UFF.FichaAnestesica.Infra.Migrations
{
    /// <inheritdoc />
    public partial class ajustacamposdomonitoramento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_administered_agents_timestamp",
                schema: "siga_db",
                table: "administered_agents");

            migrationBuilder.DropColumn(
                name: "name",
                schema: "siga_db",
                table: "fluid_balances");

            migrationBuilder.DropColumn(
                name: "description",
                schema: "siga_db",
                table: "clinical_events");

            migrationBuilder.DropColumn(
                name: "timestamp",
                schema: "siga_db",
                table: "administered_agents");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "time",
                schema: "siga_db",
                table: "vital_sign_records",
                type: "time without time zone",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            // Conversão de position de varchar para integer
            migrationBuilder.Sql("""
                ALTER TABLE siga_db.patient_positions
                ALTER COLUMN "position" TYPE integer
                USING "position"::integer;
            """);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "time",
                schema: "siga_db",
                table: "patient_positions",
                type: "time without time zone",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "time",
                schema: "siga_db",
                table: "fluid_balances",
                type: "time without time zone",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "time",
                schema: "siga_db",
                table: "clinical_events",
                type: "time without time zone",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<DateTime>(
                name: "date",
                schema: "siga_db",
                table: "administered_agents",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "time",
                schema: "siga_db",
                table: "administered_agents",
                type: "time without time zone",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.CreateIndex(
                name: "IX_administered_agents_date",
                schema: "siga_db",
                table: "administered_agents",
                column: "date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_administered_agents_date",
                schema: "siga_db",
                table: "administered_agents");

            migrationBuilder.DropColumn(
                name: "time",
                schema: "siga_db",
                table: "vital_sign_records");

            migrationBuilder.DropColumn(
                name: "time",
                schema: "siga_db",
                table: "patient_positions");

            migrationBuilder.DropColumn(
                name: "time",
                schema: "siga_db",
                table: "fluid_balances");

            migrationBuilder.DropColumn(
                name: "time",
                schema: "siga_db",
                table: "clinical_events");

            migrationBuilder.DropColumn(
                name: "date",
                schema: "siga_db",
                table: "administered_agents");

            migrationBuilder.DropColumn(
                name: "time",
                schema: "siga_db",
                table: "administered_agents");

            // Conversão de position de integer para varchar
            migrationBuilder.Sql("""
                ALTER TABLE siga_db.patient_positions
                ALTER COLUMN "position" TYPE character varying(200)
                USING "position"::character varying;
            """);

            migrationBuilder.AddColumn<string>(
                name: "name",
                schema: "siga_db",
                table: "fluid_balances",
                type: "varchar(150)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "description",
                schema: "siga_db",
                table: "clinical_events",
                type: "varchar(500)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "timestamp",
                schema: "siga_db",
                table: "administered_agents",
                type: "timestamptz",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_administered_agents_timestamp",
                schema: "siga_db",
                table: "administered_agents",
                column: "timestamp");
        }
    }
}