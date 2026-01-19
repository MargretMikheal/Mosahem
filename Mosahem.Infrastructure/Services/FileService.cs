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
            // 1. Validation
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is empty", nameof(file));

            // 2. Generate Unique Filename (Guid + Extension)
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

            // 3. Create the Key (Path in Bucket) e.g., "volunteers/guid.jpg"
            var key = $"{folderName}/{fileName}";

            // 4. Prepare the Upload Request
            using var stream = file.OpenReadStream();
            var putRequest = new PutObjectRequest
            {
                BucketName = _settings.BucketName,
                Key = key,
                InputStream = stream,
                ContentType = file.ContentType,
                // بما أن الباكت Private، لا نضع CannedACL هنا، سنتعامل مع الملفات عبر الـ API
            };

            // 5. Execute Upload
            await _s3Client.PutObjectAsync(putRequest, cancellationToken);

            // 6. Return the Full URL (للتخزين في قاعدة البيانات)
            // URL Format: https://{BucketName}.s3.{Region}.backblazeb2.com/{Key}
            // ملاحظة: الـ ServiceUrl في الإعدادات يحتوي على الـ Region والدومين
            // سنقوم ببناء الرابط يدوياً ليكون جاهزاً للفرونت إند

            // تنظيف الـ ServiceUrl لو آخره "/"
            var baseUrl = _settings.ServiceUrl.TrimEnd('/');

            // Backblaze S3 URL structure often works best as: https://{ServiceUrl}/{BucketName}/{Key}
            // أو بناءً على الـ Endpoint اللي معاك. 
            // الطريقة الأضمن مع Backblaze هي استخدام الدومين المباشر للملفات لو مفعل الـ Friendly URL، 
            // لكن هنا سنستخدم الصيغة القياسية:
            var fileUrl = $"{baseUrl}/{_settings.BucketName}/{key}";

            return fileUrl;
        }

        public async Task DeleteFileAsync(string fileUrl, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(fileUrl)) return;

            // محاولة استخراج الـ Key من الـ URL
            // الـ URL شكله: https://endpoint/bucket/folder/file.ext
            // محتاجين نحصل على: folder/file.ext

            try
            {
                var uri = new Uri(fileUrl);
                // المسار في الـ Uri هيكون: /bucketName/folder/file.ext
                // هنشيل أول "/" واسم الباكت عشان نجيب الـ Key
                var path = uri.AbsolutePath.TrimStart('/');
                var prefixToRemove = $"{_settings.BucketName}/";

                if (path.StartsWith(prefixToRemove))
                {
                    var key = path.Substring(prefixToRemove.Length);

                    var deleteRequest = new DeleteObjectRequest
                    {
                        BucketName = _settings.BucketName,
                        Key = key
                    };

                    await _s3Client.DeleteObjectAsync(deleteRequest, cancellationToken);
                }
            }
            catch (Exception ex)
            {
                // كـ سنيور، لا نوقف النظام لو الحذف فشل (مثلاً الملف مش موجود أصلاً)
                // ممكن نسجل الخطأ (Log) ونكمل
                // Console.WriteLine($"Failed to delete file: {ex.Message}");
            }
        }
    }
}