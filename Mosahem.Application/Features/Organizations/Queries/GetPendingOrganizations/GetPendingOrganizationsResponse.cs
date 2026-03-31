namespace Mosahem.Application.Features.Organizations.Queries.GetPendingOrganizations
{
    public class GetPendingOrganizationsResponse
    {
        public Guid OrganizationId { get; set; }
        public string OrganizationName { get; set; } = string.Empty;
        public string? OrganizationLogo { get; set; }
    }
}
