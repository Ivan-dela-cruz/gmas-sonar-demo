using BUE.Inscriptions.Application.Interfaces;
using BUE.Inscriptions.Domain.Elecctions.DTO;
using BUE.Inscriptions.Domain.Paging;
using BUE.Inscriptions.Domain.Response;
using BUE.Inscriptions.Infrastructure.Interfaces;
using BUE.Inscriptions.Shared.Utils;


namespace BUE.Inscriptions.Application.Implementation
{
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _candidateRepository;

        public CandidateService(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        public async Task<IBaseResponse<CandidateDTO>> CreateServiceAsync(CandidateDTO model)
        {
            var baseResponse = new BaseResponse<CandidateDTO>();
            var candidate = await _candidateRepository.CreateAsync(model);
            baseResponse.Data = candidate;
            return baseResponse;
        }

        public async Task<IBaseResponse<bool>> DeleteServiceAsync(int id)
        {
            var baseResponse = new BaseResponse<bool>();
            bool isSuccess = await _candidateRepository.DeleteAsync(id);

            if (!isSuccess)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = isSuccess;
            return baseResponse;
        }

        public async Task<IBaseResponse<CandidateDTO>> GetByIdServiceAsync(int id)
        {
            var baseResponse = new BaseResponse<CandidateDTO>();
            CandidateDTO candidate = await _candidateRepository.GetByIdAsync(id);

            if (candidate == null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
                return baseResponse;
            }
            baseResponse.Data = candidate;
            return baseResponse;
        }
        public async Task<IBaseResponse<PagedList<CandidateDTO>>>  GetByElectionIdServiceAsync(int id, PagingQueryParameters paging)
        {
            var baseResponse = new BaseResponse<PagedList<CandidateDTO>>();
            var candidates = await _candidateRepository.GetByElectionIdAsync(id);

            if (candidates == null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
                return baseResponse;
            }
            baseResponse.Data = PagedList<CandidateDTO>.ToPagedList(candidates, paging.PageNumber, paging.PageSize); 
            return baseResponse;
        }

        public async Task<IBaseResponse<PagedList<CandidateDTO>>> GetServiceAsync(PagingQueryParameters paging)
        {
            var baseResponse = new BaseResponse<PagedList<CandidateDTO>>();
            var candidates = await _candidateRepository.GetAsync();

            if (candidates == null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = PagedList<CandidateDTO>.ToPagedList(candidates, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }

        public async Task<IBaseResponse<CandidateDTO>> UpdateServiceAsync(int id, CandidateDTO model)
        {
            var baseResponse = new BaseResponse<CandidateDTO>();
            CandidateDTO candidate = await _candidateRepository.UpdateAsync(id, model);
            baseResponse.Message = MessageUtil.Instance.Updated;
            baseResponse.Data = candidate;
            return baseResponse;
        }
    }
}
