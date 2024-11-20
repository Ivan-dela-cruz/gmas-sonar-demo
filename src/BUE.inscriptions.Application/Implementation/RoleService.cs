using BUE.Inscriptions.Infrastructure.Interfaces;
using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Paging;
using BUE.Inscriptions.Domain.Response;
using BUE.Inscriptions.Application.Interfaces;
using BUE.Inscriptions.Shared.Utils;

namespace BUE.Inscriptions.Application.Implementation
{
    public class RoleService : IRoleService
    {
        private IRoleRepository _roleRep;
        public RoleService(IRoleRepository roleRep) => _roleRep = roleRep;
        public async Task<IBaseResponse<RoleDTO>> CreateServiceAsync(RoleDTO model)
        {
            var baseResponse = new BaseResponse<RoleDTO>();
            var role = await _roleRep.CreateAsync(model);
            if (role is null)
            {
                baseResponse.Message = MessageUtil.Instance.Warning;
                baseResponse.status = false;
            }
            baseResponse.Data = role;
            return baseResponse;
        }
        public async Task<IBaseResponse<bool>> DeleteServiceAsync(int id)
        {
            var baseResponse = new BaseResponse<bool>();
            bool IsSuccess = await _roleRep.DeleteAsync(id);
            if (!IsSuccess)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = IsSuccess;
            return baseResponse;
        }
        public async Task<IBaseResponse<RoleDTO>> GetByIdServiceAsync(int id)
        {
            var baseResponse = new BaseResponse<RoleDTO>();
            RoleDTO role = await _roleRep.GetByIdAsync(id);
            if (role is null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = role;
            return baseResponse;
        }

        public async Task<IBaseResponse<PagedList<RoleDTO>>> GetServiceAsync(PagingQueryParameters paging)
        {
            var baseResponse = new BaseResponse<PagedList<RoleDTO>>();
            var roles = await _roleRep.GetAsync();
            if (roles is null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = PagedList<RoleDTO>.ToPagedList(roles, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }
        public async Task<IBaseResponse<PagedList<RoleDTO>>> GetRolePermissionServiceAsync(PagingQueryParameters paging)
        {
            var baseResponse = new BaseResponse<PagedList<RoleDTO>>();
            var roles = await _roleRep.GetPermissionsAsync();
            if (roles is null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = PagedList<RoleDTO>.ToPagedList(roles, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }
        public async Task<IBaseResponse<RoleDTO>> UpdateServiceAsync(int id, RoleDTO model)
        {

            var baseResponse = new BaseResponse<RoleDTO>();
            RoleDTO role = await _roleRep.UpdateAsync(id, model);
            baseResponse.Message = MessageUtil.Instance.Updated;
            baseResponse.Data = role;
            return baseResponse;
        }
        public async Task<IBaseResponse<IEnumerable<PermissionDTO>>> GetAllPermissionsServiceAsync()
        {
            var baseResponse = new BaseResponse<IEnumerable<PermissionDTO>>();
            var permissions = await _roleRep.GetAllPermissionsAsync();
            if (permissions is null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = permissions;
            return baseResponse;
        }
    }
}
