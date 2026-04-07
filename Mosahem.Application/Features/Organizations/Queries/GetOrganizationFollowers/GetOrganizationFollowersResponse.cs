namespace Mosahem.Application.Features.Organizations.Queries.GetOrganizationFollowers
{
    public class GetOrganizationFollowersResponse
    {
        public Guid VolunteerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? ProfileImage { get; set; }
        public string? Bio { get; set; }
    }
}
