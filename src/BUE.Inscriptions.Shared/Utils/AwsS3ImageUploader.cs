using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using System;
using System.IO;

namespace BUE.Inscriptions.Shared.Utils
{
    public class AwsS3ImageUploader
    {
        private readonly string _accessKey;
        private readonly string _secretKey;
        private readonly string _bucketName;
        private readonly string _folderName;
        private readonly RegionEndpoint region = RegionEndpoint.USWest2;

        public AwsS3ImageUploader(string accessKey, string secretKey, string bucketName, string folderName)
        {
            _accessKey = accessKey;
            _secretKey = secretKey;
            _bucketName = bucketName;
            _folderName = folderName;
        }

        public void UploadImage(string fileName, byte[] byteArray)
        {
            try
            {
                var credentials = new Amazon.Runtime.BasicAWSCredentials(_accessKey, _secretKey);
                var client = new AmazonS3Client(credentials, RegionEndpoint.EUWest1);

                var fileTransferUtility = new TransferUtility(client);
                var keyName = $"{_folderName}/{fileName}";
                var fileStream = new MemoryStream(byteArray);
                var fileTransferUtilityRequest = new TransferUtilityUploadRequest
                {
                    BucketName = _bucketName,
                    InputStream = fileStream,
                    Key = keyName,
                    CannedACL = S3CannedACL.PublicRead
                };
                fileTransferUtility.Upload(fileTransferUtilityRequest);

            }
            catch (Exception e)
            {

            }
        }
    }
}
