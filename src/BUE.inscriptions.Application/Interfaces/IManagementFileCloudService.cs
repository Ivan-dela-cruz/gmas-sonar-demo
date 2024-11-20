using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Paging;
using BUE.Inscriptions.Domain.Response;

namespace BUE.Inscriptions.Application.Interfaces
{

    public interface IManagementFileCloudService
    {
        Task<IBaseResponse<string>> UpdloadFileToCloudAsync(string folderName, string fileName, byte[] byteArray);
        Task<IBaseResponse<byte[]>> GetFileBytesFromCloudAsync(string objectKey);
        Task<IBaseResponse<string>> DownloadFileFromCloudAsync(string objectKey, string localFilePath);
    }
}
