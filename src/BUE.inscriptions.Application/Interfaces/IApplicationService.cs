using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Response;

namespace BUE.Inscriptions.Application.Interfaces
{
    public interface IApplicationService
    {
        Task<IBaseResponse<ApplicationDTO>> getApplication();
        Task<IBaseResponse<ApplicationDTO>> UpdateServiceAsync(int id, ApplicationDTO model);
    }
}
