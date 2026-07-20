using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UFF.FichaAnestesica.Infra.Migrations
{
    /// <inheritdoc />
    public partial class ajustacolunahora : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "time",
                schema: "siga_db",
                table: "procedures");

            migrationBuilder.AddColumn<TimeOnly>(
                name: "time",
                schema: "siga_db",
                table: "anesthesia_record_procedures",
                type: "time without time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "time",
                schema: "siga_db",
                table: "anesthesia_record_procedures");

            migrationBuilder.AddColumn<TimeOnly>(
                name: "time",
                schema: "siga_db",
                table: "procedures",
                type: "time without time zone",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));
        }
    }
}
