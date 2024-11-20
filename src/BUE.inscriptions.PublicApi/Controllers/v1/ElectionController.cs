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
    public class ElectionController : ControllerBase
    {
        private readonly IElectionService _electionService;

        public ElectionController(IElectionService electionService)
        {
            _electionService = electionService;
        }

        [HttpGet]
        [Route("elections"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromQuery] PagingQueryParameters paging)
        {
            var elections = await _electionService.GetServiceAsync(paging);
            if (elections.Data is null)
            {
                return NotFound(elections);
            }

            var metadata = new
            {
                elections.Data.TotalCount,
                elections.Data.PageSize,
                elections.Data.CurrentPage,
                elections.Data.TotalPages,
                elections.Data.HasNext,
                elections.Data.HasPrevious
            };
            Response?.Headers?.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(elections);
        }

        [HttpGet]
        [Route("elections/{id}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var election = await _electionService.GetByIdServiceAsync(id);
            if (election.Data is null)
            {
                return NotFound(election);
            }

            return Ok(election);
        }

        [HttpPost]
        [Route("elections"), Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] ElectionDTO electionDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var election = await _electionService.CreateServiceAsync(electionDTO);
            if (election.Data is null)
            {
                return BadRequest(election);
            }

            return CreatedAtAction(nameof(Get), election);
        }

        [HttpPut]
        [Route("elections/{id}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] ElectionDTO electionDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var election = await _electionService.UpdateServiceAsync(id, electionDTO);
            if (election.Data is null)
            {
                return NotFound(election);
            }

            return Ok(election);
        }

        [HttpDelete]
        [Route("elections/{id}"), Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest(MessageUtil.Instance.NotFound);
            }

            var election = await _electionService.DeleteServiceAsync(id);
            if (election.Data is false)
            {
                return NotFound(election);
            }

            return NoContent();
        }
        [HttpGet]
        [Route("elections/levels/user/{id}")]
        //[Route("elections/levels/user/{id}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserLevels(int id)
        {
            if (id <= 0)
            {
                return BadRequest(MessageUtil.Instance.NotFound);
            }

            var levels = await _electionService.GetCoursesLevelByUserAsync(id);
            if (levels.Data is null)
            {
                return NotFound(levels);
            }

            return Ok(levels);
        }
    }
}
