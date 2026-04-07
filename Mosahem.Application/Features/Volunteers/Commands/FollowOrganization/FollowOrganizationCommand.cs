using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Volunteers.Commands.FollowOrganization
{
    public class FollowOrganizationCommand : IRequest<Response<string>>
    {
        public Guid VolunteerId { get; set; }
        public Guid OrganizationId { get; set; }
    }
}
