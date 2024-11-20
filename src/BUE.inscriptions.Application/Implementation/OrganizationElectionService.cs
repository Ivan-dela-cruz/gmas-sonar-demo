using BUE.Inscriptions.Application.Interfaces;
using BUE.Inscriptions.Domain.Elecctions.DTO;
using BUE.Inscriptions.Domain.Paging;
using BUE.Inscriptions.Domain.Response;
using BUE.Inscriptions.Infrastructure.Interfaces;
using BUE.Inscriptions.Shared.Utils;


namespace BUE.Inscriptions.Application.Implementation
{
    public class OrganizationElectionService : IOrganizationElectionService
    {
        private readonly IOrganizationElectionRepository _organizationElectionRepository;

        public OrganizationElectionService(IOrganizationElectionRepository organizationElectionRepository)
        {
            _organizationElectionRepository = organizationElectionRepository;
        }

        public async Task<IBaseResponse<OrganizationElectionDTO>> CreateServiceAsync(OrganizationElectionDTO model)
        {
            var baseResponse = new BaseResponse<OrganizationElectionDTO>();
            var organizationElection = await _organizationElectionRepository.CreateAsync(model);
            baseResponse.Data = organizationElection;
            return baseResponse;
        }

        public async Task<IBaseResponse<bool>> DeleteServiceAsync(int id)
        {
            var baseResponse = new BaseResponse<bool>();
            bool isSuccess = await _organizationElectionRepository.DeleteAsync(id);

            if (!isSuccess)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = isSuccess;
            return baseResponse;
        }

        public async Task<IBaseResponse<OrganizationElectionDTO>> GetByIdServiceAsync(int id)
        {
            var baseResponse = new BaseResponse<OrganizationElectionDTO>();
            OrganizationElectionDTO organizationElection = await _organizationElectionRepository.GetByIdAsync(id);

            if (organizationElection == null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
                return baseResponse;
            }
            baseResponse.Data = organizationElection;
            return baseResponse;
        }
        public async Task<IBaseResponse<PagedList<OrganizationElectionDTO>>>  GetByElectionIdServiceAsync(int id, PagingQueryParameters paging)
        {
            var baseResponse = new BaseResponse<PagedList<OrganizationElectionDTO>>();
            var organizationElections = await _organizationElectionRepository.GetByElectionIdAsync(id);

            if (organizationElections == null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
                return baseResponse;
            }
            baseResponse.Data = PagedList<OrganizationElectionDTO>.ToPagedList(organizationElections, paging.PageNumber, paging.PageSize); 
            return baseResponse;
        }

        public async Task<IBaseResponse<PagedList<OrganizationElectionDTO>>> GetServiceAsync(PagingQueryParameters paging)
        {
            var baseResponse = new BaseResponse<PagedList<OrganizationElectionDTO>>();
            var organizationElections = await _organizationElectionRepository.GetAsync();

            if (organizationElections == null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = PagedList<OrganizationElectionDTO>.ToPagedList(organizationElections, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }

        public async Task<IBaseResponse<OrganizationElectionDTO>> UpdateServiceAsync(int id, OrganizationElectionDTO model)
        {
            var baseResponse = new BaseResponse<OrganizationElectionDTO>();
            OrganizationElectionDTO organizationElection = await _organizationElectionRepository.UpdateAsync(id, model);
            baseResponse.Message = MessageUtil.Instance.Updated;
            baseResponse.Data = organizationElection;
            return baseResponse;
        }
    }
}
