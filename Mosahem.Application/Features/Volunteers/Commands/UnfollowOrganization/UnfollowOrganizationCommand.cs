using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Volunteers.Commands.UnfollowOrganization
{
    public class UnfollowOrganizationCommand : IRequest<Response<string>>
    {
        public Guid VolunteerId { get; set; }
        public Guid OrganizationId { get; set; }
    }
}
