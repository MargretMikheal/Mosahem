using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Users.Commands.ChangeEmail.ChangeEmailOtpVerification
{
    public class ChangeEmailOtpVerification : IRequest<Response<string>>
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string Code { get; set; }
    }
}
