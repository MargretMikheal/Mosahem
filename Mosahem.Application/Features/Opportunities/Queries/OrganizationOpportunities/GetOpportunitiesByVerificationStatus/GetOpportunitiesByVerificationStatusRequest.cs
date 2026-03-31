namespace Mosahem.Application.Features.Opportunities.Queries.OrganizationOpportunities.GetOpportunitiesByVerificationStatus
{
    public class GetOpportunitiesByVerificationStatusRequest
    {
        public string OpportunityVerificationStatus { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 50;
    }
}
