using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Guides.Backend.Migrations
{
    public partial class AddedDeathRecordAndVoluntaryExit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeathRecordId",
                table: "Respondents",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VoluntaryExitId",
                table: "Respondents",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DeathRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegisteredOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RespondentId = table.Column<int>(type: "int", nullable: false),
                    RegisteredBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegistrationLatitude = table.Column<double>(type: "float", nullable: true),
                    RegistrationLongitude = table.Column<double>(type: "float", nullable: true),
                    ReasonForDeath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfDeath = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeathReportedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeathRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeathRecords_Respondents_RespondentId",
                        column: x => x.RespondentId,
                        principalTable: "Respondents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VoluntaryExits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegisteredOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RespondentId = table.Column<int>(type: "int", nullable: false),
                    RegisteredBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegistrationLatitude = table.Column<double>(type: "float", nullable: true),
                    RegistrationLongitude = table.Column<double>(type: "float", nullable: true),
                    ReasonForExit = table.Column<int>(type: "int", nullable: false),
                    ExtraInformation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RARemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeathReportedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoluntaryExits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VoluntaryExits_Respondents_RespondentId",
                        column: x => x.RespondentId,
                        principalTable: "Respondents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeathRecords_RespondentId",
                table: "DeathRecords",
                column: "RespondentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VoluntaryExits_RespondentId",
                table: "VoluntaryExits",
                column: "RespondentId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeathRecords");

            migrationBuilder.DropTable(
                name: "VoluntaryExits");

            migrationBuilder.DropColumn(
                name: "DeathRecordId",
                table: "Respondents");

            migrationBuilder.DropColumn(
                name: "VoluntaryExitId",
                table: "Respondents");
        }
    }
}
