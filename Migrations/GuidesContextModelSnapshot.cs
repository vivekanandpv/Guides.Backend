﻿// <auto-generated />
using System;
using Guides.Backend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Guides.Backend.Migrations
{
    [DbContext(typeof(GuidesContext))]
    partial class GuidesContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("Guides.Backend.Domain.DietaryBehaviour", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("Breakfast")
                        .HasColumnType("int");

                    b.Property<int>("Carbohydrates")
                        .HasColumnType("int");

                    b.Property<int>("EggsOrDairy")
                        .HasColumnType("int");

                    b.Property<int>("FishOrChicken")
                        .HasColumnType("int");

                    b.Property<int>("Fruits")
                        .HasColumnType("int");

                    b.Property<int>("OutsideMeals")
                        .HasColumnType("int");

                    b.Property<int>("Pulses")
                        .HasColumnType("int");

                    b.Property<int>("RedMeat")
                        .HasColumnType("int");

                    b.Property<string>("RegisteredBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RegisteredOn")
                        .HasColumnType("datetime2");

                    b.Property<double?>("RegistrationLatitude")
                        .HasColumnType("float");

                    b.Property<double?>("RegistrationLongitude")
                        .HasColumnType("float");

                    b.Property<int>("RegularMeals")
                        .HasColumnType("int");

                    b.Property<int>("RespondentId")
                        .HasColumnType("int");

                    b.Property<int>("Snack")
                        .HasColumnType("int");

                    b.Property<int>("SugaryDrinks")
                        .HasColumnType("int");

                    b.Property<int>("Vegetables")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RespondentId")
                        .IsUnique();

                    b.ToTable("DietaryBehaviourCollection");
                });

            modelBuilder.Entity("Guides.Backend.Domain.PhysicalActivity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("ModerateActivityDaysPerWeek")
                        .HasColumnType("int");

                    b.Property<int?>("ModerateActivityHoursPerDay")
                        .HasColumnType("int");

                    b.Property<int?>("ModerateActivityMinutesPerDay")
                        .HasColumnType("int");

                    b.Property<string>("RegisteredBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RegisteredOn")
                        .HasColumnType("datetime2");

                    b.Property<double?>("RegistrationLatitude")
                        .HasColumnType("float");

                    b.Property<double?>("RegistrationLongitude")
                        .HasColumnType("float");

                    b.Property<int>("RespondentId")
                        .HasColumnType("int");

                    b.Property<int?>("SittingDaysPerWeek")
                        .HasColumnType("int");

                    b.Property<int?>("SittingHoursPerDay")
                        .HasColumnType("int");

                    b.Property<int?>("SittingMinutesPerDay")
                        .HasColumnType("int");

                    b.Property<int?>("VigorousActivityDaysPerWeek")
                        .HasColumnType("int");

                    b.Property<int?>("VigorousActivityHoursPerDay")
                        .HasColumnType("int");

                    b.Property<int?>("VigorousActivityMinutesPerDay")
                        .HasColumnType("int");

                    b.Property<int?>("WalkingDaysPerWeek")
                        .HasColumnType("int");

                    b.Property<int?>("WalkingHoursPerDay")
                        .HasColumnType("int");

                    b.Property<int?>("WalkingMinutesPerDay")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RespondentId")
                        .IsUnique();

                    b.ToTable("PhysicalActivityCollection");
                });

            modelBuilder.Entity("Guides.Backend.Domain.PregnancyAndGdmRiskFactors", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("BabySizeLargerThanAverage")
                        .HasColumnType("int");

                    b.Property<int>("DiabetesType")
                        .HasColumnType("int");

                    b.Property<int>("Dm")
                        .HasColumnType("int");

                    b.Property<int>("DmInFamily")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EDD")
                        .HasColumnType("datetime2");

                    b.Property<int?>("FirstAncVisitMonth")
                        .HasColumnType("int");

                    b.Property<int>("Gdm")
                        .HasColumnType("int");

                    b.Property<int>("Gravida")
                        .HasColumnType("int");

                    b.Property<int>("HtnOrPreEclampsia")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LMP")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Living")
                        .HasColumnType("int");

                    b.Property<int?>("MonthsFromLastDelivery")
                        .HasColumnType("int");

                    b.Property<int?>("Parity")
                        .HasColumnType("int");

                    b.Property<int>("PreviousBabyWeightOver4kg")
                        .HasColumnType("int");

                    b.Property<string>("RegisteredBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RegisteredOn")
                        .HasColumnType("datetime2");

                    b.Property<double?>("RegistrationLatitude")
                        .HasColumnType("float");

                    b.Property<double?>("RegistrationLongitude")
                        .HasColumnType("float");

                    b.Property<int>("RespondentId")
                        .HasColumnType("int");

                    b.Property<double?>("Weight")
                        .HasColumnType("float");

                    b.Property<int>("WeightMeasuredInCurrentPregnancy")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RespondentId")
                        .IsUnique();

                    b.ToTable("PregnancyAndGdmRiskFactorsCollection");
                });

            modelBuilder.Entity("Guides.Backend.Domain.Respondent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ANM")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ANMTelephone1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ANMTelephone2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AddressLine1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AddressLine2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Asha")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AshaTelephone1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AshaTelephone2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Country")
                        .HasColumnType("int");

                    b.Property<int?>("DietaryBehaviourId")
                        .HasColumnType("int");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HealthFacility")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HospitalId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HusbandName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OwnAMobilePhone")
                        .HasColumnType("int");

                    b.Property<string>("PHC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Person1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Person2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Person3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PhysicalActivityId")
                        .HasColumnType("int");

                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PregnancyAndGdmRiskFactorsId")
                        .HasColumnType("int");

                    b.Property<string>("RCHID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RegisteredOn")
                        .HasColumnType("datetime2");

                    b.Property<double?>("RegistrationLatitude")
                        .HasColumnType("float");

                    b.Property<double?>("RegistrationLongitude")
                        .HasColumnType("float");

                    b.Property<int>("SecondaryAccessToMobilePhone")
                        .HasColumnType("int");

                    b.Property<int>("SecondaryAccessToSmartphone")
                        .HasColumnType("int");

                    b.Property<int?>("SocioDemographicId")
                        .HasColumnType("int");

                    b.Property<string>("Telephone1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telephone2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telephone3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TobaccoAndAlcoholUseId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Respondents");
                });

            modelBuilder.Entity("Guides.Backend.Domain.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Guides.Backend.Domain.SocioDemographic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("Age")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<int>("Employment")
                        .HasColumnType("int");

                    b.Property<int>("IncomeSufficiency")
                        .HasColumnType("int");

                    b.Property<int>("MaritalStatus")
                        .HasColumnType("int");

                    b.Property<int>("MonthlyIncome")
                        .HasColumnType("int");

                    b.Property<int>("NPeopleInHousehold")
                        .HasColumnType("int");

                    b.Property<int>("NRooms")
                        .HasColumnType("int");

                    b.Property<string>("RegisteredBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RegisteredOn")
                        .HasColumnType("datetime2");

                    b.Property<double?>("RegistrationLatitude")
                        .HasColumnType("float");

                    b.Property<double?>("RegistrationLongitude")
                        .HasColumnType("float");

                    b.Property<int>("Religion")
                        .HasColumnType("int");

                    b.Property<int>("RespondentEducation")
                        .HasColumnType("int");

                    b.Property<int>("RespondentId")
                        .HasColumnType("int");

                    b.Property<int>("SpouseEducation")
                        .HasColumnType("int");

                    b.Property<int>("UgandaTribe")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RespondentId")
                        .IsUnique();

                    b.ToTable("SocioDemographics");
                });

            modelBuilder.Entity("Guides.Backend.Domain.TobaccoAndAlcoholUse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("Alcohol")
                        .HasColumnType("int");

                    b.Property<int>("OtherTobaccoUse")
                        .HasColumnType("int");

                    b.Property<string>("RegisteredBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RegisteredOn")
                        .HasColumnType("datetime2");

                    b.Property<double?>("RegistrationLatitude")
                        .HasColumnType("float");

                    b.Property<double?>("RegistrationLongitude")
                        .HasColumnType("float");

                    b.Property<int>("RespondentId")
                        .HasColumnType("int");

                    b.Property<int>("Smoking")
                        .HasColumnType("int");

                    b.Property<bool>("TobaccoUsed")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("RespondentId")
                        .IsUnique();

                    b.ToTable("TobaccoAndAlcoholUseCollection");
                });

            modelBuilder.Entity("Guides.Backend.Domain.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime?>("AdminBlockOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("AdminResetOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("Country")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("FailedAttempts")
                        .HasColumnType("int");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("IdentityInformation")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("IsAdminLocked")
                        .HasColumnType("bit");

                    b.Property<bool>("IsLoginLocked")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LockedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LoginBlockOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LoginResetOn")
                        .HasColumnType("datetime2");

                    b.Property<long>("MobileNumber")
                        .HasColumnType("bigint");

                    b.Property<string>("OfficialPosition")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<DateTime?>("PasswordResetOn")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("ResetKey")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ResetKeyExpiresOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Guides.Backend.Domain.UserRole", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Guides.Backend.Domain.DietaryBehaviour", b =>
                {
                    b.HasOne("Guides.Backend.Domain.Respondent", "Respondent")
                        .WithOne("DietaryBehaviour")
                        .HasForeignKey("Guides.Backend.Domain.DietaryBehaviour", "RespondentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Respondent");
                });

            modelBuilder.Entity("Guides.Backend.Domain.PhysicalActivity", b =>
                {
                    b.HasOne("Guides.Backend.Domain.Respondent", "Respondent")
                        .WithOne("PhysicalActivity")
                        .HasForeignKey("Guides.Backend.Domain.PhysicalActivity", "RespondentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Respondent");
                });

            modelBuilder.Entity("Guides.Backend.Domain.PregnancyAndGdmRiskFactors", b =>
                {
                    b.HasOne("Guides.Backend.Domain.Respondent", "Respondent")
                        .WithOne("PregnancyAndGdmRiskFactors")
                        .HasForeignKey("Guides.Backend.Domain.PregnancyAndGdmRiskFactors", "RespondentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Respondent");
                });

            modelBuilder.Entity("Guides.Backend.Domain.Respondent", b =>
                {
                    b.HasOne("Guides.Backend.Domain.User", "User")
                        .WithMany("Respondents")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Guides.Backend.Domain.SocioDemographic", b =>
                {
                    b.HasOne("Guides.Backend.Domain.Respondent", "Respondent")
                        .WithOne("SocioDemographic")
                        .HasForeignKey("Guides.Backend.Domain.SocioDemographic", "RespondentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Respondent");
                });

            modelBuilder.Entity("Guides.Backend.Domain.TobaccoAndAlcoholUse", b =>
                {
                    b.HasOne("Guides.Backend.Domain.Respondent", "Respondent")
                        .WithOne("TobaccoAndAlcoholUse")
                        .HasForeignKey("Guides.Backend.Domain.TobaccoAndAlcoholUse", "RespondentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Respondent");
                });

            modelBuilder.Entity("Guides.Backend.Domain.UserRole", b =>
                {
                    b.HasOne("Guides.Backend.Domain.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Guides.Backend.Domain.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Guides.Backend.Domain.Respondent", b =>
                {
                    b.Navigation("DietaryBehaviour");

                    b.Navigation("PhysicalActivity");

                    b.Navigation("PregnancyAndGdmRiskFactors");

                    b.Navigation("SocioDemographic");

                    b.Navigation("TobaccoAndAlcoholUse");
                });

            modelBuilder.Entity("Guides.Backend.Domain.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Guides.Backend.Domain.User", b =>
                {
                    b.Navigation("Respondents");

                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
