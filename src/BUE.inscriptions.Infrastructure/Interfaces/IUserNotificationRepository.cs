using BUE.Inscriptions.Domain.Entity.DTO;

namespace BUE.Inscriptions.Infrastructure.Interfaces
{

    public interface IUserNotificationRepository : IBaseRepository<UserNotificationDTO>
    {
        Task<UserNotificationDTO> GetByIdAsync(int id);
        Task<UserNotificationDTO> GetByUserIdAsync(int id);
        Task<UserNotificationDTO> GetByStudentIdAsync(int id);
    }
}
