using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Organization.Queries.GetPendingOrganizations
{
    public class GetPendingOrganizationsQuery : IRequest<Response<IReadOnlyList<GetPendingOrganizationsResponse>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
