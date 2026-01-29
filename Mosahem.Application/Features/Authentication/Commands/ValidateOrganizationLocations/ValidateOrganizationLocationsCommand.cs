using MediatR;
using mosahem.Application.Common;

namespace mosahem.Application.Features.Authentication.Commands.ValidateOrganizationLocations
{
    public class ValidateOrganizationLocationsCommand : IRequest<Response<string>>
    {
        public List<OrganizationAddressDto> Locations { get; set; }
    }

    public class OrganizationAddressDto
    {
        public Guid GovernorateId { get; set; }
        public Guid CityId { get; set; }
        public string? Details { get; set; }
    }
}