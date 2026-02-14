namespace Mosahem.Application.Features.Organization.Queries.GetOrganizationLocations
{
    public class GetOrganizationLocationsResponse
    {
        public Guid Id { get; set; }
        public string? Description { get; set; }
        public Guid CityId { get; set; }
        public string CityName { get; set; } = string.Empty;
        public Guid GovernorateId { get; set; }
        public string GovernorateName { get; set; } = string.Empty;
    }
}
