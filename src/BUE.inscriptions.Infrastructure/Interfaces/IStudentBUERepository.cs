using BUE.Inscriptions.Domain.Entity.DTO;

namespace BUE.Inscriptions.Infrastructure.Interfaces
{

    public interface IStudentBUERepository
    {
        Task<StudentBUEDTO> GetByIdAsync(int code, int currentSchoolYear);
        Task<IEnumerable<StudentBUEDTO>> GetAsync();
        Task<StudentBUEDTO> CreateAsync(StudentBUEDTO model);
        Task<bool> DeleteAsync(string id);
        Task<StudentBUEDTO> UpdateAsync(string id, StudentBUEDTO model);
        Task<StudentBUEDTO> UpdateImageAsync(int studentCode, byte[] image);
    }
}
