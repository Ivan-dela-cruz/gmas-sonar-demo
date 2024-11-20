using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Paging;
using BUE.Inscriptions.Application.Interfaces;
using BUE.Inscriptions.Shared.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Asp.Versioning;

namespace BUE.Inscriptions.PublicApi.Controllers.v1
{
    [Route("api/v{version:apiVersion}/")]
    [ApiVersion("1.0")]
    [ApiController]
    [Produces("application/json")]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService _appSer;
        public ApplicationController(IApplicationService appSer) => _appSer = appSer;

        [HttpGet]
        [Route("index")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> index()
        {
            return Ok("conected success");
        }

        [HttpGet]
        [Route("portal/applications"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetApplication()
        {

            var model = await _appSer.getApplication();
            if (model.Data is null)
            {
                return NotFound(model);
            }
            return Ok(model);
        }



        [HttpPut]
        [Route("portal/applications/{id}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] ApplicationDTO ApplicationDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var model = await _appSer.UpdateServiceAsync(id, ApplicationDTO);
            if (model.Data is null)
            {
                return NotFound(model);
            }
            return Ok(model);
        }


    }
}
