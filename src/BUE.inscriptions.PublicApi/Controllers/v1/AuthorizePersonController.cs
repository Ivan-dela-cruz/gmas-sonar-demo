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
    public class AuthorizePersonController : ControllerBase
    {
        private readonly IAuthorizePersonService _authorizePersonSer;
        public AuthorizePersonController(IAuthorizePersonService authorizePersonSer) => _authorizePersonSer = authorizePersonSer;

        [HttpGet]
        [Route("portal/authorize-people"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromQuery] PagingQueryParameters paging)
        {
            var authorizePeople = await _authorizePersonSer.GetServiceAsync(paging);
            if (authorizePeople.Data is null)
            {
                return NotFound(authorizePeople);
            }
            var metadata = new
            {
                authorizePeople.Data.TotalCount,
                authorizePeople.Data.PageSize,
                authorizePeople.Data.CurrentPage,
                authorizePeople.Data.TotalPages,
                authorizePeople.Data.HasNext,
                authorizePeople.Data.HasPrevious
            };
            Response?.Headers?.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(authorizePeople);
        }

        [HttpGet]
        [Route("portal/authorize-people/{id:int}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(MessageUtil.Instance.NotFound);
            }
            var authorizePerson = await _authorizePersonSer.GetByIdServiceAsync(id);
            if (authorizePerson.Data is null)
            {
                return NotFound(authorizePerson);
            }
            return Ok(authorizePerson);
        }
        [HttpGet]
        [Route("portal/authorize-people/request/{id:int}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByRequestId(int id)
        {
            if (id <= 0)
            {
                return BadRequest(MessageUtil.Instance.NotFound);
            }
            var authorizePerson = await _authorizePersonSer.GetByRequestIdServiceAsync(id);
            if (authorizePerson.Data is null)
            {
                return NotFound(authorizePerson);
            }
            return Ok(authorizePerson);
        }

        [HttpPost]
        [Route("portal/authorize-people"), Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] AuthorizePeopleDTO authorizePersonDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var personExist = await _authorizePersonSer.GetByIdentificationServiceAsync(authorizePersonDTO.documentNumber, (int)authorizePersonDTO.currentSchoolYear, (int)authorizePersonDTO.portalRequestCode);
            if (personExist.status == true)
            {
                personExist.status = false;
                personExist.statusCode = MessageUtil.Instance.Identification_Already_Exist;
                return BadRequest(personExist);
            }
            var authorizePerson = await _authorizePersonSer.CreateServiceAsync(authorizePersonDTO);
            if (authorizePerson.Data is null)
            {
                return BadRequest(authorizePerson);
            }
            return CreatedAtAction(nameof(Get), authorizePerson);
        }
        [HttpPost]
        [Route("portal/authorize-people/list"), Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateMany([FromBody] IEnumerable<AuthorizePeopleDTO> entities)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var authorizePerson = await _authorizePersonSer.StoreServiceAsync(entities);
            if (authorizePerson.Data is null)
            {
                return BadRequest(authorizePerson);
            }
            return CreatedAtAction(nameof(Get), authorizePerson);
        }

        [HttpPut]
        [Route("portal/authorize-people/{id}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] AuthorizePeopleDTO authorizePersonDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var authorizePerson = await _authorizePersonSer.UpdateServiceAsync(id, authorizePersonDTO);
            if (authorizePerson.Data is null)
            {
                return NotFound(authorizePerson);
            }
            return Ok(authorizePerson);
        }

        [HttpDelete]
        [Route("portal/authorize-people/{id}"), Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest(MessageUtil.Instance.NotFound);
            }
            var authorizePerson = await _authorizePersonSer.DeleteServiceAsync(id);
            if (authorizePerson.Data is false)
            {
                return NotFound(authorizePerson);
            }
            return NoContent();
        }
    }
}
