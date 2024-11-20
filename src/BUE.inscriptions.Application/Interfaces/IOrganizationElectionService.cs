using BUE.Inscriptions.Domain.Elecctions.DTO;
using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Paging;
using BUE.Inscriptions.Domain.Response;

namespace BUE.Inscriptions.Application.Interfaces
{

    public interface IOrganizationElectionService : IBaseService<OrganizationElectionDTO>
    {
        Task<IBaseResponse<OrganizationElectionDTO>> GetByIdServiceAsync(int id);
        Task<IBaseResponse<PagedList<OrganizationElectionDTO>>> GetByElectionIdServiceAsync(int id, PagingQueryParameters paging);
    }
}
