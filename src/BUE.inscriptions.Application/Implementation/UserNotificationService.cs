using BUE.Inscriptions.Infrastructure.Interfaces;
using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Paging;
using BUE.Inscriptions.Domain.Response;
using BUE.Inscriptions.Application.Interfaces;
using BUE.Inscriptions.Shared.Utils;

namespace BUE.Inscriptions.Application.Implementation
{
    public class UserNotificationService : IUserNotificationService
    {
        private IUserNotificationRepository _notiRep;
        public UserNotificationService(IUserNotificationRepository notiRep) => _notiRep = notiRep;
        public async Task<IBaseResponse<UserNotificationDTO>> CreateServiceAsync(UserNotificationDTO model)
        {
            var baseResponse = new BaseResponse<UserNotificationDTO>();
            var userNotification = await _notiRep.CreateAsync(model);
            if (userNotification is null)
            {
                baseResponse.Message = MessageUtil.Instance.Warning;
                baseResponse.status = false;
            }
            baseResponse.Data = userNotification;
            return baseResponse;
        }
        public async Task<IBaseResponse<bool>> DeleteServiceAsync(int id)
        {
            var baseResponse = new BaseResponse<bool>();
            bool IsSuccess = await _notiRep.DeleteAsync(id);
            if (!IsSuccess)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = IsSuccess;
            return baseResponse;
        }
        public async Task<IBaseResponse<UserNotificationDTO>> GetByIdServiceAsync(int id)
        {
            var baseResponse = new BaseResponse<UserNotificationDTO>();
            UserNotificationDTO userNotification = await _notiRep.GetByIdAsync(id);
            if (userNotification is null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = userNotification;
            return baseResponse;
        }
        public async Task<IBaseResponse<UserNotificationDTO>> GetByUserIdServiceAsync(int id)
        {
            var baseResponse = new BaseResponse<UserNotificationDTO>();
            UserNotificationDTO userNotification = await _notiRep.GetByUserIdAsync(id);
            if (userNotification is null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = userNotification;
            return baseResponse;
        }
        public async Task<IBaseResponse<UserNotificationDTO>> GetByStudentIdServiceAsync(int id)
        {
            var baseResponse = new BaseResponse<UserNotificationDTO>();
            UserNotificationDTO userNotification = await _notiRep.GetByStudentIdAsync(id);
            if (userNotification is null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = userNotification;
            return baseResponse;
        }
        public async Task<IBaseResponse<PagedList<UserNotificationDTO>>> GetServiceAsync(PagingQueryParameters paging)
        {
            var baseResponse = new BaseResponse<PagedList<UserNotificationDTO>>();
            var roles = await _notiRep.GetAsync();
            if (roles is null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = PagedList<UserNotificationDTO>.ToPagedList(roles, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }
        public async Task<IBaseResponse<UserNotificationDTO>> UpdateServiceAsync(int id, UserNotificationDTO model)
        {

            var baseResponse = new BaseResponse<UserNotificationDTO>();
            UserNotificationDTO userNotification = await _notiRep.UpdateAsync(id, model);
            baseResponse.Message = MessageUtil.Instance.Updated;
            baseResponse.Data = userNotification;
            return baseResponse;
        }
    }
}
