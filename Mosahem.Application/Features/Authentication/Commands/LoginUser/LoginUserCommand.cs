using MediatR;
using mosahem.Application.Common;
using Mosahem.Application.DTOs.Auth;

namespace mosahem.Application.Features.Authentication.Commands.LoginUser
{
    public class LoginUserCommand : IRequest<Response<AuthResponse>>
    {
        public string EmailOrPhone { get; set; }
        public string Password { get; set; }
    }
}