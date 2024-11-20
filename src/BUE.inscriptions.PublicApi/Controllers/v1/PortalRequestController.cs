using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Entity.DTO.storeProcedures;
using BUE.Inscriptions.Domain.Paging;
using BUE.Inscriptions.Application.Interfaces;
using BUE.Inscriptions.Shared.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Reflection;
using Newtonsoft.Json;
using BUE.Inscriptions.Domain.Request;
using Asp.Versioning;

namespace BUE.Inscriptions.PublicApi.Controllers.v1
{

    [Route("api/v{version:apiVersion}/")]
    [ApiVersion("1.0")]
    [ApiController]
    [Produces("application/json")]
    public class PortalRequestController : ControllerBase
    {
        private readonly IPortalRequestService _requestSer;
        public PortalRequestController(IPortalRequestService requestSer)
        {
            _requestSer = requestSer;
        }

        [HttpGet]
        [Route("portal/requests"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromQuery] PagingQueryParameters paging)
        {
            var roles = await _requestSer.GetServiceAsync(paging);
            if (roles.Data is null)
            {
                return NotFound(roles);
            }
            var paginate = new Paginate()
            {
                TotalCount = roles.Data.TotalCount,
                PageSize = roles.Data.PageSize,
                CurrentPage = roles.Data.CurrentPage,
                TotalPages = roles.Data.TotalPages,
                HasNext = roles.Data.HasNext,
                HasPrevious = roles.Data.HasPrevious
            };
            roles.paginate = paginate;
            return Ok(roles);
        }

