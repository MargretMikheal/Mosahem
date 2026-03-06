using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Users.Commands.ChangeUserEmail.ChangeEmail
{
    public class ChangeEmailCommand : IRequest<Response<string>>
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string Code { get; set; }
    }
}
