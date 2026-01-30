using MediatR;
using mosahem.Application.Common;

namespace mosahem.Application.Features.Authentication.Commands.VerifyEmail
{
    public class VerifyEmailCommand : IRequest<Response<string>>
    {
        public string Email { get; set; }
        public string Code { get; set; }
    }
}