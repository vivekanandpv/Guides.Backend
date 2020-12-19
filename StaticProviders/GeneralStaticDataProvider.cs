using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Guides.Backend.Domain;

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

        //  Auth policies
        public const string Roles = "Roles";
        public const string GeneralAdministratorPolicy = "GeneralAdministratorPolicy";
        public const string IndiaAdministratorPolicy = "IndiaAdministratorPolicy";
        public const string UgandaAdministratorPolicy = "UgandaAdministratorPolicy";
        public const string IndiaUserPolicy = "IndiaUserPolicy";
        public const string UgandaUserPolicy = "UgandaUserPolicy";
        public const string IndiaDataAnalystPolicy = "IndiaDataAnalystPolicy";
        public const string UgandaDataAnalystPolicy = "UgandaDataAnalystPolicy";
        public const string ProjectDataAnalystPolicy = "ProjectDataAnalystPolicy";
        public const string AllUserPolicy = "AllUserPolicy";

        public static readonly string[] GeneralAdministratorGroup = {"GeneralAdministrator", "ProgramCoordinator"};
        public static readonly string[] IndiaAdministratorGroup = {"GeneralAdministrator", "ProgramCoordinator", "IndiaAdministrator"};
        public static readonly string[] UgandaAdministratorGroup = {"GeneralAdministrator", "ProgramCoordinator", "UgandaAdministrator"};
        public static readonly string[] IndiaUserGroup = {"GeneralAdministrator", "ProgramCoordinator", "IndiaResearchAssistant", "IndiaFieldOperative"};
        public static readonly string[] UgandaUserGroup = {"GeneralAdministrator", "ProgramCoordinator", "UgandaResearchAssistant", "UgandaFieldOperative"};
        public static readonly string[] IndiaDataAnalystGroup = {"GeneralAdministrator", "ProgramCoordinator", "IndiaAdministrator", "IndiaDataAnalyst", "ProjectDataAnalyst"};
        public static readonly string[] UgandaDataAnalystGroup = {"GeneralAdministrator", "ProgramCoordinator", "UgandaAdministrator", "UgandaDataAnalyst", "ProjectDataAnalyst"};
        public static readonly string[] ProjectDataAnalystGroup = {"GeneralAdministrator", "ProgramCoordinator", "ProjectDataAnalyst"};
        public static readonly string[] AllUserGroup =
        {
            "GeneralAdministrator",
            "IndiaAdministrator",
            "UgandaAdministrator",
            "ProgramCoordinator",
            "IndiaResearchAssistant",
            "UgandaResearchAssistant",
            "IndiaFieldOperative",
            "UgandaFieldOperative",
            "IndiaDataAnalyst",
            "UgandaDataAnalyst",
            "ProjectDataAnalyst",
        };

        //  Log Categories
        public const string AuthLogCategory = "Auth Service";

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
