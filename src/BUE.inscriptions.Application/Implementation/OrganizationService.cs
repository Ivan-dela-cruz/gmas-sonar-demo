using BUE.Inscriptions.Application.Interfaces;
using BUE.Inscriptions.Domain.Elecctions.DTO;
using BUE.Inscriptions.Domain.Paging;
using BUE.Inscriptions.Domain.Response;
using BUE.Inscriptions.Infrastructure.Interfaces;
using BUE.Inscriptions.Shared.Utils;


namespace BUE.Inscriptions.Application.Implementation
{
    public class OrganizationService : IOrganizationService
    {
        private readonly IOrganizationRepository _organizationRepository;
        private readonly ICandidateOrganizationRepository _candidateOrganizationRepository;

        public OrganizationService(IOrganizationRepository organizationRepository, ICandidateOrganizationRepository candidateOrganizationRepository)
        {
            _organizationRepository = organizationRepository;
            _candidateOrganizationRepository = candidateOrganizationRepository;
        }

        public async Task<IBaseResponse<OrganizationDTO>> CreateServiceAsync(OrganizationDTO model)
        {
            var baseResponse = new BaseResponse<OrganizationDTO>();
            var organization = await _organizationRepository.CreateAsync(model);
            baseResponse.Data = organization;
            return baseResponse;
        }

        public async Task<IBaseResponse<bool>> DeleteServiceAsync(int id)
        {
            var baseResponse = new BaseResponse<bool>();
            bool isSuccess = await _organizationRepository.DeleteAsync(id);

            if (!isSuccess)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = isSuccess;
            return baseResponse;
        }

        public async Task<IBaseResponse<OrganizationDTO>> GetByIdServiceAsync(int id)
        {
            var baseResponse = new BaseResponse<OrganizationDTO>();
            OrganizationDTO organization = await _organizationRepository.GetByIdAsync(id);
            var candidates = await _candidateOrganizationRepository.GetByOrganizationIdAsync(id);
            if (candidates != null)
            {
                organization.CandidateOrganizations = candidates;
            }
            if (organization == null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
                return baseResponse;
            }
            baseResponse.Data = organization;
            return baseResponse;
        }
        public async Task<IBaseResponse<PagedList<OrganizationDTO>>>  GetByElectionIdServiceAsync(int id, PagingQueryParameters paging)
        {
            var baseResponse = new BaseResponse<PagedList<OrganizationDTO>>();
            var organizations = await _organizationRepository.GetByElectionIdAsync(id);

            if (organizations == null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
                return baseResponse;
            }
            baseResponse.Data = PagedList<OrganizationDTO>.ToPagedList(organizations, paging.PageNumber, paging.PageSize); 
            return baseResponse;
        }

        public async Task<IBaseResponse<PagedList<OrganizationDTO>>> GetServiceAsync(PagingQueryParameters paging)
        {
            var baseResponse = new BaseResponse<PagedList<OrganizationDTO>>();
            var organizations = await _organizationRepository.GetAsync();

            if (organizations == null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = PagedList<OrganizationDTO>.ToPagedList(organizations, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }

        public async Task<IBaseResponse<OrganizationDTO>> UpdateServiceAsync(int id, OrganizationDTO model)
        {
            var baseResponse = new BaseResponse<OrganizationDTO>();
            OrganizationDTO organization = await _organizationRepository.UpdateAsync(id, model);
            baseResponse.Message = MessageUtil.Instance.Updated;
            baseResponse.Data = organization;
            return baseResponse;
        }
    }
}
