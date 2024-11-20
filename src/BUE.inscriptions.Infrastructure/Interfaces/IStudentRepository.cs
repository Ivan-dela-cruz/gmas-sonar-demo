using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Inscriptions.DTO;
using BUE.Inscriptions.Domain.Inscriptions.Entities;

namespace BUE.Inscriptions.Infrastructure.Interfaces
{
    public interface IStudentRepository : IBaseRepository<StudentDTO>
    {
        public string Message {  get; set; }
        public string StatusCode {  get; set; }
        Task<StudentDTO> GetByIdAsync(int id);
        Task<IEnumerable<StudentDTO>> GetByAcademicYearAsync(int academicYearId, int companyId);
        Task<IEnumerable<StudentDTO>> GetByUserIdAsync(int userId, int companyId);
        Task<StudentDetails> GetStudentDetailsAsync(int studentId, int academicYearId);
        Task<StudentDTO> UpdateImageBueAsync(int externalId, FileStorageDTO filesStorage);
        Task<IEnumerable<StudentDetails>> GetAllStudentsAsync(int academicYearId);
    }
}
