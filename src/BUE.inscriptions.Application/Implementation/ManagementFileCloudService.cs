using BUE.Inscriptions.Infrastructure.Interfaces;
using BUE.Inscriptions.Domain.Response;
using BUE.Inscriptions.Application.Interfaces;
using BUE.Inscriptions.Shared.Utils;

namespace BUE.Inscriptions.Application.Implementation
{
    public class ManagementFileCloudService : IManagementFileCloudService
    {
        private readonly IAwsS3UploaderRepository _awsS3;
        public ManagementFileCloudService(IAwsS3UploaderRepository awsS3)
        {
            _awsS3 = awsS3;
        }

        public async Task<IBaseResponse<string>> DownloadFileFromCloudAsync(string objectKey, string localFilePath)
        {
            try
            {
                var baseResponse = new BaseResponse<string>()
                {
                    status = true,
                    statusCode = MessageUtil.Instance.FILE_FOUND,
                    Message = MessageUtil.Instance.Found
                };
                var result = await _awsS3.DownloadFileFromS3Async(objectKey, localFilePath);
                if (!result)
                {
                    baseResponse.status = false;
                    baseResponse.statusCode = MessageUtil.Instance.FILE_NOT_FOUND;
                    baseResponse.Message = MessageUtil.Instance.NotFound;
                    return baseResponse;
                }
                baseResponse.Data = localFilePath;
                return baseResponse;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IBaseResponse<byte[]>> GetFileBytesFromCloudAsync(string objectKey)
        {
            try
            {
                var baseResponse = new BaseResponse<byte[]>()
                {
                    status = true,
                    statusCode = MessageUtil.Instance.FILE_FOUND,
                    Message = MessageUtil.Instance.Found
                };
                var result = await _awsS3.GetFileBytesFromS3Async(objectKey);
                if (result is null)
                {
                    baseResponse.status = false;
                    baseResponse.statusCode = MessageUtil.Instance.FILE_NOT_FOUND;
                    baseResponse.Message = MessageUtil.Instance.NotFound;
                    return baseResponse;
                }
                baseResponse.Data = result;
                return baseResponse;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IBaseResponse<string>> UpdloadFileToCloudAsync(string pathFileCloud, string fileName, byte[] byteArray)
        {
            try
            {
                var baseResponse = new BaseResponse<string>()
                {
                    status = true,
                    statusCode = MessageUtil.Instance.FILE_UPLOAD,
                    Message = MessageUtil.Instance.FileUpload
                };
                var result = await _awsS3.UploadBucketFileAsync(pathFileCloud, fileName, byteArray);
                if (result is null)
                {
                    baseResponse.status = false;
                    baseResponse.statusCode = MessageUtil.Instance.FILE_NOT_UPLOAD;
                    baseResponse.Message = MessageUtil.Instance.FileNotUpload;
                    return baseResponse;
                }
                baseResponse.Data = result;
                return baseResponse;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
