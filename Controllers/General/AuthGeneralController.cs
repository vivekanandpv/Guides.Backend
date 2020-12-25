using System.Threading.Tasks;
using Guides.Backend.Services.Auth;
using Guides.Backend.StaticProviders;
using Guides.Backend.ViewModels.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Guides.Backend.Controllers.General
{
    [Authorize(Roles = GeneralStaticDataProvider.GeneralAdministratorRoles)]
    [Route(EndpointStaticStore.AuthGeneralTemplate)]
    [ApiController]
    public class AuthGeneralController : ControllerBase
    {
        private readonly IAuthService _authService;
        
        public AuthGeneralController(IAuthServiceFactory authServiceFactory)
        {
            this._authService = authServiceFactory.GetMaster();
        }
        
        [AllowAnonymous]
        [HttpPost(EndpointStaticStore.Login)]
        public async Task<ActionResult<AuthTokenViewModel>> Login(AuthLoginViewModel viewModel)
        {
            return await this._authService.Login(viewModel);
        }
        
        [AllowAnonymous]
        [HttpPost(EndpointStaticStore.ResetPassword)]
        public async Task<IActionResult> ResetPassword(AuthResetPasswordViewModel viewModel)
        {
            await this._authService.ResetPassword(viewModel);
            return Ok();
        }
        
        
        [HttpPost(EndpointStaticStore.ChangePassword)]
        public async Task<IActionResult> ChangePassword(AuthChangePasswordViewModel viewModel)
        {
            await this._authService.ChangePassword(viewModel);
            return Ok();
        }

        [HttpPost(EndpointStaticStore.Register)]
        public async Task<ActionResult<AuthResetKeyViewModel>> Register(AuthRegisterViewModel viewModel)
        {
            return await this._authService.Register(viewModel);
        }
        
        [HttpPut(EndpointStaticStore.Update)]
        public async Task<ActionResult> Update(AuthUpdateViewModel viewModel)
        {
            await this._authService.Update(viewModel);
            return Ok();
        }
        
        [HttpPost(EndpointStaticStore.AdminBlock)]
        public async Task<IActionResult> AdminBlock(AuthAdminActionViewModel viewModel)
        {
            await this._authService.AdminBlock(viewModel);
            return Ok();
        }
        
        [HttpPost(EndpointStaticStore.AdminReset)]
        public async Task<ActionResult<AuthResetKeyViewModel>> AdminReset(AuthAdminActionViewModel viewModel)
        {
            return await this._authService.AdminReset(viewModel);
        }
        
        [HttpPost(EndpointStaticStore.LoginReset)]
        public async Task<ActionResult<AuthResetKeyViewModel>> LoginReset(AuthAdminActionViewModel viewModel)
        {
            return await this._authService.LoginReset(viewModel);
        }
    }
}
