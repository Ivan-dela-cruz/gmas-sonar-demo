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
    public class CandidateElectionController : ControllerBase
    {
        private readonly ICandidateElectionService _candidateService;

        public CandidateElectionController(ICandidateElectionService candidateService)
        {
            _candidateService = candidateService;
        }

        [HttpGet]
        [Route("candidates-elections"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromQuery] PagingQueryParameters paging)
        {
            var candidates = await _candidateService.GetServiceAsync(paging);
            if (candidates.Data is null)
            {
                return NotFound(candidates);
            }

            var metadata = new
            {
                candidates.Data.TotalCount,
                candidates.Data.PageSize,
                candidates.Data.CurrentPage,
                candidates.Data.TotalPages,
                candidates.Data.HasNext,
                candidates.Data.HasPrevious
            };
            Response?.Headers?.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(candidates);
        }
        [HttpGet]
        [Route("candidates-elections/elections/{id}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByElection([FromQuery] PagingQueryParameters paging, int id)
        {
            var candidates = await _candidateService.GetByElectionIdServiceAsync(id, paging);
            if (candidates.Data is null)
            {
                return NotFound(candidates);
            }

            var metadata = new
            {
                candidates.Data.TotalCount,
                candidates.Data.PageSize,
                candidates.Data.CurrentPage,
                candidates.Data.TotalPages,
                candidates.Data.HasNext,
                candidates.Data.HasPrevious
            };
            Response?.Headers?.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(candidates);
        }

        [HttpGet]
        [Route("candidates-elections/{id}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var candidate = await _candidateService.GetByIdServiceAsync(id);
            if (candidate.Data is null)
            {
                return NotFound(candidate);
            }

            return Ok(candidate);
        }

        [HttpPost]
        [Route("candidates-elections"), Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CandidateElectionDTO candidateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var candidate = await _candidateService.CreateServiceAsync(candidateDTO);
            if (candidate.Data is null)
            {
                return BadRequest(candidate);
            }

            return CreatedAtAction(nameof(Get), candidate);
        }

        [HttpPut]
        [Route("candidates-elections/{id}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] CandidateElectionDTO candidateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var candidate = await _candidateService.UpdateServiceAsync(id, candidateDTO);
            if (candidate.Data is null)
            {
                return NotFound(candidate);
            }

            return Ok(candidate);
        }

        [HttpDelete]
        [Route("candidates-elections/{id}"), Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest(MessageUtil.Instance.NotFound);
            }

            var candidate = await _candidateService.DeleteServiceAsync(id);
            if (candidate.Data is false)
            {
                return NotFound(candidate);
            }

            return NoContent();
        }
    }
}
