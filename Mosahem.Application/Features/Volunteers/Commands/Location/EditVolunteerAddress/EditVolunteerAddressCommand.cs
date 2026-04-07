using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Volunteers.Commands.Location.EditVolunteerAddress
{
    public class EditVolunteerAddressCommand : IRequest<Response<string>>
    {
        public Guid VolunteerId { get; set; }
        public Guid? GovernateId { get; set; }
        public Guid? CityId { get; set; }
        public string? Description { get; set; }
    }
}
