using BUE.Inscriptions.Domain.Elecctions.DTO;
using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Paging;
using BUE.Inscriptions.Domain.Response;

namespace BUE.Inscriptions.Application.Interfaces
{

    public interface IPositionService : IBaseService<PositionDTO>
    {
        Task<IBaseResponse<PositionDTO>> GetByIdServiceAsync(int id);
    }
}
