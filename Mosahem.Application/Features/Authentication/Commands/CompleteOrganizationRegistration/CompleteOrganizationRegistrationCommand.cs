using MediatR;
using mosahem.Application.Common;
using mosahem.Application.Features.Authentication.Commands.ValidateOrganizationLocations;
using Mosahem.Application.DTOs.Auth; 

namespace mosahem.Application.Features.Authentication.Commands.CompleteOrganizationRegistration
{
    public class CompleteOrganizationRegistrationCommand : IRequest<Response<AuthResponse>>
    {
        public string OrganizationName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }

        public string LicenseUrl { get; set; }

        public List<OrganizationAddressDto> Locations { get; set; }

        public List<Guid> FieldIds { get; set; }
        public string? Description { get; set; }
    }
}