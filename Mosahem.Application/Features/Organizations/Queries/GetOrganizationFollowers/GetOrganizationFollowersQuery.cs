using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Organizations.Queries.GetOrganizationFollowers
{
    public class GetOrganizationFollowersQuery : IRequest<Response<List<GetOrganizationFollowersResponse>>>
    {
        public Guid OrganizationId { get; set; }
    }
}
