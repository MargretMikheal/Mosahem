using Microsoft.AspNetCore.Http;

namespace Mosahem.Application.Features.Files.Commands.Edit
{
    public class EditUserFileRequest
    {
        public Guid? OpportunityId { get; set; }
        public string FolderName { get; set; }
        public IFormFile File { get; set; }
    }
}
