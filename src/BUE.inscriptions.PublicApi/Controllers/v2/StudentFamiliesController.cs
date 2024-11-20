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
    public class StudentFamiliesController : ControllerBase
    {
        private readonly IStudentFamiliesService _studentFamiliesService;

        public StudentFamiliesController(IStudentFamiliesService studentFamiliesService)
        {
            _studentFamiliesService = studentFamiliesService;
        }

        [HttpGet]
        [Route("student-families"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromQuery] PagingQueryParameters paging)
        {
            var studentFamilies = await _studentFamiliesService.GetServiceAsync(paging);
            if (studentFamilies.Data is null)
            {
                return NotFound(studentFamilies);
            }

            var metadata = new
            {
                studentFamilies.Data.TotalCount,
                studentFamilies.Data.PageSize,
                studentFamilies.Data.CurrentPage,
                studentFamilies.Data.TotalPages,
                studentFamilies.Data.HasNext,
                studentFamilies.Data.HasPrevious
            };
            Response?.Headers?.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(studentFamilies);
        }

        [HttpGet]
        [Route("student-families/{id}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var studentFamilies = await _studentFamiliesService.GetByIdServiceAsync(id);
            if (studentFamilies.Data is null)
            {
                return NotFound(studentFamilies);
            }

            return Ok(studentFamilies);
        }

        [HttpPost]
        [Route("student-families"), Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] StudentFamiliesDTO studentFamiliesDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var studentFamilies = await _studentFamiliesService.CreateServiceAsync(studentFamiliesDTO);
            if (studentFamilies.Data is null)
            {
                return BadRequest(studentFamilies);
            }

            return CreatedAtAction(nameof(Get), studentFamilies);
        }

        [HttpPut]
        [Route("student-families/{id}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] StudentFamiliesDTO studentFamiliesDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var studentFamilies = await _studentFamiliesService.UpdateServiceAsync(id, studentFamiliesDTO);
            if (studentFamilies.Data is null)
            {
                return NotFound(studentFamilies);
            }

            return Ok(studentFamilies);
        }

        [HttpDelete]
        [Route("student-families/{id}"), Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest(MessageUtil.Instance.NotFound);
            }

            var studentFamilies = await _studentFamiliesService.DeleteServiceAsync(id);
            if (studentFamilies.Data is false)
            {
                return NotFound(studentFamilies);
            }

            return NoContent();
        }
    }
}
