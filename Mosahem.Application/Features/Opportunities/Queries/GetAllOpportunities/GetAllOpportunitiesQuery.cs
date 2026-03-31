using MediatR;
using mosahem.Application.Common;
using Mosahem.Application.Common.Pagination;

namespace Mosahem.Application.Features.Opportunities.Queries.GetAllOpportunities
{
    public class GetAllOpportunitiesQuery : IRequest<Response<PaginatedResponse<GetAllOpportunitiesResponse>>>
    {
        // Filters
        public string? Search { get; set; }
        public Guid? GovernateId { get; set; }
        public string? OpportunityStatus { get; set; }
        public string? WorkType { get; set; }
        public string? LocationType { get; set; }
        public DateTime? StartDate { get; set; }
        public List<Guid>? FieldIds { get; set; }
        public List<Guid>? RequiredSkillIds { get; set; }
        public List<Guid>? ProvidedSkillIds { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 50;

    }
}
