using MediatR;
using mosahem.Application.Common;

namespace mosahem.Application.Features.Authentication.Commands.SendOtp
{
    public class SendOtpCommand : IRequest<Response<string>>
    {
        public string Email { get; set; }
    }
}