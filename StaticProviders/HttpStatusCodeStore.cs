using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Guides.Backend.StaticProviders
{
    public static class HttpStatusCodeStore
    {
        public const int Unauthorized = 401;
        public const int BadRequest = 400;
        public const int NotFound = 404;
        public const int Forbidden = 403;
        public const int ServiceUnavailable = 503;
    }
}
