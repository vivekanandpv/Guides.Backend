using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Guides.Backend.Domain;

namespace Guides.Backend.Services.Auth
{
    public interface IAuthServiceFactory
    {
        IAuthService Get(Country country);
        IAuthService GetMaster();
    }
}
