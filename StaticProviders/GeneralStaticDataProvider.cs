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

        
        
        
        //  Endpoints

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
        public const string RespondentCategory = "Respondent Service";
        public const string SocioDemographicCategory = "SocioDemographic Service";
        public const string PregnancyAndGdmRiskFactorsCategory = "PregnancyAndGdmRiskFactors Service";
        public const string TobaccoAndAlcoholUseCategory = "TobaccoAndAlcoholUse Service";
        public const string PhysicalActivityCategory = "PhysicalActivity Service";
        public const string DietaryBehaviourCategory = "DietaryBehaviour Service";

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

        public static RespondentIndiaListViewModel ToIndiaListViewModel(Respondent respondent)
        {
            return new RespondentIndiaListViewModel
            {
                ANM = respondent.ANM,
                ANMTelephone1 = respondent.ANMTelephone1,
                ANMTelephone2 = respondent.ANMTelephone2,
                AddressLine1 = respondent.AddressLine1,
                AddressLine2 = respondent.AddressLine2,
                Asha = respondent.Asha,
                AshaTelephone1 = respondent.AshaTelephone1,
                AshaTelephone2 = respondent.AshaTelephone2,
                City = respondent.City,
                FullName = respondent.FullName,
                HusbandName = respondent.HusbandName,
                Id = respondent.Id,
                RegisteredBy = respondent.User.Email,
                OwnAMobilePhone = respondent.OwnAMobilePhone,
                PHC = respondent.PHC,
                Person1 = respondent.Person1,
                Person2 = respondent.Person2,
                Person3 = respondent.Person3,
                PostalCode = respondent.PostalCode,
                RCHID = respondent.RCHID,
                RegisteredOn = respondent.RegisteredOn,
                RegistrationLatitude = respondent.RegistrationLatitude,
                RegistrationLongitude = respondent.RegistrationLongitude,
                SecondaryAccessToMobilePhone = respondent.SecondaryAccessToMobilePhone,
                SecondaryAccessToSmartphone = respondent.SecondaryAccessToSmartphone,
                Telephone1 = respondent.Telephone1,
                Telephone2 = respondent.Telephone2,
                Telephone3 = respondent.Telephone3
            };
        }
        
        public static RespondentUgandaListViewModel ToUgandaListViewModel(Respondent respondent)
        {
            return new RespondentUgandaListViewModel
            {
                AddressLine1 = respondent.AddressLine1,
                AddressLine2 = respondent.AddressLine2,
                City = respondent.City,
                FullName = respondent.FullName,
                HusbandName = respondent.HusbandName,
                Id = respondent.Id,
                RegisteredBy = respondent.User.Email,
                OwnAMobilePhone = respondent.OwnAMobilePhone,
                Person1 = respondent.Person1,
                Person2 = respondent.Person2,
                PostalCode = respondent.PostalCode,
                RegisteredOn = respondent.RegisteredOn,
                RegistrationLatitude = respondent.RegistrationLatitude,
                RegistrationLongitude = respondent.RegistrationLongitude,
                SecondaryAccessToMobilePhone = respondent.SecondaryAccessToMobilePhone,
                SecondaryAccessToSmartphone = respondent.SecondaryAccessToSmartphone,
                Telephone1 = respondent.Telephone1,
                Telephone2 = respondent.Telephone2,
                HealthFacility = respondent.HealthFacility,
                HospitalId = respondent.HospitalId
            };
        }

    }
}
