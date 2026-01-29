using MediatR;
using mosahem.Application.Common;

namespace mosahem.Application.Features.Authentication.Commands.ValidateBasicInfo
{
    public class ValidateBasicInfoCommand : IRequest<Response<string>>
    {
        public string OrganizationName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}