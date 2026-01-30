using MediatR;
using mosahem.Application.Common;

namespace mosahem.Application.Features.Admin.Commands.AddAdmin
{
    public class AddAdminCommand : IRequest<Response<string>>
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}