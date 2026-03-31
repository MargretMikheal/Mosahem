using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Users.Commands.ChangePassword
{
    public class ChangePasswordCommand : IRequest<Response<string>>
    {
        public Guid Id { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }
}