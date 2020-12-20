using System;
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
        public ICollection<UserRole> UserRoles { get; set; }
        
        //  Domain
        public ICollection<Respondent> Respondents { get; set; }

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
        public ICollection<UserRole> UserRoles { get; set; }

        public Role()
        {
            this.UserRoles = new HashSet<UserRole>();
        }
    }

    public class UserRole
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }


    public class Respondent
    {
        public int Id { get; set; }
        public DateTime RegisteredOn { get; set; }
        public string FullName { get; set; }
        public string HusbandName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Telephone1 { get; set; }
        public string Person1 { get; set; }
        public string Telephone2 { get; set; }
        public string Person2 { get; set; }
        public string Telephone3 { get; set; }
        public string Person3 { get; set; }
        public string RCHID { get; set; }
        public string HospitalId { get; set; }
        public string PHC { get; set; }
        public string HealthFacility { get; set; }
        public string ANM { get; set; }
        public string ANMTelephone1 { get; set; }
        public string ANMTelephone2 { get; set; }
        public string Asha { get; set; }
        public string AshaTelephone1 { get; set; }
        public string AshaTelephone2 { get; set; }
        public MobilePhone OwnAMobilePhone { get; set; }
        public MobilePhone SecondaryAccessToMobilePhone { get; set; }
        public ConditionalBoolean SecondaryAccessToSmartphone { get; set; }
        public Country Country { get; set; }
        public double? RegistrationLatitude { get; set; }
        public double? RegistrationLongitude { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }

        public SocioDemographic SocioDemographic { get; set; }
        public int? SocioDemographicId { get; set; }

        public PregnancyAndGdmRiskFactors PregnancyAndGdmRiskFactors { get; set; }
        public int? PregnancyAndGdmRiskFactorsId { get; set; }

        public TobaccoAndAlcoholUse TobaccoAndAlcoholUse { get; set; }
        public int? TobaccoAndAlcoholUseId { get; set; }

        public PhysicalActivity PhysicalActivity { get; set; }
        public int? PhysicalActivityId { get; set; }

        public DietaryBehaviour DietaryBehaviour { get; set; }
        public int? DietaryBehaviourId { get; set; }
    }

    public class SocioDemographic
    {
        public int Id { get; set; }
        public DateTime RegisteredOn { get; set; }
        public Respondent Respondent { get; set; }
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
        public Respondent Respondent { get; set; }
        public int RespondentId { get; set; }
        public string RegisteredBy { get; set; }
        public double? RegistrationLatitude { get; set; }
        public double? RegistrationLongitude { get; set; }
        
        //  Domain

        public int Gravida { get; set; }
        public int? Parity { get; set; }
        public int? Living { get; set; }
        public GeneralTriplet PreviousBabyWeightOver4kg { get; set; }
        public GeneralTriplet BabySizeLargerThanAverage { get; set; }
        public int? MonthsFromLastDelivery { get; set; }
        public DateTime? LMP { get; set; }
        public DateTime? EDD { get; set; }
        public int? FirstAncVisitMonth { get; set; }
        public GeneralTriplet WeightMeasuredInCurrentPregnancy { get; set; }
        public double? Weight { get; set; }
        public GeneralTriplet HtnOrPreEclampsia { get; set; }
        public GeneralTriplet Gdm { get; set; }
        public GeneralTriplet Dm { get; set; }
        public DiabetesType DiabetesType { get; set; }
        public GeneralTriplet DmInFamily { get; set; }
    }

    public class TobaccoAndAlcoholUse
    {
        public int Id { get; set; }
        public DateTime RegisteredOn { get; set; }
        public Respondent Respondent { get; set; }
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
        public Respondent Respondent { get; set; }
        public int RespondentId { get; set; }
        public string RegisteredBy { get; set; }
        public double? RegistrationLatitude { get; set; }
        public double? RegistrationLongitude { get; set; }
        
        //  Domain
        public int? VigorousActivityDaysPerWeek { get; set; }
        public int? VigorousActivityHoursPerDay { get; set; }
        public int? VigorousActivityMinutesPerDay { get; set; }
        
        public int? ModerateActivityDaysPerWeek { get; set; }
        public int? ModerateActivityHoursPerDay { get; set; }
        public int? ModerateActivityMinutesPerDay { get; set; }
        
        public int? WalkingDaysPerWeek { get; set; }
        public int? WalkingHoursPerDay { get; set; }
        public int? WalkingMinutesPerDay { get; set; }
        
        public int? SittingDaysPerWeek { get; set; }
        public int? SittingHoursPerDay { get; set; }
        public int? SittingMinutesPerDay { get; set; }
        
    }

    public class DietaryBehaviour
    {
        public int Id { get; set; }
        public DateTime RegisteredOn { get; set; }
        public Respondent Respondent { get; set; }
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
}
