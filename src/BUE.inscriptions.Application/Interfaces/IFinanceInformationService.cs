using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Paging;
using BUE.Inscriptions.Domain.Response;

namespace BUE.Inscriptions.Application.Interfaces
{

    public interface IFinanceInformationService : IBaseService<FinanceInformationDTO>
    {
        Task<IBaseResponse<FinanceInformationDTO>> GetByIdServiceAsync(int id);
        Task<IBaseResponse<FinanceInformationDTO>> GetByStudentIdServiceAsync(int id);
    }
}
