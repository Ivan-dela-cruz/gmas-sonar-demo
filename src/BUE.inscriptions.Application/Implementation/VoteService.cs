using BUE.Inscriptions.Application.Interfaces;
using BUE.Inscriptions.Domain.Elecctions.DTO;
using BUE.Inscriptions.Domain.Paging;
using BUE.Inscriptions.Domain.Response;
using BUE.Inscriptions.Infrastructure.Interfaces;
using BUE.Inscriptions.Shared.Utils;


namespace BUE.Inscriptions.Application.Implementation
{
    public class VoteService : IVoteService
    {
        private readonly IVoteRepository _voteRepository;

        public VoteService(IVoteRepository voteRepository)
        {
            _voteRepository = voteRepository;
        }

        public async Task<IBaseResponse<VoteDTO>> CreateServiceAsync(VoteDTO model)
        {
            var baseResponse = new BaseResponse<VoteDTO>();
            var vote = await _voteRepository.CreateAsync(model);
            baseResponse.Data = vote;
            return baseResponse;
        }

        public async Task<IBaseResponse<bool>> DeleteServiceAsync(int id)
        {
            var baseResponse = new BaseResponse<bool>();
            bool isSuccess = await _voteRepository.DeleteAsync(id);

            if (!isSuccess)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = isSuccess;
            return baseResponse;
        }

        public async Task<IBaseResponse<VoteDTO>> GetByIdServiceAsync(int id)
        {
            var baseResponse = new BaseResponse<VoteDTO>();
            VoteDTO vote = await _voteRepository.GetByIdAsync(id);

            if (vote == null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
                return baseResponse;
            }
            baseResponse.Data = vote;
            return baseResponse;
        }  
        public async Task<IBaseResponse<PagedList<VoteDTO>>> GetByUserIdServiceAsync(int id, PagingQueryParameters paging)
        {
            var baseResponse = new BaseResponse<PagedList<VoteDTO>>();
            var votes = await _voteRepository.GetByUserIdAsync(id);

            if (votes == null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = PagedList<VoteDTO>.ToPagedList(votes, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }
        public async Task<IBaseResponse<VoteDTO>> GetByElectionAndUserIdAsync(int ElectionId, int UserId)
        {
            var baseResponse = new BaseResponse<VoteDTO>();
            VoteDTO vote = await _voteRepository.GetByElectionAndUserIdAsync(ElectionId, UserId);

            if (vote == null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
                return baseResponse;
            }
            baseResponse.Data = vote;
            return baseResponse;
        }

        public async Task<IBaseResponse<PagedList<VoteDTO>>> GetServiceAsync(PagingQueryParameters paging)
        {
            var baseResponse = new BaseResponse<PagedList<VoteDTO>>();
            var votes = await _voteRepository.GetAsync();

            if (votes == null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = PagedList<VoteDTO>.ToPagedList(votes, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }

        public async Task<IBaseResponse<VoteDTO>> UpdateServiceAsync(int id, VoteDTO model)
        {
            var baseResponse = new BaseResponse<VoteDTO>();
            VoteDTO vote = await _voteRepository.UpdateAsync(id, model);
            baseResponse.Message = MessageUtil.Instance.Updated;
            baseResponse.Data = vote;
            return baseResponse;
        }

        public async Task<IBaseResponse<PagedList<VoteCountDTO>>> GetVoteCountsByCandidateElectionAsync(PagingQueryParameters paging, int ElectionId)
        {
            var baseResponse = new BaseResponse<PagedList<VoteCountDTO>>();
            var votes = await _voteRepository.GetVoteCountsByCandidateElectionAsync(ElectionId);

            if (votes == null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = PagedList<VoteCountDTO>.ToPagedList(votes, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }
    }
}
