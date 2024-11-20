using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Paging;
using BUE.Inscriptions.Domain.Response;

namespace BUE.Inscriptions.Application.Interfaces
{

    public interface IAuthorizePersonService : IBaseService<AuthorizePeopleDTO>
    {
        Task<IBaseResponse<AuthorizePeopleDTO>> GetByIdServiceAsync(int id);
        Task<IBaseResponse<PagedList<AuthorizePeopleDTO>>> StoreServiceAsync(IEnumerable<AuthorizePeopleDTO> entities);
        Task<IBaseResponse<PagedList<AuthorizePeopleDTO>>> GetByRequestIdServiceAsync(int id);
        Task<IBaseResponse<AuthorizePeopleDTO>> GetByIdentificationServiceAsync(string identification, int currentYearSchool, int requestCode);
    }
}
