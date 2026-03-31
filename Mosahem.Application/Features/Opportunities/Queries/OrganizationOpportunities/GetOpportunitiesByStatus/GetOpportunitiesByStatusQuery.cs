using MediatR;
using mosahem.Application.Common;
using Mosahem.Application.Common.Pagination;

namespace Mosahem.Application.Features.Opportunities.Queries.OrganizationOpportunities.GetOpportunitiesByStatus
{
    public class GetOpportunitiesByStatusQuery : IRequest<Response<PaginatedResponse<GetOpportunitiesByStatusResponse>>>
    {
        public Guid OrganizationId { get; set; }
        public string OpportunityStatus { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
