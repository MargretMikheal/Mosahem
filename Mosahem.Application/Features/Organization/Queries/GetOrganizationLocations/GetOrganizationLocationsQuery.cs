using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Organization.Queries.GetOrganizationLocations
{
    public class GetOrganizationLocationsQuery : IRequest<Response<List<GetOrganizationLocationsResponse>>>
    {
        public Guid OrganizationId { get; set; }
    }
}
