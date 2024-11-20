using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Inscriptions.DTO;
using BUE.Inscriptions.Domain.Paging;
using BUE.Inscriptions.Domain.Response;

namespace BUE.Inscriptions.Application.Interfaces
{

    public interface IAcademicYearService : IBaseService<AcademicYearDTO>
    {
        Task<IBaseResponse<AcademicYearDTO>> GetByIdServiceAsync(int id);
    }
}
