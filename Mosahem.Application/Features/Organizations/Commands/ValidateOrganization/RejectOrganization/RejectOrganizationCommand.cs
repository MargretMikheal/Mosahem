using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Organizations.Commands.ValidateOrganization.RejectOrganization
{
    public class RejectOrganizationCommand : IRequest<Response<string>>
    {
        public Guid OrganizationId { get; set; }
        public string? Comment { get; set; }

        public RejectOrganizationCommand(Guid organizationId, string? comment)
        {
            OrganizationId = organizationId;
            Comment = comment;
        }
    }
}
