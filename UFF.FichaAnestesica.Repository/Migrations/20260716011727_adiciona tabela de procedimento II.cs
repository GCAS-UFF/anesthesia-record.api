using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UFF.FichaAnestesica.Infra.Migrations
{
    /// <inheritdoc />
    public partial class adicionatabeladeprocedimentoII : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "f_k_anesthesia_record_procedure__procedure_procedure_id",
                schema: "siga_db",
                table: "anesthesia_record_procedures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_procedure",
                schema: "siga_db",
                table: "procedure");

            migrationBuilder.RenameTable(
                name: "procedure",
                schema: "siga_db",
                newName: "procedures",
                newSchema: "siga_db");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                schema: "siga_db",
                table: "anesthesia_record_procedures",
                type: "timestamptz",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_sync_at",
                schema: "siga_db",
                table: "procedures",
                type: "timestamptz",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "external_id",
                schema: "siga_db",
                table: "procedures",
                type: "varchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                schema: "siga_db",
                table: "procedures",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                schema: "siga_db",
                table: "procedures",
                type: "timestamptz",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "code",
                schema: "siga_db",
                table: "procedures",
                type: "varchar(30)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "cid",
                schema: "siga_db",
                table: "procedures",
                type: "varchar(20)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_procedures",
                schema: "siga_db",
                table: "procedures",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_procedures_code",
                schema: "siga_db",
                table: "procedures",
                column: "code");

            migrationBuilder.CreateIndex(
                name: "IX_procedures_external_id",
                schema: "siga_db",
                table: "procedures",
                column: "external_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "f_k_anesthesia_record_procedure__procedures_procedure_id",
                schema: "siga_db",
                table: "anesthesia_record_procedures",
                column: "procedure_id",
                principalSchema: "siga_db",
                principalTable: "procedures",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "f_k_anesthesia_record_procedure__procedures_procedure_id",
                schema: "siga_db",
                table: "anesthesia_record_procedures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_procedures",
                schema: "siga_db",
                table: "procedures");

            migrationBuilder.DropIndex(
                name: "IX_procedures_code",
                schema: "siga_db",
                table: "procedures");

            migrationBuilder.DropIndex(
                name: "IX_procedures_external_id",
                schema: "siga_db",
                table: "procedures");

            migrationBuilder.RenameTable(
                name: "procedures",
                schema: "siga_db",
                newName: "procedure",
                newSchema: "siga_db");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                schema: "siga_db",
                table: "anesthesia_record_procedures",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamptz");

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_sync_at",
                schema: "siga_db",
                table: "procedure",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamptz",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "external_id",
                schema: "siga_db",
                table: "procedure",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                schema: "siga_db",
                table: "procedure",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                schema: "siga_db",
                table: "procedure",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamptz");

            migrationBuilder.AlterColumn<string>(
                name: "code",
                schema: "siga_db",
                table: "procedure",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(30)");

            migrationBuilder.AlterColumn<string>(
                name: "cid",
                schema: "siga_db",
                table: "procedure",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_procedure",
                schema: "siga_db",
                table: "procedure",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "f_k_anesthesia_record_procedure__procedure_procedure_id",
                schema: "siga_db",
                table: "anesthesia_record_procedures",
                column: "procedure_id",
                principalSchema: "siga_db",
                principalTable: "procedure",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
