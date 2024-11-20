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
    public class OrganizationElectionController : ControllerBase
    {
        private readonly IOrganizationElectionService _organizationElectionService;

        public OrganizationElectionController(IOrganizationElectionService organizationElectionService)
        {
            _organizationElectionService = organizationElectionService;
        }

        [HttpGet]
        [Route("organizations-elections"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromQuery] PagingQueryParameters paging)
        {
            var organizationElections = await _organizationElectionService.GetServiceAsync(paging);
            if (organizationElections.Data is null)
            {
                return NotFound(organizationElections);
            }

            var metadata = new
            {
                organizationElections.Data.TotalCount,
                organizationElections.Data.PageSize,
                organizationElections.Data.CurrentPage,
                organizationElections.Data.TotalPages,
                organizationElections.Data.HasNext,
                organizationElections.Data.HasPrevious
            };
            Response?.Headers?.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(organizationElections);
        }
        [HttpGet]
        [Route("organizations-elections/election/{id}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByElection([FromQuery] PagingQueryParameters paging, int id)
        {
            var organizationElections = await _organizationElectionService.GetByElectionIdServiceAsync(id, paging);
            if (organizationElections.Data is null)
            {
                return NotFound(organizationElections);
            }

            var metadata = new
            {
                organizationElections.Data.TotalCount,
                organizationElections.Data.PageSize,
                organizationElections.Data.CurrentPage,
                organizationElections.Data.TotalPages,
                organizationElections.Data.HasNext,
                organizationElections.Data.HasPrevious
            };
            Response?.Headers?.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(organizationElections);
        }

        [HttpGet]
        [Route("organizations-elections/{id}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var organizationElection = await _organizationElectionService.GetByIdServiceAsync(id);
            if (organizationElection.Data is null)
            {
                return NotFound(organizationElection);
            }

            return Ok(organizationElection);
        }

        [HttpPost]
        [Route("organizations-elections"), Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] OrganizationElectionDTO organizationElectionDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var organizationElection = await _organizationElectionService.CreateServiceAsync(organizationElectionDTO);
            if (organizationElection.Data is null)
            {
                return BadRequest(organizationElection);
            }

            return CreatedAtAction(nameof(Get), organizationElection);
        }

        [HttpPut]
        [Route("organizations-elections/{id}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] OrganizationElectionDTO organizationElectionDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var organizationElection = await _organizationElectionService.UpdateServiceAsync(id, organizationElectionDTO);
            if (organizationElection.Data is null)
            {
                return NotFound(organizationElection);
            }

            return Ok(organizationElection);
        }

        [HttpDelete]
        [Route("organizations-elections/{id}"), Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest(MessageUtil.Instance.NotFound);
            }

            var organizationElection = await _organizationElectionService.DeleteServiceAsync(id);
            if (organizationElection.Data is false)
            {
                return NotFound(organizationElection);
            }

            return NoContent();
        }
    }
}
