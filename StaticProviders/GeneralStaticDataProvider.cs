using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Guides.Backend.Domain;
using Guides.Backend.ViewModels.Baseline;

namespace Guides.Backend.StaticProviders
{
    public static class GeneralStaticDataProvider
    {
        public static readonly string[] AllowedHosts = { "http://localhost:4200", "https://app.guides.athenaeum.in" };
        public static readonly string[] AllowedMethods = {
            "GET", "POST", "PUT", "OPTIONS"
        };

        public const string DatabaseConnection = "GuidesConnection";

        public const string GuidesEncryptionKey =
            "o94cp0Y7UOmgion7CcegaVDHl0cAJfqwsy3qtD7bOJ7q2Lncdhzhxsq4JwNvtqIYVBnIXJ4ckWVtQ2Kt4giC7w";

        public const string GuidesConnection =
            "Data Source=139.59.69.3;Initial Catalog=guidesdb;Integrated Security=False;User Id=guidesdb_admin;Password=!Life1671!;MultipleActiveResultSets=True";

        public const string PasswordPolicyRegEx =
            @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";

        public const int MaxFailedAttempts = 5;
        public const int ResetKeyExpiresInHours = 1;
        public const Country CountryIndia = Country.India;
        public const Country CountryUganda = Country.Uganda;
        public const int THUMBNAIL_SIZE = 180;
        
        
        
        public const string GeneralAdministratorRoles = "GeneralAdministrator,ProgramCoordinator";
        public const string IndiaAdministratorRoles = "GeneralAdministrator,ProgramCoordinator,IndiaAdministrator";
        public const string UgandaAdministratorRoles = "GeneralAdministrator,ProgramCoordinator,UgandaAdministrator";
        public const string IndiaUserRoles = "GeneralAdministrator,ProgramCoordinator,IndiaAdministrator,IndiaResearchAssistant,IndiaFieldOperative";
        public const string UgandaUserRoles = "GeneralAdministrator,ProgramCoordinator,UgandaAdministrator,UgandaResearchAssistant,UgandaFieldOperative";
        public const string IndiaDataAnalystRoles = "GeneralAdministrator,ProgramCoordinator,IndiaAdministrator,IndiaDataAnalyst,ProjectDataAnalyst";
        public const string UgandaDataAnalystRoles = "GeneralAdministrator,ProgramCoordinator,UgandaAdministrator,UgandaDataAnalyst,ProjectDataAnalyst";
        public const string ProjectDataAnalystRoles = "GeneralAdministrator,ProgramCoordinator,ProjectDataAnalyst";
        public const string AllUserRoles = "GeneralAdministrator,IndiaAdministrator,UgandaAdministrator,ProgramCoordinator,IndiaResearchAssistant,UgandaResearchAssistant,IndiaFieldOperative,UgandaFieldOperative,IndiaDataAnalyst,UgandaDataAnalyst,ProjectDataAnalyst";

        //  Log Categories
        public const string AuthLogCategory = "Auth Service";
        public const string RespondentCategory = "Respondent Service";
        public const string SocioDemographicCategory = "SocioDemographic Service";
        public const string PregnancyAndGdmRiskFactorsCategory = "PregnancyAndGdmRiskFactors Service";
        public const string TobaccoAndAlcoholUseCategory = "TobaccoAndAlcoholUse Service";
        public const string PhysicalActivityCategory = "PhysicalActivity Service";
        public const string DietaryBehaviourCategory = "DietaryBehaviour Service";
        public const string DeathRecordCategory = "DeathRecord Service";
        public const string LossToFollowUpCategory = "LossToFollowUp Service";

        //  Messages
        public const string PasswordPolicyErrorMessage =
            "Minimum eight characters, at least one uppercase letter, one lowercase letter, one number and one special character";

        public const string RegistrationShouldHaveAtLeastOneRole = "At least one role is required";


        //  Methods
        public static string GetNewResetKey(int length = 10)
        {
            var random = new Random();
            const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZ123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
