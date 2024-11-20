using BUE.Inscriptions.Infrastructure.Interfaces;
using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Paging;
using BUE.Inscriptions.Domain.Response;
using BUE.Inscriptions.Application.Interfaces;
using BUE.Inscriptions.Shared.Utils;

namespace BUE.Inscriptions.Application.Implementation
{
    public class NotificationService : INotificationService
    {
        private INotificationRepository _notificationRep;
        public NotificationService(INotificationRepository notificationRep) => _notificationRep = notificationRep;
        public async Task<IBaseResponse<NotificationDTO>> CreateServiceAsync(NotificationDTO model)
        {
            var baseResponse = new BaseResponse<NotificationDTO>();
            var notification = await _notificationRep.CreateAsync(model);
            if (notification is null)
            {
                baseResponse.Message = MessageUtil.Instance.Warning;
                baseResponse.status = false;
            }
            baseResponse.Data = notification;
            return baseResponse;
        }
        public async Task<IBaseResponse<bool>> DeleteServiceAsync(int id)
        {
            var baseResponse = new BaseResponse<bool>();
            bool IsSuccess = await _notificationRep.DeleteAsync(id);
            if (!IsSuccess)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = IsSuccess;
            return baseResponse;
        }
        public async Task<IBaseResponse<NotificationDTO>> GetByIdServiceAsync(int id)
        {
            var baseResponse = new BaseResponse<NotificationDTO>();
            NotificationDTO notification = await _notificationRep.GetByIdAsync(id);
            if (notification is null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = notification;
            return baseResponse;
        }

        public async Task<IBaseResponse<NotificationDTO>> GetByTemplateServicesAsync(string templateName, string language)
        {
            var baseResponse = new BaseResponse<NotificationDTO>();
            NotificationDTO notification = await _notificationRep.GetByTemplateAsync(templateName, language);
            if (notification is null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = notification;
            return baseResponse;
        }

        public async Task<IBaseResponse<PagedList<NotificationDTO>>> GetServiceAsync(PagingQueryParameters paging)
        {
            var baseResponse = new BaseResponse<PagedList<NotificationDTO>>();
            var notifications = await _notificationRep.GetAsync();
            if (notifications is null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = PagedList<NotificationDTO>.ToPagedList(notifications, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }
        public async Task<IBaseResponse<NotificationDTO>> UpdateServiceAsync(int id, NotificationDTO model)
        {

            var baseResponse = new BaseResponse<NotificationDTO>();
            NotificationDTO notification = await _notificationRep.UpdateAsync(id, model);
            baseResponse.Message = MessageUtil.Instance.Updated;
            baseResponse.Data = notification;
            return baseResponse;
        }
    }
}
