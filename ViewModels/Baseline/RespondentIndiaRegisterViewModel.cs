using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Guides.Backend.Domain;

namespace Guides.Backend.ViewModels.Baseline
{
    public abstract class RespondentViewModelBase
    {
        [Required, MinLength(3), MaxLength(100)]
        public string FullName { get; set; }
        [MaxLength(50)]
        public string HusbandName { get; set; }
        [MaxLength(100)]
        public string AddressLine1 { get; set; }
        [MaxLength(100)]
        public string AddressLine2 { get; set; }
        [MaxLength(50)]
        public string City { get; set; }
        [MaxLength(10)]
        public string PostalCode { get; set; }
        [MaxLength(11), Required]
        public string Telephone1 { get; set; }
        [MaxLength(50), Required]
        public string Person1 { get; set; }
        [MaxLength(11)]
        public string Telephone2 { get; set; }
        [MaxLength(50)]
        public string Person2 { get; set; }
        public MobilePhone OwnAMobilePhone { get; set; }
        public MobilePhone SecondaryAccessToMobilePhone { get; set; }
        public ConditionalBoolean SecondaryAccessToSmartphone { get; set; }
    }
    
    public abstract class RespondentIndiaViewModelBase : RespondentViewModelBase
    {
        [MaxLength(11)]
        public string Telephone3 { get; set; }
        [MaxLength(50)]
        public string Person3 { get; set; }
        [MaxLength(12)]
        public string RCHID { get; set; }
        [MaxLength(50), Required]
        public string PHC { get; set; }
        [MaxLength(50), Required]
        public string ANM { get; set; }
        [MaxLength(11)]
        public string ANMTelephone1 { get; set; }
        [MaxLength(11)]
        public string ANMTelephone2 { get; set; }
        [MaxLength(50), Required]
        public string Asha { get; set; }
        [MaxLength(11)]
        public string AshaTelephone1 { get; set; }
        [MaxLength(11)]
        public string AshaTelephone2 { get; set; }
    }
    
    public class RespondentUgandaViewModelBase : RespondentViewModelBase
    {
        [MaxLength(15)]
        public string HospitalId { get; set; }
        [MaxLength(50), Required]
        public string HealthFacility { get; set; }
    }
    
    public class RespondentRegisterIndiaViewModel : RespondentIndiaViewModelBase
    {
        [Required]
        public DateTime RegisteredOn { get; set; }
        [Range(-90, 90)]
        public double? RegistrationLatitude { get; set; }
        [Range(-90, 90)]
        public double? RegistrationLongitude { get; set; }
        [MaxLength(100), Required]
        public string RegisteredBy { get; set; }
    }
    
    public class RespondentRegisterUgandaViewModel : RespondentUgandaViewModelBase
    {
        [Required]
        public DateTime RegisteredOn { get; set; }
        [Range(-90, 90)]
        public double? RegistrationLatitude { get; set; }
        [Range(-90, 90)]
        public double? RegistrationLongitude { get; set; }
        [MaxLength(100), Required]
        public string RegisteredBy { get; set; }
    }
    
    public class RespondentUpdateIndiaViewModel : RespondentIndiaViewModelBase
    {
        [Required, Range(0, 1_000_000)]
        public int Id { get; set; }
    }
    
    public class RespondentUpdateUgandaViewModel : RespondentUgandaViewModelBase
    {
        [Required, Range(0, 1_000_000)]
        public int Id { get; set; }
    }

    public abstract class SocioDemographicViewModel
    {
        //  Domain
        public DateTime? DateOfBirth { get; set; }
        [Range(18, 50)]
        public int? Age { get; set; }
        public Religion Religion { get; set; }
        public MaritalStatus MaritalStatus { get; set; }
        [Required, Range(1, 20)]
        public int NPeopleInHousehold { get; set; }
        [Required, Range(1, 20)]
        public int NRooms { get; set; }
        public Education RespondentEducation { get; set; }
        public Education SpouseEducation { get; set; }
        public Employment Employment { get; set; }
        [Required, Range(1, 2_000_000)]
        public int MonthlyIncome { get; set; }
        public IncomeSufficiency IncomeSufficiency { get; set; }
    }

    public class SocioDemographicIndiaRegisterViewModel : SocioDemographicViewModel
    {
        //  Metadata
        [Required]
        public DateTime RegisteredOn { get; set; }
        [Required, Range(1, 1_000_000)]
        public int RespondentId { get; set; }
        [Required, MaxLength(100)]
        public string RegisteredBy { get; set; }
        public double? RegistrationLatitude { get; set; }
        public double? RegistrationLongitude { get; set; }
    }

    public class SocioDemographicIndiaUpdateViewModel : SocioDemographicViewModel
    {
        //  Metadata
        [Required, Range(1, 1_000_000)]
        public int RespondentId { get; set; }
    }
    
    public class SocioDemographicUgandaRegisterViewModel : SocioDemographicViewModel
    {
        //  Metadata
        [Required]
        public DateTime RegisteredOn { get; set; }
        [Required, Range(1, 1_000_000)]
        public int RespondentId { get; set; }
        [Required, MaxLength(100)]
        public string RegisteredBy { get; set; }
        public double? RegistrationLatitude { get; set; }
        public double? RegistrationLongitude { get; set; }
        
        //  Domain
        public UgandaTribe UgandaTribe { get; set; }
    }
    
    
    public class SocioDemographicUgandaUpdateViewModel : SocioDemographicViewModel
    {
        //  Metadata
        [Required, Range(1, 1_000_000)]
        public int RespondentId { get; set; }
        
