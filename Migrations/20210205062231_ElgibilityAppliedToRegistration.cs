using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Guides.Backend.Migrations
{
    public partial class ElgibilityAppliedToRegistration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Respondents",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "AvailableForFollowup",
                table: "Respondents",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "Respondents",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EDD",
                table: "Respondents",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "InformedConsent",
                table: "Respondents",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsEligible",
                table: "Respondents",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LMP",
                table: "Respondents",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "WillingToParticipate",
                table: "Respondents",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "GdmCurrent",
                table: "PregnancyAndGdmRiskFactorsCollection",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HtnOrPreEclampsiaCurrent",
                table: "PregnancyAndGdmRiskFactorsCollection",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "Respondents");

            migrationBuilder.DropColumn(
                name: "AvailableForFollowup",
                table: "Respondents");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Respondents");

            migrationBuilder.DropColumn(
                name: "EDD",
                table: "Respondents");

            migrationBuilder.DropColumn(
                name: "InformedConsent",
                table: "Respondents");

            migrationBuilder.DropColumn(
                name: "IsEligible",
                table: "Respondents");

            migrationBuilder.DropColumn(
                name: "LMP",
                table: "Respondents");

            migrationBuilder.DropColumn(
                name: "WillingToParticipate",
                table: "Respondents");

            migrationBuilder.DropColumn(
                name: "GdmCurrent",
                table: "PregnancyAndGdmRiskFactorsCollection");

            migrationBuilder.DropColumn(
                name: "HtnOrPreEclampsiaCurrent",
                table: "PregnancyAndGdmRiskFactorsCollection");
        }
    }
}
