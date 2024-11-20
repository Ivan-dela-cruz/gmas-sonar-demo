using BUE.Inscriptions.Application.Interfaces;
using BUE.Inscriptions.Domain.Elecctions.DTO;
using BUE.Inscriptions.Domain.Paging;
using BUE.Inscriptions.Domain.Response;
using BUE.Inscriptions.Infrastructure.Interfaces;
using BUE.Inscriptions.Shared.Utils;


namespace BUE.Inscriptions.Application.Implementation
{
    public class ElectionService : IElectionService
    {
        private readonly IElectionRepository _electionRepository;

        public ElectionService(IElectionRepository electionRepository)
        {
            _electionRepository = electionRepository;
        }

        public async Task<IBaseResponse<ElectionDTO>> CreateServiceAsync(ElectionDTO model)
        {
            var baseResponse = new BaseResponse<ElectionDTO>();
            var election = await _electionRepository.CreateAsync(model);
            baseResponse.Data = election;
            return baseResponse;
        }

        public async Task<IBaseResponse<bool>> DeleteServiceAsync(int id)
        {
            var baseResponse = new BaseResponse<bool>();
            bool isSuccess = await _electionRepository.DeleteAsync(id);

            if (!isSuccess)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = isSuccess;
            return baseResponse;
        }

        public async Task<IBaseResponse<ElectionDTO>> GetByIdServiceAsync(int id)
        {
            var baseResponse = new BaseResponse<ElectionDTO>();
            ElectionDTO election = await _electionRepository.GetByIdAsync(id);

            if (election == null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
                return baseResponse;
            }
            baseResponse.Data = election;
            return baseResponse;
        }

        public async Task<IBaseResponse<PagedList<ElectionDTO>>> GetServiceAsync(PagingQueryParameters paging)
        {
            var baseResponse = new BaseResponse<PagedList<ElectionDTO>>();
            var elections = await _electionRepository.GetAsync();

            if (elections == null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = PagedList<ElectionDTO>.ToPagedList(elections, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }

        public async Task<IBaseResponse<ElectionDTO>> UpdateServiceAsync(int id, ElectionDTO model)
        {
            var baseResponse = new BaseResponse<ElectionDTO>();
            ElectionDTO election = await _electionRepository.UpdateAsync(id, model);
            baseResponse.Message = MessageUtil.Instance.Updated;
            baseResponse.Data = election;
            return baseResponse;
        }

        public async Task<IBaseResponse<IEnumerable<int>>> GetCoursesLevelByUserAsync(int userId)
        {
            var baseResponse = new BaseResponse<IEnumerable<int>>();
            var levels = await _electionRepository.GetUserLevelElectionAsync(userId);
            baseResponse.Message = MessageUtil.Instance.Updated;
            baseResponse.Data = levels;
            return baseResponse;
        }
    }
}
