using BUE.Inscriptions.Domain.Entity.DTO;

namespace BUE.Inscriptions.Infrastructure.Interfaces
{

    public interface INotificationRepository : IBaseRepository<NotificationDTO>
    {
        Task<NotificationDTO> GetByIdAsync(int id);
        Task<NotificationDTO> GetByTemplateAsync(string templateName, string language);
    }
}
