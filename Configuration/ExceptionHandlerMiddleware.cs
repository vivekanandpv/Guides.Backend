using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Guides.Backend.Middleware;
using Microsoft.AspNetCore.Builder;

namespace Guides.Backend.Configuration
{
    public static class ExceptionHandlerMiddleware
    {
        public static IApplicationBuilder UseAggregatedExceptionHandler(this IApplicationBuilder app)
        {
            return app
                .UseAuthExceptionHandler()
                .UseConfigurationExceptionHandler()
                .UseDomainExceptionHandler();
        }

        private static IApplicationBuilder UseAuthExceptionHandler(this IApplicationBuilder app)
        {
            return app.UseMiddleware<AuthExceptionHandlerMiddleware>();
        }
        
        private static IApplicationBuilder UseConfigurationExceptionHandler(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ConfigurationExceptionHandlerMiddleware>();
        }
        
        private static IApplicationBuilder UseDomainExceptionHandler(this IApplicationBuilder app)
        {
            return app.UseMiddleware<DomainExceptionHandlerMiddleware>();
        }
    }
}
