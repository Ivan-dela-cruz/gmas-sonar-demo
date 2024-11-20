using AutoMapper;
using BUE.Inscriptions.Infrastructure.Interfaces;
using BUE.Inscriptions.Domain.Entity;
using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Shared.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BUE.Inscriptions.Infrastructure.Repository
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly PortalMatriculasDBContext _dbPortal;
        private IMapper _mapper;
        private string _domain = "";
        protected readonly IConfiguration _configuration;

        public ApplicationRepository(PortalMatriculasDBContext dbPortal, IMapper mapper, IConfiguration configuration)
        {
            _dbPortal = dbPortal;
            _mapper = mapper;
            _configuration = configuration;
            _domain = _configuration.GetSection("AppSettings:Domain").Value;
        }

        public async Task<ApplicationDTO> getApplicationAsync() =>
           _mapper.Map<ApplicationDTO>(await _dbPortal.Companies
               //.Select(p => new Application()
               //{
               //    code = p.code,
               //    currentSchoolYear = p.currentSchoolYear,
               //    name = p.name,
               //    address = p.address,
               //    startDate = p.startDate,
               //    endDate = p.endDate,
               //    Status = p.Status,
               //    isEnrollment = p.isEnrollment,
               //    afterSchoolYear = p.afterSchoolYear,
               //    SchoolYear = p.SchoolYear,
               //    ruc = p.ruc,
               //    email = p.email,
               //    phone = p.phone,
               //    cellphone = p.cellphone,
               //    additionalInformation = p.additionalInformation,
               //    description = p.description,
               //    urlImage = p.urlImage
               //})
               .Where(o => (bool)o.Status).FirstOrDefaultAsync());

        public async Task<ApplicationDTO> UpdateAsync(int id, ApplicationDTO entity)
        {
            Application model = _mapper.Map<ApplicationDTO, Application>(entity);
            var modelAfter = await _dbPortal.Companies.AsNoTracking().FirstOrDefaultAsync(x => x.code == id);
            if (modelAfter is null)
            {
                throw new NullReferenceException(MessageUtil.Instance.NotFound);
            }
            //if (model.photo is not null && model.photo.Length > 0)
            //{
            //    string subPath = String.Format(@"\aplication\{0}\images", modelAfter.code);
            //    model.urlImage = ImageUtil.Instance.createStudentImage(modelAfter.code.ToString() + ".png", subPath, model.photo, _domain);
            //}
            var currentEntity = MapperNewValues(model, modelAfter);
            _dbPortal.Companies.Update(currentEntity);
            await _dbPortal.SaveChangesAsync();
            return _mapper.Map<Application, ApplicationDTO>(currentEntity);
        }


        private Application MapperNewValues(Application newEntity, Application olderEntity)
        {
            var currentEntity = new Application();
            currentEntity.code = newEntity.code != null && newEntity.code != 0 ? newEntity.code : olderEntity.code;
            currentEntity.currentSchoolYear = newEntity.currentSchoolYear is not null && newEntity.currentSchoolYear != 0 ? newEntity.currentSchoolYear : olderEntity.currentSchoolYear;
            currentEntity.name = newEntity.name is not null ? newEntity.name : olderEntity.name;
            currentEntity.address = newEntity.address is not null ? newEntity.address : olderEntity.address;
            currentEntity.startDate = newEntity.startDate is not null ? newEntity.startDate : olderEntity.startDate;
            currentEntity.endDate = newEntity.endDate is not null ? newEntity.endDate : olderEntity.endDate;
            currentEntity.Status = newEntity.Status is not null ? newEntity.Status : olderEntity.Status;
            currentEntity.isEnrollment = newEntity.isEnrollment is not null ? newEntity.isEnrollment : olderEntity.isEnrollment;
            currentEntity.afterSchoolYear = newEntity.afterSchoolYear is not null ? newEntity.afterSchoolYear : olderEntity.afterSchoolYear;
            currentEntity.photo = newEntity.photo is not null && newEntity.photo.Length > 0 ? newEntity.photo : olderEntity.photo;
            currentEntity.urlImage = newEntity.urlImage is not null ? newEntity.urlImage : olderEntity.urlImage;
            currentEntity.ruc = newEntity.ruc is not null ? newEntity.ruc : olderEntity.ruc;
            currentEntity.email = newEntity.email is not null ? newEntity.email : olderEntity.email;
            currentEntity.phone = newEntity.phone is not null ? newEntity.phone : olderEntity.phone;
            currentEntity.cellPhone = newEntity.cellPhone is not null ? newEntity.cellPhone : olderEntity.cellPhone;
            currentEntity.description = newEntity.description is not null ? newEntity.description : olderEntity.description;
            currentEntity.additionalInformation = newEntity.additionalInformation is not null ? newEntity.additionalInformation : olderEntity.additionalInformation;
            return currentEntity;
        }
    }
}
