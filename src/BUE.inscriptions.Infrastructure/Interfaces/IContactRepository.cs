using BUE.Inscriptions.Domain.Entity.DTO;

namespace BUE.Inscriptions.Infrastructure.Interfaces
{
    public interface IContactRepository : IBaseRepository<ContactDTO>
    {
        Task<ContactDTO> GetByIdAsync(int id);
        Task<ContactDTO> GetRepresentativeByContactAsync(int contactCode);
    }
}
