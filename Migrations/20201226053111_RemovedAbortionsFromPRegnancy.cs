using Microsoft.EntityFrameworkCore.Migrations;

namespace Guides.Backend.Migrations
{
    public partial class RemovedAbortionsFromPRegnancy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Abortions",
                table: "PregnancyAndGdmRiskFactorsCollection");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Abortions",
                table: "PregnancyAndGdmRiskFactorsCollection",
                type: "int",
                nullable: true);
        }
    }
}
