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
    public class AuthorizePersonRepository : BaseRepository, IAuthorizePersonRepository
    {
        private readonly PortalMatriculasDBContext _db;
        private IMapper _mapper;
        protected readonly IConfiguration _configuration;
        private readonly IAwsS3UploaderRepository _awsS3;
        public AuthorizePersonRepository(PortalMatriculasDBContext db, IMapper mapper, IConfiguration configuration, IAwsS3UploaderRepository awsS3)
        {
            _db = db;
            _mapper = mapper;
            _configuration = configuration;
            _awsS3 = awsS3;
        }
        public async Task AttachFiles(AuthorizePeople personAuthorize, AuthorizePeopleDTO entity)
        {
            string s3Result = "";
            if (entity.photo is not null && entity.photo.Length > 0)
            {
                string subPathS3 = String.Format(@"{0}/people-authorize/images", personAuthorize.currentSchoolYear);
                s3Result = await _awsS3.UploadBucketFileAsync(subPathS3, personAuthorize.documentNumber.ToString().Trim() + ".png", entity.photo);
                personAuthorize.urlImage = s3Result ?? "";
            }

            if (entity.documentFile is not null && entity.documentFile.Length > 0)
            {
                string subPathDocS3 = String.Format(@"{0}/people-authorize/identifications", personAuthorize.currentSchoolYear);
                s3Result = await _awsS3.UploadBucketFileAsync(subPathDocS3, personAuthorize.documentNumber.ToString().Trim() + ".pdf", entity.documentFile);
                personAuthorize.urlDocument = s3Result ?? "";
            }
        }
        public async Task<AuthorizePeopleDTO> CreateAsync(AuthorizePeopleDTO entity)
        {
            AuthorizePeople personAuthorize = _mapper.Map<AuthorizePeopleDTO, AuthorizePeople>(entity);
            personAuthorize.statusIntegration = 1;
            await AttachFiles(personAuthorize, entity);
            _db.People.Add(personAuthorize);
            await _db.SaveChangesAsync();
            personAuthorize.photo = null;
            personAuthorize.documentFile = null;
            return _mapper.Map<AuthorizePeople, AuthorizePeopleDTO>(personAuthorize);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var personAuthorize = await _db.People.FirstOrDefaultAsync(x => x.code == id);
                if (personAuthorize is null)
                {
                    return false;
                }
                var allAuthPeople = await _db.People.Where(x => x.contactCodeBue == personAuthorize.contactCodeBue).ToListAsync();
                //personAuthorize.DeletedAt = DateTime.Now;
                //_db.People.Update(personAuthorize);
                _db.People.RemoveRange(allAuthPeople);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                LogManagement.Instance.writeAssembly(MethodBase.GetCurrentMethod().DeclaringType, $"StackTrace: {ex.StackTrace} Error: {ex.Message}", ".Error");
                return false;
            }
        }
        public async Task<IEnumerable<AuthorizePeopleDTO>> GetAsync() =>

            _mapper.Map<IEnumerable<AuthorizePeopleDTO>>(await _db.People.ToListAsync());



        public async Task<AuthorizePeopleDTO> GetByIdAsync(int id) =>
            _mapper.Map<AuthorizePeopleDTO>(await _db.People.FirstOrDefaultAsync(x => x.code == id));
        public async Task<IEnumerable<AuthorizePeopleDTO>> GetByRequestIdAsync(int id) =>
            _mapper.Map<IEnumerable<AuthorizePeopleDTO>>(await _db.People.Where(o => o.portalRequestCode == id).ToListAsync());
        public async Task<AuthorizePeopleDTO> GetByIdentificationAsync(string identification, int currentYearSchool, int requestCode) =>
            _mapper.Map<AuthorizePeopleDTO>(await _db.People.Where(o => o.documentNumber == identification && o.currentSchoolYear == currentYearSchool && o.portalRequestCode == requestCode)
                .Select(p => new AuthorizePeople()
                {
                    code = p.code,
                    typeIdentification = p.typeIdentification,
                }).FirstOrDefaultAsync());


        public async Task<IEnumerable<AuthorizePeopleDTO>> StoreMany(IEnumerable<AuthorizePeopleDTO> entities)
        {
            List<AuthorizePeopleDTO> listInsert = new List<AuthorizePeopleDTO>();
            List<AuthorizePeopleDTO> listUpdate = new List<AuthorizePeopleDTO>();
            foreach (var entity in entities)
            {
                var personAuthorizeAfter = await _db.People.AsNoTracking().FirstOrDefaultAsync(x => x.documentNumber == entity.documentNumber);
                if (personAuthorizeAfter is null)
                {
                    string s3Result = "";
                    if (entity.photo is not null && entity.photo.Length > 0)
                    {
                        string subPathS3 = String.Format(@"people-authorize/{0}/images", entity.currentSchoolYear);
                        s3Result = await _awsS3.UploadBucketFileAsync(subPathS3, entity.documentNumber.ToString().Trim() + ".png", entity.photo);
                        entity.urlImage = s3Result ?? "2";
                    }
                    if (entity.documentFile is not null && entity.documentFile.Length > 0)
                    {
                        string subPathDocS3 = String.Format(@"people-authorize/{0}/identifications", entity.currentSchoolYear);
                        s3Result = await _awsS3.UploadBucketFileAsync(subPathDocS3, entity.documentNumber.ToString().Trim() + ".pdf", entity.documentFile);
                        entity.urlDocument = s3Result ?? "2";
                    }
                    listInsert.Add(entity);
                }
                else
                    listUpdate.Add(entity);
            }
            List<AuthorizePeople> peopleAuthorize = _mapper.Map<List<AuthorizePeople>>(listInsert);
            _db.People.AddRange(peopleAuthorize);
            await _db.SaveChangesAsync();
            var lisInsertResponse = _mapper.Map<List<AuthorizePeopleDTO>>(peopleAuthorize);
            if (listUpdate.Count > 0)
            {
                List<AuthorizePeopleDTO> listUpdateResponse = await UpdateMany(listUpdate);
                lisInsertResponse.AddRange(listUpdateResponse);

            }
            return lisInsertResponse;
        }

        public async Task<List<AuthorizePeopleDTO>> UpdateMany(IEnumerable<AuthorizePeopleDTO> entities)
        {
            List<AuthorizePeopleDTO> list = new List<AuthorizePeopleDTO>();
            foreach (var entity in entities)
            {
                var personAuthorizeAfter = await _db.People.AsNoTracking().FirstOrDefaultAsync(x => x.documentNumber == entity.documentNumber);
                if (personAuthorizeAfter is null) continue;
                AuthorizePeople personAuthorize = _mapper.Map<AuthorizePeopleDTO, AuthorizePeople>(entity);
                personAuthorize.code = personAuthorizeAfter.code;
                personAuthorize.isRegisterPerson = entity.isRegisterPerson;
                personAuthorize.relationshipStudentCode = entity.relationshipStudentCode;
                personAuthorize.address = entity.address;
                personAuthorize.postalCode = entity.postalCode;
                personAuthorize.cellPhone = entity.cellPhone;
                personAuthorize.photo = entity.photo;
                personAuthorize.documentFile = entity.documentFile;
                personAuthorize.typeIdentification = entity.typeIdentification;
                personAuthorize.documentNumber = entity.documentNumber;
                personAuthorize.specifyRelationShip = entity.specifyRelationShip;
                if (personAuthorizeAfter is not null || personAuthorize.documentNumber == entity.documentNumber)
                {
                    await AttachFiles(personAuthorize, entity);
                    var currentModel = MapProperties(personAuthorize, personAuthorizeAfter);
                    _db.People.Update(currentModel);
                    await _db.SaveChangesAsync();
                    var perDto = _mapper.Map<AuthorizePeople, AuthorizePeopleDTO>(currentModel);
                    list.Add(perDto);
                }
            }

            return list; ;
        }

        public async Task<AuthorizePeopleDTO> UpdateAsync(int id, AuthorizePeopleDTO entity)
        {
            AuthorizePeople personAuthorize = _mapper.Map<AuthorizePeopleDTO, AuthorizePeople>(entity);
            var personAuthorizeAfter = await _db.People.AsNoTracking().FirstOrDefaultAsync(x => x.code == id);
            if (personAuthorizeAfter is null)
            {
                throw new NullReferenceException(MessageUtil.Instance.NotFound);
            }
            await AttachFiles(personAuthorize, entity);
            var currentModel = MapProperties(personAuthorize, personAuthorizeAfter);
            _db.People.Update(currentModel);
            await _db.SaveChangesAsync();
            currentModel.photo = null;
            currentModel.documentFile = null;
            return _mapper.Map<AuthorizePeople, AuthorizePeopleDTO>(currentModel);
        }

    }
}
