using BUE.Inscriptions.Domain.Elecctions.DTO;
using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Paging;
using BUE.Inscriptions.Domain.Response;

namespace BUE.Inscriptions.Application.Interfaces
{

    public interface ICandidateService : IBaseService<CandidateDTO>
    {
        Task<IBaseResponse<CandidateDTO>> GetByIdServiceAsync(int id);
        Task<IBaseResponse<PagedList<CandidateDTO>>> GetByElectionIdServiceAsync(int id, PagingQueryParameters paging);
    }
}
