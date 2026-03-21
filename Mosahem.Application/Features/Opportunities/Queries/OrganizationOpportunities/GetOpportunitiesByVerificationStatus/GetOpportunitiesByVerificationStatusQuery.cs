using MediatR;
using mosahem.Application.Common;
using Mosahem.Application.Common.Pagination;

namespace Mosahem.Application.Features.Opportunities.Queries.OrganizationOpportunities.GetOpportunitiesByVerificationStatus
{
    public class GetOpportunitiesByVerificationStatusQuery : IRequest<Response<PaginatedResponse<GetOpportunitiesByVerificationStatusResponse>>>
    {
        public Guid OrganizationId { get; set; }
        public string OpportunityVerificationStatus { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
