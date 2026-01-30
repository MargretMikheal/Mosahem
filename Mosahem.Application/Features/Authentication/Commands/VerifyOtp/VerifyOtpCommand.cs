using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Authentication.Commands.VerifyOtp
{
    public class VerifyOtpCommand : IRequest<Response<string>>
    {
        public string Email { get; set; }
        public string Code { get; set; }
    }
}
