using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Paging;
using BUE.Inscriptions.Domain.Response;

namespace BUE.Inscriptions.Application.Interfaces
{

    public interface IRoleService : IBaseService<RoleDTO>
    {
        Task<IBaseResponse<RoleDTO>> GetByIdServiceAsync(int id);
        Task<IBaseResponse<IEnumerable<PermissionDTO>>> GetAllPermissionsServiceAsync();
        Task<IBaseResponse<PagedList<RoleDTO>>> GetRolePermissionServiceAsync(PagingQueryParameters paging);
    }
}
