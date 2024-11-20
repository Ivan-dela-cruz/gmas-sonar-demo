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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userSer;
        private readonly IMailNotification _mailNoti;
        private CryptoPassword _cryptoPassword = CryptoPassword.Instance;
        public UserController(IUserService userSer, IMailNotification mailNoti)
        {
            _userSer = userSer;
            _mailNoti = mailNoti;
        }


        [HttpGet]
        [Route("portal/users"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromQuery] PagingQueryParameters paging)
        {
            var users = await _userSer.GetServiceAsyncUsers(paging);
            if (users.Data is null)
            {
                users.status = false;
                users.Message = MessageUtil.Instance.NotFound;
                users.statusCode = MessageUtil.Instance.USER_NOT_FOUND;
                return NotFound(users);
            }
            var metadata = new
            {
                users.Data.TotalCount,
                users.Data.PageSize,
                users.Data.CurrentPage,
                users.Data.TotalPages,
                users.Data.HasNext,
                users.Data.HasPrevious
            };
            Response?.Headers?.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(users);
        }

        [HttpGet]
        [Route("portal/users/{id:int}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(MessageUtil.Instance.NotFound);
            }
            var user = await _userSer.GetByIdServiceAsync(id);
            if (user.Data is null)
            {
                user.status = false;
                user.Message = MessageUtil.Instance.UserNotFound;
                user.statusCode = MessageUtil.Instance.USER_NOT_FOUND;
                return NotFound(user);
            }
            return Ok(user);
        }
        [HttpGet]
        [Route("portal/users/contact/{id:int}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetWithContactByIdAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequest(MessageUtil.Instance.NotFound);
            }
            var user = await _userSer.GetWithContactByIdAsync(id);
            if (user.Data is null)
            {
                user.status = false;
                user.Message = MessageUtil.Instance.UserNotFound;
                user.statusCode = MessageUtil.Instance.USER_NOT_FOUND;
                return NotFound(user);
            }
            return Ok(user);
        }

        [HttpPost]
        [Route("portal/users")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] UserDTO userDto)
        {
            UserDTO newUser = userDto;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userFound = await _userSer.GetByMailOrIdidentificationAsync(newUser.Email);
            if (userFound.Data is not null)
            {
                userFound.status = false;
                userFound.Message = MessageUtil.Instance.EmailExist;
                userFound.statusCode = MessageUtil.Instance.USER_ALREADY_EXIST;
                return BadRequest(userFound);
            }
            _cryptoPassword.CreatePasswordHash(userDto.Password, out string passwordHash, out string passwordSalt);
            newUser.RememberToken = passwordHash;
            newUser.Password = passwordSalt;
            var user = await _userSer.CreateServiceAsync(newUser);
            if (user.Data is null)
            {
                return BadRequest(user);
            }
            var emailNotification = await _mailNoti.senMailNoticitionRegister(user.Data);
            return CreatedAtAction(nameof(Get), user);
        }
        [HttpPost]
        [Route("portal/users/password-generate")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreatePasswords([FromBody] List<UserPassDto> listDto)
        {
            List<UserPassDto> listDtoResult = new List<UserPassDto>();
            foreach (var userDto in listDto)
            {

                _cryptoPassword.CreatePasswordHash(userDto.Password, out string passwordHash, out string passwordSalt);
                listDtoResult.Add(new UserPassDto()
                {
                    UserId = userDto.UserId,
                    RememberToken = passwordHash,
                    Password = passwordSalt
                });
            }
            var result = await _userSer.updatePasswordsMassive(listDtoResult);
            return Ok(listDtoResult);
        }
        [HttpPut]
        [Route("portal/users/{id}/password"), Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> changePassword(int id, [FromBody] UserDTO userDto)
        {
            UserDTO newUser = userDto;
            _cryptoPassword.CreatePasswordHash(userDto.Password, out string passwordHash, out string passwordSalt);
            newUser.RememberToken = passwordHash;
            newUser.Password = passwordSalt;
            var user = await _userSer.changePasswordService(id, newUser);
            if (user.Data is null)
            {
                return BadRequest(user);
            }
            return Ok(user);
        }
        [HttpPost]
        [Route("email-exist-validate")]
        public async Task<IActionResult> emailValidate([FromBody] UserDTO userDto)
        {
            var userFound = await _userSer.GetByMailOrIdidentificationAsync(userDto.Email);
            if (userFound.Data is not null)
            {
                return BadRequest("USER_ALREADY_EXIST");
            }
            return Ok();
        }

        [HttpPut]
        [Route("portal/users/{id}"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] UserDTO userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _userSer.UpdateServiceAsync(id, userDto);
            if (user.Data is null)
            {
                user.status = false;
                user.Message = MessageUtil.Instance.UserNotFound;
                user.statusCode = MessageUtil.Instance.USER_NOT_FOUND;
                return NotFound(user);
            }
            return Ok(user);
        }

        [HttpDelete]
        [Route("portal/users/{id}"), Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest(MessageUtil.Instance.NotFound);
            }
            var user = await _userSer.DeleteServiceAsync(id);
            if (user.Data is false)
            {
                user.status = false;
                user.Message = MessageUtil.Instance.UserNotFound;
                user.statusCode = MessageUtil.Instance.USER_NOT_FOUND;
                return NotFound(user);
            }
            return NoContent();
        }
    }
}
