using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Guides.Backend.Exceptions.Auth;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace Guides.Backend.Middleware
{
    public class AuthExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);   //  pass it
            var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
            var exception = exceptionHandlerPathFeature?.Error;

            if (exception is LoginFailedException)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsJsonAsync(new { Message = "Cannot process the login" });
            }
    
            if (exception is RegistrationFailedException)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsJsonAsync(new { Message = "Failed to register" });
            }
            
            if (exception is DomainValidationException)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsJsonAsync(new { Message = "Invalid data" });
            }
            
            if (exception is AdminActionNotSupportedException)
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsJsonAsync(new { Message = "Action not supported" });
            }
            
            if (exception is UserActionNotSupportedException)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsJsonAsync(new { Message = "Action not supported" });
            }
            
            if (exception is UserActionPreventedException)
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsJsonAsync(new { Message = "Action prohibited" });
            }
            
            if (exception is GeneralAuthException)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsJsonAsync(new { Message = "Action prohibited" });
            }
            
        }
    }
}
