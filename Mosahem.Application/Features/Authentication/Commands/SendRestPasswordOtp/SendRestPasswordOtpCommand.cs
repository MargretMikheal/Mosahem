using MediatR;
using mosahem.Application.Common;

namespace mosahem.Application.Features.Authentication.Commands.SendRestPasswordOtp
{
    public class SendRestPasswordOtpCommand : IRequest<Response<string>>
    {
        public string Email { get; set; }
    }
}