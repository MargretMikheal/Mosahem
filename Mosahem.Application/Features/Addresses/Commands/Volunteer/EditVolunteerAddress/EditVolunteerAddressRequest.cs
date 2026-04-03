namespace Mosahem.Application.Features.Addresses.Commands.EditVolunteerAddress
{
    public class EditVolunteerAddressRequest
    {
        public Guid? GovernateId { get; set; }
        public Guid? CityId { get; set; }
        public string? Description { get; set; }
    }
}
