using Mosahem.Application.Features.Opportunities.Queries.GetOpportunityById;

namespace Mosahem.Application.Features.Opportunities.Queries.OrganizationOpportunities.GetOpportunitiesByVerificationStatus
{
    public class GetOpportunitiesByVerificationStatusResponse
    {
        public Guid OpportunityId { get; set; }
        public string OpportunityName { get; set; } = string.Empty;
        public string OpportunityDescription { get; set; } = string.Empty;
        public string? OpportunityPhotoUrl { get; set; }
        public string WorkType { get; set; } = string.Empty;
        public string LocationType { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Vacancies { get; set; }
        public int ApplicantsCount { get; set; }
        public OpportunityOrganizationResponse Organization { get; set; } = new();
        public List<OpportunityLocationResponse> Locations { get; set; } = new();
        public int LikesCount { get; set; }
        public int CommentsCount { get; set; }
        public int SavesCount { get; set; }
    }


}
