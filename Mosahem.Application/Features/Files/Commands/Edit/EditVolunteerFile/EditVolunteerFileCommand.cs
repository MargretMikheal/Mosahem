using MediatR;
using Microsoft.AspNetCore.Http;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Files.Commands.Edit.EditVolunteerFile
{
    public class EditVolunteerFileCommand : IRequest<Response<string>>
    {
        public Guid VolunteerId { get; set; }
        public string FolderName { get; set; }
        public IFormFile File { get; set; }
    }
}
