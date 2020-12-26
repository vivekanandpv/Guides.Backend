using Microsoft.EntityFrameworkCore.Migrations;

namespace Guides.Backend.Migrations
{
    public partial class AddedBooleanFlagsForPhysicalActivity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ModerateActivities",
                table: "PhysicalActivityCollection",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SittingActivities",
                table: "PhysicalActivityCollection",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "VigorousActivities",
                table: "PhysicalActivityCollection",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "WalkingActivities",
                table: "PhysicalActivityCollection",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModerateActivities",
                table: "PhysicalActivityCollection");

            migrationBuilder.DropColumn(
                name: "SittingActivities",
                table: "PhysicalActivityCollection");

            migrationBuilder.DropColumn(
                name: "VigorousActivities",
                table: "PhysicalActivityCollection");

            migrationBuilder.DropColumn(
                name: "WalkingActivities",
                table: "PhysicalActivityCollection");
        }
    }
}
