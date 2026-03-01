using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Users.Commands.ResetUserPassword.SendRestPasswordOtp
{
    public class SendRestPasswordOtpCommand : IRequest<Response<string>>
    {
        public string Email { get; set; }
    }
}