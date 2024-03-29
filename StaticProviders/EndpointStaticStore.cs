﻿namespace Guides.Backend.StaticProviders
{
    public static class EndpointStaticStore
    {
        public const string AuthIndiaTemplate = "api/auth/india";
        public const string AuthUgandaTemplate = "api/auth/uganda";
        public const string AuthGeneralTemplate = "api/auth/general";


        public const string RespondentsIndiaTemplate = "api/respondents/india";
        public const string RespondentsUgandaTemplate = "api/respondents/uganda";
        
        public const string AncillaryIndiaTemplate = "api/ancillary/india";
        public const string AncillaryUgandaTemplate = "api/ancillary/uganda";

        public const string SocioDemographicIndiaTemplate = "api/socio-demographic/india";
        public const string SocioDemographicUgandaTemplate = "api/socio-demographic/uganda";

        public const string PregnancyAndGdmRiskFactorsIndiaTemplate = "api/pregnancy-and-gdm-risk-factors/india";
        public const string PregnancyAndGdmRiskFactorsUgandaTemplate = "api/pregnancy-and-gdm-risk-factors/uganda";

        public const string TobaccoAndAlcoholUseIndiaTemplate = "api/tobacco-and-alcohol-use/india";
        public const string TobaccoAndAlcoholUseUgandaTemplate = "api/tobacco-and-alcohol-use/uganda";

        public const string PhysicalActivityIndiaTemplate = "api/physical-activity/india";
        public const string PhysicalActivityUgandaTemplate = "api/physical-activity/uganda";

        public const string DietaryBehaviourIndiaTemplate = "api/dietary-behaviour/india";
        public const string DietaryBehaviourUgandaTemplate = "api/dietary-behaviour/uganda";

        public const string DeathRecordIndiaTemplate = "api/death-record/india";
        public const string DeathRecordUgandaTemplate = "api/death-record/uganda";

        public const string LossToFollowUpIndiaTemplate = "api/loss-to-follow-up/india";
        public const string LossToFollowUpUgandaTemplate = "api/loss-to-follow-up/uganda";

        public const string Login = "login";
        public const string ResetPassword = "reset-password";
        public const string ChangePassword = "change-password";
        public const string Register = "register";
        public const string AdminBlock = "admin-block";
        public const string AdminReset = "admin-reset";
        public const string LoginReset = "login-reset";
        public const string Update = "update";
        public const string GetFormStatusNavigator = "form-status-navigator/{id:int}";
        public const string GetRespondentsWithFormStatus = "respondents-with-status";
        public const string GetRespondentWithFormStatus = "respondents-with-status/{id:int}";
        public const string GetRespondentByPattern = "respondents-by-pattern/{pattern}";

        public const string GetById = "{id:int}";
    }
}