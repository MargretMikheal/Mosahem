namespace Mosahem.Application.Features.Opportunities.Queries.OrganizationOpportunities.GetOpportunitiesByStatus
{
    public class GetOpportunitiesByStatusRequest
    {
        public string OpportunityStatus { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 50;
    }
}
