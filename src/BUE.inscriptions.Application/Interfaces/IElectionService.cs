using BUE.Inscriptions.Domain.Elecctions.DTO;
using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Paging;
using BUE.Inscriptions.Domain.Response;

namespace BUE.Inscriptions.Application.Interfaces
{

    public interface IElectionService : IBaseService<ElectionDTO>
    {
        Task<IBaseResponse<ElectionDTO>> GetByIdServiceAsync(int id);
        Task<IBaseResponse<IEnumerable<int>>> GetCoursesLevelByUserAsync(int userId);
    }
}
