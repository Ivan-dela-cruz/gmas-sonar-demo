using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Paging;
using BUE.Inscriptions.Domain.Response;

namespace BUE.Inscriptions.Application.Interfaces
{

    public interface IFileDownloadService : IBaseService<FileDownloadDTO>
    {
        Task<IBaseResponse<FileDownloadDTO>> GetByIdentificationServiceAsync(string identification);
        Task<IBaseResponse<PagedList<FileDownloadDTO>>> GetByModuleServiceAsync(PagingQueryParameters paging, string module);
    }
}
