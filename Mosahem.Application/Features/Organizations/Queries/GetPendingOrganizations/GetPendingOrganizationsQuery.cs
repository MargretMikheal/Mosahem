using MediatR;
using mosahem.Application.Common;
using Mosahem.Application.Common.Pagination;

namespace Mosahem.Application.Features.Organizations.Queries.GetPendingOrganizations
{
    public class GetPendingOrganizationsQuery : IRequest<Response<PaginatedResponse<GetPendingOrganizationsResponse>>>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 50;
    }
}
