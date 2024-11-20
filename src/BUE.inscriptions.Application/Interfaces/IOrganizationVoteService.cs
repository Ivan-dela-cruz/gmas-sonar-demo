using BUE.Inscriptions.Domain.Elecctions.DTO;
using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Paging;
using BUE.Inscriptions.Domain.Response;

namespace BUE.Inscriptions.Application.Interfaces
{

    public interface IOrganizationVoteService : IBaseService<OrganizationVoteDTO>
    {
        Task<IBaseResponse<OrganizationVoteDTO>> GetByIdServiceAsync(int id);
        Task<IBaseResponse<OrganizationVoteDTO>> GetByElectionAndUserIdAsync(int ElectionId, int UserId);
        Task<IBaseResponse<PagedList<OrganizationVoteDTO>>> GetByUserIdServiceAsync(int id, PagingQueryParameters paging);
        Task<IBaseResponse<PagedList<VoteCountDTO>>> GetVotesByOrganizationElectionAsync(PagingQueryParameters paging, int ElectionId);
        Task<IBaseResponse<ElectionDTO>> GetDHontResultOrganizationElectionAsync(int ElectionId);
    }
}
