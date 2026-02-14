using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Users.Queries.GetUserInfo
{
    public class GetUserInfoQuery : IRequest<Response<GetUserInfoResponse>>
    {
        public Guid UserId { get; set; }
    }
}
