namespace Mosahem.Application.Features.Organization.Queries.GetPendingOrganizations
{
    public class GetPendingOrganizationsResponse
    {
        public Guid OrganizationId { get; set; }
        public string OrganizationName { get; set; } = string.Empty;
        public string? OrganizationLogo { get; set; }
    }
}
