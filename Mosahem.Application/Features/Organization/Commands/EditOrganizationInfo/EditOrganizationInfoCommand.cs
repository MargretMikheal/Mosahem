using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Organization.Commands.EditOrganizationInfo
{
    public class EditOrganizationInfoCommand : IRequest<Response<string>>
    {
        public Guid OrganizationId { get; set; }
        public string? OrganizationName { get; set; }
        public string? OrganizationDescription { get; set; }
        public string? OrganizationPhoneNumber { get; set; }
    }
}
