using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Guides.Backend.Migrations
{
    public partial class AddedDateOfActualEntryToDomainClasses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfActualEntry",
                table: "TobaccoAndAlcoholUseCollection",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfActualEntry",
                table: "SocioDemographics",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfActualEntry",
                table: "Respondents",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfActualEntry",
                table: "PregnancyAndGdmRiskFactorsCollection",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfActualEntry",
                table: "PhysicalActivityCollection",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfActualEntry",
                table: "LossToFollowUpCollection",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfActualEntry",
                table: "DietaryBehaviourCollection",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfActualEntry",
                table: "DeathRecords",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfActualEntry",
                table: "TobaccoAndAlcoholUseCollection");

            migrationBuilder.DropColumn(
                name: "DateOfActualEntry",
                table: "SocioDemographics");

            migrationBuilder.DropColumn(
                name: "DateOfActualEntry",
                table: "Respondents");

            migrationBuilder.DropColumn(
                name: "DateOfActualEntry",
                table: "PregnancyAndGdmRiskFactorsCollection");

            migrationBuilder.DropColumn(
                name: "DateOfActualEntry",
                table: "PhysicalActivityCollection");

            migrationBuilder.DropColumn(
                name: "DateOfActualEntry",
                table: "LossToFollowUpCollection");

            migrationBuilder.DropColumn(
                name: "DateOfActualEntry",
                table: "DietaryBehaviourCollection");

            migrationBuilder.DropColumn(
                name: "DateOfActualEntry",
                table: "DeathRecords");
        }
    }
}
