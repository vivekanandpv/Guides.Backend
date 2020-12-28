using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Guides.Backend.Exceptions;
using Guides.Backend.Exceptions.Auth;
using Guides.Backend.StaticProviders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Guides.Backend.Filters
{
    public class GeneralExceptionFilter: IActionFilter, IOrderedFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is LoginFailedException)
            {
                context.Result = new ObjectResult(new { Message = "Cannot process the login" })
                {
                    StatusCode = HttpStatusCodeStore.Unauthorized
                };
                context.ExceptionHandled = true;
            }
            
            if (context.Exception is RegistrationFailedException)
            {
                context.Result = new ObjectResult(new { Message = "Failed to register" })
                {
                    StatusCode = HttpStatusCodeStore.BadRequest
                };
                context.ExceptionHandled = true;
            }
            
            if (context.Exception is AdminActionNotSupportedException 
                || context.Exception is UserActionNotSupportedException
                || context.Exception is UserActionPreventedException
                || context.Exception is GeneralAuthException)
            {
                context.Result = new ObjectResult(new { Message = "Action not supported" })
                {
                    StatusCode = HttpStatusCodeStore.Forbidden
                };
                context.ExceptionHandled = true;
            }
            
            if (context.Exception is DuplicatePreventionException)
            {
                context.Result = new ObjectResult(new { Message = "This form is already registered" })
                {
                    StatusCode = HttpStatusCodeStore.BadRequest
                };
                context.ExceptionHandled = true;
            }
            
            if (context.Exception is RegistrationDateDiscrepancyException)
            {
                context.Result = new ObjectResult(new { Message = "Discrepancy found in date of registration" })
                {
                    StatusCode = HttpStatusCodeStore.BadRequest
                };
                context.ExceptionHandled = true;
            }
            
            if (context.Exception is ServiceNotAvailableException)
            {
                context.Result = new ObjectResult(new { Message = "Could not process your request" })
                {
                    StatusCode = HttpStatusCodeStore.ServiceUnavailable
                };
                context.ExceptionHandled = true;
            }
            
            
            
            //  Catch-all
            if (context.Exception != null && !context.ExceptionHandled)
            {
                context.Result = new ObjectResult(new { Message = "Could not process your request" })
                {
                    StatusCode = HttpStatusCodeStore.ServiceUnavailable
                };
                context.ExceptionHandled = true;
            }
        }

        //In the following filter, the magic number 10 is subtracted
        //from the maximum integer value. Subtracting this number
        //allows other filters to run at the very end of the pipeline.
        public int Order { get; } = Int32.MaxValue - 10;
    }
}
