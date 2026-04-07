namespace Mosahem.Application.Features.Volunteers.Commands.Location.EditVolunteerAddress
{
    public class EditVolunteerAddressRequest
    {
        public Guid? GovernateId { get; set; }
        public Guid? CityId { get; set; }
        public string? Description { get; set; }
    }
}
