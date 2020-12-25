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
    [Route(EndpointStaticStore.DeathRecordIndiaTemplate)]
    [ApiController]
    public class DeathRecordController : ControllerBase
    {
        private readonly IIndiaDeathRecordService _service;
        private readonly IAppUtils _appUtils;

        public DeathRecordController(IIndiaDeathRecordService service, IAppUtils appUtils)
        {
            _service = service;
            _appUtils = appUtils;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeathRecordListViewModel>>> Get() => Ok(await this._service.Get());

        [HttpGet(EndpointStaticStore.GetById)]
        public async Task<ActionResult<DeathRecordListViewModel>> Get(int id) => Ok(await this._service.Get(id));

        [HttpPost]
        public async Task<ActionResult<DeathRecordListViewModel>> Register(
            DeathRecordRegisterViewModel viewModel)
            => Ok(await this._service.Register(viewModel, this._appUtils.GetCurrentUser(HttpContext)));

        [HttpPut(EndpointStaticStore.GetById)]
        public async Task<ActionResult<DeathRecordListViewModel>> Update(
            int id, DeathRecordUpdateViewModel viewModel)
            => Ok(await this._service.Update(id, viewModel, this._appUtils.GetCurrentUser(HttpContext)));

    }
}
