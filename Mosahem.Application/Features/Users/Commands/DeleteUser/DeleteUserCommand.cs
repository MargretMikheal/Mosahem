using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Users.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest<Response<string>>
    {
        public Guid UserId { get; set; }
    }
}
