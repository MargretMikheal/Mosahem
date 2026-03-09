using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Organizations.Commands.ValidateOrganization.ApproveOrganization
{
    public class ApproveOrganizationCommand : IRequest<Response<string>>
    {
        public Guid OrganizationId { get; set; }
        public string? Comment { get; set; }

        public ApproveOrganizationCommand(Guid organizationId, string? comment)
        {
            OrganizationId = organizationId;
            Comment = comment;
        }
    }
}
