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
        Task<AuthTokenViewModel> Login(AuthLoginViewModel viewModel);
        Task<AuthResetKeyViewModel> Register(AuthRegisterViewModel viewModel);
        Task ChangePassword(AuthChangePasswordViewModel viewModel);
        Task<AuthResetKeyViewModel> AdminReset(string email);
        Task<AuthResetKeyViewModel> LoginReset(string email);
        Task AdminBlock(string email);
        Task ResetPassword(AuthResetPasswordViewModel viewModel);
        Task<bool> IsLoginBlocked(string email);
    }

    
}
