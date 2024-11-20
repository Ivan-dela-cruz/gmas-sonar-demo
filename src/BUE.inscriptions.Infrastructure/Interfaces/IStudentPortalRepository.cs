using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Request;

namespace BUE.Inscriptions.Infrastructure.Interfaces
{

    public interface IStudentPortalRepository
    {
        Task<StudentPortalDTO> GetByIdAsync(int code, int currentSchoolYear);
        Task<IEnumerable<StudentPortalDTO>> GetAsync();
        Task<StudentPortalDTO> CreateAsync(StudentPortalDTO model);
        Task<bool> DeleteAsync(string id);
        Task<StudentPortalDTO> UpdateAsync(int id, StudentPortalDTO model);
        Task<StudentPortalDTO> GetByPrimaryKeyAsync(int primaryKey);
        Task<StudentPortalDTO> UpdateDataEnrollmentAppAsync(int id, StudentPortalDTO entity);
        Task<IEnumerable<StudentPortalDTO>> GetListBasicAsync();
        Task<IEnumerable<StudentPortalDTO>> GetStudentsByCodeAsync(IEnumerable<HelperFieldValidate> helperFields);
    }
}
