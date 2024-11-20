using BUE.Inscriptions.Domain.Entity.DTO;

namespace BUE.Inscriptions.Infrastructure.Interfaces
{

    public interface IFileDownloadRepository : IBaseRepository<FileDownloadDTO>
    {
        Task<FileDownloadDTO> GetByIdentificationAsync(string identification);
        Task<IEnumerable<FileDownloadDTO>> GetByModuleAsync(string module);
    }
}
