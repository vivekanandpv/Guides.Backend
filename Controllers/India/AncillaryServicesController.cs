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
    [Route(EndpointStaticStore.AncillaryIndiaTemplate)]
    [ApiController]
    public class AncillaryServicesController : ControllerBase
    {
        private readonly IIndiaRespondentService _service;
        private readonly IAppUtils _appUtils;

        public AncillaryServicesController(IIndiaRespondentService service, IAppUtils appUtils)
        {
            _service = service;
            _appUtils = appUtils;
        }

        [HttpGet(EndpointStaticStore.GetFormStatusNavigator)]
        public async Task<ActionResult<FormStatusNavigatorViewModel>> GetFormStatusNavigator(int id) => Ok(await _service.GetFormStatusNavigator(id));
        
        [HttpGet(EndpointStaticStore.GetRespondentsWithFormStatus)]
        public async Task<ActionResult<IEnumerable<RespondentWithFormStatusViewModel>>> GetRespondentsWithFormStatus() => Ok(await _service.GetRespondentList());
    }
}
