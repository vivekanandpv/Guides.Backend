using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Guides.Backend.Migrations
{
    public partial class CorrectedDomainAfterDiscussion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VoluntaryExits");

            migrationBuilder.RenameColumn(
                name: "VoluntaryExitId",
                table: "Respondents",
                newName: "LossToFollowUpId");

            migrationBuilder.AlterColumn<long>(
                name: "RCHID",
                table: "Respondents",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "LossToFollowUpCollection",
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
                    table.PrimaryKey("PK_LossToFollowUpCollection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LossToFollowUpCollection_Respondents_RespondentId",
                        column: x => x.RespondentId,
                        principalTable: "Respondents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LossToFollowUpCollection_RespondentId",
                table: "LossToFollowUpCollection",
                column: "RespondentId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LossToFollowUpCollection");

            migrationBuilder.RenameColumn(
                name: "LossToFollowUpId",
                table: "Respondents",
                newName: "VoluntaryExitId");

            migrationBuilder.AlterColumn<string>(
                name: "RCHID",
                table: "Respondents",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "VoluntaryExits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeathReportedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExtraInformation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RARemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReasonForExit = table.Column<int>(type: "int", nullable: false),
                    RegisteredBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegisteredOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegistrationLatitude = table.Column<double>(type: "float", nullable: true),
                    RegistrationLongitude = table.Column<double>(type: "float", nullable: true),
                    RespondentId = table.Column<int>(type: "int", nullable: false)
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
                name: "IX_VoluntaryExits_RespondentId",
                table: "VoluntaryExits",
                column: "RespondentId",
                unique: true);
        }
    }
}
