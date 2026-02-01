using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Mosahem.Application.Interfaces;
using Mosahem.Application.Settings;

namespace Mosahem.Infrastructure.Services
{
    public class FileService : IFileService
    {
        private readonly IAmazonS3 _s3Client;
        private readonly FileStorageSettings _settings;

        public FileService(IAmazonS3 s3Client, IOptions<FileStorageSettings> settings)
        {
            _s3Client = s3Client;
            _settings = settings.Value;
        }

        public async Task<string> UploadFileAsync(IFormFile file, string folderName, CancellationToken cancellationToken = default)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is empty", nameof(file));

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var key = $"{folderName}/{fileName}";

            using var stream = file.OpenReadStream();
            var putRequest = new PutObjectRequest
            {
                BucketName = _settings.BucketName,
                Key = key,
                InputStream = stream,
                ContentType = file.ContentType
            };

            await _s3Client.PutObjectAsync(putRequest, cancellationToken);

            return key;
        }

        public async Task DeleteFileAsync(string fileKey, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(fileKey)) return;

            try
            {
                var deleteRequest = new DeleteObjectRequest
                {
                    BucketName = _settings.BucketName,
                    Key = fileKey
                };

                await _s3Client.DeleteObjectAsync(deleteRequest, cancellationToken);
            }
            catch (Exception ex)
            {
                // نسجل الخطأ ولكن لا نوقف النظام
                // _logger.LogError(ex, "Failed to delete file from S3");
            }
        }

        public string? GetFileUrl(string? fileKey, bool isPrivate = true)
        {
            if (string.IsNullOrEmpty(fileKey)) return null;

            if (isPrivate)
            {
                var request = new GetPreSignedUrlRequest
                {
                    BucketName = _settings.BucketName,
                    Key = fileKey,
                    Expires = DateTime.UtcNow.AddHours(2)
                };
                return _s3Client.GetPreSignedURL(request);
            }

            var baseUrl = _settings.PublicUrl.TrimEnd('/');
            return $"{baseUrl}/{fileKey}";
        }
    }
}