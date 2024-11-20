using Asp.Versioning;
using BUE.Inscriptions.Application.Interfaces;
using BUE.Inscriptions.Domain.Entity.DTO;
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
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        [Route("students"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromQuery] PagingQueryParameters paging)
        {
            var students = await _studentService.GetByAcademicYearAsync(paging);
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
        [Route("students/me/{id}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByUser([FromQuery] PagingQueryParameters paging, int id)
        {
            var students = await _studentService.GetByUserIdAsync(paging, id);
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
        [Route("students/{id}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var student = await _studentService.GetByIdServiceAsync(id);
            if (student.Data is null)
            {
                return NotFound(student);
            }

            return Ok(student);
        } 
        [HttpGet]
        [Route("students/{id}/details/{academicYearId}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetStudentDetails(int id, int academicYearId)
        {
            var student = await _studentService.GetStudentDetailsServiceAsync(id, academicYearId);
            if (student.Data is null)
            {
                return NotFound(student);
            }

            return Ok(student);
        }

        [HttpPost]
        [Route("students"), Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] StudentDTO studentDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var student = await _studentService.CreateServiceAsync(studentDTO);
            if (student.Data is null)
            {
                return BadRequest(student);
            }

            return CreatedAtAction(nameof(Get), student);
        }

        [HttpPut]
        [Route("students/{id}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] StudentDTO studentDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            TraceLog(studentDTO, "REQUEST");
            var student = await _studentService.UpdateServiceAsync(id, studentDTO);
            if (student.Data is null)
            {
                return NotFound(student);
            }
            TraceLog(student.Data, "RESPONSE");
            return Ok(student);
        }
        [HttpPut]
        [Route("students/{externalId}/image"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int externalId, [FromBody]  FileStorageDTO filesStorage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var student = await _studentService.UpdateImageServiceAsync(externalId, filesStorage);
            if (student.Data is null)
            {
                return NotFound(student);
            }
            return Ok(student);
        }

        [HttpDelete]
        [Route("students/{id}"), Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest(MessageUtil.Instance.NotFound);
            }

            var student = await _studentService.DeleteServiceAsync(id);
            if (student.Data is false)
            {
                return NotFound(student);
            }

            return NoContent();
        }
        public void TraceLog(StudentDTO studentDTO, string action = "REQUEST")
        {
            try
            {
                var result = JsonConvert.DeserializeObject<StudentDTO>(JsonConvert.SerializeObject(studentDTO));
                result.FilesStorage = null;
                var jsonItem = JsonConvert.SerializeObject(result);
                LogManagement.Instance.write("PERSON", action, $"[{result.Id}] => [ " + jsonItem + " ]", "BUE.Inscriptions.PublicApi.Controllers.v2.STUDENTS");
            }
            catch (Exception e)
            {
            }
        }
    }
}
