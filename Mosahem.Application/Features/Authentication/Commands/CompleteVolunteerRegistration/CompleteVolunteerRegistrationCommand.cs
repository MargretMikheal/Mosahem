using MediatR;
using mosahem.Application.Common;
using mosahem.Application.Features.Authentication.Commands.ValidateLocations;
using mosahem.Domain.Enums;
using Mosahem.Application.DTOs.Auth;

namespace Mosahem.Application.Features.Authentication.Commands.CompleteVolunteerRegistration
{
    public class CompleteVolunteerRegistrationCommand : IRequest<Response<AuthResponse>>
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }

        public DateTime? DateOfBirth { get; set; }
        public Gender? Gender { get; set; }
        public string? NationalId { get; set; }

        public string? CvUrl { get; set; }
        public AddressDto? Location { get; set; }
        public List<Guid> FieldIds { get; set; }
        public List<Guid> SkillIds { get; set; }
    }
}
