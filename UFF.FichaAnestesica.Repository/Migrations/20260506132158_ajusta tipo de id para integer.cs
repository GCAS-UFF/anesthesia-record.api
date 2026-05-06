using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace UFF.FichaAnestesica.Infra.Migrations.SigaDbCtxMigrations
{
    /// <inheritdoc />
    public partial class ajustatipodeidparainteger : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "last_update",
                schema: "siga_db",
                table: "users",
                type: "timestamptz",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                schema: "siga_db",
                table: "users",
                type: "timestamptz",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp");

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_update",
                schema: "siga_db",
                table: "units",
                type: "timestamptz",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                schema: "siga_db",
                table: "units",
                type: "timestamptz",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp");

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_update",
                schema: "siga_db",
                table: "surgical_centers",
                type: "timestamptz",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                schema: "siga_db",
                table: "surgical_centers",
                type: "timestamptz",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp");

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_update",
                schema: "siga_db",
                table: "surgery_locations",
                type: "timestamptz",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                schema: "siga_db",
                table: "surgery_locations",
                type: "timestamptz",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp");

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_update",
                schema: "siga_db",
                table: "surgeries",
                type: "timestamptz",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                schema: "siga_db",
                table: "surgeries",
                type: "timestamptz",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp");

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_update",
                schema: "siga_db",
                table: "specialties",
                type: "timestamptz",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                schema: "siga_db",
                table: "specialties",
                type: "timestamptz",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp");

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_update",
                schema: "siga_db",
                table: "procedures",
                type: "timestamptz",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                schema: "siga_db",
                table: "procedures",
                type: "timestamptz",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_update",
                schema: "siga_db",
                table: "patients",
                type: "timestamptz",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                schema: "siga_db",
                table: "patients",
                type: "timestamptz",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp");

            migrationBuilder.AddColumn<string>(
                name: "external_id_huap",
                schema: "siga_db",
                table: "patients",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_update",
                schema: "siga_db",
                table: "current_locations",
                type: "timestamptz",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                schema: "siga_db",
                table: "current_locations",
                type: "timestamptz",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp");

            migrationBuilder.CreateTable(
                name: "SurgicalCase",
                schema: "siga_db",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ExternalIdHuap = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    PatientId = table.Column<int>(type: "integer", nullable: false),
                    SurgeryDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ProposedProcedure = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Room = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Bed = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Status = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false, defaultValue: "waiting"),
                    Surgeon = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Specialty = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurgicalCase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SurgicalCase_patients_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "siga_db",
                        principalTable: "patients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SurgicalCase_PatientId",
                schema: "siga_db",
                table: "SurgicalCase",
                column: "PatientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SurgicalCase",
                schema: "siga_db");

            migrationBuilder.DropColumn(
                name: "external_id_huap",
                schema: "siga_db",
                table: "patients");

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_update",
                schema: "siga_db",
                table: "users",
                type: "timestamp",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamptz");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                schema: "siga_db",
                table: "users",
                type: "timestamp",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamptz");

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_update",
                schema: "siga_db",
                table: "units",
                type: "timestamp",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamptz");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                schema: "siga_db",
                table: "units",
                type: "timestamp",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamptz");

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_update",
                schema: "siga_db",
                table: "surgical_centers",
                type: "timestamp",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamptz");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                schema: "siga_db",
                table: "surgical_centers",
                type: "timestamp",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamptz");

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_update",
                schema: "siga_db",
                table: "surgery_locations",
                type: "timestamp",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamptz");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                schema: "siga_db",
                table: "surgery_locations",
                type: "timestamp",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamptz");

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_update",
                schema: "siga_db",
                table: "surgeries",
                type: "timestamp",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamptz");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                schema: "siga_db",
                table: "surgeries",
                type: "timestamp",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamptz");

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_update",
                schema: "siga_db",
                table: "specialties",
                type: "timestamp",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamptz");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                schema: "siga_db",
                table: "specialties",
                type: "timestamp",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamptz");

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_update",
                schema: "siga_db",
                table: "procedures",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamptz");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                schema: "siga_db",
                table: "procedures",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamptz");

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_update",
                schema: "siga_db",
                table: "patients",
                type: "timestamp",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamptz");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                schema: "siga_db",
                table: "patients",
                type: "timestamp",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamptz");

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_update",
                schema: "siga_db",
                table: "current_locations",
                type: "timestamp",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamptz");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                schema: "siga_db",
                table: "current_locations",
                type: "timestamp",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamptz");
        }
    }
}
