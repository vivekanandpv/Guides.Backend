using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Guides.Backend.Domain;
using Guides.Backend.StaticProviders;
using Guides.Backend.ViewModels.Auth;

namespace Guides.Backend.Services.Auth
{
    public interface IAuthService
    {
        AuthRegionViewModel GetRegion();
        Task<AuthTokenViewModel> Login(AuthLoginViewModel viewModel);
        Task<AuthResetKeyViewModel> Register(AuthRegisterViewModel viewModel);
        Task Update(AuthUpdateViewModel viewModel);
        Task ChangePassword(AuthChangePasswordViewModel viewModel);
        Task<AuthResetKeyViewModel> AdminReset(AuthAdminActionViewModel viewModel);
        Task<AuthResetKeyViewModel> LoginReset(AuthAdminActionViewModel viewModel);
        Task AdminBlock(AuthAdminActionViewModel viewModel);
        Task ResetPassword(AuthResetPasswordViewModel viewModel);
    }
}
