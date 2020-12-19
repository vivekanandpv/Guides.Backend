using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Guides.Backend.Domain
{
    public enum Country
    {
        India,
        Uganda
    }

    public enum GeneralTriplet
    {
        Not_applicable,
        Yes,
        No,
        Not_sure
    }

    public enum ConditionalBoolean
    {
        Not_applicable,
        Yes,
        No
    }
    
    public enum GeneralQuadruplet
    {
        Not_applicable,
        Yes,
        No,
        Do_not_know,
        No_answer
    }

    public enum MobilePhone
    {
        Not_applicable,
        No,
        Yes_a_smartphone,
        Yes_but_not_a_smartphone
    }
    
    public enum Religion
    {
        None,
        Muslim,
        Hindu,
        Christian_non_specific,
        Seventh_day_adventist_SDA,
        Catholic,
        Church_of_Uganda,
        Pentecostal,
        Jain,
        Sikh,
        Buddhist,
        Other
    }

    public enum UgandaTribe
    {
        Not_applicable,
        Muganda,
        Musoga,
        Lango,
        Itesot,
        Mutooro,
        Munyankore_Mukiga,
        Other
    }

    public enum MaritalStatus
    {
        Married_or_cohabiting = 1,
        Never_married,
        Separated_divorced,
        Widow
    }

    public enum Education
    {
        Not_applicable,
        None_or_early_childhood_education,
        Primary,
        Secondary_or_high_school,
        Graduate_or_post_graduate
    }

    public enum Employment
    {
        Homemaker_or_unemployed = 1,
        Employed
    }

    public enum IncomeSufficiency
    {
        Yes_it_allows_to_build_savings = 1,
        Yes_it_allows_to_save_a_little,
        Yes_it_is_just_enough,
        No_should_use_savings,
        No_should_borrow
    }

    public enum DiabetesType
    {
        Not_applicable,
        Type_1,
        Type_2,
        Not_sure
    }

    public enum TobaccoAndAlcoholConsumption
    {
        Not_applicable,
        Yes_during_this_pregnancy,
        Yes_but_not_during_this_pregnancy,
        No
    }

    public enum DietaryBehaviourSelectionA
    {
        Never_or_very_rarely,
        Less_than_once_a_week,
        Once_a_week,
        Weekly_2_to_4_times,
        Weekly_5_to_6_times,
        Every_day
    }
    
    public enum DietaryBehaviourSelectionB
    {
        Never_or_very_rarely,
        Once_a_week_or_less_often,
        Weekly_2_to_4_times,
        Weekly_5_to_6_times,
        Daily_1_to_2_times,
        Daily_3_or_more_times
    }
}
