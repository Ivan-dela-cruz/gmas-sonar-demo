using BUE.Inscriptions.Domain.Entity;
using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Entity.DTO.storeProcedures;
using BUE.Inscriptions.Domain.Paging;
using BUE.Inscriptions.Domain.Request;

namespace BUE.Inscriptions.Infrastructure.Interfaces
{

    public interface IPortalRequestRepository : IBaseRepository<PortalRequestDTO>
    {
        Task<PortalRequestDTO> GetByIdAsync(int id);
        Task<IEnumerable<PortalRequestDTO>> UpdateAnyAsync(IEnumerable<PortalRequestDTO> entities);
        Task<IEnumerable<PortalRequestDTO>> GetByUserAsync(int userCode = 1, int currentYearSchool = 1);
        Task<IEnumerable<PortalRequestDTO>> GetBuilderAsync(string search);
        Task<IEnumerable<PortalRequestDTO>> GetWithRepresentativeAsync(string search);
        Task<IEnumerable<PortalRequestDTO>> GetWithSecondRepresentativeAsync(string search);
        Task<IEnumerable<PortalRequestDTO>> UpdateStatusAnyAsync(IEnumerable<PortalRequestDTO> entities);
        Task<IEnumerable<PortalRequestDTO>> UpdateNotesAnyAsync(IEnumerable<PortalRequestDTO> entities);
        Task<int> CreateIntegrationAsync(IntegrationRegisterDTO entity);
        Task<int> ExecuteIntegrationStudentsAsync(IntegrationRegisterDTO entity);
        Task<int> ExecuteIntegrationContactsAsync(IntegrationRegisterDTO entity);
        Task<IEnumerable<DashBoardChartDTO>> GetDashboardAsync(IntegrationRegisterDTO entity);
        Task<int> CreateIntegrationFirstContactAsync(IntegrationRegisterDTO entity);
        Task<int> CreateIntegrationSecondContactAsync(IntegrationRegisterDTO entity);
        Task<int> CreateIntegrationAutotizationPeopleAsync(IntegrationRegisterDTO entity);
        Task<int> ExecuteIntegrationAutorizationPeopleAsync(IntegrationRegisterDTO entity);
        Task<IEnumerable<PortalRequestDTO>> GetByFilterServiceAsync(int status = 1, int currentYearSchool = 1, PagingQueryParameters parameters = null);
        Task<IEnumerable<PortalRequestDTO>> GetListNotificationAsync(List<int> ids);
        Task<IEnumerable<MailNotificactionDTO>> BuildSendNotification(IEnumerable<PortalRequestDTO> models);
        Task<PortalRequestDTO> UpdateUrlReportCompleteAsync(int id, string urlComplete);
        Task<IEnumerable<PortalRequestDTO>> GetStudentsByCodeAsync(IEnumerable<HelperFieldValidate> helperFields);
        Task<IEnumerable<WelcomeRequestDTO>> GetStudentsAndRequestsByUserAsync(int userId, int currentSchoolYear);
    }
}
