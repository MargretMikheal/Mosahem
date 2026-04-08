namespace Mosahem.Application.Features.Opportunities.Queries.GetOpportunityComments
{
    public class GetOpportunityCommentsResponse
    {
        public string VolunteerName { get; set; } = string.Empty;
        public string? ProfilePhoto { get; set; }
        public string Comment { get; set; } = string.Empty;
    }
}
