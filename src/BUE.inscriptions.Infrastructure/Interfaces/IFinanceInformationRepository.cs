using BUE.Inscriptions.Domain.Entity.DTO;

namespace BUE.Inscriptions.Infrastructure.Interfaces
{

    public interface IFinanceInformationRepository : IBaseRepository<FinanceInformationDTO>
    {
        Task<FinanceInformationDTO> GetByIdAsync(int id);
        Task<FinanceInformationDTO> GetByStudentIdAsync(int id);
    }
}
