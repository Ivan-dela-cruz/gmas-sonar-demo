using AutoMapper;
using BUE.Inscriptions.Infrastructure.Interfaces;
using BUE.Inscriptions.Domain.Entity;
using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Shared.Utils;
using Microsoft.EntityFrameworkCore;

namespace BUE.Inscriptions.Infrastructure.Repository
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly PortalMatriculasDBContext _db;
        private IMapper _mapper;
        public NotificationRepository(PortalMatriculasDBContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<NotificationDTO> CreateAsync(NotificationDTO entity)
        {
            try
            {
                Notification notification = _mapper.Map<NotificationDTO, Notification>(entity);
                _db.Notifications.Add(notification);
                await _db.SaveChangesAsync();
                return _mapper.Map<Notification, NotificationDTO>(notification);
            }
            catch (Exception)
            {
                throw new NullReferenceException(MessageUtil.Instance.Error);
            }

        }
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                Notification? notification = await _db.Notifications.FirstOrDefaultAsync(x => x.Code == id);
                if (notification is null)
                {
                    return false;
                }
                notification.DeletedAt = DateTime.Now;
                _db.Notifications.Update(notification);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(MessageUtil.Instance.Error, e);
            }
        }
        public async Task<IEnumerable<NotificationDTO>> GetAsync()
        {
            try
            {
                var notification = _mapper.Map<IEnumerable<NotificationDTO>>(await _db.Notifications.ToListAsync());
                return notification;
            }
            catch (Exception e)
            {
                throw new Exception(MessageUtil.Instance.Error, e);
            }
        }

        public async Task<NotificationDTO> GetByTemplateAsync(string templateName, string language)
        {
            try
            {
                var notification = _mapper.Map<NotificationDTO>(await _db.Notifications.FirstOrDefaultAsync(x => x.TemplateName == templateName && x.Language == language));
                return notification;
            }
            catch (Exception e)
            {
                throw new Exception(MessageUtil.Instance.Error, e);
            }
        }


        public async Task<NotificationDTO> GetByIdAsync(int id)
        {
            try
            {
                var notification = _mapper.Map<NotificationDTO>(await _db.Notifications.FirstOrDefaultAsync(x => x.Code == id));
                return notification;
            }
            catch (Exception e)
            {
                throw new Exception(MessageUtil.Instance.Error, e);
            }
        }



        public async Task<NotificationDTO> UpdateAsync(int id, NotificationDTO entity)
        {
            try
            {
                Notification notification = _mapper.Map<NotificationDTO, Notification>(entity);
                var notificationAfter = await _db.Notifications.AsNoTracking().FirstOrDefaultAsync(x => x.Code == id);
                if (notificationAfter is null || notification.Code != id)
                {
                    throw new NullReferenceException(MessageUtil.Instance.NotFound);
                }
                _db.Notifications.Update(notification);
                await _db.SaveChangesAsync();
                return _mapper.Map<Notification, NotificationDTO>(notification);
            }
            catch (Exception e)
            {
                throw new Exception(MessageUtil.Instance.Error, e);
            }
        }
    }
}
