using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Response;

namespace BUE.Inscriptions.Infrastructure.Interfaces
{

    public interface IUserRepository : IBaseRepository<UserDTO>
    {
        Task<UserDTO> GetByIdAsync(int id);
        Task<UserDTO> GetByMailOrIdidentificationAsync(string id);
        Task<UserDTO> GetByTokenActivateAsync(string iText);
        Task<UserDTO> RegisterToken(int id, string token);
        Task<IEnumerable<UserResponse>> GetAsyncUsers();
        Task<UserDTO> updateStatusAccount(int id);
        Task<IEnumerable<UserRoleDTO>> AssingRolesAsync(int userId, int[] roles);
        Task<UserDTO> changePasswordAsync(int id, UserDTO entity);
        Task<Boolean> updatePasswordsMassive(List<UserPassDto> listDto);
        Task<UserDTO> UpdateDataAsync(int id, UserDTO entity);
        Task<UserDTO> UpdateUserWithContacAsync(int userId, int contactCode);
        Task<UserDTO> GetWithContactByIdAsync(int id);
    }
}
