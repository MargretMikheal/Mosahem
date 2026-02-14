namespace Mosahem.Application.Features.Organization.Queries.GetAllOrganizations
{
    public class GetAllOrganizationsResponse
    {
        public Guid OrganizationId { get; set; }
        public string OrganizationName { get; set; } = string.Empty;
        public string OrganizationDescription { get; set; } = string.Empty;
        public string? OrganizationLogo { get; set; }
    }
}
