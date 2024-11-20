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
    public class StudentRepresentativeController : ControllerBase
    {
        private readonly IStudentRepresentativeService _strSer;
        public StudentRepresentativeController(IStudentRepresentativeService strSer) => _strSer = strSer;

        [HttpGet]
        [Route("bue/contacts/{code}/students/{currentYearSchool}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> geContactStudents([FromQuery] PagingQueryParameters paging, int code, int currentYearSchool)
        {
            if (code == null || currentYearSchool == null)
            {
                return BadRequest(MessageUtil.Instance.NotFound);
            }
            var student = await _strSer.getContactStudentsBUEService(paging, code, currentYearSchool);
            if (student.Data is null)
            {
                return NotFound(student);
            }
            return Ok(student);
        }
        [HttpGet]
        [Route("bue/people/students/{studentCode}/{currentYearSchool}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAuthPeopleStudents([FromQuery] PagingQueryParameters paging, int studentCode, int currentYearSchool)
        {
            if (studentCode == null || currentYearSchool == null)
            {
                return BadRequest(MessageUtil.Instance.NotFound);
            }
            var authPeople = await _strSer.GetAuthPeopleByStudentAsync(paging, studentCode, currentYearSchool);
            if (authPeople.Data is null)
            {
                return NotFound(authPeople);
            }
            return Ok(authPeople);
        }
        [HttpGet]
        [Route("bue/students/{studentCode}/{currentYearSchool}/contacts/{currentFirstRepresentative}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSecondContactByStudent(int studentCode, int currentYearSchool, int currentFirstRepresentative)
        {
            if (studentCode == null || currentYearSchool == null || currentFirstRepresentative == null)
            {
                return BadRequest(MessageUtil.Instance.Empty);
            }
            var secondContact = await _strSer.GetSecondContactByStudentAsync(studentCode, currentYearSchool, currentFirstRepresentative);
            if (secondContact.Data is null)
            {
                return NotFound(secondContact);
            }
            return Ok(secondContact);
        }
    }
}
