using AutoMapper;
using BUE.Inscriptions.Infrastructure.Interfaces;
using BUE.Inscriptions.Domain.Entity;
using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Shared.Utils;
using Microsoft.EntityFrameworkCore;

namespace BUE.Inscriptions.Infrastructure.Repository
{
    public class UserNotificationRepository : IUserNotificationRepository
    {
        private readonly PortalMatriculasDBContext _db;
        private IMapper _mapper;
        public UserNotificationRepository(PortalMatriculasDBContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<UserNotificationDTO> CreateAsync(UserNotificationDTO entity)
        {
            UserNotification userNotification = _mapper.Map<UserNotificationDTO, UserNotification>(entity);
            _db.UserNotification.Add(userNotification);
            await _db.SaveChangesAsync();
            return _mapper.Map<UserNotification, UserNotificationDTO>(userNotification);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                UserNotification? userNotification = await _db.UserNotification.FirstOrDefaultAsync(x => x.code == id);
                if (userNotification is null)
                {
                    return false;
                }
                userNotification.DeletedAt = DateTime.Now;
                _db.UserNotification.Update(userNotification);
                //_db.UserNotification.Remove(userNotification);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<IEnumerable<UserNotificationDTO>> GetAsync() =>

            _mapper.Map<IEnumerable<UserNotificationDTO>>(await _db.UserNotification.ToListAsync());



        public async Task<UserNotificationDTO> GetByIdAsync(int id) =>

            _mapper.Map<UserNotificationDTO>(await _db.UserNotification.FirstOrDefaultAsync(x => x.code == id));
        public async Task<UserNotificationDTO> GetByUserIdAsync(int id) =>

             _mapper.Map<UserNotificationDTO>(await _db.UserNotification.FirstOrDefaultAsync(x => x.userCode == id && x.status == true));

        public async Task<UserNotificationDTO> GetByStudentIdAsync(int id) =>

             _mapper.Map<UserNotificationDTO>(await _db.UserNotification.FirstOrDefaultAsync(x => x.studentCodeSchoolYear == id && x.status == true));


        public async Task<UserNotificationDTO> UpdateAsync(int id, UserNotificationDTO entity)
        {
            UserNotification userNotification = _mapper.Map<UserNotificationDTO, UserNotification>(entity);
            var roleAfter = await _db.UserNotification.AsNoTracking().FirstOrDefaultAsync(x => x.code == id);
            if (roleAfter is null || userNotification.code != id)
            {
                throw new NullReferenceException(MessageUtil.Instance.NotFound);
            }
            _db.UserNotification.Update(userNotification);
            await _db.SaveChangesAsync();
            return _mapper.Map<UserNotification, UserNotificationDTO>(userNotification);
        }

    }
}
