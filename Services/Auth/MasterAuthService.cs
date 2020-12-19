using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Guides.Backend.Domain;
using Guides.Backend.Repositories.Auth;
using Guides.Backend.ViewModels.Auth;
using Microsoft.Extensions.Logging;

namespace Guides.Backend.Services.Auth
{
    public class MasterAuthService : AuthServiceBase, IAuthService
    {
        private readonly AuthRegionViewModel _region;
        
        public MasterAuthService(IAuthRepository repository, ILoggerFactory loggerFactory, IMapper mapper, AuthRegionViewModel region) : base(repository, loggerFactory, mapper)
        {
            this._region = new AuthRegionViewModel
            {
                Country = Country.India,
                IsMaster = true
            };
        }
        
        AuthRegionViewModel IAuthService.GetRegion()
        {
            return this._region;
        }

        async Task<AuthTokenViewModel> IAuthService.Login(AuthLoginViewModel viewModel)
        {
            return await base.Login(viewModel, this._region);
        }

        async Task<AuthResetKeyViewModel> IAuthService.Register(AuthRegisterViewModel viewModel)
        {
            return await base.Register(viewModel, this._region);
        }

        async Task IAuthService.ChangePassword(AuthChangePasswordViewModel viewModel)
        {
            await base.ChangePassword(viewModel, this._region);
        }

        async Task<AuthResetKeyViewModel> IAuthService.AdminReset(AuthAdminActionViewModel viewModel)
        {
            return await base.AdminReset(viewModel, this._region);
        }

        async Task<AuthResetKeyViewModel> IAuthService.LoginReset(AuthAdminActionViewModel viewModel)
        {
            return await base.LoginReset(viewModel, this._region);
        }

        async Task IAuthService.AdminBlock(AuthAdminActionViewModel viewModel)
        {
            await base.AdminBlock(viewModel, this._region);
        }

        async Task IAuthService.ResetPassword(AuthResetPasswordViewModel viewModel)
        {
            await base.ResetPassword(viewModel, this._region);
        }
    }
}
