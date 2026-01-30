using MediatR;
using mosahem.Application.Common;

namespace mosahem.Application.Features.Authentication.Commands.SendEmailVerificationCode
{
    public class SendEmailVerificationCodeCommand : IRequest<Response<string>>
    {
        public string Email { get; set; }
    }
}