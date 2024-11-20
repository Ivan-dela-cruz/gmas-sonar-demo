using AutoMapper;
using BUE.Inscriptions.Infrastructure.Interfaces;
using BUE.Inscriptions.Shared.Utils;
using Microsoft.EntityFrameworkCore;
using BUE.Inscriptions.Domain.Inscriptions.Entities;
using BUE.Inscriptions.Domain.Inscriptions.DTO;
using BUE.Inscriptions.Domain.Entity.DTO;



namespace BUE.Inscriptions.Infrastructure.Repository
{
    public class StudentRepository : BaseRepository, IStudentRepository
    {
        private readonly PortalMatriculasDBContext _db;
        private readonly IMapper _mapper;
        private readonly IAwsS3UploaderRepository _awsS3;
        private readonly IStudentBUERepository _studentBue;
        public string Message { get; set; }
        public string StatusCode { get; set; }

        public StudentRepository(IStudentBUERepository studentBue,PortalMatriculasDBContext db, IMapper mapper, IAwsS3UploaderRepository awsS3)
        {
            _db = db;
            _mapper = mapper;
            _awsS3 = awsS3;
            _studentBue = studentBue;
        }
        public async Task AttachFiles(Student student, StudentDTO entityDto)
        {
            if (entityDto.FilesStorage != null)
            {
                foreach (var row in entityDto.FilesStorage)
                {
                    string fileName = student.Id + "_" + row.FileName + "." + row.FileType;
                    if (row.FileBytes is not null && row.FileBytes.Length > 0)
                    {
                        var s3Result = await _awsS3.UploadBucketFileAsync(row.FilePath, fileName, row.FileBytes);
                        if (!string.IsNullOrEmpty(s3Result))
                        {
                            switch (row.FieldName.ToLower())
                            {
                                case "image":
                                    student.Image = s3Result;
                                    break;
                                case "documentidentification":
                                    student.DocumentIdentification = s3Result;
                                    break;
                            }
                        }
                    }
                }
            }
        }
        public async Task AttachPhoto(Student student, FileStorageDTO filesStorage)
        {
            string fileName = student.Id + "_" + filesStorage.FileName + "." + filesStorage.FileType;
            if (filesStorage.FileBytes is not null && filesStorage.FileBytes.Length > 0)
            {
                var s3Result = await _awsS3.UploadBucketFileAsync(filesStorage.FilePath, fileName, filesStorage.FileBytes);
                if (!string.IsNullOrEmpty(s3Result))
                {
                    switch (filesStorage.FieldName.ToLower())
                    {
                        case "image":
                            student.Image = s3Result;
                            break;
                    }
                }
            }
        }

