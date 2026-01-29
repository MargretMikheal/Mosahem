using MediatR;
using mosahem.Application.Common;
using Mosahem.Application.DTOs.Auth;

namespace Mosahem.Application.Features.Authentication.Commands.RefreshTokens
{
    public class RefreshTokenCommand : IRequest<Response<AuthResponse>>
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
