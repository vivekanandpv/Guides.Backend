﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Guides.Backend.Domain
{
    public class User
    {
        public int Id { get; set; }
        [MaxLength(100), Required]
        public string FullName { get; set; }
        [MaxLength(200), Required]
        public string Email { get; set; }
        [Required]
        public long MobileNumber { get; set; }
        [MaxLength(50), Required]
        public string DisplayName { get; set; }
        public Country Country { get; set; }
        [MaxLength(50)]
        public string IdentityInformation { get; set; }
        [MaxLength(50)]
        public string OfficialPosition { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }
        
        //  Auth
        public DateTime? LockedOn { get; set; }
        public DateTime? AdminResetOn { get; set; }
        public DateTime? LoginResetOn { get; set; }
        public DateTime? AdminBlockOn { get; set; }
        public DateTime? LoginBlockOn { get; set; }
        public DateTime? PasswordResetOn { get; set; }
        public bool IsAdminLocked { get; set; }
        public bool IsLoginLocked { get; set; }
        public string ResetKey { get; set; }
        public DateTime? ResetKeyExpiresOn { get; set; }
        public int FailedAttempts { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string ImageUrl { get; set; }
        public DateTime? LastUpdateOn { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        
        //  Domain
        public virtual ICollection<Respondent> Respondents { get; set; }

        public User()
        {
            this.UserRoles = new HashSet<UserRole>();
            this.Respondents = new List<Respondent>();
        }
    }

    public class Role
    {
        public int Id { get; set; }
        [MaxLength(50), Required]
        public string Name { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }

        public Role()
        {
            this.UserRoles = new HashSet<UserRole>();
        }
    }

    public class UserRole
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
    }


    public class Respondent
    {
        public int Id { get; set; }
        public DateTime RegisteredOn { get; set; }
        public DateTime DateOfActualEntry { get; set; }
        public string FullName { get; set; }
        public string HusbandName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public int? PostalCode { get; set; }
        public long? Telephone1 { get; set; }
        public string Person1 { get; set; }
        public long? Telephone2 { get; set; }
        public string Person2 { get; set; }
        public long? Telephone3 { get; set; }
        public string Person3 { get; set; }
        public long? RCHID { get; set; }
        public string HospitalId { get; set; }
        public string PHC { get; set; }
        public string HealthFacility { get; set; }
        public string ANM { get; set; }
        public long? ANMTelephone1 { get; set; }
        public long? ANMTelephone2 { get; set; }
        public string Asha { get; set; }
        public long? AshaTelephone1 { get; set; }
        public long? AshaTelephone2 { get; set; }
        public MobilePhone OwnAMobilePhone { get; set; }
        public MobilePhone SecondaryAccessToMobilePhone { get; set; }
        public ConditionalBoolean SecondaryAccessToSmartphone { get; set; }
        public Country Country { get; set; }
        public double? RegistrationLatitude { get; set; }
        public double? RegistrationLongitude { get; set; }

        //  Revision: new fields for eligibility determination
        public DateTime? DateOfBirth { get; set; }
        public int? Age { get; set; }
        public DateTime? LMP { get; set; }
        public DateTime? EDD { get; set; }
        public bool WillingToParticipate { get; set; }
        public bool AvailableForFollowup { get; set; }
        public bool InformedConsent { get; set; }
        public bool IsEligible { get; set; }

        public virtual User User { get; set; }
        public int UserId { get; set; }

        public virtual SocioDemographic SocioDemographic { get; set; }
        public int? SocioDemographicId { get; set; }

        public virtual PregnancyAndGdmRiskFactors PregnancyAndGdmRiskFactors { get; set; }
        public int? PregnancyAndGdmRiskFactorsId { get; set; }

        public virtual TobaccoAndAlcoholUse TobaccoAndAlcoholUse { get; set; }
        public int? TobaccoAndAlcoholUseId { get; set; }

        public virtual PhysicalActivity PhysicalActivity { get; set; }
        public int? PhysicalActivityId { get; set; }

        public virtual DietaryBehaviour DietaryBehaviour { get; set; }
        public int? DietaryBehaviourId { get; set; }

        public virtual DeathRecord DeathRecord { get; set; }
        public int? DeathRecordId { get; set; }

        public virtual LossToFollowUp LossToFollowUp { get; set; }
        public int? LossToFollowUpId { get; set; }
    }

    public class SocioDemographic
    {
        public int Id { get; set; }
        public DateTime RegisteredOn { get; set; }
        public DateTime DateOfActualEntry { get; set; }
        public virtual Respondent Respondent { get; set; }
        public int RespondentId { get; set; }
        public string RegisteredBy { get; set; }
        public double? RegistrationLatitude { get; set; }
        public double? RegistrationLongitude { get; set; }
        
        //  Domain
        public DateTime? DateOfBirth { get; set; }
        public int? Age { get; set; }
        public Religion Religion { get; set; }
        public UgandaTribe UgandaTribe { get; set; }
        public MaritalStatus MaritalStatus { get; set; }
        public int NPeopleInHousehold { get; set; }
        public int NRooms { get; set; }
        public Education RespondentEducation { get; set; }
        public Education SpouseEducation { get; set; }
        public Employment Employment { get; set; }
        public int MonthlyIncome { get; set; }
        public IncomeSufficiency IncomeSufficiency { get; set; }
        
    }

    public class PregnancyAndGdmRiskFactors
    {
        public int Id { get; set; }
        public DateTime RegisteredOn { get; set; }
        public DateTime DateOfActualEntry { get; set; }
        public virtual Respondent Respondent { get; set; }
        public int RespondentId { get; set; }
        public string RegisteredBy { get; set; }
        public double? RegistrationLatitude { get; set; }
        public double? RegistrationLongitude { get; set; }
        
        //  Domain

        public int Gravida { get; set; }
        public int? Parity { get; set; }
        public int? Living { get; set; }
        public int? Stillbirth { get; set; }
        public GeneralTriplet PreviousBabyWeightOver4kg { get; set; }
        public GeneralTriplet BabySizeLargerThanAverage { get; set; }
        public int? MonthsFromLastDelivery { get; set; }
        public DateTime? LMP { get; set; }
        public DateTime? EDD { get; set; }
        public int? FirstAncVisitMonth { get; set; }
        public GeneralTriplet WeightMeasuredInCurrentPregnancy { get; set; }
        public double? Weight { get; set; }
        public GeneralTriplet HtnOrPreEclampsia { get; set; }
        public GeneralTriplet HtnOrPreEclampsiaCurrent { get; set; }
        public GeneralTriplet Gdm { get; set; }
        public GeneralTriplet GdmCurrent { get; set; }
        public GeneralTriplet Dm { get; set; }
        public DiabetesType DiabetesType { get; set; }
        public GeneralTriplet DmInFamily { get; set; }
    }

    public class TobaccoAndAlcoholUse
    {
        public int Id { get; set; }
        public DateTime RegisteredOn { get; set; }
        public DateTime DateOfActualEntry { get; set; }
        public virtual Respondent Respondent { get; set; }
        public int RespondentId { get; set; }
        public string RegisteredBy { get; set; }
        public double? RegistrationLatitude { get; set; }
        public double? RegistrationLongitude { get; set; }
        
        //  Domain
        public bool TobaccoUsed { get; set; }
        public TobaccoAndAlcoholConsumption Smoking { get; set; }
        public TobaccoAndAlcoholConsumption OtherTobaccoUse { get; set; }
        public TobaccoAndAlcoholConsumption Alcohol { get; set; }
    }

    public class PhysicalActivity
    {
        public int Id { get; set; }
        public DateTime RegisteredOn { get; set; }
        public DateTime DateOfActualEntry { get; set; }
        public virtual Respondent Respondent { get; set; }
        public int RespondentId { get; set; }
        public string RegisteredBy { get; set; }
        public double? RegistrationLatitude { get; set; }
        public double? RegistrationLongitude { get; set; }
        
        //  Domain
        public bool VigorousActivities { get; set; }
        public int? VigorousActivityDaysPerWeek { get; set; }
        public int? VigorousActivityHoursPerDay { get; set; }
        public int? VigorousActivityMinutesPerDay { get; set; }
        
        public bool ModerateActivities { get; set; }
        public int? ModerateActivityDaysPerWeek { get; set; }
        public int? ModerateActivityHoursPerDay { get; set; }
        public int? ModerateActivityMinutesPerDay { get; set; }

        public bool WalkingActivities { get; set; }
        public int? WalkingDaysPerWeek { get; set; }
        public int? WalkingHoursPerDay { get; set; }
        public int? WalkingMinutesPerDay { get; set; }

        public bool SittingActivities { get; set; }
        public int? SittingDaysPerWeek { get; set; }
        public int? SittingHoursPerDay { get; set; }
        public int? SittingMinutesPerDay { get; set; }
        
    }

    public class DietaryBehaviour
    {
        public int Id { get; set; }
        public DateTime RegisteredOn { get; set; }
        public DateTime DateOfActualEntry { get; set; }
        public virtual Respondent Respondent { get; set; }
        public int RespondentId { get; set; }
        public string RegisteredBy { get; set; }
        public double? RegistrationLatitude { get; set; }
        public double? RegistrationLongitude { get; set; }
        
        //  Domain
        public DietaryBehaviourSelectionA Breakfast { get; set; }
        public DietaryBehaviourSelectionA RegularMeals { get; set; }
        public DietaryBehaviourSelectionB Fruits { get; set; }
        public DietaryBehaviourSelectionB Vegetables { get; set; }
        public DietaryBehaviourSelectionB Carbohydrates { get; set; }
        public DietaryBehaviourSelectionB SugaryDrinks { get; set; }
        public DietaryBehaviourSelectionB Pulses { get; set; }
        public DietaryBehaviourSelectionB EggsOrDairy { get; set; }
        public DietaryBehaviourSelectionB FishOrChicken { get; set; }
        public DietaryBehaviourSelectionB RedMeat { get; set; }
        public DietaryBehaviourSelectionB Snack { get; set; }
        public DietaryBehaviourSelectionA OutsideMeals { get; set; }
    }


    public class DeathRecord
    {
        //  Metadata
        public int Id { get; set; }
        public DateTime RegisteredOn { get; set; }
        public DateTime DateOfActualEntry { get; set; }
        public virtual Respondent Respondent { get; set; }
        public int RespondentId { get; set; }
        public string RegisteredBy { get; set; }
        public double? RegistrationLatitude { get; set; }
        public double? RegistrationLongitude { get; set; }
        
        //  Domain
        public string ReasonForDeath { get; set; }
        public DateTime? DateOfDeath { get; set; }
        public string DeathReportedBy { get; set; }
    }
    
    public class LossToFollowUp
    {
        //  Metadata
        public int Id { get; set; }
        public DateTime RegisteredOn { get; set; }
        public DateTime DateOfActualEntry { get; set; }
        public virtual Respondent Respondent { get; set; }
        public int RespondentId { get; set; }
        public string RegisteredBy { get; set; }
        public double? RegistrationLatitude { get; set; }
        public double? RegistrationLongitude { get; set; }
        
        //  Domain
        public VoluntaryExitReason ReasonForExit { get; set; }
        public string ExtraInformation { get; set; }
        public string RARemarks { get; set; }
    }
}
