using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Users.Queries.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<Response<List<GetAllUsersResponse>>>
    {
    }
}
