using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Users.Commands.ChangeEmail.SendChangeEmailOtp
{
    public class SendChangeEmailOtp : IRequest<Response<string>>
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
    }
}
