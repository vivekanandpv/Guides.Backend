using System.Security.Claims;
using Guides.Backend.Exceptions.Auth;
using Microsoft.AspNetCore.Http;

namespace Guides.Backend.Utils
{
    public class AppUtils : IAppUtils
    {
        public string GetCurrentUser(HttpContext context)
        {
            var identity = context.User.Identity as ClaimsIdentity;
            if (identity?.FindFirst(ClaimTypes.Email) != null)
            {
                return identity.FindFirst(ClaimTypes.Email)?.Value;
            }

            throw new GeneralAuthException();
        }
    }
}