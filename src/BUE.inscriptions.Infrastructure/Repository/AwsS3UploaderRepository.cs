using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using BUE.Inscriptions.Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using BUE.Inscriptions.Shared.Utils;

namespace BUE.Inscriptions.Infrastructure.Repository
{
    public class AwsS3UploaderRepository : IAwsS3UploaderRepository
    {

        protected readonly string _s3Domain;
        protected readonly string _accessKey;
        protected readonly string _secretKey;
        protected readonly string _s3Bucket;
        protected readonly string _s3Region;
        protected readonly string _s3RootPath;
        protected readonly IConfiguration _configuration;
        private readonly RegionEndpoint region = RegionEndpoint.USEast1;
        public AwsS3UploaderRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _s3Domain = _configuration.GetSection("AppSettings:S3Domain").Value;
            _accessKey = _configuration.GetSection("AppSettings:AccessKey").Value;
            _secretKey = _configuration.GetSection("AppSettings:SecretKey").Value;
            _s3Bucket = _configuration.GetSection("AppSettings:S3Bucket").Value;
            _s3Region = _configuration.GetSection("AppSettings:S3Region").Value;
            _s3RootPath = _configuration.GetSection("AppSettings:S3RootPath").Value;
        }

        public async Task<string> UploadBucketFileAsync(string folderName, string fileName, byte[] byteArray)
        {
            string resultUrlFile = "";
            try
            {
                var credentials = new BasicAWSCredentials(_accessKey, _secretKey);
                var client = new AmazonS3Client(credentials, region);

                var fileTransferUtility = new TransferUtility(client);
                var keyName = $"{_s3RootPath}/{folderName}/{fileName}";
                var fileStream = new MemoryStream(byteArray);
                var fileTransferUtilityRequest = new TransferUtilityUploadRequest
                {
                    BucketName = _s3Bucket,
                    InputStream = fileStream,
                    Key = keyName,
                    CannedACL = S3CannedACL.PublicRead
                };
                await fileTransferUtility.UploadAsync(fileTransferUtilityRequest);
                resultUrlFile = _s3Domain + keyName;
                return resultUrlFile;

            }
            catch (Exception ex)
            {
                LogManagement.Instance.writeAssembly(MethodBase.GetCurrentMethod().DeclaringType, $"StackTrace: {ex.StackTrace} Error: {ex.Message}", "ErrorS3");
                return resultUrlFile;
            }
        }
        public async Task<byte[]> GetFileBytesFromS3Async(string objectKey)
        {
            try
            {
                var relativePath = objectKey.Replace($"{_s3Domain}", "");
                BasicAWSCredentials credentials = new BasicAWSCredentials(this._accessKey, this._secretKey);
                using (AmazonS3Client client = new AmazonS3Client(credentials, region))
                {
                    var request = new GetObjectRequest()
                    {
                        BucketName = _s3Bucket,
                        Key = relativePath
                    };
                    using (var response = await client.GetObjectAsync(request, new CancellationToken()))
                    {
                        using (var stream = response.ResponseStream)
                        {
                            using (var memoryStream = new MemoryStream())
                            {
                                stream.CopyTo((Stream)memoryStream);
                                return memoryStream.ToArray();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogManagement.Instance.writeAssembly(MethodBase.GetCurrentMethod().DeclaringType, $"StackTrace: {ex.StackTrace} Error: {ex.Message}", "ErrorS3");
                return null;
            }
        }
        public async Task<bool> DownloadFileFromS3Async(string objectKey, string localFilePath)
        {
            try
            {
                var relativePath = objectKey.Replace($"{_s3RootPath}/", "");
                using (var client = new AmazonS3Client(_accessKey, _secretKey, region))
                {
                    var request = new GetObjectRequest
                    {
                        BucketName = _s3Bucket,
                        Key = relativePath
                    };

                    using (var response = await client.GetObjectAsync(request))
                    using (var responseStream = response.ResponseStream)
                    using (var fileStream = File.Create(localFilePath))
                    {
                        await responseStream.CopyToAsync(fileStream);
                    }
                }
                return true;
            }
            catch (AmazonS3Exception ex)
            {
                LogManagement.Instance.writeAssembly(MethodBase.GetCurrentMethod().DeclaringType, $"StackTrace: {ex.StackTrace} Error: {ex.Message}", ".ErrorS3");
                return false;
            }
            catch (Exception e)
            {
                LogManagement.Instance.writeAssembly(MethodBase.GetCurrentMethod().DeclaringType, $"StackTrace: {e.StackTrace} Error: {e.Message}", ".ErrorS3");
                return false;
            }
        }
        public async Task<bool> DownloadFolderFromS3(string bucketName, string s3FolderPath, string localFolderPath)
        {
            try
            {
                using (var client = new AmazonS3Client(_accessKey, _secretKey, region))
                {
                    var request = new ListObjectsRequest
                    {
                        BucketName = _s3Bucket,
                        Prefix = s3FolderPath
                    };

                    ListObjectsResponse response;
                    do
                    {
                        response = await client.ListObjectsAsync(request);

                        foreach (var s3Object in response.S3Objects)
                        {
                            string localFilePath = Path.Combine(localFolderPath, s3Object.Key);
                            await DownloadFileFromS3(client, bucketName, s3Object.Key, localFilePath);
                        }

                        request.Marker = response.NextMarker;
                    } while (response.IsTruncated);
                    return true;
                }
            }
            catch (AmazonS3Exception ex)
            {
                LogManagement.Instance.writeAssembly(MethodBase.GetCurrentMethod().DeclaringType, $"StackTrace: {ex.StackTrace} Error: {ex.Message}", ".ErrorS3");
                return false;
            }
            catch (Exception e)
            {
                LogManagement.Instance.writeAssembly(MethodBase.GetCurrentMethod().DeclaringType, $"StackTrace: {e.StackTrace} Error: {e.Message}", ".ErrorS3");
                return false;
            }
        }
        private async Task DownloadFileFromS3(AmazonS3Client client, string bucketName, string s3ObjectKey, string localFilePath)
        {
            var request = new GetObjectRequest
            {
                BucketName = bucketName,
                Key = s3ObjectKey
            };

            using (var response = await client.GetObjectAsync(request))
            using (var responseStream = response.ResponseStream)
            using (var fileStream = File.Create(localFilePath))
            {
                responseStream.CopyTo(fileStream);
                Console.WriteLine($"Archivo descargado a: {localFilePath}");
            }
        }
    }
}