        //  Domain
        public UgandaTribe UgandaTribe { get; set; }
    }

    public abstract class PregnancyAndGdmRiskFactorsViewModel
    {
        [Required, Range(1, 25)]
        public int Gravida { get; set; }
        [Range(0, 20)]
        public int? Parity { get; set; }
        [Range(0, 20)]
        public int? Living { get; set; }
        public GeneralTriplet PreviousBabyWeightOver4kg { get; set; }
        public GeneralTriplet BabySizeLargerThanAverage { get; set; }
        [Range(0, 240)]
        public int? MonthsFromLastDelivery { get; set; }
        public DateTime? LMP { get; set; }
        public DateTime? EDD { get; set; }
        [Range(0, 8)]
        public int? FirstAncVisitMonth { get; set; }
        public GeneralTriplet WeightMeasuredInCurrentPregnancy { get; set; }
        [Range(25, 150)]
        public double? Weight { get; set; }
        public GeneralTriplet HtnOrPreEclampsia { get; set; }
        public GeneralTriplet Gdm { get; set; }
        public GeneralTriplet Dm { get; set; }
        public DiabetesType DiabetesType { get; set; }
        public GeneralTriplet DmInFamily { get; set; }
    }

    public class PregnancyAndGdmRiskFactorsRegisterViewModel : PregnancyAndGdmRiskFactorsViewModel
    {
        //  Metadata
        [Required]
        public DateTime RegisteredOn { get; set; }
        [Required, Range(1, 1_000_000)]
        public int RespondentId { get; set; }
        [Required, MaxLength(100)]
        public string RegisteredBy { get; set; }
        public double? RegistrationLatitude { get; set; }
        public double? RegistrationLongitude { get; set; }
    }
    
    public class PregnancyAndGdmRiskFactorsUpdateViewModel : PregnancyAndGdmRiskFactorsViewModel
    {
        //  Metadata
        [Required, Range(1, 1_000_000)]
        public int RespondentId { get; set; }
    }

    public abstract class ToboccoAndAlcoholUseViewModel
    {
        public bool TobaccoUsed { get; set; }
        public TobaccoAndAlcoholConsumption Smoking { get; set; }
        public TobaccoAndAlcoholConsumption OtherTobaccoUse { get; set; }
        public TobaccoAndAlcoholConsumption Alcohol { get; set; }
    }
    
    

    public class ToboccoAndAlcoholUseRegisterViewModel : ToboccoAndAlcoholUseViewModel
    {
        //  Metadata
        [Required]
        public DateTime RegisteredOn { get; set; }
        [Required, Range(1, 1_000_000)]
        public int RespondentId { get; set; }
        [Required, MaxLength(100)]
        public string RegisteredBy { get; set; }
        public double? RegistrationLatitude { get; set; }
        public double? RegistrationLongitude { get; set; }
        
    }
    
    public class ToboccoAndAlcoholUseUpdateViewModel : ToboccoAndAlcoholUseViewModel
    {
        //  Metadata
        [Required, Range(1, 1_000_000)]
        public int RespondentId { get; set; }
    }

    public abstract class PhysicalActivityViewModel
    {
        //  Domain
        [Range(0, 7)]
        public int? VigorousActivityDaysPerWeek { get; set; }
        [Range(0, 12)]
        public int? VigorousActivityHoursPerDay { get; set; }
        [Range(0, 59)]
        public int? VigorousActivityMinutesPerDay { get; set; }
        
        [Range(0, 7)]
        public int? ModerateActivityDaysPerWeek { get; set; }
        [Range(0, 12)]
        public int? ModerateActivityHoursPerDay { get; set; }
        [Range(0, 59)]
        public int? ModerateActivityMinutesPerDay { get; set; }
        
        [Range(0, 7)]
        public int? WalkingDaysPerWeek { get; set; }
        [Range(0, 12)]
        public int? WalkingHoursPerDay { get; set; }
        [Range(0, 59)]
        public int? WalkingMinutesPerDay { get; set; }
        
        [Range(0, 7)]
        public int? SittingDaysPerWeek { get; set; }
        [Range(0, 12)]
        public int? SittingHoursPerDay { get; set; }
        [Range(0, 59)]
        public int? SittingMinutesPerDay { get; set; }
    }
    
    

    public class PhysicalActivityRegisterViewModel : PhysicalActivityViewModel
    {
        //  Metadata
        [Required]
        public DateTime RegisteredOn { get; set; }
        [Required, Range(1, 1_000_000)]
        public int RespondentId { get; set; }
        [Required, MaxLength(100)]
        public string RegisteredBy { get; set; }
        public double? RegistrationLatitude { get; set; }
        public double? RegistrationLongitude { get; set; }
    }
    
    public class PhysicalActivityUpdateViewModel : PhysicalActivityViewModel
    {
        //  Metadata
        [Required, Range(1, 1_000_000)]
        public int RespondentId { get; set; }
    }

    public abstract class DietaryBehaviourViewModel
    {
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

    public class DietaryBehaviourRegisterViewModel : DietaryBehaviourViewModel
    {
        //  Metadata
        [Required]
        public DateTime RegisteredOn { get; set; }
        [Required, Range(1, 1_000_000)]
        public int RespondentId { get; set; }
        [Required, MaxLength(100)]
        public string RegisteredBy { get; set; }
        public double? RegistrationLatitude { get; set; }
        public double? RegistrationLongitude { get; set; }
    }
    
    public class DietaryBehaviourUpdateViewModel : DietaryBehaviourViewModel
    {
        //  Metadata
        [Required, Range(1, 1_000_000)]
        public int RespondentId { get; set; }
    }
}
