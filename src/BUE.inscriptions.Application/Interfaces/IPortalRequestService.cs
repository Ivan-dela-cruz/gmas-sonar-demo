using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Entity.DTO.storeProcedures;
using BUE.Inscriptions.Domain.Paging;
using BUE.Inscriptions.Domain.Request;
using BUE.Inscriptions.Domain.Response;

namespace BUE.Inscriptions.Application.Interfaces
{

    public interface IPortalRequestService : IBaseService<PortalRequestDTO>
    {
        Task<IBaseResponse<PortalRequestDTO>> GetByIdServiceAsync(int id);
        Task<IBaseResponse<PagedList<PortalRequestDTO>>> UpdateAnyServiceAsync(IEnumerable<PortalRequestDTO> models);
        Task<IBaseResponse<PagedList<PortalRequestDTO>>> GetByFilterServiceAsync(PagingQueryParameters paging, int status = 1, int currentYearSchool = 1);
        Task<IBaseResponse<PagedList<PortalRequestDTO>>> GetByUserServiceAsync(PagingQueryParameters paging, int userCode = 1, int currentYearSchool = 1);
        Task<IBaseResponse<PagedList<PortalRequestDTO>>> GetWithRepresentativeAsync(PagingQueryParameters paging);
        Task<IBaseResponse<PagedList<PortalRequestDTO>>> GetWithSecondRepresentativeAsync(PagingQueryParameters paging);
        Task<IBaseResponse<PagedList<PortalRequestDTO>>> UpdateStatusServiceAsync(IEnumerable<PortalRequestDTO> models);
        Task<IBaseResponse<PagedList<PortalRequestDTO>>> UpdateNotesServiceAsync(IEnumerable<PortalRequestDTO> models);
        Task<IBaseResponse<int>> CreateIntegrationAsync(IntegrationRegisterDTO entity);
        Task<IBaseResponse<int>> ExecuteIntegrationStudentsAsync(IntegrationRegisterDTO entity);
        Task<IBaseResponse<int>> ExecuteIntegrationContactsAsync(IntegrationRegisterDTO entity);
        Task<IBaseResponse<PagedList<DashBoardChartDTO>>> GetDashboardServiceAsync(IntegrationRegisterDTO entity);
        Task<IBaseResponse<int>> CreateIntegrationFirstContactAsync(IntegrationRegisterDTO entity);
        Task<IBaseResponse<int>> CreateIntegrationSecondContactAsync(IntegrationRegisterDTO entity);
        Task<IBaseResponse<int>> CreateIntegrationAutorizationPeopleAsync(IntegrationRegisterDTO entity);
        Task<IBaseResponse<int>> ExecuteIntegrationAutorizationPeopleAsync(IntegrationRegisterDTO entity);
        Task<IBaseResponse<PortalRequestDTO>> UpdateUrlReportCompleteAsync(int id, string urlReportComplete);
        Task<IBaseResponse<IEnumerable<PortalRequestDTO>>> GetStudentsByCodeAsync(IEnumerable<HelperFieldValidate> helperFields);
        Task<IBaseResponse<IEnumerable<WelcomeRequestDTO>>> GetStudentsAndRequestsByUserAsync(int userId, int currentSchoolYear);
    }
}
