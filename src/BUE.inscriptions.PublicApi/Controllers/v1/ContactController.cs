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
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactSer;
        public ContactController(IContactService contactSer) => _contactSer = contactSer;

        [HttpGet]
        [Route("bue/contacts"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromQuery] PagingQueryParameters paging)
        {
            var contacts = await _contactSer.GetServiceAsync(paging);
            if (contacts.Data is null)
            {
                return NotFound(contacts);
            }
            var metadata = new
            {
                contacts.Data.TotalCount,
                contacts.Data.PageSize,
                contacts.Data.CurrentPage,
                contacts.Data.TotalPages,
                contacts.Data.HasNext,
                contacts.Data.HasPrevious
            };
            Response?.Headers?.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(contacts);
        }

        [HttpGet]
        [Route("bue/contacts/{id:int}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(MessageUtil.Instance.NotFound);
            }
            var contact = await _contactSer.GetByIdServiceAsync(id);
            if (contact.Data is null)
            {
                return NotFound(contact);
            }
            return Ok(contact);
        }
        [HttpGet]
        [Route("bue/contacts/{id:int}/representatives"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRepresentativeByContactAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequest(MessageUtil.Instance.NotFound);
            }
            var contact = await _contactSer.GetRepresentativeByContactAsync(id);
            if (contact.Data is null)
            {
                return NotFound(contact);
            }
            return Ok(contact);
        }

        [HttpPost]
        [Route("bue/contacts"), Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] ContactDTO contactDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var contact = await _contactSer.CreateServiceAsync(contactDTO);
            if (contact.Data is null)
            {
                return BadRequest(contact);
            }

            return CreatedAtAction(nameof(Get), contact);
        }

        [HttpPut]
        [Route("bue/contacts/{id}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] ContactDTO contactDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var contact = await _contactSer.UpdateServiceAsync(id, contactDTO);
            if (contact.Data is null)
            {
                return NotFound(contact);
            }
            return Ok(contact);
        }

        [HttpDelete]
        [Route("bue/contacts/{id}"), Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest(MessageUtil.Instance.NotFound);
            }
            var contact = await _contactSer.DeleteServiceAsync(id);
            if (contact.Data is false)
            {
                return NotFound(contact);
            }
            return NoContent();
        }
    }
}
