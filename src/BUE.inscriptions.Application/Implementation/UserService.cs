using AutoMapper;
using BUE.Inscriptions.Infrastructure.Interfaces;
using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Paging;
using BUE.Inscriptions.Domain.Response;
using BUE.Inscriptions.Application.Interfaces;
using BUE.Inscriptions.Shared.Utils;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace BUE.Inscriptions.Application.Implementation
{
    public class UserService : IUserService
    {
        private CryptoPassword _cryptoPassword = CryptoPassword.Instance;
        private IUserRepository _userRep;
        private IMapper _mapper;
        public UserService(IUserRepository userRep, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _userRep = userRep;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }
        private readonly IHttpContextAccessor _httpContextAccessor;
        public async Task<IBaseResponse<UserDTO>> CreateServiceAsync(UserDTO model)
        {
            var baseResponse = new BaseResponse<UserDTO>();
            var user = await _userRep.CreateAsync(model);
            if (user is null)
            {
                baseResponse.Message = MessageUtil.Instance.Warning;
                baseResponse.status = false;
            }
            var roleId = model.RoleId == null ? 2 : model.RoleId;
            var listRoleUser = new int[] { Convert.ToInt16(roleId) };
            var res = await _userRep.AssingRolesAsync((int)user.Code, listRoleUser);
            baseResponse.Data = user;
            return baseResponse;
        }
        public async Task<IBaseResponse<UserDTO>> CreateWithPassServiceAsync(UserDTO model)
        {
            var baseResponse = new BaseResponse<UserDTO>();
            var userFoundId = await _userRep.GetByMailOrIdidentificationAsync(model.Identification);
            var userFoundEmail = await _userRep.GetByMailOrIdidentificationAsync(model.Email);
            if (userFoundId != null || userFoundEmail != null)
            {
                baseResponse.Message = MessageUtil.Instance.Identification_Already_Exist;
                baseResponse.statusCode = MessageUtil.Instance.USER_ALREADY_EXIST;
                baseResponse.status = false;
                return baseResponse;
            }
            _cryptoPassword.CreatePasswordHash(model.Password, out string passwordHash, out string passwordSalt);
            model.RememberToken = passwordHash;
            model.Password = passwordSalt;
            var user = await _userRep.CreateAsync(model);
            if (user is null)
            {
                baseResponse.Message = MessageUtil.Instance.Warning;
                baseResponse.status = false;
            }
            var roleId = model.RoleId == null ? 2 : model.RoleId;
            var listRoleUser = new int[] { Convert.ToInt16(roleId) };
            var res = await _userRep.AssingRolesAsync((int)user.Code, listRoleUser);
            baseResponse.Data = user;
            return baseResponse;
        }
        public async Task<IBaseResponse<bool>> DeleteServiceAsync(int id)
        {
            var baseResponse = new BaseResponse<bool>();
            bool IsSuccess = await _userRep.DeleteAsync(id);
            if (!IsSuccess)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = IsSuccess;
            return baseResponse;
        }
        public async Task<IBaseResponse<UserDTO>> GetByIdServiceAsync(int id)
        {
            var baseResponse = new BaseResponse<UserDTO>();
            UserDTO user = await _userRep.GetByIdAsync(id);
            if (user is null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = user;
            return baseResponse;
        }
        public async Task<IBaseResponse<UserDTO>> GetWithContactByIdAsync(int id)
        {
            var baseResponse = new BaseResponse<UserDTO>();
            UserDTO user = await _userRep.GetWithContactByIdAsync(id);
            if (user is null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = user;
            return baseResponse;
        }
        public async Task<IBaseResponse<UserDTO>> GetByMailOrIdidentificationAsync(string EmailOrIdentification)
        {
            var baseResponse = new BaseResponse<UserDTO>();
            UserDTO user = await _userRep.GetByMailOrIdidentificationAsync(EmailOrIdentification);
            if (user is null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = user;
            return baseResponse;
        }
        public async Task<IBaseResponse<UserDTO>> GetByTokenActivateAsync(string token)
        {
            var baseResponse = new BaseResponse<UserDTO>();
            UserDTO user = await _userRep.GetByTokenActivateAsync(token);
            if (user is null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = user;
            return baseResponse;
        }
        public async Task<IBaseResponse<PagedList<UserDTO>>> GetServiceAsync(PagingQueryParameters paging)
        {
            var baseResponse = new BaseResponse<PagedList<UserDTO>>();
            var users = await _userRep.GetAsync();
            if (users is null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = PagedList<UserDTO>.ToPagedList(users, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }
        public async Task<IBaseResponse<PagedList<UserResponse>>> GetServiceAsyncUsers(PagingQueryParameters paging)
        {
            var baseResponse = new BaseResponse<PagedList<UserResponse>>();
            var users = await _userRep.GetAsyncUsers();
            if (users is null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = PagedList<UserResponse>.ToPagedList(users, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }
        public async Task<IBaseResponse<UserDTO>> UpdateServiceAsync(int id, UserDTO model)
        {

            var baseResponse = new BaseResponse<UserDTO>();
            UserDTO user = await _userRep.UpdateAsync(id, model);
            baseResponse.Message = MessageUtil.Instance.Updated;
            baseResponse.Data = user;
            return baseResponse;
        }
        public async Task<IBaseResponse<UserDTO>> UpdateDataServiceAsync(int id, UserDTO model)
        {
            try
            {
                BaseResponse<UserDTO> baseResponse = new BaseResponse<UserDTO>();
                UserDTO user = await _userRep.UpdateDataAsync(id, model);
                baseResponse.Message = MessageUtil.Instance.Updated;
                baseResponse.Data = user;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<IBaseResponse<CurrentUserResponse>> GetUserAuth()
        {
            var EmailOrIdentification = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                EmailOrIdentification = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.PrimarySid);
            }

            var baseResponse = new BaseResponse<CurrentUserResponse>();
            UserDTO user = await _userRep.GetByMailOrIdidentificationAsync(EmailOrIdentification);
            if (user is null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = _mapper.Map<UserDTO, CurrentUserResponse>(user);
            return baseResponse;
        }

        public async Task<IBaseResponse<UserDTO>> RegisterToken(int id, string token)
        {
            var baseResponse = new BaseResponse<UserDTO>();
            UserDTO user = await _userRep.RegisterToken(id, token);
            baseResponse.Message = MessageUtil.Instance.Updated;
            baseResponse.Data = user;
            return baseResponse;
        }
        public async Task<IBaseResponse<UserDTO>> updateStatusAccount(int id)
        {
            var baseResponse = new BaseResponse<UserDTO>();
            UserDTO user = await _userRep.updateStatusAccount(id);
            baseResponse.Message = MessageUtil.Instance.Updated;
            baseResponse.Data = user;
            return baseResponse;
        }
        public async Task<IBaseResponse<Boolean>> updatePasswordsMassive(List<UserPassDto> listDto)
        {
            var baseResponse = new BaseResponse<Boolean>();
            await _userRep.updatePasswordsMassive(listDto);
            baseResponse.Message = MessageUtil.Instance.Updated;
            return baseResponse;
        }
        public async Task<IBaseResponse<UserDTO>> changePasswordService(int id, UserDTO model)
        {
            var baseResponse = new BaseResponse<UserDTO>();
            UserDTO user = await _userRep.changePasswordAsync(id, model);
            baseResponse.Message = MessageUtil.Instance.Updated;
            baseResponse.Data = user;
            return baseResponse;
        }
    }
}
