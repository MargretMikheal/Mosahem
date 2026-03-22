using MediatR;
using mosahem.Application.Common;
using Mosahem.Application.Common.Pagination;

namespace Mosahem.Application.Features.Opportunities.Queries.GetAllOpportunitiesByVerificationStatus
{
    public class GetAllOpportunitiesByVerificationStatusQuery : IRequest<Response<PaginatedResponse<GetAllOpportunitiesByVerificationStatusResponse>>>
    {
        public string OpportunityVerificationStatus { get; set; } = string.Empty;
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 50;
    }
}
