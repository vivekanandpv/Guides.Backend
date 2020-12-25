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
        public int? PostalCode { get; set; }
        public long? Telephone1 { get; set; }
        [MaxLength(50), Required]
        public string Person1 { get; set; }
        public long? Telephone2 { get; set; }
        [MaxLength(50)]
        public string Person2 { get; set; }
        public string OwnAMobilePhone { get; set; }
        public string SecondaryAccessToMobilePhone { get; set; }
        public string SecondaryAccessToSmartphone { get; set; }
    }

    public abstract class RespondentIndiaViewModelBase : RespondentViewModelBase
    {
        public long? Telephone3 { get; set; }
        [MaxLength(50)]
        public string Person3 { get; set; }
        public long? RCHID { get; set; }
        [MaxLength(50), Required]
        public string PHC { get; set; }
        [MaxLength(50), Required]
        public string ANM { get; set; }
        public long? ANMTelephone1 { get; set; }
        public long? ANMTelephone2 { get; set; }
        [MaxLength(50), Required]
        public string Asha { get; set; }
        public long? AshaTelephone1 { get; set; }
        public long? AshaTelephone2 { get; set; }
    }

    public class RespondentUgandaViewModelBase : RespondentViewModelBase
    {
        [MaxLength(15)]
        public string HospitalId { get; set; }
        [MaxLength(50), Required]
        public string HealthFacility { get; set; }
    }

    public class RespondentIndiaRegisterViewModel : RespondentIndiaViewModelBase
    {
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime RegisteredOn { get; set; }
        [Range(-90, 90)]
        public double? RegistrationLatitude { get; set; }
        [Range(-90, 90)]
        public double? RegistrationLongitude { get; set; }
        [MaxLength(100), Required]
        public string RegisteredBy { get; set; }
    }

    public class RespondentUgandaRegisterViewModel : RespondentUgandaViewModelBase
    {
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime RegisteredOn { get; set; }
        [Range(-90, 90)]
        public double? RegistrationLatitude { get; set; }
        [Range(-90, 90)]
        public double? RegistrationLongitude { get; set; }
        [MaxLength(100), Required]
        public string RegisteredBy { get; set; }
    }

    public class RespondentIndiaUpdateViewModel : RespondentIndiaViewModelBase
    {
        [Required, Range(0, 1_000_000)]
        public int Id { get; set; }
    }

    public class RespondentUgandaUpdateViewModel : RespondentUgandaViewModelBase
    {
        [Required, Range(0, 1_000_000)]
        public int Id { get; set; }
    }


    public class RespondentIndiaListViewModel : RespondentIndiaRegisterViewModel
    {
        public int Id { get; set; }
    }

    public class RespondentUgandaListViewModel : RespondentUgandaRegisterViewModel
    {
        public int Id { get; set; }
    }

    public abstract class SocioDemographicViewModel
    {
        //  Domain
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? DateOfBirth { get; set; }
        [Range(18, 50)]
        public int? Age { get; set; }
        public string Religion { get; set; }
        public string MaritalStatus { get; set; }
        [Required, Range(1, 20)]
        public int NPeopleInHousehold { get; set; }
        [Required, Range(1, 20)]
        public int NRooms { get; set; }
        public string RespondentEducation { get; set; }
        public string SpouseEducation { get; set; }
        public string Employment { get; set; }
        [Required, Range(1, 2_000_000)]
        public int MonthlyIncome { get; set; }
        public string IncomeSufficiency { get; set; }
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
        public string UgandaTribe { get; set; }
    }


    public class SocioDemographicUgandaUpdateViewModel : SocioDemographicViewModel
    {
        //  Metadata
        [Required, Range(1, 1_000_000)]
        public int RespondentId { get; set; }

        //  Domain
        public string UgandaTribe { get; set; }
    }

    public class SocioDemographicIndiaListViewModel : SocioDemographicIndiaUpdateViewModel
    {
    }

    public class SocioDemographicUgandaListViewModel : SocioDemographicUgandaUpdateViewModel
    {
    }

    public abstract class PregnancyAndGdmRiskFactorsViewModel
    {
        [Required, Range(1, 25)]
        public int Gravida { get; set; }
        [Range(0, 20)]
        public int? Parity { get; set; }
        [Range(0, 20)]
        public int? Living { get; set; }
        public string PreviousBabyWeightOver4kg { get; set; }
        public string BabySizeLargerThanAverage { get; set; }
        [Range(0, 240)]
        public int? MonthsFromLastDelivery { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? LMP { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? EDD { get; set; }
        [Range(0, 8)]
        public int? FirstAncVisitMonth { get; set; }
        public string WeightMeasuredInCurrentPregnancy { get; set; }
        [Range(25, 150)]
        public double? Weight { get; set; }
        public string HtnOrPreEclampsia { get; set; }
        public string Gdm { get; set; }
        public string Dm { get; set; }
        public string DiabetesType { get; set; }
        public string DmInFamily { get; set; }
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

    public class PregnancyAndGdmRiskFactorsListViewModel : PregnancyAndGdmRiskFactorsUpdateViewModel
    {
    }

    public abstract class TobaccoAndAlcoholUseViewModel
    {
        public bool TobaccoUsed { get; set; }
        public string Smoking { get; set; }
        public string OtherTobaccoUse { get; set; }
        public string Alcohol { get; set; }
    }

    public class TobaccoAndAlcoholUseRegisterViewModel : TobaccoAndAlcoholUseViewModel
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

    public class TobaccoAndAlcoholUseUpdateViewModel : TobaccoAndAlcoholUseViewModel
    {
        //  Metadata
        [Required, Range(1, 1_000_000)]
        public int RespondentId { get; set; }
    }

    public class TobaccoAndAlcoholUseListViewModel : TobaccoAndAlcoholUseUpdateViewModel
    {
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

    public class PhysicalActivityListViewModel : PhysicalActivityUpdateViewModel
    {
    }

    public abstract class DietaryBehaviourViewModel
    {
        //  Domain
        public string Breakfast { get; set; }
        public string RegularMeals { get; set; }
        public string Fruits { get; set; }
        public string Vegetables { get; set; }
        public string Carbohydrates { get; set; }
        public string SugaryDrinks { get; set; }
        public string Pulses { get; set; }
        public string EggsOrDairy { get; set; }
        public string FishOrChicken { get; set; }
        public string RedMeat { get; set; }
        public string Snack { get; set; }
        public string OutsideMeals { get; set; }
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

    public class DietaryBehaviourListViewModel : DietaryBehaviourUpdateViewModel
    {
    }
    
    
    public abstract class DeathRecordViewModel
    {
        //  Domain
        [Required]
        public string ReasonForDeath { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DateOfDeath { get; set; }
        public string DeathReportedBy { get; set; }
    }
    
    public class DeathRecordRegisterViewModel : DeathRecordViewModel
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
    
    public class DeathRecordUpdateViewModel : DeathRecordViewModel
    {
        //  Metadata
        [Required, Range(1, 1_000_000)]
        public int RespondentId { get; set; }
    }
    
    public class DeathRecordListViewModel : DeathRecordUpdateViewModel
    {
    }
    
    
    
    public abstract class LossToFollowUpViewModel
    {
        //  Domain
        public string ReasonForExit { get; set; }
        public string ExtraInformation { get; set; }
        public string RARemarks { get; set; }
    }
    
    public class LossToFollowUpRegisterViewModel : LossToFollowUpViewModel
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
    
    public class LossToFollowUpUpdateViewModel : LossToFollowUpViewModel
    {
        //  Metadata
        [Required, Range(1, 1_000_000)]
        public int RespondentId { get; set; }
    }
    
    public class LossToFollowUpListViewModel : LossToFollowUpUpdateViewModel
    {
    }
}
