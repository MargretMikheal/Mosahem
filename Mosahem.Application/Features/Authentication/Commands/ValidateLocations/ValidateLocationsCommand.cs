using MediatR;
using mosahem.Application.Common;

namespace mosahem.Application.Features.Authentication.Commands.ValidateLocations
{
    public class ValidateLocationsCommand : IRequest<Response<string>>
    {
        public List<AddressDto> Locations { get; set; }
    }

    public class AddressDto
    {
        public Guid GovernorateId { get; set; }
        public Guid CityId { get; set; }
        public string? Details { get; set; }
    }
}