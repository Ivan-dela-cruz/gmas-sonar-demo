using BUE.Inscriptions.Domain.Elecctions.DTO;
using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Paging;
using BUE.Inscriptions.Domain.Response;

namespace BUE.Inscriptions.Application.Interfaces
{

    public interface ICandidateElectionService : IBaseService<CandidateElectionDTO>
    {
        Task<IBaseResponse<CandidateElectionDTO>> GetByIdServiceAsync(int id);
        Task<IBaseResponse<PagedList<CandidateElectionDTO>>> GetByElectionIdServiceAsync(int id, PagingQueryParameters paging);
    }
}
