using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Guides.Backend.Utils
{
    public interface IAppUtils
    {
        string GetCurrentUser(HttpContext context);
    }
}
