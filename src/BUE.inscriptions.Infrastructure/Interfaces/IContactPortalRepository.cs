using BUE.Inscriptions.Domain.Entity.DTO;

namespace BUE.Inscriptions.Infrastructure.Interfaces
{
    public interface IContactPortalRepository : IBaseRepository<ContactPortalDTO>
    {
        Task<ContactPortalDTO> GetByIdAsync(int id);
        Task<ContactPortalDTO> GetByCodeContactCurrentSchoolAsync(int codeContact, int currentYearSchool);
        Task<IEnumerable<ContactPortalDTO>> GetlistBasicAsync();
        Task<ContactPortalDTO> GetByIdentificactionAsync(string documentNumber);
        Task<ContactPortalDTO> GetByIdEmailAsync(string email);
    }
}
