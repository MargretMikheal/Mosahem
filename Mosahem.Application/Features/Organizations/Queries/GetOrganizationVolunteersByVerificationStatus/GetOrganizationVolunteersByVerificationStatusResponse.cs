namespace Mosahem.Application.Features.Organizations.Queries.GetOrganizationVolunteersByVerificationStatus
{
    public class GetOrganizationVolunteersByVerificationStatusResponse
    {
        public Guid VolunteerId { get; set; }
        public string Name { get; set; }
        public string? ProfileImgUrl { get; set; }
        public int? Age { get; set; }
        public int TotalHours { get; set; }
        public string? Bio { get; set; }
    }
}
