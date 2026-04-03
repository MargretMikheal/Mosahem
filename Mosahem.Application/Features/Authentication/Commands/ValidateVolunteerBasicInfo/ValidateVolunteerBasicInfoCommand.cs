using MediatR;
using mosahem.Application.Common;
using mosahem.Domain.Enums;

namespace Mosahem.Application.Features.Authentication.Commands.ValidateVolunteerBasicInfo
{
    public class ValidateVolunteerBasicInfoCommand : IRequest<Response<string>>
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Gender? Gender { get; set; }
        public string? NationalId { get; set; }
    }
}
