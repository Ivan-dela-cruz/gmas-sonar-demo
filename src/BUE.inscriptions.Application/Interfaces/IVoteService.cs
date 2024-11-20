using BUE.Inscriptions.Domain.Elecctions.DTO;
using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Paging;
using BUE.Inscriptions.Domain.Response;

namespace BUE.Inscriptions.Application.Interfaces
{

    public interface IVoteService : IBaseService<VoteDTO>
    {
        Task<IBaseResponse<VoteDTO>> GetByIdServiceAsync(int id);
        Task<IBaseResponse<VoteDTO>> GetByElectionAndUserIdAsync(int ElectionId, int UserId);
        Task<IBaseResponse<PagedList<VoteDTO>>> GetByUserIdServiceAsync(int id, PagingQueryParameters paging);
        Task<IBaseResponse<PagedList<VoteCountDTO>>> GetVoteCountsByCandidateElectionAsync(PagingQueryParameters paging, int ElectionId);
    }
}
