using AutoMapper;
using BUE.Inscriptions.Infrastructure.Interfaces;
using BUE.Inscriptions.Shared.Utils;
using Microsoft.EntityFrameworkCore;
using BUE.Inscriptions.Domain.Inscriptions.Entities;
using BUE.Inscriptions.Domain.Inscriptions.DTO;


namespace BUE.Inscriptions.Infrastructure.Repository
{
    public class StudentFamiliesRepository : BaseRepository, IStudentFamiliesRepository
    {
        private readonly PortalMatriculasDBContext _db;
        private readonly IMapper _mapper;
        public string Message { get; set; }
        public string StatusCode { get; set; }

        public StudentFamiliesRepository(PortalMatriculasDBContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
       

        public async Task<StudentFamiliesDTO> CreateAsync(StudentFamiliesDTO entity)
        {
            try
            {
                StudentFamilies studentFamilies = _mapper.Map<StudentFamiliesDTO, StudentFamilies>(entity);
                _db.StudentFamilies.Add(studentFamilies);
                await _db.SaveChangesAsync();
                return _mapper.Map<StudentFamilies, StudentFamiliesDTO>(studentFamilies);

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
                StudentFamilies studentFamilies = await _db.StudentFamilies.FindAsync(id);
                if (studentFamilies == null)
                {
                    return false;
                }
                _db.StudentFamilies.Remove(studentFamilies);
                await _db.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<StudentFamiliesDTO>> GetAsync()
        {
            var studentFamiliess = await _db.StudentFamilies
                   .ToListAsync();
            return _mapper.Map<IEnumerable<StudentFamiliesDTO>>(studentFamiliess);

        }

        public async Task<StudentFamiliesDTO> GetByIdAsync(int id)
        {
            var studentFamilies = await _db.StudentFamilies
                .FirstOrDefaultAsync(e => e.Id == id);
            return _mapper.Map<StudentFamiliesDTO>(studentFamilies);
        }

        public async Task<StudentFamiliesDTO> UpdateAsync(int id, StudentFamiliesDTO entity)
        {
            try
            {
                var studentFamilies = _mapper.Map<StudentFamiliesDTO, StudentFamilies>(entity);
                var existingStudentFamilies = await _db.StudentFamilies.FindAsync(id);

                if (existingStudentFamilies == null)
                {
                    throw new NullReferenceException(MessageUtil.Instance.NotFound);
                }
                var currentStudentFamilies = MapProperties(studentFamilies, existingStudentFamilies);
                _db.StudentFamilies.Update(currentStudentFamilies);
                await _db.SaveChangesAsync();
                return _mapper.Map<StudentFamilies, StudentFamiliesDTO>(currentStudentFamilies);
            }
            catch (Exception ex)
            {
                HandlerOperationException(ex);
                Message = this.MessageException;
                StatusCode = this.CodeException;
                return null;
            }
        }
    }
}

