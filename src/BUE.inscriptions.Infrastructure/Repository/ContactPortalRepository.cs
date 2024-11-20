using AutoMapper;
using BUE.Inscriptions.Infrastructure.Interfaces;
using BUE.Inscriptions.Domain.Entity;
using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Shared.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace BUE.Inscriptions.Infrastructure.Repository
{
    public class ContactPortalRepository : BaseRepository, IContactPortalRepository
    {
        private readonly PortalMatriculasDBContext _db;
        private IMapper _mapper;
        private string _domain = "";
        protected readonly IConfiguration _configuration;
        private readonly IAwsS3UploaderRepository _awsS3;
        public ContactPortalRepository(PortalMatriculasDBContext db, IMapper mapper, IConfiguration configuration, IAwsS3UploaderRepository awsS3)
        {
            _db = db;
            _awsS3 = awsS3;
            _mapper = mapper;
            _configuration = configuration;
            _domain = _configuration.GetSection("AppSettings:Domain").Value;
        }
        public async Task AttachFile(ContactPortal contact, ContactPortalDTO entity)
        {
            string s3Result = "";
            string fileName = contact.documentNumber.ToString().Trim() + "_" + contact.ContactCodeBue;
            if (entity.photo is not null && entity.photo.Length > 0)
            {
                string subPathS3 = String.Format(@"{0}/contacts/images", contact.currentSchoolYear);
                s3Result = await _awsS3.UploadBucketFileAsync(subPathS3, "photo_contact" + fileName + ".png", entity.photo);
                contact.urlImage = s3Result ?? "";
            }
            if (entity.documentFile is not null && entity.documentFile.Length > 0)
            {
                string subPathDocS3 = String.Format(@"{0}/contacts/identifications", contact.currentSchoolYear);
                s3Result = await _awsS3.UploadBucketFileAsync(subPathDocS3, "document_contact" + fileName + ".pdf", entity.documentFile);
                contact.urlDocument = s3Result ?? "";
            }

        }
        public async Task<ContactPortalDTO> CreateAsync(ContactPortalDTO entity)
        {

            var dateDate = DateTime.Now;
            int alterCodeContact = dateDate.Year + dateDate.Month + dateDate.Day + dateDate.Hour * 10000 + dateDate.Minute * 100 + dateDate.Second;
            var bueCode = entity.code is not null ? entity.code : alterCodeContact;
            var newContact = entity;
            newContact.code = null;
            ContactPortal contact = _mapper.Map<ContactPortalDTO, ContactPortal>(newContact);
            contact.ContactCodeBue = bueCode;
            contact.completeName = String.Format("{0} {1} {2}", contact.firstName, contact.secondName, contact.names);
            await AttachFile(contact, entity);
            _db.Contacts.Add(contact);
            await _db.SaveChangesAsync();

            return _mapper.Map<ContactPortal, ContactPortalDTO>(contact);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                ContactPortal? contact = await _db.Contacts.FirstOrDefaultAsync(x => x.code == id);
                if (contact is null)
                {
                    return false;
                }
                contact.DeletedAt = DateTime.Now;
                _db.Contacts.Update(contact);
                //_db.Contacts.Remove(contact);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                LogManagement.Instance.writeAssembly(MethodBase.GetCurrentMethod().DeclaringType, $"StackTrace: {ex.StackTrace} Error: {ex.Message}", ".ErrorS3");
                return false;
            }
        }
        public async Task<IEnumerable<ContactPortalDTO>> GetAsync() =>

            _mapper.Map<IEnumerable<ContactPortalDTO>>(await _db.Contacts.ToListAsync());

        public async Task<IEnumerable<ContactPortalDTO>> GetlistBasicAsync() =>

           _mapper.Map<IEnumerable<ContactPortalDTO>>(await _db.Contacts.Select(p => new ContactPortal()
           {
               code = p.code,
               documentNumber = p.documentNumber,
               typeIdentification = p.typeIdentification,
               names = p.names,
               firstName = p.firstName,
               secondName = p.secondName,
               completeName = p.completeName,
               email = p.email,
               prefix = p.prefix,
               typeRepresentative = p.typeRepresentative,
               currentSchoolYear = p.currentSchoolYear,
           }).ToListAsync());



        public async Task<ContactPortalDTO> GetByIdAsync(int id) =>

            _mapper.Map<ContactPortalDTO>(await _db.Contacts.FirstOrDefaultAsync(x => x.code == id));

        public async Task<ContactPortalDTO> GetByIdentificactionAsync(string documentNumber) =>

           _mapper.Map<ContactPortalDTO>(await _db.Contacts.FirstOrDefaultAsync(x => x.documentNumber.Trim() == documentNumber.Trim()));
        public async Task<ContactPortalDTO> GetByIdEmailAsync(string email) =>
           _mapper.Map<ContactPortalDTO>(await _db.Contacts.FirstOrDefaultAsync(x => x.email.Trim().ToLower() == email.Trim().ToLower()));

        public async Task<ContactPortalDTO> GetByCodeContactCurrentSchoolAsync(int codeContact, int currentYearSchool) =>

            _mapper.Map<ContactPortalDTO>(await _db.Contacts.FirstOrDefaultAsync(x => x.code == codeContact && x.currentSchoolYear == currentYearSchool));

        public async Task<ContactPortalDTO> UpdateAsync(int id, ContactPortalDTO entity)
        {
            ContactPortal contact = _mapper.Map<ContactPortalDTO, ContactPortal>(entity);
            var contactBefore = await _db.Contacts.AsNoTracking().FirstOrDefaultAsync(x => x.code == id);
            if (contactBefore is null)
            {
                throw new NullReferenceException(MessageUtil.Instance.NotFound);
            }

            contact.completeName = String.Format("{0} {1} {2}", contact.firstName, contact.secondName, contact.names);
            await AttachFile(contact, entity);
            var currentEntity = MapProperties(contact, contactBefore);

            _db.Contacts.Update(currentEntity);
            await _db.SaveChangesAsync();
            return _mapper.Map<ContactPortal, ContactPortalDTO>(currentEntity);
        }
    }
}
