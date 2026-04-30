namespace Mosahem.Application.Features.Opportunities.Queries.GetApplicantsByStatus
{
    public class GetApplicantsByStatusRequest
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 50;
        public string Status { get; set; }
    }
}
