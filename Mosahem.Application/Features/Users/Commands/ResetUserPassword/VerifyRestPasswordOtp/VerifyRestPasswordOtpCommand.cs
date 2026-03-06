using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Users.Commands.ResetUserPassword.VerifyRestPasswordOtp
{
    public class VerifyRestPasswordOtpCommand : IRequest<Response<string>>
    {
        public string Email { get; set; }
        public string Code { get; set; }
    }
}
