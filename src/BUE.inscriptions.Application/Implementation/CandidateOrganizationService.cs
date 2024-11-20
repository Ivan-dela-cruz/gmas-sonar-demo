using BUE.Inscriptions.Application.Interfaces;
using BUE.Inscriptions.Domain.Elecctions.DTO;
using BUE.Inscriptions.Domain.Paging;
using BUE.Inscriptions.Domain.Response;
using BUE.Inscriptions.Infrastructure.Interfaces;
using BUE.Inscriptions.Shared.Utils;


namespace BUE.Inscriptions.Application.Implementation
{
    public class CandidateOrganizationService : ICandidateOrganizationService
    {
        private readonly ICandidateOrganizationRepository _candidateOrganizationRepository;

        public CandidateOrganizationService(ICandidateOrganizationRepository candidateOrganizationRepository)
        {
            _candidateOrganizationRepository = candidateOrganizationRepository;
        }

        public async Task<IBaseResponse<CandidateOrganizationDTO>> CreateServiceAsync(CandidateOrganizationDTO model)
        {
            var baseResponse = new BaseResponse<CandidateOrganizationDTO>();
            var candidateOrganization = await _candidateOrganizationRepository.CreateAsync(model);
            baseResponse.Data = candidateOrganization;
            return baseResponse;
        }

        public async Task<IBaseResponse<bool>> DeleteServiceAsync(int id)
        {
            var baseResponse = new BaseResponse<bool>();
            bool isSuccess = await _candidateOrganizationRepository.DeleteAsync(id);

            if (!isSuccess)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = isSuccess;
            return baseResponse;
        }
        public async Task<IBaseResponse<bool>> DeleteCandidateOrganizationServiceAsync(int organizationId, int candidateId)
        {
            var baseResponse = new BaseResponse<bool>();
            bool isSuccess = await _candidateOrganizationRepository.DeleteOrganizationCandidateAsync(organizationId, candidateId);

            if (!isSuccess)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = isSuccess;
            return baseResponse;
        }

        public async Task<IBaseResponse<CandidateOrganizationDTO>> GetByIdServiceAsync(int id)
        {
            var baseResponse = new BaseResponse<CandidateOrganizationDTO>();
            CandidateOrganizationDTO candidateOrganization = await _candidateOrganizationRepository.GetByIdAsync(id);

            if (candidateOrganization == null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
                return baseResponse;
            }
            baseResponse.Data = candidateOrganization;
            return baseResponse;
        }
        public async Task<IBaseResponse<PagedList<CandidateOrganizationDTO>>>  GetByOrganizationIdServiceAsync(int id, PagingQueryParameters paging)
        {
            var baseResponse = new BaseResponse<PagedList<CandidateOrganizationDTO>>();
            var candidateOrganizations = await _candidateOrganizationRepository.GetByOrganizationIdAsync(id);

            if (candidateOrganizations == null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
                return baseResponse;
            }
            baseResponse.Data = PagedList<CandidateOrganizationDTO>.ToPagedList(candidateOrganizations, paging.PageNumber, paging.PageSize); 
            return baseResponse;
        }

        public async Task<IBaseResponse<PagedList<CandidateOrganizationDTO>>> GetServiceAsync(PagingQueryParameters paging)
        {
            var baseResponse = new BaseResponse<PagedList<CandidateOrganizationDTO>>();
            var candidateOrganizations = await _candidateOrganizationRepository.GetAsync();

            if (candidateOrganizations == null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = PagedList<CandidateOrganizationDTO>.ToPagedList(candidateOrganizations, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }

        public async Task<IBaseResponse<CandidateOrganizationDTO>> UpdateServiceAsync(int id, CandidateOrganizationDTO model)
        {
            var baseResponse = new BaseResponse<CandidateOrganizationDTO>();
            CandidateOrganizationDTO candidateOrganization = await _candidateOrganizationRepository.UpdateAsync(id, model);
            baseResponse.Message = MessageUtil.Instance.Updated;
            baseResponse.Data = candidateOrganization;
            return baseResponse;
        }
    }
}
