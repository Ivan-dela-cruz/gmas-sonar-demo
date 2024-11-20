using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Paging;
using BUE.Inscriptions.Domain.Response;

namespace BUE.Inscriptions.Application.Interfaces
{

    public interface IStudentBUEService
    {
        Task<IBaseResponse<StudentBUEDTO>> GetByIdServiceAsync(int id, int currentSchoolYear);
        Task<IBaseResponse<PagedList<StudentBUEDTO>>> GetServiceAsync(PagingQueryParameters paging);
        Task<IBaseResponse<StudentBUEDTO>> CreateServiceAsync(StudentBUEDTO entity);
        Task<IBaseResponse<StudentBUEDTO>> UpdateServiceAsync(string id, StudentBUEDTO entity);
        Task<IBaseResponse<bool>> DeleteServiceAsync(string id);
    }
}