        public async Task<StudentDTO> CreateAsync(StudentDTO entity)
        {
            try
            {
                Student student = _mapper.Map<StudentDTO, Student>(entity);
                student.Inscriptions = null;
                student.StudentFamilies = null;
                _db.Student.Add(student);
                await _db.SaveChangesAsync();
                await AttachFiles(student, entity);
                await _db.SaveChangesAsync();
                return _mapper.Map<Student, StudentDTO>(student);

            }
            catch (Exception ex)
            {
                HandlerOperationException(ex);
                Message = this.MessageException;
                StatusCode = this.CodeException;
                return null;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                Student student = await _db.Student.FindAsync(id);
                if (student == null)
                {
                    return false;
                }
                _db.Student.Remove(student);
                await _db.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<StudentDTO>> GetAsync()
        {
            var students = await _db.Student
                .Include(x => x.StudentFamilies)
                .Include(x => x.Inscriptions)
                .ToListAsync();
            return _mapper.Map<IEnumerable<StudentDTO>>(students);
        }

        public async Task<IEnumerable<StudentDTO>> GetByAcademicYearAsync(int academicYearId, int companyId)
        {
            var students = await _db.Student
                .Include(x => x.StudentFamilies)
                .Include(x => x.Inscriptions)
                .Where(x => x.CompanyId == companyId && x.Inscriptions.Any(i => i.AcademicYearId == academicYearId))
                .ToListAsync();
            return _mapper.Map<IEnumerable<StudentDTO>>(students);
        }

        public async Task<IEnumerable<StudentDTO>> GetByUserIdAsync(int userId, int companyId)
        {
            var students = await _db.Student
                .Include(x => x.StudentFamilies)
                .ThenInclude(f => f.Person)
                .Include(x => x.Inscriptions)
                .Include(x => x.PaymentInformation)
                .Where(x => x.CompanyId == companyId && (x.StudentFamilies.Any(i => i.Person.UserId == userId && i.IsLegalRepresentative == true) || x.Inscriptions.Any(i => i.UserId == userId)))
                .ToListAsync();
            return _mapper.Map<IEnumerable<StudentDTO>>(students);
        }

        public async Task<StudentDTO> GetByIdAsync(int id)
        {
            var student = await _db.Student
                .Include(x => x.StudentFamilies)
                .ThenInclude(f => f.Person)
                .Include(x => x.Inscriptions)
                .Include(x => x.PaymentInformation)
                .FirstOrDefaultAsync(e => e.Id == id);
            return _mapper.Map<StudentDTO>(student);
        }
        public async Task<StudentDTO> GetToContractByIdAsync(int id, int academicYearId)
        {
            var student = await _db.Student
                .Include(x => x.StudentFamilies)
                .ThenInclude(f => f.Person)
                .Include(x => x.Inscriptions)
                .Include(x => x.PaymentInformation)
                .FirstOrDefaultAsync(x => x.Id == id && (x.StudentFamilies.Any(i => i.IsPrincipalRepresentative == true && i.IsLegalRepresentative == true) && x.Inscriptions.Any(i => i.AcademicYearId == academicYearId)));

            return _mapper.Map<StudentDTO>(student);
        }

        public async Task<StudentDTO> UpdateAsync(int id, StudentDTO entity)
        {
            try
            {
                var student = _mapper.Map<StudentDTO, Student>(entity);
                var existingStudent = await _db.Student.FindAsync(id);

                if (existingStudent == null)
                {
                    throw new NullReferenceException(MessageUtil.Instance.NotFound);
                }
                await AttachFiles(student, entity);
                var currentStudent = MapProperties(student, existingStudent);
                currentStudent.Inscriptions = null;
                currentStudent.StudentFamilies = null;
                _db.Student.Update(currentStudent);
                await _db.SaveChangesAsync();
                return _mapper.Map<Student, StudentDTO>(currentStudent);
            }
            catch (Exception ex)
            {
                HandlerOperationException(ex);
                Message = this.MessageException;
                StatusCode = this.CodeException;
                return null;
            }
        }
        public async Task<StudentDTO> UpdateImageBueAsync(int externalId, FileStorageDTO filesStorage)
        {
            try
            {
             
                var existingStudent = await _db.Student.FirstOrDefaultAsync((x)=> x.ExternalId == externalId);
                if (existingStudent == null)
                {
                    throw new NullReferenceException(MessageUtil.Instance.NotFound);
                }
                await _studentBue.UpdateImageAsync(externalId, filesStorage.FileBytes);
                await AttachPhoto(existingStudent, filesStorage);
                _db.Student.Update(existingStudent);
                await _db.SaveChangesAsync();
                return _mapper.Map<Student, StudentDTO>(existingStudent);
            }
            catch (Exception ex)
            {
                HandlerOperationException(ex);
                Message = this.MessageException;
                StatusCode = this.CodeException;
                return null;
            }
        }
        public async Task<StudentDetails> GetStudentDetailsAsync(int studentId, int academicYearId)
        {
            var result = await _db.StudentDetails.FromSqlInterpolated($"EXEC [dbo].[gmas_sp_GetStudentDetailsV2] @StudentId = {studentId}, @AcademicYearId = {academicYearId}")
                                              .ToListAsync();
            if (result.Count > 0)
            {
                return result[0];
            }
            return null;
        }
        public async Task<IEnumerable<StudentDetails>> GetAllStudentsAsync(int academicYearId)
        {
            var result = await _db.StudentDetails.FromSqlInterpolated($"EXEC [dbo].[gmas_sp_GetStudentAllDetails] @AcademicYearId = {academicYearId}")
                                              .ToListAsync();
            return result;
        }
    }
}

