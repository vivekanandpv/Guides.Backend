using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Guides.Backend.Data;
using Guides.Backend.Filters;
using Guides.Backend.Repositories.Auth;
using Guides.Backend.Repositories.Baseline.Implementations;
using Guides.Backend.Repositories.Baseline.Interfaces;
using Guides.Backend.Services.Auth;
using Guides.Backend.Services.Baseline.Implementations.India;
using Guides.Backend.Services.Baseline.Implementations.Uganda;
using Guides.Backend.Services.Baseline.Interfaces.India;
using Guides.Backend.Services.Baseline.Interfaces.Uganda;
using Guides.Backend.StaticProviders;
using Guides.Backend.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Guides.Backend.Configuration
{
    public static class ServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddApiControllers()
                .AddCorsConfiguration()
                .AddGuidesContext(configuration)
                .AddAutoMapperConfigured()
                .AddAuthenticationConfigured()
                .AddAuthorizationConfigured()
                .AddRepositories()
                .AddServices()
                .AddAppUtils()
                .AddSwagger();

            return services;
        }

        private static IServiceCollection AddApiControllers(this IServiceCollection services)
        {
            services.AddControllers(
                options => options.Filters.Add(new GeneralExceptionFilter())
                );
            return services;
        }

        private static IServiceCollection AddCorsConfiguration(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins(GeneralStaticDataProvider.AllowedHosts)
                        .WithMethods(GeneralStaticDataProvider.AllowedMethods)
                        .AllowAnyHeader();
                });
            });

            return services;
        }

        private static IServiceCollection AddGuidesContext(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<GuidesContext>(options =>
                {
                    options
                        .UseLazyLoadingProxies()
                        .UseSqlServer(GeneralStaticDataProvider.GuidesConnection);
                });

            return services;
        }

        private static IServiceCollection AddAutoMapperConfigured(this IServiceCollection services)
        {
            services.AddAutoMapper(config =>
            {
                config.AddProfile<GuidesAutoMapperProfile>();
            });

            return services;
        }


        private static IServiceCollection AddAuthenticationConfigured(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(GeneralStaticDataProvider.GuidesEncryptionKey)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            return services;
        }

        private static IServiceCollection AddAuthorizationConfigured(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(
                    GeneralStaticDataProvider.GeneralAdministratorPolicy,
                    policy => policy.RequireClaim(
                        GeneralStaticDataProvider.Roles,
                        GeneralStaticDataProvider.GeneralAdministratorGroup
                        )
                    );
                options.AddPolicy(
                    GeneralStaticDataProvider.IndiaAdministratorPolicy, 
                    policy => policy.RequireClaim(
                        GeneralStaticDataProvider.Roles, 
                        GeneralStaticDataProvider.IndiaAdministratorGroup
                        )
                    );
                options.AddPolicy(
                    GeneralStaticDataProvider.UgandaAdministratorPolicy, 
                    policy => policy.RequireClaim(
                        GeneralStaticDataProvider.Roles,
                        GeneralStaticDataProvider.UgandaAdministratorGroup
                        )
                    );
                options.AddPolicy(
                    GeneralStaticDataProvider.IndiaUserPolicy,
                    policy => policy.RequireClaim(
                        GeneralStaticDataProvider.Roles, 
                        GeneralStaticDataProvider.IndiaUserGroup
                        )
                    );
                options.AddPolicy(
                    GeneralStaticDataProvider.UgandaUserPolicy,
                    policy => policy.RequireClaim(
                        GeneralStaticDataProvider.Roles, 
                        GeneralStaticDataProvider.UgandaUserGroup
                        )
                    );
                options.AddPolicy(
                    GeneralStaticDataProvider.IndiaDataAnalystPolicy, 
                    policy => policy.RequireClaim(
                        GeneralStaticDataProvider.Roles, 
                        GeneralStaticDataProvider.IndiaDataAnalystGroup
                        )
                    );
                options.AddPolicy(
                    GeneralStaticDataProvider.UgandaDataAnalystPolicy,
                    policy => policy.RequireClaim(
                        GeneralStaticDataProvider.Roles, 
                        GeneralStaticDataProvider.UgandaDataAnalystGroup
                        )
                    );
                options.AddPolicy(
                    GeneralStaticDataProvider.ProjectDataAnalystPolicy, 
                    policy => policy.RequireClaim(
                        GeneralStaticDataProvider.Roles, 
                        GeneralStaticDataProvider.ProjectDataAnalystGroup
                        )
                    );
                
                options.AddPolicy(
                    GeneralStaticDataProvider.AllUserPolicy, 
                    policy => policy.RequireClaim(
                        GeneralStaticDataProvider.Roles, 
                        GeneralStaticDataProvider.AllUserGroup
                    )
                );
            });

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            //  Auth
            services.TryAddScoped<IAuthRepository, AuthRepository>();
            
            //  Domain: Baseline
            services.TryAddScoped<IRespondentRepository, RespondentRepository>();
            services.TryAddScoped<ISocioDemographicRepository, SocioDemographicRepository>();
            services.TryAddScoped<IPregnancyAndGdmRiskFactorsRepository, PregnancyAndGdmRiskFactorsRepository>();
            services.TryAddScoped<ITobaccoAndAlcoholUseRepository, TobaccoAndAlcoholUseRepository>();
            services.TryAddScoped<IPhysicalActivityRepository, PhysicalActivityRepository>();
            services.TryAddScoped<IDietaryBehaviourRepository, DietaryBehaviourRepository>();
            services.TryAddScoped<IDeathRecordRepository, DeathRecordRepository>();
            services.TryAddScoped<ILossToFollowUpRepository, LossToFollowUpRepository>();

            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            //  Needed all 3 registrations. Do not use TryAddScoped for Auth section
            services.AddScoped<IAuthService, IndiaAuthService>();
            services.AddScoped<IAuthService, UgandaAuthService>();
            services.AddScoped<IAuthService, MasterAuthService>();
            
            services.TryAddScoped<IAuthServiceFactory, AuthServiceFactory>();
            
            services.TryAddScoped<IIndiaRespondentService, IndiaRespondentService>();
            services.TryAddScoped<IIndiaSocioDemographicService, IndiaSocioDemographicService>();
            services.TryAddScoped<IIndiaPregnancyAndGdmRiskFactorsService, IndiaPregnancyAndGdmRiskFactorsService>();
            services.TryAddScoped<IIndiaTobaccoAndAlcoholUseService, IndiaTobaccoAndAlcoholUseService>();
            services.TryAddScoped<IIndiaPhysicalActivityService, IndiaPhysicalActivityService>();
            services.TryAddScoped<IIndiaDietaryBehaviourService, IndiaDietaryBehaviourService>();
            services.TryAddScoped<IIndiaDeathRecordService, IndiaDeathRecordService>();
            services.TryAddScoped<IIndiaLossToFollowUpService, IndiaLossToFollowUpService>();
            
            
            services.TryAddScoped<IUgandaRespondentService, UgandaRespondentService>();
            services.TryAddScoped<IUgandaSocioDemographicService, UgandaSocioDemographicService>();
            services.TryAddScoped<IUgandaPregnancyAndGdmRiskFactorsService, UgandaPregnancyAndGdmRiskFactorsService>();
            services.TryAddScoped<IUgandaTobaccoAndAlcoholUseService, UgandaTobaccoAndAlcoholUseService>();
            services.TryAddScoped<IUgandaPhysicalActivityService, UgandaPhysicalActivityService>();
            services.TryAddScoped<IUgandaDietaryBehaviourService, UgandaDietaryBehaviourService>();
            services.TryAddScoped<IUgandaDeathRecordService, UgandaDeathRecordService>();
            services.TryAddScoped<IUgandaLossToFollowUpService, UgandaLossToFollowUpService>();

            return services;
        }

        private static IServiceCollection AddAppUtils(this IServiceCollection services)
        {
            services.TryAddScoped<IAppUtils, AppUtils>();

            return services;
        }


        private static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Guides Backend Project",
                });
            });

            return services;
        }
    }
}
