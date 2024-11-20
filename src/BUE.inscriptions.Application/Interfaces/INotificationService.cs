using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Response;

namespace BUE.Inscriptions.Application.Interfaces
{
    public interface INotificationService : IBaseService<NotificationDTO>
    {
        Task<IBaseResponse<NotificationDTO>> GetByIdServiceAsync(int id);
        Task<IBaseResponse<NotificationDTO>> GetByTemplateServicesAsync(string templateName, string language);
    }
}
