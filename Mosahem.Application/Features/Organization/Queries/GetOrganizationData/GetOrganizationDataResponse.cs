namespace Mosahem.Application.Features.Organization.Queries.GetOrganizationData
{
    namespace Mosahem.Application.Features.Organization.Queries.GetOrganizationData
    {
        public class GetOrganizationDataResponse
        {
            public Guid OrganizationId { get; set; }
            public string OrganizationName { get; set; } = string.Empty;
            public string OrganizationDescription { get; set; } = string.Empty;
            public string? OrganizationLogo { get; set; }
            public string VerificationStatus { get; set; } = string.Empty;
            public string? VerificationComment { get; set; }
            public List<GetOrganizationDataFieldResponse> Fields { get; set; } = new();
            public List<GetOrganizationDataAddressResponse> Locations { get; set; } = new();
        }

        public class GetOrganizationDataFieldResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; } = string.Empty;
        }

        public class GetOrganizationDataAddressResponse
        {
            public Guid Id { get; set; }
            public string? Description { get; set; }
            public Guid CityId { get; set; }
            public string CityName { get; set; } = string.Empty;
            public Guid GovernorateId { get; set; }
            public string GovernorateName { get; set; } = string.Empty;
        }
    }
}
