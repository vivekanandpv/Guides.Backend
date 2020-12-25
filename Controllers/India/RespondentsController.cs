using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Guides.Backend.Exceptions.Auth;
using Guides.Backend.Services.Baseline.Interfaces.India;
using Guides.Backend.StaticProviders;
using Guides.Backend.Utils;
using Guides.Backend.ViewModels.Baseline;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace Guides.Backend.Controllers.India
{
    [Authorize(Roles = GeneralStaticDataProvider.IndiaUserRoles)]
    [Route(EndpointStaticStore.RespondentsIndiaTemplate)]
    [ApiController]
    public class RespondentsController : ControllerBase
    {
        private readonly IIndiaRespondentService _service;
        private readonly IAppUtils _appUtils;

        //  HttpContext is still null in constructor
        public RespondentsController(IIndiaRespondentService service, IAppUtils appUtils)
        {
            _service = service;
            _appUtils = appUtils;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RespondentIndiaListViewModel>>> Get() => Ok(await this._service.Get());

        [HttpGet(EndpointStaticStore.GetById)]
        public async Task<ActionResult<RespondentIndiaListViewModel>> Get(int id) => Ok(await this._service.Get(id));

        [HttpPost]
        public async Task<ActionResult<RespondentIndiaListViewModel>> Register(
            RespondentIndiaRegisterViewModel viewModel)
            => Ok(await this._service.Register(viewModel, this._appUtils.GetCurrentUser(HttpContext)));

        [HttpPut(EndpointStaticStore.GetById)]
        public async Task<ActionResult<RespondentIndiaListViewModel>> Update(
            int id, RespondentIndiaUpdateViewModel viewModel)
            => Ok(await this._service.Update(id, viewModel, this._appUtils.GetCurrentUser(HttpContext)));

    }
}
