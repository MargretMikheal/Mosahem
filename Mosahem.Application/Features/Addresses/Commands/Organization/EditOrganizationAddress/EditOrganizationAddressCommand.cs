using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Addresses.Commands.Organization.EditOrganizationAddress
{
    public class EditOrganizationAddressCommand : IRequest<Response<string>>
    {
        public Guid OrganizationId { get; set; }
        public Guid AddressId { get; set; }
        public Guid? GovernateId { get; set; }
        public Guid? CityId { get; set; }
        public string? Description { get; set; }
    }
}
