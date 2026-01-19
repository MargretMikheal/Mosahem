using Microsoft.AspNetCore.Http;

namespace Mosahem.Application.Interfaces
{
    public interface IFileService
    {
        Task<string> UploadFileAsync(IFormFile file, string folderName, CancellationToken cancellationToken = default);

        Task DeleteFileAsync(string fileUrl, CancellationToken cancellationToken = default);
    }
}