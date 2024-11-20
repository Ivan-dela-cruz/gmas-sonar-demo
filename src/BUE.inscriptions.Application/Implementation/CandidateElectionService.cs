using BUE.Inscriptions.Application.Interfaces;
using BUE.Inscriptions.Domain.Elecctions.DTO;
using BUE.Inscriptions.Domain.Paging;
using BUE.Inscriptions.Domain.Response;
using BUE.Inscriptions.Infrastructure.Interfaces;
using BUE.Inscriptions.Shared.Utils;


namespace BUE.Inscriptions.Application.Implementation
{
    public class CandidateElectionService : ICandidateElectionService
    {
        private readonly ICandidateElectionRepository _candidateRepository;

        public CandidateElectionService(ICandidateElectionRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        public async Task<IBaseResponse<CandidateElectionDTO>> CreateServiceAsync(CandidateElectionDTO model)
        {
            var baseResponse = new BaseResponse<CandidateElectionDTO>();
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

        public async Task<IBaseResponse<CandidateElectionDTO>> GetByIdServiceAsync(int id)
        {
            var baseResponse = new BaseResponse<CandidateElectionDTO>();
            CandidateElectionDTO candidate = await _candidateRepository.GetByIdAsync(id);

            if (candidate == null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
                return baseResponse;
            }
            baseResponse.Data = candidate;
            return baseResponse;
        }
        public async Task<IBaseResponse<PagedList<CandidateElectionDTO>>>  GetByElectionIdServiceAsync(int id, PagingQueryParameters paging)
        {
            var baseResponse = new BaseResponse<PagedList<CandidateElectionDTO>>();
            var candidates = await _candidateRepository.GetByElectionIdAsync(id);

            if (candidates == null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
                return baseResponse;
            }
            baseResponse.Data = PagedList<CandidateElectionDTO>.ToPagedList(candidates, paging.PageNumber, paging.PageSize); 
            return baseResponse;
        }

        public async Task<IBaseResponse<PagedList<CandidateElectionDTO>>> GetServiceAsync(PagingQueryParameters paging)
        {
            var baseResponse = new BaseResponse<PagedList<CandidateElectionDTO>>();
            var candidates = await _candidateRepository.GetAsync();

            if (candidates == null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = PagedList<CandidateElectionDTO>.ToPagedList(candidates, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }

        public async Task<IBaseResponse<CandidateElectionDTO>> UpdateServiceAsync(int id, CandidateElectionDTO model)
        {
            var baseResponse = new BaseResponse<CandidateElectionDTO>();
            CandidateElectionDTO candidate = await _candidateRepository.UpdateAsync(id, model);
            baseResponse.Message = MessageUtil.Instance.Updated;
            baseResponse.Data = candidate;
            return baseResponse;
        }
    }
}
