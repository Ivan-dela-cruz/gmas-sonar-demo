using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Inscriptions.DTO;
using BUE.Inscriptions.Domain.Inscriptions.Entities;
using BUE.Inscriptions.Domain.Paging;
using BUE.Inscriptions.Domain.Response;

namespace BUE.Inscriptions.Application.Interfaces
{
    public interface IStudentService : IBaseService<StudentDTO>
    {
        Task<IBaseResponse<StudentDTO>> GetByIdServiceAsync(int id);
        Task<IBaseResponse<PagedList<StudentDTO>>> GetByAcademicYearAsync(PagingQueryParameters paging);
        Task<IBaseResponse<PagedList<StudentDTO>>> GetByUserIdAsync(PagingQueryParameters paging, int userId);
        Task<IBaseResponse<StudentDetails>> GetStudentDetailsServiceAsync(int studentId, int academicYearId);
        Task<IBaseResponse<StudentDTO>> UpdateImageServiceAsync(int externalId, FileStorageDTO filesStorage);
    }
}
