using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Paging;
using BUE.Inscriptions.Domain.Request;
using BUE.Inscriptions.Domain.Response;

namespace BUE.Inscriptions.Application.Interfaces
{

    public interface IStudentPortalService
    {
        Task<IBaseResponse<StudentPortalDTO>> GetByIdServiceAsync(int id, int currentSchoolYear);
        Task<IBaseResponse<PagedList<StudentPortalDTO>>> GetServiceAsync(PagingQueryParameters paging);
        Task<IBaseResponse<StudentPortalDTO>> CreateServiceAsync(StudentPortalDTO entity);
        Task<IBaseResponse<StudentPortalDTO>> UpdateServiceAsync(int id, StudentPortalDTO entity);
        Task<IBaseResponse<bool>> DeleteServiceAsync(string id);
        Task<IBaseResponse<StudentBUEDTO>> CrearteToBueServiceAsync(PortalRequestDTO model);
        Task<IBaseResponse<StudentPortalDTO>> UpdateEnrollmentAppServiceAsync(int id, StudentPortalDTO model);
        Task<IBaseResponse<PagedList<StudentPortalDTO>>> GetListBasicServiceAsync(PagingQueryParameters paging);
        Task<IBaseResponse<IEnumerable<StudentPortalDTO>>> GetStudentsByCodeAsync(IEnumerable<HelperFieldValidate> helperFields);
    }
}
