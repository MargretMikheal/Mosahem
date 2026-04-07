using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Organizations.Commands.EditOrganizationVerificationComment
{
    public class EditOrganizationVerificationCommentCommand : IRequest<Response<string>>
    {
        public Guid OrganizationId { get; set; }
        public string VerificationComment { get; set; } = string.Empty;
    }
}
