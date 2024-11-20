using BUE.Inscriptions.Domain.Inscriptions.DTO;
using BUE.Inscriptions.Domain.Paging;
using BUE.Inscriptions.Domain.Response;

namespace BUE.Inscriptions.Application.Interfaces
{
    public interface IInscriptionService : IBaseService<InscriptionDTO>
    {
        Task<IBaseResponse<InscriptionDTO>> GetByIdServiceAsync(int id);
        Task<IBaseResponse<InscriptionDTO>> UpdateProcessServiceAsync(int id, InscriptionDTO model);
        Task<IBaseResponse<PagedList<StudentDTO>>> GetByAcademicYearAsync(PagingQueryParameters paging);
        Task<IBaseResponse<bool>> UpdateStatusServiceAsync(RequestStatusChange requestStatus);
    }
}
