using AutoMapper;
using BUE.Inscriptions.Domain.Entity;
using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Response;
using BUE.Inscriptions.Application.Interfaces;
using BUE.Inscriptions.Shared.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Asp.Versioning;

namespace BUE.Inscriptions.PublicApi.Controllers.v1
{
    [Route("api/v{version:apiVersion}/auth")]
    [ApiVersion("1.0")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private CryptoPassword _cryptoPassword = CryptoPassword.Instance;
        public static User user = new User();
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly IApplicationService _appService;
        private IMapper _mapper;
        private readonly IGeneralPortalService _generalSer;


        public AuthController(IConfiguration configuration, IUserService userService, IApplicationService appService, IMapper mapper, IGeneralPortalService generalSer)
        {
            _configuration = configuration;
            _userService = userService;
            _appService = appService;
            _mapper = mapper;
            _generalSer = generalSer;
        }

        [HttpGet("current"), Authorize]
        public async Task<ActionResult<CurrentUserResponse>> GetMe()
        {
            var userAuth = await _userService.GetUserAuth();
            if (userAuth.Data is null)
            {
                userAuth.Message = MessageUtil.Instance.UserNotFound;
                userAuth.statusCode = MessageUtil.Instance.USER_NOT_FOUND;
                return BadRequest(userAuth);
            }
            return Ok(userAuth);
        }
        [HttpPost("activate-account")]
        public async Task<ActionResult<CurrentUserResponse>> ActivateAccount([FromQuery(Name = "tokenActivateAccount")] string tokenActivateAccount)
        {
            var usersR = await _userService.GetByTokenActivateAsync(tokenActivateAccount);
            if (usersR.Data is null)
            {
                usersR.Message = MessageUtil.Instance.UserNotFound;
                usersR.statusCode = MessageUtil.Instance.USER_NOT_FOUND;
                return BadRequest(usersR);
            }
            var userDto = usersR.Data;
            var user = await _userService.updateStatusAccount((int)userDto.Code);
            if (user.Data is null)
            {
                user.Message = MessageUtil.Instance.UserNotFound;
                user.statusCode = MessageUtil.Instance.USER_NOT_FOUND;
                return BadRequest(user);
            }
            return Ok(user);
        }
        [HttpPost("reset-password")]
        public async Task<ActionResult<CurrentUserResponse>> ResetPassword([FromQuery(Name = "tokenActivateAccount")] string tokenActivateAccount)
        {
            var usersR = await _userService.GetByTokenActivateAsync(tokenActivateAccount);
            if (usersR.Data is null)
            {
                usersR.Message = MessageUtil.Instance.UserNotFound;
                usersR.statusCode = MessageUtil.Instance.USER_NOT_FOUND;
                return BadRequest(usersR);
            }
            var userDto = usersR.Data;
            var user = await _userService.updateStatusAccount((int)userDto.Code);
            if (user.Data is null)
            {
                user.Message = MessageUtil.Instance.UserNotFound;
                user.statusCode = MessageUtil.Instance.USER_NOT_FOUND;
                return BadRequest(user);
            }
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<CurrentUserResponse>> Login(AuthDTO request)
        {
            var usersR = await _userService.GetByMailOrIdidentificationAsync(request.Email);
            if (usersR.Data is null || !(bool)usersR.Data.Activo)
            {
                usersR.Message = MessageUtil.Instance.UserNotFound;
                usersR.statusCode = MessageUtil.Instance.USER_NOT_FOUND;
                usersR.Data = null;
                return BadRequest(usersR);
            }
            if (!_cryptoPassword.VerifyPasswordHash(request.Password, usersR.Data.Password, usersR.Data.RememberToken))
            {
                usersR.Message = MessageUtil.Instance.PasswordInvalid;
                usersR.statusCode = MessageUtil.Instance.WRONG_PASSWORD_OR_EMAIL;
                usersR.Data = null;
                return BadRequest(usersR);
            }
            string token = CreateToken(usersR.Data);
            var refreshToken = GenerateRefreshToken();
            SetRefreshToken(refreshToken);
            var appInfo = await _appService.getApplication();
            var resulToken = await _userService.RegisterToken((int)usersR.Data.Code, token);
            var userAuth = _mapper.Map<UserDTO, CurrentUserResponse>(resulToken.Data);
            var userRoles = await _userService.GetByIdServiceAsync((int)usersR.Data.Code);
            if (request.CurrentSchoolYear is not null)
            {
                var yearLogin = await _generalSer.getSchoolYearByCodeAsync((int)request.CurrentSchoolYear);
                userAuth.CurrentSchoolYear = yearLogin.Data;
            }
            userAuth.roles = userRoles.Data.roles;
            userAuth.application = appInfo.Data;
            return Ok(userAuth);
        }

        [HttpPost("refresh-token"), Authorize]
        public async Task<ActionResult<string>> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            if (!user.RefreshToken.Equals(refreshToken))
            {
                return Unauthorized("Invalid Refresh Token.");
            }
            else if (user.TokenExpires < DateTime.Now)
            {
                return Unauthorized("Token expired.");
            }

            string token = "";//CreateToken(user);
            var newRefreshToken = GenerateRefreshToken();
            SetRefreshToken(newRefreshToken);

            return Ok(token);
        }

        private RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(30),
                Created = DateTime.Now
            };

            return refreshToken;
        }

        private void SetRefreshToken(RefreshToken newRefreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires
            };
            Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

            user.RefreshToken = newRefreshToken.Token;
            user.TokenCreated = newRefreshToken.Created;
            user.TokenExpires = newRefreshToken.Expires;
        }

        private string CreateToken(UserDTO user)
        {
            int minutesExpiration = int.Parse(_configuration.GetSection("AppSettings:MinutesExpiration").Value);

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.PrimarySid, user.Identification),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(minutesExpiration),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
