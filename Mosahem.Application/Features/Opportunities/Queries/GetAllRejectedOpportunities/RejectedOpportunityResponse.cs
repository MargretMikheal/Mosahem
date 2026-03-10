namespace Mosahem.Application.Features.Opportunities.Queries.GetAllRejectedOpportunities
{
    public class RejectedOpportunityResponse
    {
        public Guid OpportunityId { get; set; }
        public string OpportunityName { get; set; } = string.Empty;
        public string OrganizationName { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? OrganizationLogoUrl { get; set; }
    }
}
