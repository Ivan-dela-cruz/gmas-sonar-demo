using Asp.Versioning;
using BUE.Inscriptions.Application.Interfaces;
using BUE.Inscriptions.Domain.Inscriptions.DTO;
using BUE.Inscriptions.Domain.Paging;
using BUE.Inscriptions.Shared.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BUE.Inscriptions.PublicApi.Controllers.v2
{
    [Route("api/v{version:apiVersion}/")]
    [ApiVersion("2.0")]
    [ApiController]
    [Produces("application/json")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        [Route("persons"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromQuery] PagingQueryParameters paging)
        {
            var persons = await _personService.GetServiceAsync(paging);
            if (persons.Data is null)
            {
                return NotFound(persons);
            }

            var metadata = new
            {
                persons.Data.TotalCount,
                persons.Data.PageSize,
                persons.Data.CurrentPage,
                persons.Data.TotalPages,
                persons.Data.HasNext,
                persons.Data.HasPrevious
            };
            Response?.Headers?.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(persons);
        }

        [HttpGet]
        [Route("persons/{id}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var person = await _personService.GetByIdServiceAsync(id);
            if (person.Data is null)
            {
                return NotFound(person);
            }

            return Ok(person);
        }
        [HttpGet]
        [Route("person/{identification}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdentification(string identification)
        {
            var person = await _personService.GetByIdentificationServiceAsync(identification);
            if (person.Data is null)
            {
                return NotFound(person);
            }

            return Ok(person);
        }

        [HttpPost]
        [Route("persons"), Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] PersonDTO personDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var person = await _personService.CreateServiceAsync(personDTO);
            if (person.Data is null)
            {
                return BadRequest(person);
            }

            return CreatedAtAction(nameof(Get), person);
        }

        [HttpPut]
        [Route("persons/{id}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] PersonDTO personDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            TraceLog(personDTO, "REQUEST");
            var person = await _personService.UpdateServiceAsync(id, personDTO);
            if (person.Data is null)
            {
                return NotFound(person);
            }
            TraceLog(person.Data, "RESPONSE");

            return Ok(person);
        }

        [HttpDelete]
        [Route("persons/{id}"), Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest(MessageUtil.Instance.NotFound);
            }

            var person = await _personService.DeleteServiceAsync(id);
            if (person.Data is false)
            {
                return NotFound(person);
            }

            return NoContent();
        }
        public void TraceLog(PersonDTO personDTO, string action = "REQUEST")
        {
            try
            {
                var result = JsonConvert.DeserializeObject<PersonDTO>(JsonConvert.SerializeObject(personDTO));
                result.FilesStorage = null;
                var jsonItem = JsonConvert.SerializeObject(result);
                LogManagement.Instance.write("PERSON", action, $"[{result.Id}] => [ " + jsonItem + " ]", "BUE.Inscriptions.PublicApi.Controllers.v2.PERSON");
            }
            catch (Exception e)
            {
            }
        }
    }
}
