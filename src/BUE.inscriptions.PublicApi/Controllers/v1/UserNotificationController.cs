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
    public class UserNotificationController : ControllerBase
    {
        private readonly IUserNotificationService _notiSer;
        public UserNotificationController(IUserNotificationService notiSer) => _notiSer = notiSer;

        [HttpGet]
        [Route("portal/user-notifications"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromQuery] PagingQueryParameters paging)
        {
            var roles = await _notiSer.GetServiceAsync(paging);
            if (roles.Data is null)
            {
                return NotFound(roles);
            }
            var metadata = new
            {
                roles.Data.TotalCount,
                roles.Data.PageSize,
                roles.Data.CurrentPage,
                roles.Data.TotalPages,
                roles.Data.HasNext,
                roles.Data.HasPrevious
            };
            Response?.Headers?.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(roles);
        }

        [HttpGet]
        [Route("portal/user-notifications/{id:int}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(MessageUtil.Instance.NotFound);
            }
            var userNoti = await _notiSer.GetByIdServiceAsync(id);
            if (userNoti.Data is null)
            {
                return NotFound(userNoti);
            }
            return Ok(userNoti);
        }
        [HttpGet]
        [Route("portal/user-notifications/{id:int}/user"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByUserId(int id)
        {
            if (id <= 0)
            {
                return BadRequest(MessageUtil.Instance.NotFound);
            }
            var userNoti = await _notiSer.GetByUserIdServiceAsync(id);
            if (userNoti.Data is null)
            {
                return NotFound(userNoti);
            }
            return Ok(userNoti);
        }

        [HttpGet]
        [Route("portal/user-notifications/{id:int}/student"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByStudentId(int id)
        {
            if (id <= 0)
            {
                return BadRequest(MessageUtil.Instance.NotFound);
            }
            var userNoti = await _notiSer.GetByStudentIdServiceAsync(id);
            if (userNoti.Data is null)
            {
                return NotFound(userNoti);
            }
            return Ok(userNoti);
        }


        [HttpPost]
        [Route("portal/user-notifications"), Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] UserNotificationDTO userNotificationDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userNoti = await _notiSer.CreateServiceAsync(userNotificationDTO);
            if (userNoti.Data is null)
            {
                return BadRequest(userNoti);
            }
            return CreatedAtAction(nameof(Get), userNoti);
        }

        [HttpPut]
        [Route("portal/user-notifications/{id}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] UserNotificationDTO UserNotificationDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userNoti = await _notiSer.UpdateServiceAsync(id, UserNotificationDTO);
            if (userNoti.Data is null)
            {
                return NotFound(userNoti);
            }
            return Ok(userNoti);
        }

        [HttpDelete]
        [Route("portal/user-notifications/{id}"), Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest(MessageUtil.Instance.NotFound);
            }
            var userNoti = await _notiSer.DeleteServiceAsync(id);
            if (userNoti.Data is false)
            {
                return NotFound(userNoti);
            }
            return NoContent();
        }
    }
}
