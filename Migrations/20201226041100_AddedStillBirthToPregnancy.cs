using Microsoft.EntityFrameworkCore.Migrations;

namespace Guides.Backend.Migrations
{
    public partial class AddedStillBirthToPregnancy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Stillbirth",
                table: "PregnancyAndGdmRiskFactorsCollection",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Stillbirth",
                table: "PregnancyAndGdmRiskFactorsCollection");
        }
    }
}
