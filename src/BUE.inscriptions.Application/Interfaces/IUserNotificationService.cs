using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Paging;
using BUE.Inscriptions.Domain.Response;

namespace BUE.Inscriptions.Application.Interfaces
{

    public interface IUserNotificationService : IBaseService<UserNotificationDTO>
    {
        Task<IBaseResponse<UserNotificationDTO>> GetByIdServiceAsync(int id);
        Task<IBaseResponse<UserNotificationDTO>> GetByUserIdServiceAsync(int id);
        Task<IBaseResponse<UserNotificationDTO>> GetByStudentIdServiceAsync(int id);
    }
}
