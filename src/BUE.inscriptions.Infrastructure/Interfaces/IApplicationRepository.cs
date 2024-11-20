using BUE.Inscriptions.Domain.Entity.DTO;

namespace BUE.Inscriptions.Infrastructure.Interfaces
{
    public interface IApplicationRepository
    {
        Task<ApplicationDTO> getApplicationAsync();
        Task<ApplicationDTO> UpdateAsync(int id, ApplicationDTO entity);
    }
}
