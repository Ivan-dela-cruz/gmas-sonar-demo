using BUE.Inscriptions.Domain.Entity.DTO;

namespace BUE.Inscriptions.Infrastructure.Interfaces
{

    public interface IAuthorizePersonRepository : IBaseRepository<AuthorizePeopleDTO>
    {
        Task<AuthorizePeopleDTO> GetByIdAsync(int id);
        Task<IEnumerable<AuthorizePeopleDTO>> StoreMany(IEnumerable<AuthorizePeopleDTO> entities);
        Task<List<AuthorizePeopleDTO>> UpdateMany(IEnumerable<AuthorizePeopleDTO> entities);
        Task<IEnumerable<AuthorizePeopleDTO>> GetByRequestIdAsync(int id);
        Task<AuthorizePeopleDTO> GetByIdentificationAsync(string identification, int currentYearSchool, int requestCode);
    }
}
