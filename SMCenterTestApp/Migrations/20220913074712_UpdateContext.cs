using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMCenterTestApp.Migrations
{
    public partial class UpdateContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cabinet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cabinet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Region",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Region", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RegionDTO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegionDTO", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Speciality",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Speciality", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Patient",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SurName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sex = table.Column<bool>(type: "bit", nullable: false),
                    RegionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Patient__RegionI__5DCAEF64",
                        column: x => x.RegionId,
                        principalTable: "Region",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PatientDTO",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sex = table.Column<bool>(type: "bit", nullable: false),
                    RegionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientDTO", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientDTO_RegionDTO_RegionId",
                        column: x => x.RegionId,
                        principalTable: "RegionDTO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Doctor",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Initials = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CabinetId = table.Column<int>(type: "int", nullable: false),
                    SpecialityId = table.Column<int>(type: "int", nullable: false),
                    RegionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctor", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Doctor__CabinetI__60A75C0F",
                        column: x => x.CabinetId,
                        principalTable: "Cabinet",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Doctor__RegionId__628FA481",
                        column: x => x.RegionId,
                        principalTable: "Region",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Doctor__Speciali__619B8048",
                        column: x => x.SpecialityId,
                        principalTable: "Speciality",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_CabinetId",
                table: "Doctor",
                column: "CabinetId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_RegionId",
                table: "Doctor",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_SpecialityId",
                table: "Doctor",
                column: "SpecialityId");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_RegionId",
                table: "Patient",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientDTO_RegionId",
                table: "PatientDTO",
                column: "RegionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Doctor");

            migrationBuilder.DropTable(
                name: "Patient");

            migrationBuilder.DropTable(
                name: "PatientDTO");

            migrationBuilder.DropTable(
                name: "Cabinet");

            migrationBuilder.DropTable(
                name: "Speciality");

            migrationBuilder.DropTable(
                name: "Region");

            migrationBuilder.DropTable(
                name: "RegionDTO");
        }
    }
}
