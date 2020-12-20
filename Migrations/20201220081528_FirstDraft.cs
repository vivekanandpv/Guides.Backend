using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Guides.Backend.Migrations
{
    public partial class FirstDraft : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    MobileNumber = table.Column<long>(type: "bigint", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Country = table.Column<int>(type: "int", nullable: false),
                    IdentityInformation = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OfficialPosition = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LockedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AdminResetOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LoginResetOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AdminBlockOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LoginBlockOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PasswordResetOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsAdminLocked = table.Column<bool>(type: "bit", nullable: false),
                    IsLoginLocked = table.Column<bool>(type: "bit", nullable: false),
                    ResetKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResetKeyExpiresOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FailedAttempts = table.Column<int>(type: "int", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Respondents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegisteredOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HusbandName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressLine1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressLine2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telephone1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Person1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telephone2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Person2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telephone3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Person3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RCHID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HospitalId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PHC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HealthFacility = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ANM = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ANMTelephone1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ANMTelephone2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Asha = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AshaTelephone1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AshaTelephone2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OwnAMobilePhone = table.Column<int>(type: "int", nullable: false),
                    SecondaryAccessToMobilePhone = table.Column<int>(type: "int", nullable: false),
                    SecondaryAccessToSmartphone = table.Column<int>(type: "int", nullable: false),
                    Country = table.Column<int>(type: "int", nullable: false),
                    RegistrationLatitude = table.Column<double>(type: "float", nullable: true),
                    RegistrationLongitude = table.Column<double>(type: "float", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    SocioDemographicId = table.Column<int>(type: "int", nullable: true),
                    PregnancyAndGdmRiskFactorsId = table.Column<int>(type: "int", nullable: true),
                    TobaccoAndAlcoholUseId = table.Column<int>(type: "int", nullable: true),
                    PhysicalActivityId = table.Column<int>(type: "int", nullable: true),
                    DietaryBehaviourId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Respondents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Respondents_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DietaryBehaviourCollection",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegisteredOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RespondentId = table.Column<int>(type: "int", nullable: false),
                    RegisteredBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegistrationLatitude = table.Column<double>(type: "float", nullable: true),
                    RegistrationLongitude = table.Column<double>(type: "float", nullable: true),
                    Breakfast = table.Column<int>(type: "int", nullable: false),
                    RegularMeals = table.Column<int>(type: "int", nullable: false),
                    Fruits = table.Column<int>(type: "int", nullable: false),
                    Vegetables = table.Column<int>(type: "int", nullable: false),
                    Carbohydrates = table.Column<int>(type: "int", nullable: false),
                    SugaryDrinks = table.Column<int>(type: "int", nullable: false),
                    Pulses = table.Column<int>(type: "int", nullable: false),
                    EggsOrDairy = table.Column<int>(type: "int", nullable: false),
                    FishOrChicken = table.Column<int>(type: "int", nullable: false),
                    RedMeat = table.Column<int>(type: "int", nullable: false),
                    Snack = table.Column<int>(type: "int", nullable: false),
                    OutsideMeals = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DietaryBehaviourCollection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DietaryBehaviourCollection_Respondents_RespondentId",
                        column: x => x.RespondentId,
                        principalTable: "Respondents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhysicalActivityCollection",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegisteredOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RespondentId = table.Column<int>(type: "int", nullable: false),
                    RegisteredBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegistrationLatitude = table.Column<double>(type: "float", nullable: true),
                    RegistrationLongitude = table.Column<double>(type: "float", nullable: true),
                    VigorousActivityDaysPerWeek = table.Column<int>(type: "int", nullable: true),
                    VigorousActivityHoursPerDay = table.Column<int>(type: "int", nullable: true),
                    VigorousActivityMinutesPerDay = table.Column<int>(type: "int", nullable: true),
                    ModerateActivityDaysPerWeek = table.Column<int>(type: "int", nullable: true),
                    ModerateActivityHoursPerDay = table.Column<int>(type: "int", nullable: true),
                    ModerateActivityMinutesPerDay = table.Column<int>(type: "int", nullable: true),
                    WalkingDaysPerWeek = table.Column<int>(type: "int", nullable: true),
                    WalkingHoursPerDay = table.Column<int>(type: "int", nullable: true),
                    WalkingMinutesPerDay = table.Column<int>(type: "int", nullable: true),
                    SittingDaysPerWeek = table.Column<int>(type: "int", nullable: true),
                    SittingHoursPerDay = table.Column<int>(type: "int", nullable: true),
                    SittingMinutesPerDay = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhysicalActivityCollection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhysicalActivityCollection_Respondents_RespondentId",
                        column: x => x.RespondentId,
                        principalTable: "Respondents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PregnancyAndGdmRiskFactorsCollection",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegisteredOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RespondentId = table.Column<int>(type: "int", nullable: false),
                    RegisteredBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegistrationLatitude = table.Column<double>(type: "float", nullable: true),
                    RegistrationLongitude = table.Column<double>(type: "float", nullable: true),
                    Gravida = table.Column<int>(type: "int", nullable: false),
                    Parity = table.Column<int>(type: "int", nullable: true),
                    Living = table.Column<int>(type: "int", nullable: true),
                    PreviousBabyWeightOver4kg = table.Column<int>(type: "int", nullable: false),
                    BabySizeLargerThanAverage = table.Column<int>(type: "int", nullable: false),
                    MonthsFromLastDelivery = table.Column<int>(type: "int", nullable: false),
                    LMP = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EDD = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FirstAncVisitMonth = table.Column<int>(type: "int", nullable: false),
                    WeightMeasuredInCurrentPregnancy = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: true),
                    HtnOrPreEclampsia = table.Column<int>(type: "int", nullable: false),
                    Gdm = table.Column<int>(type: "int", nullable: false),
                    Dm = table.Column<int>(type: "int", nullable: false),
                    DiabetesType = table.Column<int>(type: "int", nullable: false),
                    DmInFamily = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PregnancyAndGdmRiskFactorsCollection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PregnancyAndGdmRiskFactorsCollection_Respondents_RespondentId",
                        column: x => x.RespondentId,
                        principalTable: "Respondents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SocioDemographics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegisteredOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RespondentId = table.Column<int>(type: "int", nullable: false),
                    RegisteredBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegistrationLatitude = table.Column<double>(type: "float", nullable: true),
                    RegistrationLongitude = table.Column<double>(type: "float", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: true),
                    Religion = table.Column<int>(type: "int", nullable: false),
                    UgandaTribe = table.Column<int>(type: "int", nullable: false),
                    MaritalStatus = table.Column<int>(type: "int", nullable: false),
                    NPeopleInHousehold = table.Column<int>(type: "int", nullable: false),
                    NRooms = table.Column<int>(type: "int", nullable: false),
                    RespondentEducation = table.Column<int>(type: "int", nullable: false),
                    SpouseEducation = table.Column<int>(type: "int", nullable: false),
                    Employment = table.Column<int>(type: "int", nullable: false),
                    MonthlyIncome = table.Column<int>(type: "int", nullable: false),
                    IncomeSufficiency = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocioDemographics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SocioDemographics_Respondents_RespondentId",
                        column: x => x.RespondentId,
                        principalTable: "Respondents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TobaccoAndAlcoholUseCollection",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegisteredOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RespondentId = table.Column<int>(type: "int", nullable: false),
                    RegisteredBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegistrationLatitude = table.Column<double>(type: "float", nullable: true),
                    RegistrationLongitude = table.Column<double>(type: "float", nullable: true),
                    TobaccoUsed = table.Column<bool>(type: "bit", nullable: false),
                    Smoking = table.Column<int>(type: "int", nullable: false),
                    OtherTobaccoUse = table.Column<int>(type: "int", nullable: false),
                    Alcohol = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TobaccoAndAlcoholUseCollection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TobaccoAndAlcoholUseCollection_Respondents_RespondentId",
                        column: x => x.RespondentId,
                        principalTable: "Respondents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DietaryBehaviourCollection_RespondentId",
                table: "DietaryBehaviourCollection",
                column: "RespondentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PhysicalActivityCollection_RespondentId",
                table: "PhysicalActivityCollection",
                column: "RespondentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PregnancyAndGdmRiskFactorsCollection_RespondentId",
                table: "PregnancyAndGdmRiskFactorsCollection",
                column: "RespondentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Respondents_UserId",
                table: "Respondents",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SocioDemographics_RespondentId",
                table: "SocioDemographics",
                column: "RespondentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TobaccoAndAlcoholUseCollection_RespondentId",
                table: "TobaccoAndAlcoholUseCollection",
                column: "RespondentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DietaryBehaviourCollection");

            migrationBuilder.DropTable(
                name: "PhysicalActivityCollection");

            migrationBuilder.DropTable(
                name: "PregnancyAndGdmRiskFactorsCollection");

            migrationBuilder.DropTable(
                name: "SocioDemographics");

            migrationBuilder.DropTable(
                name: "TobaccoAndAlcoholUseCollection");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Respondents");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
