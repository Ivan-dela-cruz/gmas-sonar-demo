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
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/")]
    [ApiController]
    [Produces("application/json")]
    public class AcademicYearController : ControllerBase
    {
        private readonly IAcademicYearService _academicYearService;

        public AcademicYearController(IAcademicYearService academicYearService)
        {
            _academicYearService = academicYearService;
        }

        [HttpGet]
        [Route("academic-years"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromQuery] PagingQueryParameters paging)
        {
            var academicYears = await _academicYearService.GetServiceAsync(paging);
            if (academicYears.Data is null)
            {
                return NotFound(academicYears);
            }

            var metadata = new
            {
                academicYears.Data.TotalCount,
                academicYears.Data.PageSize,
                academicYears.Data.CurrentPage,
                academicYears.Data.TotalPages,
                academicYears.Data.HasNext,
                academicYears.Data.HasPrevious
            };
            Response?.Headers?.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(academicYears);
        }

        [HttpGet]
        [Route("academic-years/{id}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var academicYear = await _academicYearService.GetByIdServiceAsync(id);
            if (academicYear.Data is null)
            {
                return NotFound(academicYear);
            }

            return Ok(academicYear);
        }

        [HttpPost]
        [Route("academic-years"), Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] AcademicYearDTO academicYearDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var academicYear = await _academicYearService.CreateServiceAsync(academicYearDTO);
            if (academicYear.Data is null)
            {
                return BadRequest(academicYear);
            }

            return CreatedAtAction(nameof(Get), academicYear);
        }

        [HttpPut]
        [Route("academic-years/{id}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] AcademicYearDTO academicYearDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var academicYear = await _academicYearService.UpdateServiceAsync(id, academicYearDTO);
            if (academicYear.Data is null)
            {
                return NotFound(academicYear);
            }

            return Ok(academicYear);
        }

        [HttpDelete]
        [Route("academic-years/{id}"), Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest(MessageUtil.Instance.NotFound);
            }

            var academicYear = await _academicYearService.DeleteServiceAsync(id);
            if (academicYear.Data is false)
            {
                return NotFound(academicYear);
            }

            return NoContent();
        }
    }
}
