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
    public class CandidateOrganizationController : ControllerBase
    {
        private readonly ICandidateOrganizationService _candidateOrganizationService;

        public CandidateOrganizationController(ICandidateOrganizationService candidateOrganizationService)
        {
            _candidateOrganizationService = candidateOrganizationService;
        }

        [HttpGet]
        [Route("candidates-organizations"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromQuery] PagingQueryParameters paging)
        {
            var candidateOrganizations = await _candidateOrganizationService.GetServiceAsync(paging);
            if (candidateOrganizations.Data is null)
            {
                return NotFound(candidateOrganizations);
            }

            var metadata = new
            {
                candidateOrganizations.Data.TotalCount,
                candidateOrganizations.Data.PageSize,
                candidateOrganizations.Data.CurrentPage,
                candidateOrganizations.Data.TotalPages,
                candidateOrganizations.Data.HasNext,
                candidateOrganizations.Data.HasPrevious
            };
            Response?.Headers?.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(candidateOrganizations);
        }
        [HttpGet]
        [Route("candidates-organizations/organization/{id}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByOrganization([FromQuery] PagingQueryParameters paging, int id)
        {
            var candidateOrganizations = await _candidateOrganizationService.GetByOrganizationIdServiceAsync(id, paging);
            if (candidateOrganizations.Data is null)
            {
                return NotFound(candidateOrganizations);
            }

            var metadata = new
            {
                candidateOrganizations.Data.TotalCount,
                candidateOrganizations.Data.PageSize,
                candidateOrganizations.Data.CurrentPage,
                candidateOrganizations.Data.TotalPages,
                candidateOrganizations.Data.HasNext,
                candidateOrganizations.Data.HasPrevious
            };
            Response?.Headers?.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(candidateOrganizations);
        }

        [HttpGet]
        [Route("candidates-organizations/{id}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var candidateOrganization = await _candidateOrganizationService.GetByIdServiceAsync(id);
            if (candidateOrganization.Data is null)
            {
                return NotFound(candidateOrganization);
            }

            return Ok(candidateOrganization);
        }

        [HttpPost]
        [Route("candidates-organizations"), Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CandidateOrganizationDTO candidateOrganizationDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var candidateOrganization = await _candidateOrganizationService.CreateServiceAsync(candidateOrganizationDTO);
            if (candidateOrganization.Data is null)
            {
                return BadRequest(candidateOrganization);
            }

            return CreatedAtAction(nameof(Get), candidateOrganization);
        }

        [HttpPut]
        [Route("candidates-organizations/{id}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] CandidateOrganizationDTO candidateOrganizationDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var candidateOrganization = await _candidateOrganizationService.UpdateServiceAsync(id, candidateOrganizationDTO);
            if (candidateOrganization.Data is null)
            {
                return NotFound(candidateOrganization);
            }

            return Ok(candidateOrganization);
        }

        [HttpDelete]
        [Route("candidates-organizations/{organizationId}/{candidateId}"), Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int organizationId, int candidateId)
        {
            if (organizationId == 0)
            {
                return NotFound(MessageUtil.Instance.NotFound);
            }
            if (organizationId <= 0 && candidateId <= 0)
            {
                return BadRequest(MessageUtil.Instance.NotFound);
            }

            var candidateOrganization = await _candidateOrganizationService.DeleteCandidateOrganizationServiceAsync(organizationId, candidateId);
            if (candidateOrganization.Data is false)
            {
                return NotFound(candidateOrganization);
            }

            return NoContent();
        }
    }
}
