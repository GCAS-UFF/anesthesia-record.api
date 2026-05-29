using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UFF.FichaAnestesica.Infra.Migrations
{
    /// <inheritdoc />
    public partial class hangfire : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                schema: "siga_db",
                table: "drugs",
                newName: "description");

            migrationBuilder.RenameIndex(
                name: "IX_drugs_name",
                schema: "siga_db",
                table: "drugs",
                newName: "IX_drugs_description");

            migrationBuilder.AddColumn<bool>(
                name: "active",
                schema: "siga_db",
                table: "drugs",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "external_id",
                schema: "siga_db",
                table: "drugs",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "last_sync_at",
                schema: "siga_db",
                table: "drugs",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "active",
                schema: "siga_db",
                table: "drugs");

            migrationBuilder.DropColumn(
                name: "external_id",
                schema: "siga_db",
                table: "drugs");

            migrationBuilder.DropColumn(
                name: "last_sync_at",
                schema: "siga_db",
                table: "drugs");

            migrationBuilder.RenameColumn(
                name: "description",
                schema: "siga_db",
                table: "drugs",
                newName: "name");

            migrationBuilder.RenameIndex(
                name: "IX_drugs_description",
                schema: "siga_db",
                table: "drugs",
                newName: "IX_drugs_name");
        }
    }
}
