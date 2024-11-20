using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Paging;
using BUE.Inscriptions.Domain.Response;

namespace BUE.Inscriptions.Application.Interfaces
{

    public interface IContactPortalService : IBaseService<ContactPortalDTO>
    {
        Task<IBaseResponse<ContactPortalDTO>> GetByIdServiceAsync(int id);
        Task<IBaseResponse<ContactPortalDTO>> GetByCodeContactCurrentSchoolAsync(int codeContact, int currentYearSchool);
        Task<IBaseResponse<PagedList<ContactPortalDTO>>> GetListBasicServiceAsync(PagingQueryParameters paging);
        Task<IBaseResponse<bool>> GetByIdenditicationServiceAsync(string documentNumber);
        Task<IBaseResponse<bool>> GetByEmailServiceAsync(string email);
    }
}
