using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UFF.FichaAnestesica.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddPatientAndSurgicalCaseEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "patient",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    external_id_huap = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    medical_record = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    birth_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    gender = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    weight_kg = table.Column<float>(type: "real", nullable: true),
                    height_cm = table.Column<float>(type: "real", nullable: true),
                    registering_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    last_update = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patient", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_name = table.Column<string>(type: "text", nullable: false),
                    pass_word = table.Column<string>(type: "text", nullable: false),
                    registering_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    last_update = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "surgical_case",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    external_id_huap = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    patient_id = table.Column<Guid>(type: "uuid", nullable: false),
                    surgery_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    proposed_procedure = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    room = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    bed = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    status = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false, defaultValue: "waiting"),
                    surgeon = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    specialty = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    registering_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    last_update = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_surgical_case", x => x.id);
                    table.ForeignKey(
                        name: "f_k_surgical_case_patient_patient_id",
                        column: x => x.patient_id,
                        principalTable: "patient",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_surgical_case_patient_id",
                table: "surgical_case",
                column: "patient_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "surgical_case");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "patient");
        }
    }
}
