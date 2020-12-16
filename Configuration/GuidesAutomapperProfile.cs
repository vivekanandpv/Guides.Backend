using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Guides.Backend.Domain;
using Guides.Backend.ViewModels.Auth;

namespace Guides.Backend.Configuration
{
    public class GuidesAutomapperProfile:Profile
    {
        public GuidesAutomapperProfile()
        {
            CreateMap<AuthRegisterViewModel, User>();
        }
    }
}
