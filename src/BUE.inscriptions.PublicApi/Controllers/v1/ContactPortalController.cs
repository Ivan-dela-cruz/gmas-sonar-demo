using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Paging;
using BUE.Inscriptions.Domain.Request;
using BUE.Inscriptions.Application.Interfaces;
using BUE.Inscriptions.Shared.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;
using Asp.Versioning;

namespace BUE.Inscriptions.PublicApi.Controllers.v1
{

    [Route("api/v{version:apiVersion}/")]
    [ApiVersion("1.0")]
    [ApiController]
    [Produces("application/json")]
    public class ContactPortalController : ControllerBase
    {
        private readonly IContactPortalService _contactSer;
        private readonly IPortalRequestService _requestSer;
        public ContactPortalController(IContactPortalService contactSer, IPortalRequestService requestSer)
        {
            _contactSer = contactSer;
            _requestSer = requestSer;
        }

        [HttpGet]
        [Route("portal/contacts"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromQuery] PagingQueryParameters paging)
        {
            var contacts = await _contactSer.GetServiceAsync(paging);
            if (contacts.Data is null)
            {
                return NotFound(contacts);
            }
            var metadata = new
            {
                contacts.Data.TotalCount,
                contacts.Data.PageSize,
                contacts.Data.CurrentPage,
                contacts.Data.TotalPages,
                contacts.Data.HasNext,
                contacts.Data.HasPrevious
            };
            Response?.Headers?.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(contacts);
        }
        [HttpGet]
        [Route("portal/contacts/list"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetList([FromQuery] PagingQueryParameters paging)
        {
            var contacts = await _contactSer.GetListBasicServiceAsync(paging);
            if (contacts.Data is null)
            {
                return NotFound(contacts);
            }
            var metadata = new
            {
                contacts.Data.TotalCount,
                contacts.Data.PageSize,
                contacts.Data.CurrentPage,
                contacts.Data.TotalPages,
                contacts.Data.HasNext,
                contacts.Data.HasPrevious
            };
            Response?.Headers?.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(contacts);
        }

        [HttpGet]
        [Route("portal/contacts/{id:int}/{currentYearSchool:int}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetContactCurrentSchoolAsync(int id, int currentYearSchool)
        {
            if (id <= 0)
            {
                return BadRequest(MessageUtil.Instance.NotFound);
            }
            var contact = await _contactSer.GetByCodeContactCurrentSchoolAsync(id, currentYearSchool);
            if (contact.Data is null)
            {
                return NotFound(contact);
            }
            return Ok(contact);
        }

        [HttpGet]
        [Route("portal/contacts/{id:int}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(MessageUtil.Instance.NotFound);
            }
            var contact = await _contactSer.GetByIdServiceAsync(id);
            if (contact.Data is null)
            {
                return NotFound(contact);
            }
            return Ok(contact);
        }
        [HttpGet]
        [Route("portal/contacts/validate"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ContactValidations([FromQuery] QueryValidation queryValidation)
        {
            if (queryValidation.type == "" || queryValidation.value == "")
            {
                return BadRequest(MessageUtil.Instance.NotFound);
            }
            if (queryValidation.type == "identification")
            {
                var contactIdentification = await _contactSer.GetByIdenditicationServiceAsync(queryValidation.value);
                return Ok(contactIdentification);
            }
            else if (queryValidation.type == "email")
            {
                var contactEmail = await _contactSer.GetByEmailServiceAsync(queryValidation.value);
                return Ok(contactEmail);
            }
            return BadRequest();
        }
        [HttpGet]
        [Route("portal/contacts/{email}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByEmail(string email)
        {
            if (email == null || email == "")
            {
                return BadRequest(MessageUtil.Instance.NotFound);
            }
            var contact = await _contactSer.GetByEmailServiceAsync(email);
            return Ok(contact);
        }


        [HttpPost]
        [Route("portal/contacts"), Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] ContactPortalDTO contactDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            #region TRACE CONTACT INSERT
            try
            {
                var resolveRQ = JsonConvert.SerializeObject(contactDTO);
                LogManagement.Instance.write("REQUEST", "INSERT", resolveRQ + "", $"BUE.Services.Public.Contact.{contactDTO.documentNumber}");
            }
            catch (Exception ex)
            {
                LogManagement.Instance.write(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, $"Error: {ex.Message}", "BUE.Services.Public.Error");

            }
            #endregion
            var newRequest = contactDTO.portalRequest;
            var beforeContactDto = contactDTO;
            beforeContactDto.portalRequest = null;
            var contact = await _contactSer.CreateServiceAsync(beforeContactDto);
            if (contact.Data is null)
            {
                return BadRequest(contact);
            }
            if (contactDTO.typeRepresentative == 1)
            {
                newRequest.contactCodeFirst = contact.Data.code;
                newRequest.statusFirstContact = 8;
            }
            else
            {
                newRequest.contactCodeSecond = contact.Data.code;
                newRequest.statusSecondContact = 8;
            }

            var request = await _requestSer.UpdateServiceAsync((int)newRequest.code, newRequest);
            request.Data.AuthorizePeople = null;
            request.Data.StudentPortal = null;
            request.Data.FirstContact = null;
            request.Data.SecondContact = null;
            request.Data.AuthorizePeople = null;
            contact.Data.portalRequest = request.Data;
            contact.Data.photo = null;
            contact.Data.documentFile = null;
            return CreatedAtAction(nameof(Get), contact);
        }

        [HttpPut]
        [Route("portal/contacts/{id}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] ContactPortalDTO contactDTO)
        {
            var newRequest = contactDTO.portalRequest;
            var beforeContact = contactDTO;
            beforeContact.portalRequest = null;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            #region TRACE CONTACT UPDATE
            try
            {
                var resolveRQ = JsonConvert.SerializeObject(contactDTO);
                LogManagement.Instance.write("REQUEST", "UPDATE", resolveRQ + "", $"BUE.Services.Public.Contact.{id}");
            }
            catch (Exception ex)
            {
                LogManagement.Instance.write(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, $"Error: {ex.Message}", "BUE.Services.Public.Error");

            }
            #endregion
            var contact = await _contactSer.UpdateServiceAsync(id, beforeContact);
            if (contact.Data is null)
            {
                return NotFound(contact);
            }
            if (newRequest is not null)
            {
                if (contactDTO.typeRepresentative == 1)
                    newRequest.contactCodeFirst = contact.Data.code;
                else
                    newRequest.contactCodeSecond = contact.Data.code;

                var request = await _requestSer.UpdateServiceAsync((int)newRequest.code, newRequest);
                request.Data.AuthorizePeople = null;
                request.Data.StudentPortal = null;
                request.Data.FirstContact = null;
                request.Data.SecondContact = null;
                request.Data.AuthorizePeople = null;
                contact.Data.portalRequest = request.Data;
                contact.Data.photo = null;
                contact.Data.documentFile = null;

            }
            try
            {
                var resolveRP = JsonConvert.SerializeObject(contact);
                LogManagement.Instance.write("RESPONSE", "UPDATE", resolveRP + "", $"BUE.Services.Public.Contact.{id}");
            }
            catch (Exception ex)
            {
                LogManagement.Instance.write(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, $"Error: {ex.Message}", "BUE.Services.Public.Contact.Error");

            }
            return Ok(contact);
        }

        [HttpDelete]
        [Route("portal/contacts/{id}"), Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest(MessageUtil.Instance.NotFound);
            }
            var contact = await _contactSer.DeleteServiceAsync(id);
            if (contact.Data is false)
            {
                return NotFound(contact);
            }
            return NoContent();
        }
    }
}
