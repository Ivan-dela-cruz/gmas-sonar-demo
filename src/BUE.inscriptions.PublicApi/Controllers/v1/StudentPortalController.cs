using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Paging;
using BUE.Inscriptions.Application.Interfaces;
using BUE.Inscriptions.Shared.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection;
using BUE.Inscriptions.Domain.Request;
using Asp.Versioning;

namespace BUE.Inscriptions.PublicApi.Controllers.v1
{

    [Route("api/v{version:apiVersion}/")]
    [ApiVersion("1.0")]
    [ApiController]
    [Produces("application/json")]
    public class StudentPortalController : ControllerBase
    {
        private readonly IStudentPortalService _studentser;
        private readonly IPortalRequestService _requestSer;
        public StudentPortalController(IStudentPortalService studentser, IPortalRequestService requestSer)
        {
            _studentser = studentser;
            _requestSer = requestSer;
        }

        [HttpGet]
        [Route("portal/image"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int imageId)
        {
            return PhysicalFile(@"C:\imagesBue\3456.jpeg", "image/jpeg");
        }

        [HttpGet]
        [Route("portal/students"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromQuery] PagingQueryParameters paging)
        {
            var students = await _studentser.GetServiceAsync(paging);
            if (students.Data is null)
            {
                return NotFound(students);
            }
            var paginate = new Paginate()
            {
                TotalCount = students.Data.TotalCount,
                PageSize = students.Data.PageSize,
                CurrentPage = students.Data.CurrentPage,
                TotalPages = students.Data.TotalPages,
                HasNext = students.Data.HasNext,
                HasPrevious = students.Data.HasPrevious
            };
            students.paginate = paginate;
            return Ok(students);
        }

        [HttpPost]
        [Route("portal/students/validations"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetStudentsByCodes([FromBody] List<HelperFieldValidate> helperFields)
        {
            var students = await _studentser.GetStudentsByCodeAsync(helperFields);
            if (students.Data is null)
            {
                return NotFound(students);
            }
            return Ok(students);
        }

        [HttpGet]
        [Route("portal/students/list"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBasic([FromQuery] PagingQueryParameters paging)
        {
            var students = await _studentser.GetListBasicServiceAsync(paging);
            if (students.Data is null)
            {
                return NotFound(students);
            }
            var paginate = new Paginate()
            {
                TotalCount = students.Data.TotalCount,
                PageSize = students.Data.PageSize,
                CurrentPage = students.Data.CurrentPage,
                TotalPages = students.Data.TotalPages,
                HasNext = students.Data.HasNext,
                HasPrevious = students.Data.HasPrevious
            };
            students.paginate = paginate;
            return Ok(students);
        }

        [HttpGet]
        [Route("portal/students/{code}/{currentSchoolYear}"), Authorize]
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
        [Route("portal/students"), Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] StudentPortalDTO studentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            #region TRACE STUDENT INSERT
            try
            {
                var resolveRQ = JsonConvert.SerializeObject(studentDto);
                LogManagement.Instance.writeAssembly(MethodBase.GetCurrentMethod().DeclaringType, $"Data: {resolveRQ}", $".Public.Student.{studentDto.documentNumber}");
            }
            catch (Exception ex)
            {
                LogManagement.Instance.writeAssembly(MethodBase.GetCurrentMethod().DeclaringType, $"Data: {ex.Message}", ".Public.Error");
            }
            #endregion
            var student = await _studentser.CreateServiceAsync(studentDto);
            if (student.Data is null)
            {
                return BadRequest(student);
            }
            var newRequest = new PortalRequestDTO()
            {
                userCode = studentDto.codeUser,
                currentSchoolYear = student.Data.currentSchoolYear,
                studentCodeSchoolYear = student.Data.studentCodeSchoolYear,
                photoStatus = 1,
                requestStatus = 8,
                status = true,
                urlFile = "",
            };
            var request = await _requestSer.CreateServiceAsync(newRequest);
            newRequest.code = request.Data.code;
            student.Data.portalRequest = newRequest;
            student.Data.photo = null;
            student.Data.documentIdentification = null;
            return CreatedAtAction(nameof(Get), student);
        }

        [HttpPut]
        [Route("portal/students/{id}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] StudentPortalDTO studentPortalDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            #region TRACE STUDENT UPDATE
            try
            {
                var resolveRQ = JsonConvert.SerializeObject(studentPortalDTO);
                LogManagement.Instance.writeAssembly(MethodBase.GetCurrentMethod().DeclaringType, $"Data: {resolveRQ}", $".Public.Student.{id}");
            }
            catch (Exception ex)
            {
                LogManagement.Instance.writeAssembly(MethodBase.GetCurrentMethod().DeclaringType, $"Data: {ex.Message}", ".Public.Error");
            }
            #endregion
            var student = await _studentser.UpdateServiceAsync(id, studentPortalDTO);
            if (student.Data is null)
            {
                return NotFound(student);
            }
            student.Data.photo = null;
            student.Data.documentIdentification = null;
            return Ok(student);
        }

        [HttpDelete]
        [Route("portal/students/{id}"), Authorize]
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

        [HttpPost]
        [Route("portal/students/send-bue"), Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateToBue([FromBody] PortalRequestDTO requestDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var student = await _studentser.CrearteToBueServiceAsync(requestDTO);
            if (student.Data is null)
            {
                return BadRequest(student);
            }
            return CreatedAtAction(nameof(Get), student);
        }
        [HttpPut]
        [Route("portal/students/{id}/data-app"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateDataApp(int id, [FromBody] StudentPortalDTO studentPortalDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var student = await _studentser.UpdateEnrollmentAppServiceAsync(id, studentPortalDTO);
            if (student.Data is null)
            {
                return NotFound(student);
            }
            return Ok();
        }

    }
}
