using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Guides.Backend.Services.Baseline.Interfaces.Uganda;
using Guides.Backend.StaticProviders;
using Guides.Backend.Utils;
using Guides.Backend.ViewModels.Baseline;
using Microsoft.AspNetCore.Authorization;

namespace Guides.Backend.Controllers.Uganda
{
    [Authorize(Roles = GeneralStaticDataProvider.UgandaUserRoles)]
    [Route(EndpointStaticStore.DeathRecordUgandaTemplate)]
    [ApiController]
    public class DeathRecordController : ControllerBase
    {
        private readonly IUgandaDeathRecordService _service;
        private readonly IAppUtils _appUtils;

        public DeathRecordController(IUgandaDeathRecordService service, IAppUtils appUtils)
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
