using BUE.Inscriptions.Domain.Inscriptions.DTO;
using BUE.Inscriptions.Domain.Response;

namespace BUE.Inscriptions.Application.Interfaces
{
    public interface IPersonService : IBaseService<PersonDTO>
    {
        Task<IBaseResponse<PersonDTO>> GetByIdServiceAsync(int id);
        Task<IBaseResponse<PersonDTO>> GetByIdentificationServiceAsync(string identificaction);
    }
}
