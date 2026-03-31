using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Addresses.Commands.Organization.DeleteOrganizationAddress
{
    public class DeleteOrganizationAddressCommand : IRequest<Response<string>>
    {
        public DeleteOrganizationAddressCommand(Guid organizationId, Guid addressId)
        {
            OrganizationId = organizationId;
            AddressId = addressId;
        }

        public Guid OrganizationId { get; set; }
        public Guid AddressId { get; set; }

    }
}
