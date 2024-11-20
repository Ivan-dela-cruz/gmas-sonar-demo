using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Paging;
using BUE.Inscriptions.Domain.Response;

namespace BUE.Inscriptions.Application.Interfaces
{

    public interface IUserService : IBaseService<UserDTO>
    {
        Task<IBaseResponse<UserDTO>> GetByIdServiceAsync(int id);
        Task<IBaseResponse<UserDTO>> GetByMailOrIdidentificationAsync(string iText);
        Task<IBaseResponse<UserDTO>> GetByTokenActivateAsync(string token);
        Task<IBaseResponse<CurrentUserResponse>> GetUserAuth();
        Task<IBaseResponse<UserDTO>> RegisterToken(int id, string token);
        Task<IBaseResponse<UserDTO>> updateStatusAccount(int id);
        Task<IBaseResponse<PagedList<UserResponse>>> GetServiceAsyncUsers(PagingQueryParameters paging);
        Task<IBaseResponse<UserDTO>> changePasswordService(int id, UserDTO model);
        Task<IBaseResponse<Boolean>> updatePasswordsMassive(List<UserPassDto> listDto);
        Task<IBaseResponse<UserDTO>> GetWithContactByIdAsync(int id);
        Task<IBaseResponse<UserDTO>> CreateWithPassServiceAsync(UserDTO model);
    }
}
