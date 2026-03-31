namespace Mosahem.Application.Features.Addresses.Commands.Organization.AddOrganizationAddress
{
    public class AddOrganizationAddressCommandRequest
    {
        public Guid GovernateId { get; set; }
        public Guid CityID { get; set; }
        public string? Description { get; set; }
    }
}
