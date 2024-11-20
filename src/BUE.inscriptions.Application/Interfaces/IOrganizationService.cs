using BUE.Inscriptions.Domain.Elecctions.DTO;
using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Paging;
using BUE.Inscriptions.Domain.Response;

namespace BUE.Inscriptions.Application.Interfaces
{

    public interface IOrganizationService : IBaseService<OrganizationDTO>
    {
        Task<IBaseResponse<OrganizationDTO>> GetByIdServiceAsync(int id);
        Task<IBaseResponse<PagedList<OrganizationDTO>>> GetByElectionIdServiceAsync(int id, PagingQueryParameters paging);
    }
}
