using AutoMapper;
using BUE.Inscriptions.Infrastructure.Interfaces;
using BUE.Inscriptions.Domain.Entity;
using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Shared.Utils;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using BUE.Inscriptions.Domain.Request;

namespace BUE.Inscriptions.Infrastructure.Repository
{
    public class StudentPortalRepository : BaseRepository, IStudentPortalRepository
    {
        private readonly PortalMatriculasDBContext _db;
        private readonly IAwsS3UploaderRepository _awsS3;
        protected readonly IConfiguration _configuration;
        private IMapper _mapper;
        private string _domain = "";
        public StudentPortalRepository(PortalMatriculasDBContext db, IMapper mapper, IConfiguration configuration, IAwsS3UploaderRepository awsS3)
        {
            _db = db;
            _awsS3 = awsS3;
            _mapper = mapper;
            _configuration = configuration;
            _domain = _configuration.GetSection("AppSettings:Domain").Value;
        }
        public async Task AttachFiles(StudentPortal student, StudentPortalDTO entityDto)
        {
            string s3Result = "";
            string fileName = student.documentNumber.ToString().Trim() + "_" + student.studentCode;
            if (entityDto.photo is not null && entityDto.photo.Length > 0)
            {
                string subPathS3 = String.Format(@"{0}/students/images", student.currentSchoolYear);
                s3Result = await _awsS3.UploadBucketFileAsync(subPathS3, "photo_student_" + fileName + ".png", entityDto.photo);
                student.urlImage = s3Result ?? "";
            }
            if (entityDto.documentIdentification is not null && entityDto.documentIdentification.Length > 0)
            {
                string subPathDocS3 = String.Format(@"{0}/students/identifications", student.currentSchoolYear);
                s3Result = await _awsS3.UploadBucketFileAsync(subPathDocS3, "document_student_" + fileName +".pdf", entityDto.documentIdentification);
                student.urlDocument = s3Result ?? "";
            }
        }
        public async Task<StudentPortalDTO> CreateAsync(StudentPortalDTO entity)
        {
            var dateDate = DateTime.Now;
            int alterCodeStudent = dateDate.Year + dateDate.Month + dateDate.Day + dateDate.Hour * 10000 + dateDate.Minute * 100 + dateDate.Second * 60;
            StudentPortal student = _mapper.Map<StudentPortalDTO, StudentPortal>(entity);
            student.completeName = String.Format("{0} {1} {2}", student.firstName, student.secondName, student.names);
            student.studentCode = student.studentCode is not null ? student.studentCode : alterCodeStudent;
            student.BeforeCourseGradeCode = student.courseGradeCode;
            await AttachFiles(student, entity);
            student.level = null;
            student.courseGrade = null;
            student.PortalRequest = null;
            _db.Students.Add(student);
            await _db.SaveChangesAsync();
            var newStudent = _mapper.Map<StudentPortal, StudentPortalDTO>(student);
            return newStudent;
        }
        public async Task<bool> DeleteAsync(string documentNumber)
        {
            try
            {
                StudentPortal? student = await _db.Students.FirstOrDefaultAsync(x => x.documentNumber == documentNumber);
                if (student is null)
                {
                    return false;
                }
                _db.Students.Update(student);
                //_db.student.Remove(student);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<IEnumerable<StudentPortalDTO>> GetAsync()
        {
            var items = await _db.Students.ToListAsync();
            var resultDTO = _mapper.Map<IEnumerable<StudentPortalDTO>>(items);
            return resultDTO;
        }  
        public async Task<IEnumerable<StudentPortalDTO>> GetStudentsByCodeAsync(IEnumerable<HelperFieldValidate> helperFields)
        {
            var codes = helperFields.Select(x => x.Code).ToList();
            var codesText = helperFields.Select(x => x.CodeText.Trim()).ToList();
            var studentsCodesAndIdentifications = await _db.Students.Where(x => codes.Contains(x.studentCode) || codesText.Contains(x.documentNumber)).ToListAsync();
            var resultDTO = _mapper.Map<IEnumerable<StudentPortalDTO>>(studentsCodesAndIdentifications);
            return resultDTO;
        }

        public async Task<IEnumerable<StudentPortalDTO>> GetListBasicAsync()
        {
            var items = await _db.Students.Select(p => new StudentPortal()
            {
                studentCodeSchoolYear = p.studentCodeSchoolYear,
                studentCode = p.studentCode,
                typeIdentification = p.typeIdentification,
                BloodTypeCode = p.BloodTypeCode,
                currentSchoolYear = p.currentSchoolYear,
                levelCode = p.levelCode,
                courseGradeCode = p.courseGradeCode,
                documentNumber = p.documentNumber,
                names = p.names,
                firstName = p.firstName,
                completeName = p.firstName + " " + p.secondName + " " + p.names,

            }).ToListAsync();

            var resultDTO = _mapper.Map<IEnumerable<StudentPortalDTO>>(items);
            return resultDTO;
        }

        public async Task<StudentPortalDTO> GetByIdAsync(int code, int currentSchoolYear) =>

            _mapper.Map<StudentPortalDTO>(await _db.Students.FirstOrDefaultAsync(x => x.studentCode == code && x.currentSchoolYear == currentSchoolYear));
        public async Task<StudentPortalDTO> GetByPrimaryKeyAsync(int primaryKey) =>

            _mapper.Map<StudentPortalDTO>(await _db.Students.FirstOrDefaultAsync(x => x.studentCodeSchoolYear == primaryKey));

        public async Task<StudentPortalDTO> UpdateAsync(int id, StudentPortalDTO entity)
        {
            StudentPortal student = _mapper.Map<StudentPortalDTO, StudentPortal>(entity);
            var studentAfter = await _db.Students.AsNoTracking().FirstOrDefaultAsync(x => x.studentCodeSchoolYear == id);
            if (studentAfter is null)
            {
                throw new NullReferenceException(MessageUtil.Instance.NotFound);
            }
            student = MapProperties(student, studentAfter);
            if (student.firstName != null || student.secondName != null || student.names != null)
                student.completeName = String.Format("{0} {1} {2}", student.firstName, student.secondName, student.names);
            await AttachFiles(student, entity);
            student.level = null;
            student.courseGrade = null;
            student.PortalRequest = null;
            var currentEntity = MapProperties(student, studentAfter);
            _db.Students.Update(currentEntity);
            await _db.SaveChangesAsync();
            currentEntity.dataEnrollmentApp = null;
            return _mapper.Map<StudentPortal, StudentPortalDTO>(currentEntity);
        }

        public async Task<StudentPortalDTO> UpdateAdminAsync(int id, StudentPortalDTO entity)
        {
            string s3Result = "";
            StudentPortal student = _mapper.Map<StudentPortalDTO, StudentPortal>(entity);
            var studentAfter = await _db.Students.AsNoTracking().FirstOrDefaultAsync(x => x.studentCodeSchoolYear == id);
            if (studentAfter is null)
            {
                throw new NullReferenceException(MessageUtil.Instance.NotFound);
            }
            if (student.firstName != null || student.secondName != null || student.names != null)
                student.completeName = String.Format("{0} {1} {2}", student.firstName, student.secondName, student.names);

            if (entity.photo is not null && entity.photo.Length > 0)
            {

                string subPathS3 = String.Format(@"students/{0}/images", studentAfter.currentSchoolYear);
                await _awsS3.UploadBucketFileAsync(subPathS3, student.studentCodeSchoolYear.ToString().Trim() + ".png", entity.photo);
                s3Result = await _awsS3.UploadBucketFileAsync(subPathS3, student.studentCodeSchoolYear.ToString().Trim() + ".png", entity.photo);
                student.urlImage = s3Result ?? "";
            }
            if (entity.documentIdentification is not null && entity.documentIdentification.Length > 0)
            {
                string subPathDocS3 = String.Format(@"students/{0}/identifications", studentAfter.currentSchoolYear);
                s3Result = await _awsS3.UploadBucketFileAsync(subPathDocS3, student.studentCodeSchoolYear.ToString().Trim() + ".pdf", entity.documentIdentification);
                student.urlDocument = s3Result ?? "";
            }
            var currentEntity = MapProperties(student, studentAfter);
            _db.Students.Update(currentEntity);
            await _db.SaveChangesAsync();
            return _mapper.Map<StudentPortal, StudentPortalDTO>(currentEntity);
        }



        public async Task<StudentPortalDTO> UpdateDataEnrollmentAppAsync(int id, StudentPortalDTO entity)
        {
            StudentPortal student = _mapper.Map<StudentPortalDTO, StudentPortal>(entity);
            var studentAfter = await _db.Students.AsNoTracking().FirstOrDefaultAsync(x => x.studentCodeSchoolYear == id);
            if (studentAfter is null)
            {
                throw new NullReferenceException(MessageUtil.Instance.NotFound);
            }
            studentAfter.dataEnrollmentApp = student.dataEnrollmentApp;
            _db.Students.Update(studentAfter);
            await _db.SaveChangesAsync();
            return _mapper.Map<StudentPortal, StudentPortalDTO>(studentAfter);
        }
    }
}
