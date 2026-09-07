using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UFF.FichaAnestesica.Infra.Migrations
{
    /// <inheritdoc />
    public partial class MakeMonitoringStartedAtNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "started_at",
                schema: "siga_db",
                table: "monitoring_records",
                type: "timestamptz",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamptz");

            // Limpeza de dados legados: registros criados antes desta correção podiam ter
            // started_at gravado como DateTime.MinValue, que o Npgsql persiste como
            // "-infinity". Esse valor nunca representou um início de anestesia real —
            // normaliza para NULL (monitorização "ainda não iniciada").
            migrationBuilder.Sql(
                "UPDATE siga_db.monitoring_records SET started_at = NULL WHERE started_at = '-infinity';");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "started_at",
                schema: "siga_db",
                table: "monitoring_records",
                type: "timestamptz",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamptz",
                oldNullable: true);
        }
    }
}
