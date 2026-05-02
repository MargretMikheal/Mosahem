using MediatR;
using mosahem.Application.Common;
using Mosahem.Application.Common.Pagination;

namespace Mosahem.Application.Features.Volunteers.Queries.GetUnratedVolunteers
{
    public class GetUnratedVolunteersQuery : IRequest<Response<PaginatedResponse<GetUnratedVolunteersResponse>>>
    {
        public Guid OrganizationId { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
