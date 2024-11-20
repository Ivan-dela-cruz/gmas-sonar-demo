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
    public class VoteController : ControllerBase
    {
        private readonly IVoteService _voteService;

        public VoteController(IVoteService voteService)
        {
            _voteService = voteService;
        }

        [HttpGet]
        [Route("votes"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromQuery] PagingQueryParameters paging)
        {
            var votes = await _voteService.GetServiceAsync(paging);
            if (votes.Data is null)
            {
                return NotFound(votes);
            }

            var metadata = new
            {
                votes.Data.TotalCount,
                votes.Data.PageSize,
                votes.Data.CurrentPage,
                votes.Data.TotalPages,
                votes.Data.HasNext,
                votes.Data.HasPrevious
            };
            Response?.Headers?.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(votes);
        }

        [HttpGet]
        [Route("votes/{id}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var vote = await _voteService.GetByIdServiceAsync(id);
            if (vote.Data is null)
            {
                return NotFound(vote);
            }

            return Ok(vote);
        }
        [HttpGet]
        [Route("votes/user/{id}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByUserId([FromQuery] PagingQueryParameters paging, int id)
        {
            var votes = await _voteService.GetByUserIdServiceAsync(id, paging);
            if (votes.Data is null)
            {
                return NotFound(votes);
            }

            var metadata = new
            {
                votes.Data.TotalCount,
                votes.Data.PageSize,
                votes.Data.CurrentPage,
                votes.Data.TotalPages,
                votes.Data.HasNext,
                votes.Data.HasPrevious
            };
            Response?.Headers?.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(votes);
        }
        [HttpGet]
        [Route("votes/{ElectionId}/{UserId}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByElectionAndUserId(int ElectionId, int UserId)
        {
            var vote = await _voteService.GetByElectionAndUserIdAsync(ElectionId, UserId);
            if (vote.Data is null)
            {
                return NotFound(vote);
            }

            return Ok(vote);
        }
        [HttpGet]
        [Route("votes/results/{ElectionId}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetVoteCountsByCandidateElectionAsync(PagingQueryParameters paging, int ElectionId)
        {
            var vote = await _voteService.GetVoteCountsByCandidateElectionAsync(paging, ElectionId);
            if (vote.Data is null)
            {
                return NotFound(vote);
            }

            return Ok(vote);
        }

        [HttpPost]
        [Route("votes"), Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] VoteDTO voteDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            voteDTO.VoteDate = DateTime.UtcNow;
            var vote = await _voteService.CreateServiceAsync(voteDTO);
            if (vote.Data is null)
            {
                return BadRequest(vote);
            }
            vote.Data = null;
            return CreatedAtAction(nameof(Get), vote);
        }

        [HttpPut]
        [Route("votes/{id}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] VoteDTO voteDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vote = await _voteService.UpdateServiceAsync(id, voteDTO);
            if (vote.Data is null)
            {
                return NotFound(vote);
            }

            return Ok(vote);
        }

        [HttpDelete]
        [Route("votes/{id}"), Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest(MessageUtil.Instance.NotFound);
            }

            var vote = await _voteService.DeleteServiceAsync(id);
            if (vote.Data is false)
            {
                return NotFound(vote);
            }

            return NoContent();
        }
    }
}
