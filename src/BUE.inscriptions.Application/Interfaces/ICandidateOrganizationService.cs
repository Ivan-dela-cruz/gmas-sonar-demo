using BUE.Inscriptions.Domain.Elecctions.DTO;
using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Paging;
using BUE.Inscriptions.Domain.Response;

namespace BUE.Inscriptions.Application.Interfaces
{

    public interface ICandidateOrganizationService : IBaseService<CandidateOrganizationDTO>
    {
        Task<IBaseResponse<CandidateOrganizationDTO>> GetByIdServiceAsync(int id);
        Task<IBaseResponse<PagedList<CandidateOrganizationDTO>>> GetByOrganizationIdServiceAsync(int id, PagingQueryParameters paging);
        Task<IBaseResponse<bool>> DeleteCandidateOrganizationServiceAsync(int organizationId, int candidateId);
    }
}
