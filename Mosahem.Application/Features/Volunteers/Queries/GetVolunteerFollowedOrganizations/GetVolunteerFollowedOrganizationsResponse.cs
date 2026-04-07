namespace Mosahem.Application.Features.Volunteers.Queries.GetVolunteerFollowedOrganizations
{
    public class GetVolunteerFollowedOrganizationsResponse
    {
        public Guid OrganizationId { get; set; }
        public string OrganizationName { get; set; } = string.Empty;
        public string OrganizationDescription { get; set; } = string.Empty;
        public string? OrganizationLogo { get; set; }
    }
}
