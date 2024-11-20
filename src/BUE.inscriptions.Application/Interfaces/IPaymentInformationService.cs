using BUE.Inscriptions.Domain.Inscriptions.DTO;
using BUE.Inscriptions.Domain.Response;

namespace BUE.Inscriptions.Application.Interfaces
{
    public interface IPaymentInformationService : IBaseService<PaymentInformationDTO>
    {
        Task<IBaseResponse<PaymentInformationDTO>> GetByIdServiceAsync(int id);
    }
}