        [HttpGet]
        [Route("portal/requests/representatives"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetWithRepresentatives([FromQuery] PagingQueryParameters paging)
        {
            var roles = await _requestSer.GetWithRepresentativeAsync(paging);
            if (roles.Data is null)
            {
                return NotFound(roles);
            }
            var paginate = new Paginate()
            {
                TotalCount = roles.Data.TotalCount,
                PageSize = roles.Data.PageSize,
                CurrentPage = roles.Data.CurrentPage,
                TotalPages = roles.Data.TotalPages,
                HasNext = roles.Data.HasNext,
                HasPrevious = roles.Data.HasPrevious
            };
            roles.paginate = paginate;
            return Ok(roles);
        }
        [HttpGet]
        [Route("portal/requests/second-representatives"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetWithSecondRepresentatives([FromQuery] PagingQueryParameters paging)
        {
            var roles = await _requestSer.GetWithSecondRepresentativeAsync(paging);
            if (roles.Data is null)
            {
                return NotFound(roles);
            }
            var paginate = new Paginate()
            {
                TotalCount = roles.Data.TotalCount,
                PageSize = roles.Data.PageSize,
                CurrentPage = roles.Data.CurrentPage,
                TotalPages = roles.Data.TotalPages,
                HasNext = roles.Data.HasNext,
                HasPrevious = roles.Data.HasPrevious
            };
            roles.paginate = paginate;
            return Ok(roles);
        }

        [HttpGet]
        [Route("portal/requests/{status:int}/{currentYearSchool:int}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByStatusImg([FromQuery] PagingQueryParameters paging, int status, int currentYearSchool)
        {
            var roles = await _requestSer.GetByFilterServiceAsync(paging, status, currentYearSchool);
            if (roles.Data is null)
            {
                return NotFound(roles);
            }
            var paginate = new Paginate()
            {
                TotalCount = roles.Data.TotalCount,
                PageSize = roles.Data.PageSize,
                CurrentPage = roles.Data.CurrentPage,
                TotalPages = roles.Data.TotalPages,
                HasNext = roles.Data.HasNext,
                HasPrevious = roles.Data.HasPrevious
            };
            roles.paginate = paginate;
            return Ok(roles);
        }
        [HttpGet]
        [Route("portal/me-requests/{userCode:int}/{currentYearSchool:int}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByUserRequests([FromQuery] PagingQueryParameters paging, int userCode, int currentYearSchool)
        {
            var roles = await _requestSer.GetByUserServiceAsync(paging, userCode, currentYearSchool);
            if (roles.Data is null)
            {
                return NotFound();
            }
            var paginate = new Paginate()
            {
                TotalCount = roles.Data.TotalCount,
                PageSize = roles.Data.PageSize,
                CurrentPage = roles.Data.CurrentPage,
                TotalPages = roles.Data.TotalPages,
                HasNext = roles.Data.HasNext,
                HasPrevious = roles.Data.HasPrevious
            };
            roles.paginate = paginate;
            return Ok(roles);
        }
        [HttpPost]
        [Route("portal/requests/validations"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRequestsValidations([FromBody] List<HelperFieldValidate> helperFields)
        {
            var requests = await _requestSer.GetStudentsByCodeAsync(helperFields);
            if (requests.Data is null)
            {
                return NotFound();
            }
            return Ok(requests);
        }

        [HttpGet]
        [Route("portal/requests/{id:int}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(MessageUtil.Instance.NotFound);
            }
            var request = await _requestSer.GetByIdServiceAsync(id);
            if (request.Data is null)
            {
                return NotFound(request);
            }
            try
            {
                var resolve = JsonConvert.SerializeObject(request);
                LogManagement.Instance.write("", "", resolve + "", $"BUE.Services.Public.{id}");
            }
            catch (Exception ex)
            {
                LogManagement.Instance.write(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, $"Error: {ex.Message}", "BUE.Services.Public.Error");

            }
            return Ok(request);
        }

        [HttpPost]
        [Route("portal/requests"), Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] PortalRequestDTO rolDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var role = await _requestSer.CreateServiceAsync(rolDto);
            if (role.Data is null)
            {
                return BadRequest(role);
            }
            return CreatedAtAction(nameof(Get), role);
        }

        [HttpPut]
        [Route("portal/requests/{id}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] PortalRequestDTO PortalRequestDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _requestSer.UpdateServiceAsync(id, PortalRequestDTO);
            if (response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        [HttpPut]
        [Route("portal/requests/any"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAny(int id, [FromBody] IEnumerable<PortalRequestDTO> requests)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var role = await _requestSer.UpdateAnyServiceAsync(requests);
            if (role.Data is null)
            {
                return NotFound(role);
            }
            return Ok(role);
        }

        [HttpPut]
        [Route("portal/requests/status"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAnyStatus(int id, [FromBody] IEnumerable<PortalRequestDTO> requests)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var role = await _requestSer.UpdateStatusServiceAsync(requests);
            if (role.Data is null)
            {
                return NotFound(role);
            }
            return Ok(role);
        }

        [HttpPut]
        [Route("portal/requests/notes"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAnyNotes(int id, [FromBody] IEnumerable<PortalRequestDTO> requests)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var role = await _requestSer.UpdateNotesServiceAsync(requests);
            if (role.Data is null)
            {
                return NotFound(role);
            }
            return Ok(role);
        }
        [HttpDelete]
        [Route("portal/requests/{id}"), Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest(MessageUtil.Instance.NotFound);
            }
            var role = await _requestSer.DeleteServiceAsync(id);
            if (role.Data is false)
            {
                return NotFound(role);
            }
            return NoContent();
        }

        [HttpPost]
        [Route("portal/requests/register-integrations"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> createIntegrations([FromBody] IntegrationRegisterDTO requests)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (requests.currentYearSchool <= 0 || requests.userCode <= 0 || requests.requestCodes == "")
            {
                return BadRequest(MessageUtil.Instance.ERROR_DATA);
            }
            var reponse = await _requestSer.CreateIntegrationAsync(requests);

            return Ok(reponse);
        }
        [HttpPost]
        [Route("portal/requests/register-integrations/first-contact"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> createIntegrationsFirstContact([FromBody] IntegrationRegisterDTO requests)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (requests.currentYearSchool <= 0 || requests.userCode <= 0 || requests.requestCodes == "")
            {
                return BadRequest(MessageUtil.Instance.ERROR_DATA);
            }
            var reponse = await _requestSer.CreateIntegrationFirstContactAsync(requests);

            return Ok(reponse);
        }
        [HttpPost]
        [Route("portal/requests/register-integrations/second-contact"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> createIntegrationsSecondContact([FromBody] IntegrationRegisterDTO requests)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (requests.currentYearSchool <= 0 || requests.userCode <= 0 || requests.requestCodes == "")
            {
                return BadRequest(MessageUtil.Instance.ERROR_DATA);
            }
            var reponse = await _requestSer.CreateIntegrationSecondContactAsync(requests);

            return Ok(reponse);
        }
        [HttpPost]
        [Route("portal/requests/register-integrations/autorization-people"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> createIntegrationsAutorizationPeople([FromBody] IntegrationRegisterDTO requests)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (requests.currentYearSchool <= 0 || requests.userCode <= 0 || requests.requestCodes == "")
            {
                return BadRequest(MessageUtil.Instance.ERROR_DATA);
            }
            var reponse = await _requestSer.CreateIntegrationAutorizationPeopleAsync(requests);

            return Ok(reponse);
        }
        [HttpPost]
        [Route("portal/requests/integrations/autorization-people"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ExecuteIntegrationAutorizationPeople([FromBody] IntegrationRegisterDTO requests)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (requests.currentYearSchool <= 0 || requests.currentYearSchool == null)
            {
                return BadRequest(MessageUtil.Instance.ERROR_DATA);
            }
            var reponse = await _requestSer.ExecuteIntegrationAutorizationPeopleAsync(requests);

            return Ok(reponse);
        }
        [HttpPost]
        [Route("portal/requests/integrations/students"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ExecuteIntegrationStudents([FromBody] IntegrationRegisterDTO requests)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (requests.currentYearSchool <= 0 || requests.currentYearSchool == null)
            {
                return BadRequest(MessageUtil.Instance.ERROR_DATA);
            }
            var reponse = await _requestSer.ExecuteIntegrationStudentsAsync(requests);

            return Ok(reponse);
        }
        [HttpPost]
        [Route("portal/requests/integrations/contacts"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ExecuteIntegrationContacts([FromBody] IntegrationRegisterDTO requests)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (requests.currentYearSchool <= 0 || requests.currentYearSchool == null)
            {
                return BadRequest(MessageUtil.Instance.ERROR_DATA);
            }
            var reponse = await _requestSer.ExecuteIntegrationContactsAsync(requests);

            return Ok(reponse);
        }

        [HttpGet]
        [Route("portal/dashboard/charts"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRptDashBoards([FromQuery] IntegrationRegisterDTO requests)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (requests.currentYearSchool <= 0 || requests.currentYearSchool == null)
            {
                return BadRequest(MessageUtil.Instance.ERROR_DATA);
            }
            var reponse = await _requestSer.GetDashboardServiceAsync(requests);

            return Ok(reponse);
        }
        [HttpGet]
        [Route("portal/welcome/students/{userId}/{currentYearSchool}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetStudentsAndRequestsByUserAsync(int userId, int currentYearSchool)
        {
            if (userId <= 0 || currentYearSchool == null)
            {
                return BadRequest(MessageUtil.Instance.ERROR_DATA);
            }
            var reponse = await _requestSer.GetStudentsAndRequestsByUserAsync(userId, currentYearSchool);

            return Ok(reponse);
        }
    }
}
