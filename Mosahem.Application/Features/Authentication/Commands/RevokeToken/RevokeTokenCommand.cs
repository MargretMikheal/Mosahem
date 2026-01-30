using MediatR;
using mosahem.Application.Common;

namespace mosahem.Application.Features.Authentication.Commands.RevokeToken
{
    public class RevokeTokenCommand : IRequest<Response<bool>>
    {
        public string Token { get; set; }
    }
}