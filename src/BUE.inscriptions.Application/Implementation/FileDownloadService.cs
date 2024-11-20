using BUE.Inscriptions.Infrastructure.Interfaces;
using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Paging;
using BUE.Inscriptions.Domain.Response;
using BUE.Inscriptions.Application.Interfaces;
using BUE.Inscriptions.Shared.Utils;
using Newtonsoft.Json;

namespace BUE.Inscriptions.Application.Implementation
{
    public class FileDownloadService : IFileDownloadService
    {
        private IFileDownloadRepository _fileRep;
        public FileDownloadService(IFileDownloadRepository fileRep) => _fileRep = fileRep;
        public async Task<IBaseResponse<FileDownloadDTO>> CreateServiceAsync(FileDownloadDTO model)
        {
            var baseResponse = new BaseResponse<FileDownloadDTO>();
            var fileDownload = await _fileRep.CreateAsync(model);
            if (fileDownload is null)
            {
                baseResponse.Message = MessageUtil.Instance.Warning;
                baseResponse.status = false;
            }
            baseResponse.Data = fileDownload;
            return baseResponse;
        }
        public async Task<IBaseResponse<bool>> DeleteServiceAsync(int id)
        {
            var baseResponse = new BaseResponse<bool>();
            bool IsSuccess = await _fileRep.DeleteAsync(id);
            if (!IsSuccess)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = IsSuccess;
            return baseResponse;
        }
        public async Task<IBaseResponse<FileDownloadDTO>> GetByIdentificationServiceAsync(string identification)
        {
            var baseResponse = new BaseResponse<FileDownloadDTO>();
            FileDownloadDTO fileDownload = await _fileRep.GetByIdentificationAsync(identification);
            if (fileDownload is null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
                return baseResponse;
            }
            var langFiles = JsonConvert.DeserializeObject<LangFilesDTO>(fileDownload.langNames);
            fileDownload.langNames = null;
            fileDownload.lang = langFiles;
            baseResponse.Data = fileDownload;
            return baseResponse;
        }

        public async Task<IBaseResponse<PagedList<FileDownloadDTO>>> GetByModuleServiceAsync(PagingQueryParameters paging, string module)
        {
            var baseResponse = new BaseResponse<PagedList<FileDownloadDTO>>();
            var fileDownloads = await _fileRep.GetByModuleAsync(module);
            if (fileDownloads is null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = PagedList<FileDownloadDTO>.ToPagedList(fileDownloads, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }
        public async Task<IBaseResponse<PagedList<FileDownloadDTO>>> GetServiceAsync(PagingQueryParameters paging)
        {
            var baseResponse = new BaseResponse<PagedList<FileDownloadDTO>>();
            var fileDownloads = await _fileRep.GetAsync();
            if (fileDownloads is null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
            }
            baseResponse.Data = PagedList<FileDownloadDTO>.ToPagedList(fileDownloads, paging.PageNumber, paging.PageSize);
            return baseResponse;
        }
        public async Task<IBaseResponse<FileDownloadDTO>> UpdateServiceAsync(int id, FileDownloadDTO model)
        {

            var baseResponse = new BaseResponse<FileDownloadDTO>();
            FileDownloadDTO fileDownload = await _fileRep.UpdateAsync(id, model);
            baseResponse.Message = MessageUtil.Instance.Updated;
            baseResponse.Data = fileDownload;
            return baseResponse;
        }
    }
}
