using BUE.Inscriptions.Infrastructure.Interfaces;
using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Entity.DTO.storeProcedures;
using BUE.Inscriptions.Domain.Paging;
using BUE.Inscriptions.Domain.Response;
using BUE.Inscriptions.Application.Interfaces;
using BUE.Inscriptions.Shared.Utils;
using Newtonsoft.Json;
using BUE.Inscriptions.Domain.Request;

namespace BUE.Inscriptions.Application.Implementation
{
    public class PortalRequestService : IPortalRequestService
    {
        private IPortalRequestRepository _requestRep;
        private IMailNotification _serMailNotification;
        public PortalRequestService(IPortalRequestRepository requestRep, IMailNotification serMailNotification)
        {
            _requestRep = requestRep;
            _serMailNotification = serMailNotification;
        }
        public async Task<IBaseResponse<PortalRequestDTO>> CreateServiceAsync(PortalRequestDTO model)
        {
            var baseResponse = new BaseResponse<PortalRequestDTO>();
            var request = await _requestRep.CreateAsync(model);
            if (request is null)
            {
                baseResponse.Message = MessageUtil.Instance.Warning;
                baseResponse.status = false;
            }
            baseResponse.Data = request;
            return baseResponse;
        }
        public async Task<IBaseResponse<bool>> DeleteServiceAsync(int id)
        {
            var baseResponse = new BaseResponse<bool>();
            bool IsSuccess = await _requestRep.DeleteAsync(id);
            if (!IsSuccess)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = IsSuccess;
            return baseResponse;
        }
        public async Task<IBaseResponse<PortalRequestDTO>> GetByIdServiceAsync(int id)
        {
            var baseResponse = new BaseResponse<PortalRequestDTO>();
            PortalRequestDTO request = await _requestRep.GetByIdAsync(id);
            if (request is null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = request;
            return baseResponse;
        }
        public async Task<IBaseResponse<PagedList<PortalRequestDTO>>> GetServiceAsync(PagingQueryParameters paging)
        {
            var baseResponse = new BaseResponse<PagedList<PortalRequestDTO>>();
            var requests = await _requestRep.GetBuilderAsync(paging.search);
            if (requests is null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = PagedList<PortalRequestDTO>.ToPagedList(requests, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }

        public async Task<IBaseResponse<PagedList<PortalRequestDTO>>> GetWithRepresentativeAsync(PagingQueryParameters paging)
        {
            var baseResponse = new BaseResponse<PagedList<PortalRequestDTO>>();
            var requests = await _requestRep.GetWithRepresentativeAsync(paging.search);
            if (requests is null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = PagedList<PortalRequestDTO>.ToPagedList(requests, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }

        public async Task<IBaseResponse<PagedList<PortalRequestDTO>>> GetWithSecondRepresentativeAsync(PagingQueryParameters paging)
        {
            var baseResponse = new BaseResponse<PagedList<PortalRequestDTO>>();
            var requests = await _requestRep.GetWithSecondRepresentativeAsync(paging.search);
            if (requests is null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = PagedList<PortalRequestDTO>.ToPagedList(requests, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }

        public async Task<IBaseResponse<PagedList<PortalRequestDTO>>> GetByFilterServiceAsync(PagingQueryParameters paging, int status = 1, int currentYearSchool = 1)
        {
            var baseResponse = new BaseResponse<PagedList<PortalRequestDTO>>();
            var requests = await _requestRep.GetByFilterServiceAsync(status, currentYearSchool, paging);
            if (requests is null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = PagedList<PortalRequestDTO>.ToPagedList(requests, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }
        public async Task<IBaseResponse<PagedList<PortalRequestDTO>>> GetByUserServiceAsync(PagingQueryParameters paging, int userCode = 1, int currentYearSchool = 1)
        {
            var baseResponse = new BaseResponse<PagedList<PortalRequestDTO>>();
            var requests = await _requestRep.GetByUserAsync(userCode, currentYearSchool);
            if (requests is null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = PagedList<PortalRequestDTO>.ToPagedList(requests, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }

        public async Task<IBaseResponse<PortalRequestDTO>> UpdateServiceAsync(int id, PortalRequestDTO model)
        {

            var baseResponse = new BaseResponse<PortalRequestDTO>();
            PortalRequestDTO request = await _requestRep.UpdateAsync(id, model);
            baseResponse.Message = MessageUtil.Instance.Updated;
            baseResponse.Data = request;
            return baseResponse;
        }  
        public async Task<IBaseResponse<PortalRequestDTO>> UpdateUrlReportCompleteAsync(int id, string urlReportComplete)
        {

            var baseResponse = new BaseResponse<PortalRequestDTO>();
            PortalRequestDTO request = await _requestRep.UpdateUrlReportCompleteAsync(id, urlReportComplete);
            baseResponse.Message = MessageUtil.Instance.Updated;
            baseResponse.Data = request;
            return baseResponse;
        }
        public async Task<IBaseResponse<PagedList<PortalRequestDTO>>> UpdateAnyServiceAsync(IEnumerable<PortalRequestDTO> models)
        {

            var baseResponse = new BaseResponse<PagedList<PortalRequestDTO>>();
            var requests = await _requestRep.UpdateAnyAsync(models);
            baseResponse.Message = MessageUtil.Instance.Updated;
            baseResponse.Data = PagedList<PortalRequestDTO>.ToPagedList(requests, 1, 1000);
            return baseResponse;
        }
        public async Task<IBaseResponse<PagedList<PortalRequestDTO>>> UpdateStatusServiceAsync(IEnumerable<PortalRequestDTO> models)
        {
            var baseResponse = new BaseResponse<PagedList<PortalRequestDTO>>();
            var requests = await _requestRep.UpdateStatusAnyAsync(models);
            baseResponse.Message = MessageUtil.Instance.Updated;
            baseResponse.Data = PagedList<PortalRequestDTO>.ToPagedList(requests, 1, 1000);
            var emailNotifications = await _requestRep.BuildSendNotification(baseResponse.Data);
            foreach(var item in emailNotifications)
            {
                await _serMailNotification.sendMailNoticationWithTreating(item);
            }
            //Task.Run(() => sendNotification(models));
            return baseResponse;
        }

        public async Task<IBaseResponse<PagedList<PortalRequestDTO>>> UpdateNotesServiceAsync(IEnumerable<PortalRequestDTO> models)
        {

            var baseResponse = new BaseResponse<PagedList<PortalRequestDTO>>();
            var requests = await _requestRep.UpdateNotesAnyAsync(models);
            baseResponse.Message = MessageUtil.Instance.Updated;
            baseResponse.Data = PagedList<PortalRequestDTO>.ToPagedList(requests, 1, 1000); ;
            return baseResponse;
        }
        public async Task<IBaseResponse<int>> CreateIntegrationAsync(IntegrationRegisterDTO entity)
        {
            var baseResponse = new BaseResponse<int>();
            var response = await _requestRep.CreateIntegrationAsync(entity);
            if (response <= 0)
                baseResponse.Message = MessageUtil.Instance.RECORD_NOT_CREATED;
            else
                baseResponse.Message = MessageUtil.Instance.Created;
            baseResponse.Data = response;
            return baseResponse;
        }
        public async Task<IBaseResponse<int>> CreateIntegrationFirstContactAsync(IntegrationRegisterDTO entity)
        {
            var baseResponse = new BaseResponse<int>();
            var response = await _requestRep.CreateIntegrationFirstContactAsync(entity);
            if (response <= 0)
                baseResponse.Message = MessageUtil.Instance.RECORD_NOT_CREATED;
            else
                baseResponse.Message = MessageUtil.Instance.Created;
            baseResponse.Data = response;
            return baseResponse;
        }
        public async Task<IBaseResponse<int>> CreateIntegrationSecondContactAsync(IntegrationRegisterDTO entity)
        {
            var baseResponse = new BaseResponse<int>();
            var response = await _requestRep.CreateIntegrationSecondContactAsync(entity);
            if (response <= 0)
                baseResponse.Message = MessageUtil.Instance.RECORD_NOT_CREATED;
            else
                baseResponse.Message = MessageUtil.Instance.Created;
            baseResponse.Data = response;
            return baseResponse;
        }

        public async Task<IBaseResponse<int>> ExecuteIntegrationStudentsAsync(IntegrationRegisterDTO entity)
        {
            var baseResponse = new BaseResponse<int>();
            var response = await _requestRep.ExecuteIntegrationStudentsAsync(entity);
            if (response <= 0)
                baseResponse.Message = MessageUtil.Instance.RECORD_NOT_CREATED;
            else
                baseResponse.Message = MessageUtil.Instance.Created;
            baseResponse.Data = response;
            return baseResponse;
        }
        public async Task<IBaseResponse<int>> ExecuteIntegrationContactsAsync(IntegrationRegisterDTO entity)
        {
            var baseResponse = new BaseResponse<int>();
            var response = await _requestRep.ExecuteIntegrationContactsAsync(entity);
            if (response <= 0)
                baseResponse.Message = MessageUtil.Instance.RECORD_NOT_CREATED;
            else
                baseResponse.Message = MessageUtil.Instance.Created;
            baseResponse.Data = response;
            return baseResponse;
        }
        public async Task<IBaseResponse<int>> CreateIntegrationAutorizationPeopleAsync(IntegrationRegisterDTO entity)
        {
            var baseResponse = new BaseResponse<int>();
            var response = await _requestRep.CreateIntegrationAutotizationPeopleAsync(entity);
            if (response <= 0)
                baseResponse.Message = MessageUtil.Instance.RECORD_NOT_CREATED;
            else
                baseResponse.Message = MessageUtil.Instance.Created;
            baseResponse.Data = response;
            return baseResponse;
        }
        public async Task<IBaseResponse<int>> ExecuteIntegrationAutorizationPeopleAsync(IntegrationRegisterDTO entity)
        {
            var baseResponse = new BaseResponse<int>();
            var response = await _requestRep.ExecuteIntegrationAutorizationPeopleAsync(entity);
            if (response <= 0)
                baseResponse.Message = MessageUtil.Instance.RECORD_NOT_CREATED;
            else
                baseResponse.Message = MessageUtil.Instance.Created;
            baseResponse.Data = response;
            return baseResponse;
        }
        public async Task<IBaseResponse<PagedList<DashBoardChartDTO>>> GetDashboardServiceAsync(IntegrationRegisterDTO entity)
        {
            var baseResponse = new BaseResponse<PagedList<DashBoardChartDTO>>();
            var response = await _requestRep.GetDashboardAsync(entity);
            if (response is null)
                baseResponse.Message = MessageUtil.Instance.RECORD_NOT_FOUND;
            else
                baseResponse.Message = MessageUtil.Instance.Success;
            baseResponse.Data = PagedList<DashBoardChartDTO>.ToPagedList(response, 1, 1000);
            return baseResponse;
        }
        public async Task<IBaseResponse<IEnumerable<PortalRequestDTO>>> GetStudentsByCodeAsync(IEnumerable<HelperFieldValidate> helperFields)
        {
            var baseResponse = new BaseResponse<IEnumerable<PortalRequestDTO>>();
            var students = await _requestRep.GetStudentsByCodeAsync(helperFields);
            if (students is null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = students;
            return baseResponse;
        }

        
        public async Task<IBaseResponse<IEnumerable<WelcomeRequestDTO>>> GetStudentsAndRequestsByUserAsync(int userId, int currentSchoolYear)
        {
            var baseResponse = new BaseResponse<IEnumerable<WelcomeRequestDTO>>();
            var students = await _requestRep.GetStudentsAndRequestsByUserAsync(userId, currentSchoolYear);
            if (students is null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = students;
            return baseResponse;
        }
    }
}
