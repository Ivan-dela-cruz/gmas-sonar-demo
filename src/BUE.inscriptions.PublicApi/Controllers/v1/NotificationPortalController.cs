using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;


namespace BUE.Inscriptions.PublicApi.Controllers.v1
{

    [Route("api/v{version:apiVersion}/")]
    [ApiVersion("1.0")]
    [ApiController]
    [Produces("application/json")]
    public class NotificationPortalController : ControllerBase
    {
        private readonly IMailNotification _mailNoti;
        public NotificationPortalController(IMailNotification mailNoti) => _mailNoti = mailNoti;

        [HttpPost("portal/notification/mail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> MailNotification([FromBody] MailNotificactionDTO notificactionDTO)
        {
            var mail = await _mailNoti.sendMailNotication(notificactionDTO);
            if (mail == null)
            {
                return NotFound(mail);
            }
            return Ok(mail);
        }

        [HttpPost("portal/notification/request-status"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> MailNotificationRequestStatus([FromBody] MailNotificactionDTO notificactionDTO)
        {
            var mail = await _mailNoti.sendMailNoticationWithTreating(notificactionDTO);
            if (mail == null)
            {
                return NotFound(mail);
            }
            return Ok(mail);
        }

        [HttpPost("portal/notification/reset-password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> MailNotificationResetPassword([FromBody] UserDTO userDto)
        {
            var mail = await _mailNoti.senMailNoticitionResetPassword(userDto);
            if (mail == null)
            {
                return NotFound(mail);
            }
            return Ok(mail);
        }


    }
}
