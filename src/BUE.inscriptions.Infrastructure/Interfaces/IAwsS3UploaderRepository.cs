namespace BUE.Inscriptions.Infrastructure.Interfaces
{

    public interface IAwsS3UploaderRepository
    {
        Task<string> UploadBucketFileAsync(string folderName, string fileName, byte[] byteArray);
        Task<byte[]> GetFileBytesFromS3Async(string objectKey);
        Task<bool> DownloadFileFromS3Async(string objectKey, string localFilePath);
    }
}
