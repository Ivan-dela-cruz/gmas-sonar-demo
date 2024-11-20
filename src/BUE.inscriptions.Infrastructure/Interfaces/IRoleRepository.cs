using BUE.Inscriptions.Domain.Entity.DTO;

namespace BUE.Inscriptions.Infrastructure.Interfaces
{

    public interface IRoleRepository : IBaseRepository<RoleDTO>
    {
        Task<RoleDTO> GetByIdAsync(int id);
        Task<IEnumerable<PermissionDTO>> GetAllPermissionsAsync();
        Task<IEnumerable<RoleDTO>> GetPermissionsAsync();
    }
}
