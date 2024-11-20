using AutoMapper;
using BUE.Inscriptions.Infrastructure.Interfaces;
using BUE.Inscriptions.Domain.Entity;
using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Shared.Utils;
using Microsoft.EntityFrameworkCore;

namespace BUE.Inscriptions.Infrastructure.Repository
{
    public class StudentBUERepository : IStudentBUERepository
    {
        private readonly BueDBContext _db;
        private IMapper _mapper;
        public StudentBUERepository(BueDBContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<StudentBUEDTO> CreateAsync(StudentBUEDTO entity)
        {
            StudentBUE student = _mapper.Map<StudentBUEDTO, StudentBUE>(entity);
            student.studentCodeSchoolYear = null;
            _db.Students.Add(student);
            await _db.SaveChangesAsync();
            return _mapper.Map<StudentBUE, StudentBUEDTO>(student);
        }

        public async Task<bool> DeleteAsync(string documentNumber)
        {
            try
            {
                StudentBUE? student = await _db.Students.FirstOrDefaultAsync(x => x.documentNumber == documentNumber);
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
        public async Task<IEnumerable<StudentBUEDTO>> GetAsync() =>

            _mapper.Map<IEnumerable<StudentBUEDTO>>(await _db.Students.ToListAsync());

        public async Task<StudentBUEDTO> GetByIdAsync(int code, int currentSchoolYear)
        {
            var student = await _db.Students.Include(s => s.courseGrade).Include(s => s.level)
                .FirstOrDefaultAsync(x => x.studentCode == code && x.currentSchoolYear == currentSchoolYear);
            if (student is not null)
            {
                var nextCourse = await _db.CourseGrades.FirstOrDefaultAsync(x => x.Code == student.courseGrade.NextCourse);
                student.nextGrade = nextCourse;
            }
            var studentDto = _mapper.Map<StudentBUEDTO>(student);

            return studentDto;
        }



        public async Task<StudentBUEDTO> UpdateAsync(string documentNumber, StudentBUEDTO entity)
        {
            StudentBUE student = _mapper.Map<StudentBUEDTO, StudentBUE>(entity);
            var studentAfter = await _db.Students.AsNoTracking().FirstOrDefaultAsync(x => x.documentNumber == documentNumber);
            if (studentAfter is null)
            {
                throw new NullReferenceException(MessageUtil.Instance.NotFound);
            }
            _db.Students.Update(student);
            await _db.SaveChangesAsync();
            return _mapper.Map<StudentBUE, StudentBUEDTO>(student);
        }
        public async Task<StudentBUEDTO> UpdateImageAsync(int studentCode, byte[] image)
        {
            var studentAfter = await _db.Students.AsNoTracking().FirstOrDefaultAsync(x => x.studentCode == studentCode && x.currentSchoolYear == 10);
            if (studentAfter is null)
            {
                throw new NullReferenceException(MessageUtil.Instance.NotFound);
            }
            studentAfter.photo = image;
            _db.Students.Update(studentAfter);
            await _db.SaveChangesAsync();
            return _mapper.Map<StudentBUE, StudentBUEDTO>(studentAfter);
        }
    }
}
