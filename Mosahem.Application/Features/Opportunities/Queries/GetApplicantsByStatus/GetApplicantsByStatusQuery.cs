using MediatR;
using mosahem.Application.Common;
using Mosahem.Application.Common.Pagination;

namespace Mosahem.Application.Features.Opportunities.Queries.GetApplicantsByStatus
{
    public class GetApplicantsByStatusQuery : IRequest<Response<PaginatedResponse<GetApplicantsByStatusResponse>>>
    {
        public Guid OpportunityId { get; set; }
        public Guid OrganizationId { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 50;
        public string Status { get; set; }
    }
}
