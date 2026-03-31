namespace Mosahem.Application.Features.Addresses.Commands.Organization.EditOrganizationAddress
{
    public class EditOrganizationAddressRequest
    {
        public Guid? GovernateId { get; set; }
        public Guid? CityId { get; set; }
        public string? Description { get; set; }
    }
}
