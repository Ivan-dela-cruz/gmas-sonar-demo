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
    public class PositionController : ControllerBase
    {
        private readonly IPositionService _positionService;

        public PositionController(IPositionService positionService)
        {
            _positionService = positionService;
        }

        [HttpGet]
        [Route("positions"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromQuery] PagingQueryParameters paging)
        {
            var positions = await _positionService.GetServiceAsync(paging);
            if (positions.Data is null)
            {
                return NotFound(positions);
            }

            var metadata = new
            {
                positions.Data.TotalCount,
                positions.Data.PageSize,
                positions.Data.CurrentPage,
                positions.Data.TotalPages,
                positions.Data.HasNext,
                positions.Data.HasPrevious
            };
            Response?.Headers?.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(positions);
        }

        [HttpGet]
        [Route("positions/{id}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var position = await _positionService.GetByIdServiceAsync(id);
            if (position.Data is null)
            {
                return NotFound(position);
            }

            return Ok(position);
        }

        [HttpPost]
        [Route("positions"), Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] PositionDTO positionDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var position = await _positionService.CreateServiceAsync(positionDTO);
            if (position.Data is null)
            {
                return BadRequest(position);
            }

            return CreatedAtAction(nameof(Get), position);
        }

        [HttpPut]
        [Route("positions/{id}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] PositionDTO positionDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var position = await _positionService.UpdateServiceAsync(id, positionDTO);
            if (position.Data is null)
            {
                return NotFound(position);
            }

            return Ok(position);
        }

        [HttpDelete]
        [Route("positions/{id}"), Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest(MessageUtil.Instance.NotFound);
            }

            var position = await _positionService.DeleteServiceAsync(id);
            if (position.Data is false)
            {
                return NotFound(position);
            }

            return NoContent();
        }
    }
}
