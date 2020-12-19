using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Guides.Backend.Domain;
using Guides.Backend.Exceptions;

namespace Guides.Backend.Services.Auth
{
    public class AuthServiceFactory : IAuthServiceFactory
    {
        private readonly IEnumerable<IAuthService> _services;

        public AuthServiceFactory(IEnumerable<IAuthService> services)
        {
            _services = services;
        }
        public IAuthService Get(Country country)
        {
            var service = this._services.FirstOrDefault(s => s.GetRegion().Country == country);

            if (service != null)
            {
                return service;
            }

            throw new ServiceNotAvailableException();
        }

        public IAuthService GetMaster()
        {
            var service = this._services.FirstOrDefault(s => s.GetRegion().IsMaster);
            
            if (service != null)
            {
                return service;
            }

            throw new ServiceNotAvailableException();
        }
    }

    
}
