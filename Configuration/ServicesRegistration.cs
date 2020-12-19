﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Guides.Backend.Data;
using Guides.Backend.Repositories.Auth;
using Guides.Backend.Services.Auth;
using Guides.Backend.StaticProviders;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Guides.Backend.Configuration
{
    public static class ServicesRegistration
    {
        public static IServiceCollection AddApiControllers(this IServiceCollection services)
        {
            services.AddControllers();
            return services;
        }

        public static IServiceCollection AddCorsConfiguration(this IServiceCollection services)
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

        public static IServiceCollection AddGuidesContext(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<GuidesContext>(options =>
                    options.UseSqlServer(GeneralStaticDataProvider.GuidesConnection));

            return services;
        }

        public static IServiceCollection AddAutoMapperConfigured(this IServiceCollection services)
        {
            services.AddAutoMapper(config =>
            {
                config.AddProfile<GuidesAutoMapperProfile>();
            });

            return services;
        }


        public static IServiceCollection AddAuthenticationConfigured(this IServiceCollection services)
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

        public static IServiceCollection AddAuthorizationConfigured(this IServiceCollection services)
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

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAuthRepository, AuthRepository>();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, IndiaAuthService>();
            services.AddScoped<IAuthService, UgandaAuthService>();

            services.AddScoped<IAuthServiceFactory, AuthServiceFactory>();

            return services;
        }
    }
}
