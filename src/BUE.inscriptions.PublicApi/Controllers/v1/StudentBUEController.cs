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
    public class StudentBUEController : ControllerBase
    {
        private readonly IStudentBUEService _studentser;
        public StudentBUEController(IStudentBUEService studentser) => _studentser = studentser;

        [HttpGet]
        [Route("bue/students"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromQuery] PagingQueryParameters paging)
        {
            var students = await _studentser.GetServiceAsync(paging);
            if (students.Data is null)
            {
                return NotFound(students);
            }
            var metadata = new
            {
                students.Data.TotalCount,
                students.Data.PageSize,
                students.Data.CurrentPage,
                students.Data.TotalPages,
                students.Data.HasNext,
                students.Data.HasPrevious
            };
            Response?.Headers?.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(students);
        }

        [HttpGet]
        [Route("bue/students/{code}/{currentSchoolYear}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int code, int currentSchoolYear)
        {
            if (code == null || currentSchoolYear == null)
            {
                return BadRequest(MessageUtil.Instance.NotFound);
            }
            var student = await _studentser.GetByIdServiceAsync(code, currentSchoolYear);
            if (student.Data is null)
            {
                return NotFound(student);
            }
            return Ok(student);
        }

        [HttpPost]
        [Route("bue/students"), Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] StudentBUEDTO studentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var student = await _studentser.CreateServiceAsync(studentDto);
            if (student.Data is null)
            {
                return BadRequest(student);
            }
            return CreatedAtAction(nameof(Get), student);
        }

        [HttpPut]
        [Route("bue/students/{identificacion}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(string identificacion, [FromBody] StudentBUEDTO StudentDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var student = await _studentser.UpdateServiceAsync(identificacion, StudentDTO);
            if (student.Data is null)
            {
                return NotFound(student);
            }
            return Ok(student);
        }

        [HttpDelete]
        [Route("bue/students/{id}"), Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string identificacion)
        {
            if (identificacion is null || identificacion.Trim() == "")
            {
                return BadRequest(MessageUtil.Instance.NotFound);
            }
            var student = await _studentser.DeleteServiceAsync(identificacion);
            if (student.Data is false)
            {
                return NotFound(student);
            }
            return NoContent();
        }
    }
}
