using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace UFF.FichaAnestesica.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Incluitabelas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "siga_db");

            migrationBuilder.CreateTable(
                name: "patients",
                schema: "siga_db",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    patient_id = table.Column<string>(type: "varchar(50)", nullable: false),
                    medical_record_number = table.Column<string>(type: "varchar(50)", nullable: false),
                    full_name = table.Column<string>(type: "varchar(200)", nullable: false),
                    birth_date = table.Column<DateTime>(type: "date", nullable: false),
                    gender = table.Column<string>(type: "varchar(1)", nullable: false),
                    weight_kg = table.Column<double>(type: "numeric(10,2)", nullable: false),
                    height_cm = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    last_update = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patients", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "procedures",
                schema: "siga_db",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    procedure_id = table.Column<string>(type: "varchar(50)", nullable: false),
                    description = table.Column<string>(type: "varchar(500)", nullable: false),
                    cid = table.Column<string>(type: "varchar(20)", nullable: false),
                    is_primary = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    last_update = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_procedures", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "specialties",
                schema: "siga_db",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "varchar(50)", nullable: false),
                    description = table.Column<string>(type: "varchar(200)", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    last_update = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_specialties", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "surgical_centers",
                schema: "siga_db",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "varchar(50)", nullable: false),
                    description = table.Column<string>(type: "varchar(200)", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    last_update = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_surgical_centers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "units",
                schema: "siga_db",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "varchar(50)", nullable: false),
                    description = table.Column<string>(type: "varchar(200)", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    last_update = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_units", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "siga_db",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    username = table.Column<string>(type: "varchar(100)", nullable: false),
                    password = table.Column<string>(type: "varchar(255)", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    last_update = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "surgery_locations",
                schema: "siga_db",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    surgical_center_id = table.Column<int>(type: "integer", nullable: false),
                    room = table.Column<string>(type: "varchar(50)", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    last_update = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_surgery_locations", x => x.id);
                    table.ForeignKey(
                        name: "f_k_surgery_locations__surgical_centers_surgical_center_id",
                        column: x => x.surgical_center_id,
                        principalSchema: "siga_db",
                        principalTable: "surgical_centers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "current_locations",
                schema: "siga_db",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    unit_id = table.Column<int>(type: "integer", nullable: false),
                    bed = table.Column<string>(type: "varchar(50)", nullable: false),
                    floor = table.Column<string>(type: "varchar(10)", nullable: false),
                    room = table.Column<string>(type: "varchar(50)", nullable: false),
                    patient_id = table.Column<int>(type: "integer", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    last_update = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_current_locations", x => x.id);
                    table.ForeignKey(
                        name: "f_k_current_locations__units_unit_id",
                        column: x => x.unit_id,
                        principalSchema: "siga_db",
                        principalTable: "units",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "f_k_patients_current_locations_current_location_id",
                        column: x => x.patient_id,
                        principalSchema: "siga_db",
                        principalTable: "patients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "surgeries",
                schema: "siga_db",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    surgery_id = table.Column<string>(type: "varchar(50)", nullable: false),
                    surgery_date = table.Column<DateTime>(type: "date", nullable: false),
                    status = table.Column<string>(type: "varchar(20)", nullable: false),
                    patient_id = table.Column<string>(type: "text", nullable: false),
                    specialty_id = table.Column<int>(type: "integer", nullable: false),
                    surgery_location_id = table.Column<int>(type: "integer", nullable: false),
                    patient_id1 = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    last_update = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_surgeries", x => x.id);
                    table.ForeignKey(
                        name: "f_k_surgeries__surgery_locations_location_id",
                        column: x => x.surgery_location_id,
                        principalSchema: "siga_db",
                        principalTable: "surgery_locations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "f_k_surgeries_patients_patient_id1",
                        column: x => x.id,
                        principalSchema: "siga_db",
                        principalTable: "patients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "f_k_surgeries_specialties_specialty_id",
                        column: x => x.specialty_id,
                        principalSchema: "siga_db",
                        principalTable: "specialties",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "surgery_procedures",
                schema: "siga_db",
                columns: table => new
                {
                    ProceduresId = table.Column<int>(type: "integer", nullable: false),
                    SurgeryId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_surgery_procedures", x => new { x.ProceduresId, x.SurgeryId });
                    table.ForeignKey(
                        name: "FK_surgery_procedures_procedures_ProceduresId",
                        column: x => x.ProceduresId,
                        principalSchema: "siga_db",
                        principalTable: "procedures",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_surgery_procedures_surgeries_SurgeryId",
                        column: x => x.SurgeryId,
                        principalSchema: "siga_db",
                        principalTable: "surgeries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_current_locations_patient_id",
                schema: "siga_db",
                table: "current_locations",
                column: "patient_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_current_locations_unit_id",
                schema: "siga_db",
                table: "current_locations",
                column: "unit_id");

            migrationBuilder.CreateIndex(
                name: "IX_procedures_procedure_id",
                schema: "siga_db",
                table: "procedures",
                column: "procedure_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_specialties_code",
                schema: "siga_db",
                table: "specialties",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_surgeries_specialty_id",
                schema: "siga_db",
                table: "surgeries",
                column: "specialty_id");

            migrationBuilder.CreateIndex(
                name: "IX_surgeries_surgery_location_id",
                schema: "siga_db",
                table: "surgeries",
                column: "surgery_location_id");

            migrationBuilder.CreateIndex(
                name: "IX_surgery_locations_surgical_center_id",
                schema: "siga_db",
                table: "surgery_locations",
                column: "surgical_center_id");

            migrationBuilder.CreateIndex(
                name: "IX_surgery_procedures_SurgeryId",
                schema: "siga_db",
                table: "surgery_procedures",
                column: "SurgeryId");

            migrationBuilder.CreateIndex(
                name: "IX_surgical_centers_code",
                schema: "siga_db",
                table: "surgical_centers",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_units_code",
                schema: "siga_db",
                table: "units",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_username",
                schema: "siga_db",
                table: "users",
                column: "username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "current_locations",
                schema: "siga_db");

            migrationBuilder.DropTable(
                name: "surgery_procedures",
                schema: "siga_db");

            migrationBuilder.DropTable(
                name: "users",
                schema: "siga_db");

            migrationBuilder.DropTable(
                name: "units",
                schema: "siga_db");

            migrationBuilder.DropTable(
                name: "procedures",
                schema: "siga_db");

            migrationBuilder.DropTable(
                name: "surgeries",
                schema: "siga_db");

            migrationBuilder.DropTable(
                name: "surgery_locations",
                schema: "siga_db");

            migrationBuilder.DropTable(
                name: "patients",
                schema: "siga_db");

            migrationBuilder.DropTable(
                name: "specialties",
                schema: "siga_db");

            migrationBuilder.DropTable(
                name: "surgical_centers",
                schema: "siga_db");
        }
    }
}
