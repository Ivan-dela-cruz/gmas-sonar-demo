using AutoMapper;
using BUE.Inscriptions.Infrastructure.Interfaces;
using BUE.Inscriptions.Domain.Entity;
using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Response;
using BUE.Inscriptions.Shared.Utils;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BUE.Inscriptions.Infrastructure.Repository
{
    public class UserRepository : BaseRepository,IUserRepository
    {
        private readonly PortalMatriculasDBContext _db;
        private IMapper _mapper;
        private string _domain = "";
        protected readonly IConfiguration _configuration;
        private readonly IAwsS3UploaderRepository _awsS3;
        public UserRepository(PortalMatriculasDBContext db, IMapper mapper, IConfiguration configuration, IAwsS3UploaderRepository awsS3)
        {
            _db = db;
            _mapper = mapper;
            _configuration = configuration;
            _domain = _configuration.GetSection("AppSettings:Domain").Value;
            _awsS3 = awsS3;
        }
        public async Task<UserDTO> CreateAsync(UserDTO entity)
        {
            User user = _mapper.Map<UserDTO, User>(entity);
            string s3Result = "";
            if (user.photo is not null && user.photo.Length > 0)
            {
                string subPathS3 = @"users/images";
                s3Result = await _awsS3.UploadBucketFileAsync(subPathS3, user.Identification.ToString().Trim() + ".png", user.photo);
                user.Image = s3Result ?? "";
            }
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return _mapper.Map<User, UserDTO>(user);
        }
        public async Task<IEnumerable<UserRoleDTO>> AssingRolesAsync(int userId, int[] roles)
        {
            foreach (var rolId in roles)
            {
                var NewUserRoleDTO = new UserRoleDTO()
                {
                    roleCode = rolId,
                    userCode = userId
                };
                var NewUserRole = _mapper.Map<UserRoleDTO, UserRole>(NewUserRoleDTO);
                _db.UserRoles.Add(NewUserRole);
                await _db.SaveChangesAsync();
            }
            var result = _mapper.Map<IEnumerable<UserRoleDTO>>(await _db.UserRoles.Where(x => x.userCode == userId).ToListAsync());
            return result;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                User? user = await _db.Users.FirstOrDefaultAsync(x => x.Code == id);
                if (user is null)
                {
                    return false;
                }
                user.DeletedAt = DateTime.Now;
                _db.Users.Update(user);
                //_db.Users.Remove(user);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<IEnumerable<UserDTO>> GetAsync() =>

            _mapper.Map<IEnumerable<UserDTO>>(await _db.Users.ToListAsync());
        public async Task<IEnumerable<UserResponse>> GetAsyncUsers() =>

            _mapper.Map<IEnumerable<UserResponse>>(await _db.Users.ToListAsync());



        public async Task<UserDTO> GetByIdAsync(int id)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Code == id);
            var rtd = _mapper.Map<UserDTO>(user);
            var roles = _db.Role
                                  .FromSqlRaw("Select r.* from roles r join Usuarios_Roles ur on ur.codigoRol = r.codigo where ur.codigoUsuario = @id", new SqlParameter("@id", id))
                                  .ToList();
            var rolesDto = _mapper.Map<IEnumerable<RoleDTO>>(roles);
            foreach (var item in rolesDto)
            {
                var permissions = _db.Permissions
                                      .FromSqlRaw("SELECT p.* FROM Permisos p join Roles_Permisos rp on rp.codigoPermiso = p.codigo where rp.codigoRol = @id", new SqlParameter("@id", item.code))
                                      .ToList();
                item.permissions = _mapper.Map<IEnumerable<PermissionDTO>>(permissions);
            }
            rtd.roles = rolesDto;
            return rtd;
        }
        public async Task<UserDTO> GetWithContactByIdAsync(int id)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Code == id);
            if (user is not null)
            {
                if (user.ContactCode is not null)
                {
                    var contact = await _db.Contacts.FirstOrDefaultAsync(x => x.code == (int)user.ContactCode);
                    user.Contact = contact;
                }
            }
            var result = _mapper.Map<UserDTO>(user);
            return result;
        }

        public async Task<UserDTO> GetByMailOrIdidentificationAsync(string iText)
        {
            var userFound = await _db.Users.FirstOrDefaultAsync(x => x.Email == iText || x.Identification == iText);
            var result = _mapper.Map<UserDTO>(userFound);
            return result;
        }

        public async Task<UserDTO> GetByTokenActivateAsync(string iText)
        {
            var userFound = await _db.Users.FirstOrDefaultAsync(x => x.RememberToken == iText);
            var result = _mapper.Map<UserDTO>(userFound);
            return result;
        }

        public async Task<UserDTO> RegisterToken(int id, string token)
        {
            var userAfter = _db.Users.Find(id);
            if (userAfter is null || userAfter.Code != id)
            {
                throw new NullReferenceException(MessageUtil.Instance.NotFound);
            }
            userAfter.Token = token;
            await _db.SaveChangesAsync();
            return _mapper.Map<User, UserDTO>(userAfter);
        }
        public async Task<UserDTO> updateStatusAccount(int id)
        {
            var userAfter = _db.Users.Find(id);
            if (userAfter is null || userAfter.Code != id)
            {
                throw new NullReferenceException(MessageUtil.Instance.NotFound);
            }
            userAfter.Activo = true;
            await _db.SaveChangesAsync();
            return _mapper.Map<User, UserDTO>(userAfter);
        }
        public async Task<Boolean> updatePasswordsMassive(List<UserPassDto> listDto)
        {
            foreach (var userDto in listDto)
            {
                var userAfter = _db.Users.Find(userDto.UserId);
                userAfter.Password = userDto.Password;
                userAfter.RememberToken = userDto.RememberToken;
                await _db.SaveChangesAsync();
            }
            return true;
        }

        public async Task<UserDTO> UpdateAsync(int id, UserDTO entity)
        {
            User user = _mapper.Map<UserDTO, User>(entity);
            var userAfter = await _db.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Code == id);
            if (userAfter is null)
            {
                throw new NullReferenceException(MessageUtil.Instance.NotFound);
            }
            string s3Result = "";
            if (user.photo is not null && user.photo.Length > 0)
            {
                string subPathS3 = @"users/images";
                s3Result = await _awsS3.UploadBucketFileAsync(subPathS3, user.Identification.ToString().Trim() + ".png", user.photo);
                user.Image = s3Result ?? "";
            }
            var currentUser = MapProperties(user, userAfter);
            _db.Users.Update(currentUser);
            await _db.SaveChangesAsync();
            return _mapper.Map<User, UserDTO>(currentUser);
        }
        public async Task<UserDTO> UpdateUserWithContacAsync(int userId, int contactCode)
        {

            var userAfter = await _db.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Code == userId);
            if (userAfter is null)
            {
                throw new NullReferenceException(MessageUtil.Instance.NotFound);
            }
            userAfter.ContactCode = contactCode;
            _db.Users.Update(userAfter);
            await _db.SaveChangesAsync();
            return _mapper.Map<User, UserDTO>(userAfter);
        }
        public async Task<UserDTO> changePasswordAsync(int id, UserDTO entity)
        {
            var userAfter = await _db.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Code == id);
            if (userAfter is null)
            {
                throw new NullReferenceException(MessageUtil.Instance.NotFound);
            }
            userAfter.Password = entity.Password;
            userAfter.RememberToken = entity.RememberToken;
            _db.Users.Update(userAfter);
            await _db.SaveChangesAsync();
            return _mapper.Map<User, UserDTO>(userAfter);
        }
        public async Task<UserDTO> UpdateDataAsync(int id, UserDTO entity)
        {
            try
            {
                User user = _mapper.Map<UserDTO, User>(entity);
                User userAfter = await _db.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Code == id);
                if (userAfter == null)
                    return null;
                userAfter.Names = user.Names == null ? userAfter.Names : user.Names;
                userAfter.FirstLastName = user.FirstLastName == null ? userAfter.FirstLastName : user.FirstLastName;
                userAfter.SecondaryLastName = user.SecondaryLastName == null ? userAfter.SecondaryLastName : user.SecondaryLastName;
                userAfter.Identification = user.Identification == null ? userAfter.Identification : user.Identification;
                userAfter.Cellphone = user.Cellphone == null ? userAfter.Cellphone : user.Cellphone;
                userAfter.Address = user.Address == null ? userAfter.Address : user.Address;
                _db.Users.Update(userAfter);
                await _db.SaveChangesAsync();
                return _mapper.Map<User, UserDTO>(userAfter);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        private User MapperNewValues(User newEntity, User olderEntity)
        {
            var currentEntity = new User();
            currentEntity.Code = newEntity.Code is not null && newEntity.Code != 0 ? newEntity.Code : olderEntity.Code;
            currentEntity.FirstLastName = newEntity.FirstLastName is not null ? newEntity.FirstLastName : olderEntity.FirstLastName;
            currentEntity.SecondaryLastName = newEntity.SecondaryLastName is not null ? newEntity.SecondaryLastName : olderEntity.SecondaryLastName;
            currentEntity.BirthDate = newEntity.BirthDate is not null ? newEntity.BirthDate : olderEntity.BirthDate;
            currentEntity.BirthPlace = newEntity.BirthPlace is not null ? newEntity.BirthPlace : olderEntity.BirthPlace;
            currentEntity.Prefix = newEntity.Prefix is not null ? newEntity.Prefix : olderEntity.Prefix;
            currentEntity.SecondaryEmail = newEntity.SecondaryEmail is not null ? newEntity.SecondaryEmail : olderEntity.SecondaryEmail;
            currentEntity.ContactCode = newEntity.ContactCode is not null && newEntity.ContactCode != 0 ? newEntity.ContactCode : olderEntity.ContactCode;
            currentEntity.LanguagueCode = newEntity.LanguagueCode is not null ? newEntity.LanguagueCode : olderEntity.LanguagueCode;
            currentEntity.UserName = newEntity.UserName is not null ? newEntity.UserName : olderEntity.UserName;
            currentEntity.Names = newEntity.Names is not null ? newEntity.Names : olderEntity.Names;
            currentEntity.Identification = newEntity.Identification is not null ? newEntity.Identification : olderEntity.Identification;
            currentEntity.Status = newEntity.Status != null ? newEntity.Status : olderEntity.Status;
            currentEntity.Activo = newEntity.Activo != null ? newEntity.Activo : olderEntity.Activo;
            currentEntity.Image = newEntity.Image is not null ? newEntity.Image : olderEntity.Image;
            currentEntity.photo = newEntity.photo is not null && newEntity.photo.Length > 0 ? newEntity.photo : olderEntity.photo;
            currentEntity.EmailVerification = newEntity.EmailVerification is not null ? newEntity.EmailVerification : olderEntity.EmailVerification;
            currentEntity.Email = newEntity.Email is not null ? newEntity.Email : olderEntity.Email;
            currentEntity.Password = newEntity.Password is not null ? newEntity.Password : olderEntity.Password;
            currentEntity.Token = newEntity.Token is not null ? newEntity.Token : olderEntity.Token;
            currentEntity.RememberToken = newEntity.RememberToken is not null ? newEntity.RememberToken : olderEntity.RememberToken;
            currentEntity.CreatedAt = newEntity.CreatedAt is not null ? newEntity.CreatedAt : olderEntity.CreatedAt;
            currentEntity.UpdatedAt = newEntity.UpdatedAt is not null ? newEntity.UpdatedAt : olderEntity.UpdatedAt;
            currentEntity.DeletedAt = newEntity.DeletedAt is not null ? newEntity.DeletedAt : olderEntity.DeletedAt;
            //currentEntity.PasswordHash = newEntity.PasswordHash is not null ? newEntity.PasswordHash : olderEntity.PasswordHash;
            //currentEntity.PasswordSalt = newEntity.PasswordSalt is not null ? newEntity.PasswordSalt : olderEntity.PasswordSalt;
            //currentEntity.TokenCreated = newEntity.TokenCreated is not null ? newEntity.TokenCreated : olderEntity.TokenCreated;
            //currentEntity.TokenExpires = newEntity.TokenExpires is not null ? newEntity.TokenExpires : olderEntity.TokenExpires;
            //currentEntity.RefreshToken = newEntity.RefreshToken is not null ? newEntity.RefreshToken : olderEntity.RefreshToken;

            return currentEntity;
        }
    }
}
