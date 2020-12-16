using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Guides.Backend.Data;
using Guides.Backend.StaticProviders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
                config.AddProfile<GuidesAutomapperProfile>();
            });

            return services;
        }
    }
}
