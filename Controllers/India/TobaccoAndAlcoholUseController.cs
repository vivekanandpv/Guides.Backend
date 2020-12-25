﻿using Microsoft.AspNetCore.Http;
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
    [Route(EndpointStaticStore.TobaccoAndAlcoholUseIndiaTemplate)]
    [ApiController]
    public class TobaccoAndAlcoholUseController : ControllerBase
    {
        private readonly IIndiaTobaccoAndAlcoholUseService _service;
        private readonly IAppUtils _appUtils;

        public TobaccoAndAlcoholUseController(IIndiaTobaccoAndAlcoholUseService service, IAppUtils appUtils)
        {
            _service = service;
            _appUtils = appUtils;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TobaccoAndAlcoholUseListViewModel>>> Get() => Ok(await this._service.Get());

        [HttpGet(EndpointStaticStore.GetById)]
        public async Task<ActionResult<TobaccoAndAlcoholUseListViewModel>> Get(int id) => Ok(await this._service.Get(id));

        [HttpPost]
        public async Task<ActionResult<TobaccoAndAlcoholUseListViewModel>> Register(
            TobaccoAndAlcoholUseRegisterViewModel viewModel)
            => Ok(await this._service.Register(viewModel, this._appUtils.GetCurrentUser(HttpContext)));

        [HttpPut(EndpointStaticStore.GetById)]
        public async Task<ActionResult<TobaccoAndAlcoholUseListViewModel>> Update(
            int id, TobaccoAndAlcoholUseUpdateViewModel viewModel)
            => Ok(await this._service.Update(id, viewModel, this._appUtils.GetCurrentUser(HttpContext)));

    }
}
