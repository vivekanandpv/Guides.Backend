using Microsoft.EntityFrameworkCore.Migrations;

namespace Guides.Backend.Migrations
{
    public partial class MinorCorrectionsToDomain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeathReportedBy",
                table: "LossToFollowUpCollection");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeathReportedBy",
                table: "LossToFollowUpCollection",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
