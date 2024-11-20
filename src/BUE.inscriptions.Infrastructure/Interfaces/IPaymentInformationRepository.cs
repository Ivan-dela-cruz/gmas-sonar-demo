using BUE.Inscriptions.Domain.Inscriptions.DTO;

namespace BUE.Inscriptions.Infrastructure.Interfaces
{
    public interface IPaymentInformationRepository : IBaseRepository<PaymentInformationDTO>
    {
        public string Message {  get; set; }
        public string StatusCode {  get; set; }
        Task<PaymentInformationDTO> GetByIdAsync(int id);
    }
}
