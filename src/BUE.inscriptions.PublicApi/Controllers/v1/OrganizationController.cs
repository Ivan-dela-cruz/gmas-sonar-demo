using Asp.Versioning;
using Azure;
using BUE.Inscriptions.Application.Interfaces;
using BUE.Inscriptions.Domain.Elecctions.DTO;
using BUE.Inscriptions.Domain.Paging;
using BUE.Inscriptions.Shared.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BUE.Inscriptions.PublicApi.Controllers.v1
{
    [Route("api/v{version:apiVersion}/portal/")]
    [ApiVersion("1.0")]
    [ApiController]
    [Produces("application/json")]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationService _organizationService;

        public OrganizationController(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        [HttpGet]
        [Route("organizations"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromQuery] PagingQueryParameters paging)
        {
            var organizations = await _organizationService.GetServiceAsync(paging);
            if (organizations.Data is null)
            {
                return NotFound(organizations);
            }

            var metadata = new
            {
                organizations.Data.TotalCount,
                organizations.Data.PageSize,
                organizations.Data.CurrentPage,
                organizations.Data.TotalPages,
                organizations.Data.HasNext,
                organizations.Data.HasPrevious
            };
            Response?.Headers?.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(organizations);
        }
        [HttpGet]
        [Route("organizations/elections/{id}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByElection([FromQuery] PagingQueryParameters paging, int id)
        {
            var organizations = await _organizationService.GetByElectionIdServiceAsync(id, paging);
            if (organizations.Data is null)
            {
                return NotFound(organizations);
            }

            var metadata = new
            {
                organizations.Data.TotalCount,
                organizations.Data.PageSize,
                organizations.Data.CurrentPage,
                organizations.Data.TotalPages,
                organizations.Data.HasNext,
                organizations.Data.HasPrevious
            };
            Response?.Headers?.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(organizations);
        }

        [HttpGet]
        [Route("organizations/{id}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var organization = await _organizationService.GetByIdServiceAsync(id);
            if (organization.Data is null)
            {
                return NotFound(organization);
            }

            return Ok(organization);
        }

        [HttpPost]
        [Route("organizations"), Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] OrganizationDTO organizationDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var organization = await _organizationService.CreateServiceAsync(organizationDTO);
            if (organization.Data is null)
            {
                return BadRequest(organization);
            }

            return CreatedAtAction(nameof(Get), organization);
        }

        [HttpPut]
        [Route("organizations/{id}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] OrganizationDTO organizationDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var organization = await _organizationService.UpdateServiceAsync(id, organizationDTO);
            if (organization.Data is null)
            {
                return NotFound(organization);
            }

            return Ok(organization);
        }

        [HttpDelete]
        [Route("organizations/{id}"), Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest(MessageUtil.Instance.NotFound);
            }

            var organization = await _organizationService.DeleteServiceAsync(id);
            if (organization.Data is false)
            {
                return NotFound(organization);
            }

            return NoContent();
        }
    }
}
