using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Volunteers.Queries.GetVolunteerFollowedOrganizations
{
    public class GetVolunteerFollowedOrganizationsQuery : IRequest<Response<List<GetVolunteerFollowedOrganizationsResponse>>>
    {
        public Guid VolunteerId { get; set; }
    }
}
