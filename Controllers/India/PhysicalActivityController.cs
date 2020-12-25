using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Guides.Backend.Services.Baseline.Interfaces.India;
using Guides.Backend.StaticProviders;
using Guides.Backend.Utils;
using Guides.Backend.ViewModels.Baseline;
using Microsoft.AspNetCore.Authorization;

namespace Guides.Backend.Controllers.India
{
    [Authorize(Roles = GeneralStaticDataProvider.IndiaUserRoles)]
    [Route(EndpointStaticStore.PhysicalActivityIndiaTemplate)]
    [ApiController]
    public class PhysicalActivityController : ControllerBase
    {
        private readonly IIndiaPhysicalActivityService _service;
        private readonly IAppUtils _appUtils;

        public PhysicalActivityController(IIndiaPhysicalActivityService service, IAppUtils appUtils)
        {
            _service = service;
            _appUtils = appUtils;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhysicalActivityListViewModel>>> Get() => Ok(await this._service.Get());

        [HttpGet(EndpointStaticStore.GetById)]
        public async Task<ActionResult<PhysicalActivityListViewModel>> Get(int id) => Ok(await this._service.Get(id));

        [HttpPost]
        public async Task<ActionResult<PhysicalActivityListViewModel>> Register(
            PhysicalActivityRegisterViewModel viewModel)
            => Ok(await this._service.Register(viewModel, this._appUtils.GetCurrentUser(HttpContext)));

        [HttpPut(EndpointStaticStore.GetById)]
        public async Task<ActionResult<PhysicalActivityListViewModel>> Update(
            int id, PhysicalActivityUpdateViewModel viewModel)
            => Ok(await this._service.Update(id, viewModel, this._appUtils.GetCurrentUser(HttpContext)));

    }
}
