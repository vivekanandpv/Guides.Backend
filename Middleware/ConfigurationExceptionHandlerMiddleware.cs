using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Guides.Backend.Exceptions;
using Guides.Backend.Exceptions.Auth;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace Guides.Backend.Middleware
{
    public class ConfigurationExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ConfigurationExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);   //  pass it
            var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
            var exception = exceptionHandlerPathFeature?.Error;

            if (exception is ServiceNotAvailableException)
            {
                context.Response.StatusCode = 503;
                await context.Response.WriteAsJsonAsync(new { Message = "Error in configuration" });
            }
        }
    }
}
