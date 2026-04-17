namespace Mosahem.Application.Features.Opportunities.Queries.GetApplicantsByStatus
{
    public class GetApplicantsByStatusResponse
    {
        public string Name { get; set; }
        public string? ProfileImgUrl { get; set; }
        public int? Age { get; set; }
        public int TotalHours { get; set; }
    }
}