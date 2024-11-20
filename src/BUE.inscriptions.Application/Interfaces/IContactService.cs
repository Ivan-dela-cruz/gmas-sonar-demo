using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Paging;
using BUE.Inscriptions.Domain.Response;

namespace BUE.Inscriptions.Application.Interfaces
{
    public interface IContactService : IBaseService<ContactDTO>
    {
        Task<IBaseResponse<ContactDTO>> GetByIdServiceAsync(int id);
        Task<IBaseResponse<ContactDTO>> GetRepresentativeByContactAsync(int contactCode);
    }
}
