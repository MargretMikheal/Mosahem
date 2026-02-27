using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Addresses.Commands.Organization.AddOrganizationAddress
{
    public class AddOrganizationAddressCommand : IRequest<Response<string>>
    {
        public Guid OrganizationId { get; set; }
        public Guid GovernateId { get; set; }
        public Guid CityID { get; set; }
        public string? Description { get; set; }
    }
}
