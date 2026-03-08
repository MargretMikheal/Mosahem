using MediatR;
using Microsoft.AspNetCore.Http;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Files.Commands.Edit.EditOrganizationFile
{
    public class EditOrganizationFileCommand : IRequest<Response<string>>
    {
        public Guid OrganizationId { get; set; }
        public Guid? OpportunityId { get; set; }
        public string FolderName { get; set; }
        public IFormFile File { get; set; }
    }
}
